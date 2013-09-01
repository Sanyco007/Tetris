using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games
{
    public class Logo : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;

        private readonly List<Point> _logo;

        private long _ellapsed;
        private const long UpdateTime = 50L;

        private int _steps;

        public Logo(Image canvas)
        {
            _canvas = canvas;
            _steps = 0;
            _logo = new List<Point>
            {
                //D
                new Point(12, 3),
                new Point(12, 4),
                new Point(12, 5),
                new Point(12, 6),
                new Point(12, 7),
                new Point(12, 8),
                new Point(13, 3),
                new Point(13, 8),
                new Point(14, 3),
                new Point(14, 8),
                new Point(15, 4),
                new Point(15, 5),
                new Point(15, 6),
                new Point(15, 7),
                //R
                new Point(17, 3),
                new Point(17, 4),
                new Point(17, 5),
                new Point(17, 6),
                new Point(17, 7),
                new Point(17, 8),
                new Point(18, 3),
                new Point(18, 6),
                new Point(19, 3),
                new Point(19, 6),
                new Point(20, 4),
                new Point(20, 5),
                new Point(20, 7),
                new Point(20, 8),
                //O
                new Point(22, 4),
                new Point(22, 5),
                new Point(22, 6),
                new Point(22, 7),
                new Point(23, 3),
                new Point(23, 8),
                new Point(24, 3),
                new Point(24, 8),
                new Point(25, 4),
                new Point(25, 5),
                new Point(25, 6),
                new Point(25, 7),
                //N
                new Point(27, 3),
                new Point(27, 4),
                new Point(27, 5),
                new Point(27, 6),
                new Point(27, 7),
                new Point(27, 8),
                new Point(28, 4),
                new Point(28, 5),
                new Point(29, 6),
                new Point(29, 7),
                new Point(30, 3),
                new Point(30, 4),
                new Point(30, 5),
                new Point(30, 6),
                new Point(30, 7),
                new Point(30, 8),
            };
            _buffer = new Bitmap(180, 360);
            Redraw();
        }

        public void Update(long delta)
        {
            _ellapsed += delta;

            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Keyboard.KeyUp(Key.Enter);
                Game.Instance.SetScreen();
            }

            if (_ellapsed >= UpdateTime)
            {
                for (var i = 0; i < _logo.Count; i++)
                {
                    _logo[i] = new Point(_logo[i].X - 1, _logo[i].Y);
                }
                _ellapsed -= UpdateTime;
                _steps++;
                Redraw();
                if (_steps >= 38)
                {
                    Game.Instance.SetScreen();
                }
            }
        }

        public void Redraw()
        {
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(Images.Grid, 0, 0);
                foreach (var item in _logo)
                {
                    if (item.X >= 0 && item.X < Game.Width)
                    {
                        gr.DrawImage(Images.Colors[4], item.X * Game.H, item.Y * Game.H);
                    }
                }
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }
    }
}
