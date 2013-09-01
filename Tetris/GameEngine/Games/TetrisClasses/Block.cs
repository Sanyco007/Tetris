using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tetris.GameEngine.Games.TetrisClasses
{
    public class Block
    {
        private const int FiguresCount = 7;
        private const int Rotations = 4;
    
        private readonly int[,,,] _figures = { //[figure][rotation] -> matrix
            { //O figure
                { //first rotation
                    {0,0,0,0},
                    {0,1,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,0,0,0},
                    {0,1,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,0,0,0},
                    {0,1,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {0,0,0,0},
                    {0,1,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                }
            },
            { //T figure
                { //first rotation
                    {0,1,0,0},
                    {1,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,0,0,0},
                    {1,1,1,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,1,0,0},
                    {0,1,1,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {0,1,0,0},
                    {1,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            },
            { //L figure
                { //first rotation
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,0,1,0},
                    {1,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {1,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {1,1,1,0},
                    {1,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            },
            { //I figure
                { //first rotation
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0}
                },
                { //second rotation
                    {0,0,0,0},
                    {1,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0}
                },
                { //fourth rotation
                    {0,0,0,0},
                    {1,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            },
            { //Z figure
                { //first rotation
                    {0,0,0,0},
                    {1,1,0,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,0,1,0},
                    {0,1,1,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,0,0,0},
                    {1,1,0,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {0,0,1,0},
                    {0,1,1,0},
                    {0,1,0,0},
                    {0,0,0,0}
                }
            },
            { //S figure
                { //first rotation
                    {0,0,0,0},
                    {0,0,1,1},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,1,0,0},
                    {0,1,1,0},
                    {0,0,1,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,0,0,0},
                    {0,0,1,1},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {0,1,0,0},
                    {0,1,1,0},
                    {0,0,1,0},
                    {0,0,0,0}
                }
            },
            { //J figure
                { //first rotation
                    {0,0,1,0},
                    {0,0,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                },
                { //second rotation
                    {0,1,1,1},
                    {0,0,0,1},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                { //third rotation
                    {0,1,1,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
                { //fourth rotation
                    {0,1,0,0},
                    {0,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            }
        };

        private Point _position;

        private int _currentFigure;
        private Bitmap _currentColor;
        private int _rotation;

        private int _nextFigure;
        private Bitmap _nextColor;
        private int _nextIntColor;

        private readonly Random _rand = new Random();

        public Block()
        {
            _position = new Point();
            _nextFigure = _rand.Next(FiguresCount);
            _nextColor = Images.Colors[_rand.Next(Images.Colors.Length)];
        }

        public void Generate()
        {
            _currentFigure = _nextFigure;
            _currentColor = _nextColor;
            _rotation = 0;
            _position.X = 3;
            int y = 20;
            for (int j = 0; j < 4; j++)
            {
                bool find = false;
                for (int i = 0; i < 4; i++)
                {
                    if (_figures[_currentFigure, _rotation, i, j] == 1)
                    {
                        y += j;
                        find = true;
                        break;
                    }
                }
                if (find) break;
            }
            _position.Y = y;
            _nextFigure = _rand.Next(FiguresCount);
            _nextIntColor = _rand.Next(Images.Colors.Length);
            _nextColor = Images.Colors[_nextIntColor];
        }

        public void Rotate(Item[,] field)
        {
            int oldRotation = _rotation;
            if (++_rotation >= Rotations) {
                _rotation = 0;
            }
            int oldX = _position.X;
            if (Intersects(field))
            {
                if (_position.X < Game.Width / 2)
                {
                    _position.X += 1;
                }
                else _position.X -= 1;
            }
            if (Intersects(field))
            {
                _position.X = oldX;
                _rotation = oldRotation;
            }
        }

        public void MoveLeft(Item[,] field)
        {
            _position.X -= 1;
            if (Intersects(field))
            {
                _position.X += 1;
            }
        }

        public bool Intersects(Item[,] field)
        {
            var matrix = _figures;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[_currentFigure, _rotation, i, j] == 1 && _position.X + i < 0)
                    {
                        return true;
                    }
                    if (matrix[_currentFigure, _rotation, i, j] == 1 && 
                        _position.X + i >= Game.Width)
                    {
                        return true;
                    }
                    if (_position.X + i >= Game.Width || _position.X + i < 0 ||
                        _position.Y - j >= Game.Height || _position.Y - j < 0)
                    {
                        continue;
                    }
                    if (matrix[_currentFigure, _rotation, i, j] == 1 &&
                            field[_position.X + i, _position.Y - j] != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveRight(Item[,] field)
        {
            _position.X += 1;
            if (Intersects(field))
            {
                _position.X -= 1;
            }
        }

        public void MoveDown()
        {
            _position.Y -= 1;
        }
    
        public List<Item> GetItems() {
            var items = new List<Item>();
            int[, , ,] matrix = _figures;
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (matrix[_currentFigure, _rotation, i, j] == 1)
                    {
                        var pos = new Point(_position.X + i, _position.Y - j);
                        var item = new Item(pos, _currentColor);
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public Point GetNextOffset()
        {
            var result = new Point(0, 0);
            switch (_nextFigure)
            {
                case 1: result = new Point(5, 9);
                    break;
                case 2: result = new Point(5, 0);
                    break;
                case 3: result = new Point(0, 5);
                    break;
                case 4: result = new Point(0, 5);
                    break;
                case 5: result = new Point(0, -5);
                    break;
                case 6: result = new Point(5, 0);
                    break;
            }
            return result;
        }

        public List<Item> GetNextItems()
        {
            var items = new List<Item>();
            int[, , ,] matrix = _figures;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[_nextFigure, 0, i, j] == 1)
                    {
                        var pos = new Point(i, j);
                        var item = new Item(pos, Images.SmallColors[_nextIntColor]);
                        items.Add(item);
                    }
                }
            }
            return items;
        }
    }
}
