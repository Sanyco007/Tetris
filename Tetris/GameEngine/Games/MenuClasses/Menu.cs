using System.Collections.Generic;
using System.Windows.Input;
using Tetris.GameEngine.Games.SnakeClasses;
using Tetris.GameEngine.Games.TetrisClasses;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;
using Label = System.Windows.Controls.Label;

namespace Tetris.GameEngine.Games.MenuClasses
{
    public class Menu : IGameScreen
    {
        private readonly Game _game;

        private readonly Image _canvas;
        private readonly Image _next;
        private readonly Label _lines;

        private readonly Stack<IGameScreen> _menus;

        public Menu(Image canvas, Image next, Label lines, Game game)
        {
            _canvas = canvas;
            _next = next;
            _lines = lines;
            _game = game;
            _menus = new Stack<IGameScreen>();
            _menus.Push(new TetrisMenu(_canvas, this));
        }

        public void Push(IGameScreen menu)
        {
            lock (_menus)
            {
                _menus.Push(menu);
            }
        }

        public void Back()
        {
            lock (_menus)
            {
                if (_menus.Count > 1)
                {
                    _menus.Pop();
                }
            }
        }

        public void Update(long delta)
        {
            var screen = _menus.Peek();
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if (screen.GetType() == typeof(TetrisMenu))
                {
                    var tetris = new TetrisGame(_canvas, _next, _lines, _game);
                    _game.PushScreen(tetris);
                }
                if (screen.GetType() == typeof(SnakeMenu))
                {
                    var snake = new SnakeGame(_canvas, _game);
                    _game.PushScreen(snake);
                }
            }
            screen.Update(delta);
        }

    }
}
