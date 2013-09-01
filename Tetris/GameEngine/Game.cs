using System.Collections.Generic;
using System.Windows;
using Tetris.GameEngine.Games;
using Tetris.GameEngine.Games.MenuClasses;
using Tetris.GameEngine.Games.SnakeClasses;
using Tetris.GameEngine.Games.TetrisClasses;
using Image = System.Windows.Controls.Image;
using Label = System.Windows.Controls.Label;

namespace Tetris.GameEngine
{
    public class Game
    {
        public const int Width = 10;
        public const int Height = 20;

        public const int H = 18;

        private readonly GameLoop _gameLoop;

        private readonly Image _canvas;
        private readonly Image _next;
        private readonly Label _lines;

        private readonly Stack<IGameScreen> _screens;

        public static Game Instance;

        public Game(Image canvas, Image next, Label lines)
        {
            _canvas = canvas;
            _next = next;
            _lines = lines;
            _screens = new Stack<IGameScreen>();
            var gameScreen = new Logo(_canvas);
            _screens.Push(gameScreen);
            _gameLoop = new GameLoop(this);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void PushScreen(IGameScreen screen)
        {
            lock (_screens)
            {
                _screens.Push(screen);
            }
        }

        public Game BackScreen()
        {
            lock (_screens)
            {
                _screens.Pop();
            }
            return this;
        }

        public void SetScreen()
        {
            lock (_screens)
            {
                _screens.Clear();
                var screen = new Menu(_canvas, _next, _lines, this);
                _screens.Push(screen);
            }
        }

        public void Start()
        {
            _gameLoop.Start();
        }

        public void Stop()
        {
            _gameLoop.Stop();
        }

        public void Update(long delta)
        {
            AudioPlayer.Update();
           _screens.Peek().Update(delta);
        }

    }
}
