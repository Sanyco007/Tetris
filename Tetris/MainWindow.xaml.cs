using System.Windows;
using System.Windows.Input;
using Tetris.GameEngine;
using Tetris.GameEngine.Inputs;
using Kbd = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Game _game;

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game(canvas, next, lines);
            Game.Instance = _game;
            _game.Start();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Kbd.KeyDown(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Kbd.KeyUp(e.Key);
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            Kbd.ClearState();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _game.Stop();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var x = (int)e.GetPosition(canvas).X;
            var y = (int)e.GetPosition(canvas).Y;
            MouseState.SetPosition(x, y);
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var x = (int)e.GetPosition(canvas).X;
            var y = (int)e.GetPosition(canvas).Y;
            MouseState.SetMouseDown(x, y);
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseState.Reset();
        }
    }
}
