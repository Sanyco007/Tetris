using System.Drawing;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games.MenuClasses
{
    class TetrisMenu : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;

        private readonly Menu _menu;

        private long _ellapsed;
        private const long UpdateTime = 150L;

        private long _ellapsedFigure;
        private const long UpdateTimeFigure = 500L;

        private int _dxFigure;

        private int _dx;
        private int _inc;

        private readonly int[,] _tetris =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,0,0,1,0,1,1,1,0},
                {0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,1,0,1,0,1,0,0,0},
                {0,0,1,0,0,1,1,0,0,0,1,0,0,1,1,0,0,1,0,1,1,1,0},
                {0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,1,0,1,0,0,0,1,0},
                {0,0,1,0,0,1,1,1,0,0,1,0,0,1,0,1,0,1,0,1,1,1,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };

        private readonly int[,] _image =
            {
                {9,9,9,9,9,9,9,9,9,9},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,6,6,0,0,0,0},
                {0,0,0,0,6,6,0,0,0,0},
                {0,0,0,0,7,2,2,0,0,0},
                {4,4,0,7,7,3,2,0,0,6},
                {4,4,4,4,7,3,2,0,0,6},
                {2,2,3,4,4,3,5,5,0,6},
                {2,2,3,3,3,3,5,5,0,6},
                {9,9,9,9,9,9,9,9,9,9}
            };

        private readonly int[,] _figure =
            {
                {2,2},
                {0,2},
                {0,2}
            };

        public TetrisMenu(Image canvas, Menu menu)
        {
            _canvas = canvas;
            _menu = menu;
            _dx = 0;
            _inc = 1;
            _dxFigure = 0;
            _buffer = new Bitmap(180, 360);
        }

        public void Update(long delta)
        {
            _ellapsed += delta;

            _ellapsedFigure += delta;

            if (Keyboard.IsKeyDown(Key.Right))
            {
                var screen = new SnakeMenu(_canvas, _menu);
                _menu.Push(screen);
            }

            if (_ellapsed >= UpdateTime)
            {
                _dx += _inc;
                if (_dx >= 14 || _dx < 0)
                {
                    _inc = -_inc;
                    _dx += 2 * _inc;
                }
                _ellapsed = 0;
                Redraw();
            }
            if (_ellapsedFigure >= UpdateTimeFigure)
            {
                _dxFigure += 1;
                if (_dxFigure >= 9)
                {
                    _dxFigure = 0;
                }
                _ellapsedFigure = 0;
                Redraw();
            }
        }

        public void Redraw()
        {
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(Images.Grid, 0, 0);
                DrawTetris(gr);
                DrawImage(gr);
                DrawFigure(gr);
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }

        private void DrawFigure(Graphics gr)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int pixel = _figure[i, j];
                    if (pixel == 0) continue;
                    int x = j + 7;
                    int y = i + 8 + _dxFigure;
                    gr.DrawImage(Images.Colors[pixel - 1], x * Game.H, y * Game.H);
                }
            }
        }

        private void DrawTetris(Graphics gr)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < Game.Width; j++)
                {
                    int pixel = _tetris[i, j + _dx];
                    if (pixel == 0) continue;
                    gr.DrawImage(Images.Colors[pixel - 1], j * Game.H, i * Game.H);
                }
            }
        }

        private void DrawImage(Graphics gr)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < Game.Width; j++)
                {
                    int pixel = _image[i, j];
                    if (pixel == 0) continue;
                    int x = j;
                    int y = i + 7;
                    gr.DrawImage(pixel == 9 ? 
                        Images.Feature : Images.Colors[pixel - 1], x * Game.H, y * Game.H);
                }
            }
        }
    }
}
