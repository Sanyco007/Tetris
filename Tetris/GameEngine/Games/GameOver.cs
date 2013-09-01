using System.Drawing;
using System.Windows.Input;
using Tetris.GameEngine.Games.TetrisClasses;
using Tetris.GameEngine.Inputs;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games
{
    class GameOver : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;
        private readonly Bitmap _prevState;
        private readonly Game _game;

        private const int ButtonWidth = 161;
        private const int ButtonHeight = 43;

        private readonly Point _posRepeat = new Point(10, 130);
        private readonly Point _posMenu = new Point(10, 190);
        private readonly Point _posExit = new Point(10, 250);

        private readonly IGameScreen _screen;

        public GameOver(Image canvas, Bitmap prevState, Game game, IGameScreen screen)
        {
            _canvas = canvas;
            _prevState = prevState;
            _game = game;
            _screen = screen;
            _buffer = new Bitmap(180, 360);
            Redraw();
        }

        public void Update(long delta)
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                _game.BackScreen().BackScreen();
                return;
            }
            if (!MouseState.IsMouseMove()) return;
            var mouse = MouseState.GetMouseDown();
            if (!mouse.IsEmpty && Intersection(_posRepeat, mouse))
            {
                var tetrisGame = _screen as TetrisGame;
                if (tetrisGame != null)
                {
                    _game.BackScreen();
                    tetrisGame.Repeat();
                    return;
                }
            }
            if (!mouse.IsEmpty && Intersection(_posMenu, mouse))
            {
                _game.BackScreen().BackScreen();
                return;
            }
            if (!mouse.IsEmpty && Intersection(_posExit, mouse))
            {
                _game.Exit();
            }
            Redraw();
        }

        private bool Intersection(Point pos, Point mouseDown)
        {
            int x = MouseState.X;
            int y = MouseState.Y;
            if (!mouseDown.IsEmpty)
            {
                x = mouseDown.X;
                y = mouseDown.Y;
            }
            if (x > pos.X && x < pos.X + ButtonWidth)
            {
                if (y > pos.Y && y < pos.Y + ButtonHeight)
                {
                    return true;
                }
            }
            return false;
        }

        public void Redraw()
        {
            bool change = false;
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(_prevState, 0, 0);
                gr.DrawImage(Images.GoBackground, 0, 0);
                if (!Intersection(_posRepeat, Point.Empty))
                {
                    gr.DrawImage(Images.GoRepeat, _posRepeat.X, _posRepeat.Y);
                }
                else
                {
                    gr.DrawImage(Images.GoRepeatHover, _posRepeat.X, _posRepeat.Y);
                    _canvas.Cursor = Cursors.Hand;
                    change = true;
                }
                if (!Intersection(_posMenu, Point.Empty))
                {
                    gr.DrawImage(Images.GoMenu, _posMenu.X, _posMenu.Y);
                }
                else
                {
                    gr.DrawImage(Images.GoMenuHover, _posMenu.X, _posMenu.Y);
                    _canvas.Cursor = Cursors.Hand;
                    change = true;
                }
                if (!Intersection(_posExit, Point.Empty))
                {
                    gr.DrawImage(Images.GoExit, _posExit.X, _posExit.Y);
                }
                else
                {
                    _canvas.Cursor = Cursors.Hand;
                    change = true;
                    gr.DrawImage(Images.GoExitHover, _posExit.X, _posExit.Y);
                }
                if (!change)
                {
                    _canvas.Cursor = Cursors.Arrow;
                }
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }
    }
}
