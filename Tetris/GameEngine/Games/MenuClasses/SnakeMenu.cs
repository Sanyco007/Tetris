using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games.MenuClasses
{
    public class SnakeMenu : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;

        private readonly Menu _menu;

        private long _ellapsedFigure;
        private const long UpdateTimeFigure = 500L;

        private long _ellapsed;
        private const long UpdateTime = 150L;

        private int _dx;
        private int _inc;

        private readonly int[,] _snake =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,1,1,1,0,1,0,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0},
                {0,1,0,0,0,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0},
                {0,1,1,1,0,1,1,1,1,0,1,1,1,0,1,1,0,0,1,1,0,0},
                {0,0,0,1,0,1,0,1,1,0,1,0,1,0,1,0,1,0,1,0,0,0},
                {0,1,1,1,0,1,0,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };

        private readonly int[,] _image =
            {
                {9,9,9,9,9,9,9,9,9,9},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,3,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {9,9,9,9,9,9,9,9,9,9}
            };

        private List<Point> _list;

        private int _steps;

        public SnakeMenu(Image canvas, Menu menu)
        {
            _canvas = canvas;
            _steps = 0;
            _dx = 0;
            _inc = 1;
            _list = new List<Point> {new Point(1, 16), new Point(2, 16), new Point(3, 16)};
            _menu = menu;
            _buffer = new Bitmap(180, 360);
        }

        public void Update(long delta)
        {
            _ellapsed += delta;
            _ellapsedFigure += delta;

            if (Keyboard.IsKeyDown(Key.Left))
            {
                _menu.Back();
            }

            if (_ellapsed >= UpdateTime)
            {
                _dx += _inc;
                if (_dx >= 13 || _dx < 0)
                {
                    _inc = -_inc;
                    _dx += 2 * _inc;
                }
                _ellapsed = 0;
                Redraw();
            }

            if (_ellapsedFigure >= UpdateTimeFigure)
            {
                _steps++;
                if (_steps >= 11)
                {
                    _list = new List<Point> 
                        { new Point(1, 16), new Point(2, 16), new Point(3, 16) };
                    _steps = 0;
                }
                else
                {
                    var head = new Point
                    {
                        X = _list[_list.Count - 1].X + (_steps <= 5 ? 1 : 0),
                        Y = _list[_list.Count - 1].Y + (_steps <= 5 ? 0 : -1)
                    };
                    if (_steps == 10)
                    {
                        _list.Add(head);
                    }
                    else
                    {

                        for (int i = 0; i < _list.Count - 1; i++)
                        {
                            _list[i] = _list[i + 1];
                        }
                        _list[_list.Count - 1] = head;
                    }
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
                DrawSnake(gr);
                DrawImage(gr);
                DrawList(gr);
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }

        private void DrawList(Graphics gr)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                gr.DrawImage(i < _list.Count - 1 ? Images.Colors[0] : Images.Colors[1],
                             _list[i].X * Game.H, _list[i].Y * Game.H);
            }
        }

        private void DrawSnake(Graphics gr)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < Game.Width; j++)
                {
                    int pixel = _snake[i, j + _dx];
                    if (pixel == 0) continue;
                    gr.DrawImage(Images.Colors[4], j * Game.H, i * Game.H);
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
