using System.Drawing;

namespace Tetris.GameEngine.Games.TetrisClasses
{
    public class Item
    {
        public Bitmap Image;
        public Point Position;

        public Item(Point position, Bitmap image)
        {
            Position = position;
            Image = image;
        }
    }
}
