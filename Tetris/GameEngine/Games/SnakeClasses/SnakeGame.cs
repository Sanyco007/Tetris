using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games.SnakeClasses
{
    public class SnakeGame : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;

        private readonly List<Point> _snake;

        private Point _wayDelta;
        private Point _newBlock;

        private readonly Random _rand;

        private bool _pause;

        //Время для обновления состояния игры (падение)
        private long _ellapsed;
        private long _levelTime = 300L;
        private long _updateTime = 300L;

        //Время для считывания событий с клавиатуры (перемещение, трансформация)
        private long _keyboardEllapsed;
        private const long KeyboardUpdate = 60L;

        private readonly Game _game;

        public SnakeGame(Image canvas, Game game)
        {
            _canvas = canvas;
            _game = game;
            _pause = false;
            _rand = new Random();
            _wayDelta = new Point(1, 0);
            _newBlock = new Point();
            _buffer = new Bitmap(180, 360);
            _snake = new List<Point> {new Point(0, 0), new Point(1, 0), new Point(2, 0)};
            GenerateNewBlock();
            AudioPlayer.PlayMusic(AudioPlayer.SnakeTheme);
            Redraw();
        }

        private void GenerateNewBlock()
        {
            int x;
            int y;
            bool find;
            do
            {
                x = _rand.Next(Game.Width);
                y = _rand.Next(Game.Height);
                find = _snake.All(t => x != t.X || y != t.Y);
            } 
            while (!find);
            _newBlock.X = x;
            _newBlock.Y = y;
        }

        public void Update(long delta)
        {
            //Увеличение прошедшего времени
            _ellapsed += delta;
            _keyboardEllapsed += delta;

            if (Keyboard.IsKeyDown(Key.P))
            {
                _pause = !_pause;
                AudioPlayer.Pause(_pause);
                Keyboard.KeyUp(Key.P);
                Redraw();
            }

            if (_pause) return;

            _updateTime = Keyboard.IsKeyDown(Key.Space) ? 50L : _levelTime;

            //Обработка нажатых клавиш
            if (_keyboardEllapsed >= KeyboardUpdate)
            {
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    if (_wayDelta.Y != -1)
                    {
                        _wayDelta = new Point(0, 1);
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDirection);
                        Keyboard.KeyUp(Key.Down);
                    }
                }
                else if (Keyboard.IsKeyDown(Key.Up))
                {
                    if (_wayDelta.Y != 1)
                    {
                        _wayDelta = new Point(0, -1);
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDirection);
                        Keyboard.KeyUp(Key.Up);
                    }
                }
                else if (Keyboard.IsKeyDown(Key.Left))
                {
                    if (_wayDelta.X != 1)
                    {
                        _wayDelta = new Point(-1, 0);
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDirection);
                        Keyboard.KeyUp(Key.Left);
                    }
                }
                else if (Keyboard.IsKeyDown(Key.Right))
                {
                    if (_wayDelta.X != -1)
                    {
                        _wayDelta = new Point(1, 0);
                        AudioPlayer.PlayEffect(AudioPlayer.EffectDirection);
                        Keyboard.KeyUp(Key.Right);
                    }
                }
            }

            //Падение фигуры
            if (_ellapsed >= _updateTime)
            {
                var head = new Point
                    {
                        X = _snake[_snake.Count - 1].X + _wayDelta.X,
                        Y = _snake[_snake.Count - 1].Y + _wayDelta.Y
                    };
                if (head.X == _newBlock.X && head.Y == _newBlock.Y)
                {
                    _snake.Add(head);
                    AudioPlayer.PlayEffect(AudioPlayer.EffectBite);
                    GenerateNewBlock();
                }
                else if (Intersects(head))
                {
                    AudioPlayer.Pause(false);
                    var gameOver = new GameOver(_canvas, _buffer, _game, this);
                    _game.PushScreen(gameOver);
                    return;
                }
                else
                {
                    for (int i = 0; i < _snake.Count - 1; i++)
                    {
                        _snake[i] = _snake[i + 1];
                    }
                    _snake[_snake.Count - 1] = head;
                }
                _ellapsed = 0;
                Redraw();
            } 
        }

        private bool Intersects(Point head)
        {
            if (head.X < 0 || head.X >= Game.Width ||
                head.Y < 0 || head.Y >= Game.Height)
            {
                return true;
            }
            return _snake.Any(item => head.X == item.X && head.Y == item.Y);
        }

        public void Redraw()
        {
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(Images.Grid, 0, 0);
                for (int i = 0; i < _snake.Count; i++)
                {
                    gr.DrawImage(i < _snake.Count - 1 ? Images.Colors[0] : Images.Colors[1],
                                 _snake[i].X*Game.H, _snake[i].Y*Game.H);
                }
                gr.DrawImage(Images.Colors[2],
                            _newBlock.X * Game.H, _newBlock.Y * Game.H);
                if (_pause)
                {
                    gr.DrawImage(Images.Pause, 0, 0);
                }
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }
    }
}
