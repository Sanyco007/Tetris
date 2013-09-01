using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;
using Label = System.Windows.Controls.Label;

namespace Tetris.GameEngine.Games.TetrisClasses
{
    public class TetrisGame : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Image _next;
        private readonly Label _linesSent;

        private readonly Bitmap _buffer;
        private readonly Bitmap _nextBuffer;

        public const int Width = 10;
        public const int Height = 20;

        public const int H = 18;

        private Item[,] _field = new Item[Width, Height];

        //Время для обновления состояния игры (падение)
        private long _ellapsed;
        private long _updateTime = 1000L;
    
        //Время для считывания событий с клавиатуры (перемещение, трансформация)
        private long _keyboardEllapsed;
        private const long KeyboardUpdate = 30L;

        //Блок (текущая фигура и данные о следующей)
        private Block _block;

        private bool _drawFeature;
        private bool _pause;

        private int _animateStep;

        private int _total;

        private bool _animate;
        private List<int> _lines;

        private readonly Game _game;

        private bool _repeat;

        public TetrisGame(Image canvas, Image next, Label linesSent, Game game)
        {
            _linesSent = linesSent;
            _game = game;
            _repeat = false;
            _animate = false;
            _animateStep = 0;
            _total = 0;
            _pause = false;
            _buffer = new Bitmap(180, 360);
            _nextBuffer = new Bitmap(36, 36);
            _canvas = canvas;
            _next = next;
            _lines = new List<int>();
            _drawFeature = false;
            Start();
        }

        public void Repeat()
        {
            Start();
        }

        private void Start()
        {
            InitializeGame();
            _block.MoveDown();
            Redraw();
            AudioPlayer.PlayMusic(AudioPlayer.MusicTheme);
            DrawNext();
        }

        private void InitializeGame()
        {
            _field = new Item[Width, Height];
            _block = new Block();
            _block.Generate();
            _total = 0;
            _linesSent.Content = "0";
        }

        public void Update(long delta)
        {

            if (_repeat)
            {
                Start();
                _repeat = false;
            }

            //Увеличение прошедшего времени
            _ellapsed += delta;
            _keyboardEllapsed += delta;
        
            //Ускорение падения при нажатии ВНИЗ
            _updateTime = Keyboard.IsKeyDown(Key.Down) ? 10L : 1000L;

            if (Keyboard.IsKeyDown(Key.P))
            {
                _pause = !_pause;
                AudioPlayer.Pause(_pause);
                Keyboard.KeyUp(Key.P);
                Redraw();
            }

            if (_pause) return;

            if (_animate)
            {
                Redraw();
                return;
            }

            //Обработка нажатых клавиш
            if (_keyboardEllapsed >= KeyboardUpdate) {
                if (Keyboard.IsKeyDown(Key.Left)) {
                    _block.MoveLeft(_field);
                    Redraw();
                }
                if (Keyboard.IsKeyDown(Key.Right)) {
                    _block.MoveRight(_field);
                    Redraw();
                }
                if (Keyboard.IsKeyDown(Key.F))
                {
                    _drawFeature = !_drawFeature;
                    Keyboard.KeyUp(Key.F);
                    Redraw();
                }
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    while (!CheckCollisins())
                    {
                        _block.MoveDown();
                    }
                    AudioPlayer.PlayEffect(AudioPlayer.EffectDrop);
                    Keyboard.KeyUp(Key.Space);
                    _block.MoveDown();
                    Redraw();
                }
                if (Keyboard.IsKeyDown(Key.Enter) || Keyboard.IsKeyDown(Key.Up))
                {
                    _block.Rotate(_field);
                    AudioPlayer.PlayEffect(AudioPlayer.EffectRotate);
                    Keyboard.KeyUp(Key.Enter);
                    Keyboard.KeyUp(Key.Up);
                    Redraw();
                }
                _keyboardEllapsed = 0;
            }

            //Падение фигуры
            if (_ellapsed >= _updateTime) {
                CheckCollisins();
                _block.MoveDown();
                Redraw();
                _ellapsed = 0;
            }
        }

        //Определение падения фигуры
        private bool CheckCollisins(List<Item> list = null)
        {
            bool add = false;
            if (list == null)
            {
                add = true;
                list = _block.GetItems();
            }
            foreach (var t in list)
            {
                int x = t.Position.X;
                int y = t.Position.Y - 1;
                if (y < 0)
                {
                    if (add)
                    {
                        WriteItemsOnField();
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDown);
                        DeleteLines();
                        _block.Generate();
                        if (_block.Intersects(_field))
                        {
                            ShowGameOver();
                        }
                        DrawNext();
                    }
                    return true;
                }
                if (IntersectsWithField(x, y))
                {
                    if (add)
                    {
                        WriteItemsOnField();
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDown);
                        DeleteLines();
                        _block.Generate();
                        if (_block.Intersects(_field))
                        {
                            ShowGameOver();
                        }
                        DrawNext();
                    }
                    return true;
                }
            }
            return false;
        }

        private void ShowGameOver()
        {
            var gameOver = new GameOver(_canvas, _buffer, _game, this);
            _game.PushScreen(gameOver);
        }

        private void DeleteLines()
        {
            var list = new List<int>();
            for (int i = 0; i < Height; i++)
            {
                int count = 0;
                for (int j = 0; j < Width; j++)
                {
                    if (_field[j, i] != null)
                    {
                        count++;
                    }    
                }
                if (count == Width)
                {
                    list.Add(i);
                }
            }
            list.Reverse();
            if (list.Count > 0)
            {
                _total += list.Count;
                _linesSent.Content = _total;
                _lines = list;
                _animate = true;
                AudioPlayer.PlayEffect(AudioPlayer.EffectLine);
            }
        }

        private void DeleteLine(int index)
        {
            for (int i = index; i < Height - 1; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _field[j, i] = _field[j, i + 1];
                }
            }
        }

        private bool IntersectsWithField(int x, int y)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (_field[i, j] != null)
                    {
                        if (i == x && y == j)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void WriteItemsOnField()
        {
            List<Item> list = _block.GetItems();
            foreach (Item item in list)
            {
                int x = item.Position.X;
                int y = item.Position.Y;
                _field[x, y] = item;
            }
        }

        public void DrawNext()
        {
            using (Graphics gr = Graphics.FromImage(_nextBuffer))
            {
                gr.Clear(Color.Black);
                List<Item> list = _block.GetNextItems();
                var offset = _block.GetNextOffset();
                foreach (Item item in list)
                {
                    int x = item.Position.X * 9 + offset.X;
                    int y = item.Position.Y * 9 + offset.Y;
                    gr.DrawImage(item.Image, new Rectangle(x, y, 9, 9), 
                      0, 0, 9, 9, GraphicsUnit.Pixel);
                }
            }
            _next.Source = Images.ToBitmapSource(_nextBuffer);
        }

        public void Redraw()
        {
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(Images.Grid, 0, 0);
                DrawField(gr);
                if (_drawFeature)
                {
                    DrawFeature(gr);
                }
                DrawBlock(gr);
                if (_pause)
                {
                    gr.DrawImage(Images.Pause, 0, 0);
                }
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }

        private void DrawField(Graphics gr)
        {
            if (_animate)
            {
                _animateStep++;
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_field[j, i] != null)
                    {
                        int x = j * H;
                        int y = 360 - i * H - H;
                        if (_lines.Contains(i))
                        {
                            x += _animateStep;
                            y += _animateStep;
                            int newH = H - _animateStep * 2;
                            gr.DrawImage(_field[j, i].Image, new Rectangle(x, y, newH, newH), 0, 0, H, H, GraphicsUnit.Pixel);
                        }
                        else gr.DrawImage(_field[j, i].Image, x, y);
                    }
                }
            }
            if (_animateStep >= H / 2)
            {
                _animateStep = 0;
                _animate = false;
                foreach (var line in _lines)
                {
                    DeleteLine(line);
                }
                Redraw();
            }
        }

        
        private void DrawBlock(Graphics gr)
        {
            List<Item> list = _block.GetItems();
            foreach (Item item in list)
            {
                int x = item.Position.X * H;
                int y = 360 - item.Position.Y * H - H;
                gr.DrawImage(item.Image, x, y);
            }
        }

        private void DrawFeature(Graphics gr)
        {
            var tlist = _block.GetItems();
            var list = tlist.Select(t => 
                new Item(new Point(t.Position.X, t.Position.Y), Images.Feature)).ToList();
            while (!CheckCollisins(list))
            {
                foreach (Item t in list)
                {
                    t.Position.Y -= 1;
                }
            }
            foreach (Item item in list)
            {
                int x = item.Position.X * H;
                int y = 360 - item.Position.Y * H - H;
                gr.DrawImage(item.Image, x, y);
            }
        }
    }
}
