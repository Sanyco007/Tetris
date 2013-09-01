using System.Drawing;
using System.Windows.Input;
using Image = System.Windows.Controls.Image;
using Keyboard = Tetris.GameEngine.Inputs.Keyboard;

namespace Tetris.GameEngine.Games
{
    public class Options : IGameScreen
    {
        private readonly Image _canvas;
        private readonly Bitmap _buffer;
        private readonly Game _game;

        public Options(Image canvas, Game game)
        {
            _canvas = canvas;
            _game = game;
            _buffer = new Bitmap(180, 360);
        }

        public void Update(long delta)
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                _game.BackScreen();
            }
        }

        public void Redraw()
        {
            using (Graphics gr = Graphics.FromImage(_buffer))
            {
                gr.DrawImage(Images.Grid, 0, 0);
            }
            _canvas.Source = Images.ToBitmapSource(_buffer);
        }
    }
}
