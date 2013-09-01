using System.Drawing;

namespace Tetris.GameEngine.Inputs
{
    class MouseState
    {
        public static int X;
        public static int Y;

        private static Point _mouseDown;

        public static bool MouseMove;

        static MouseState()
        {
            X = 0;
            Y = 0;
            MouseMove = false;
            _mouseDown = Point.Empty;
        }

        public static void SetMouseDown(int x, int y)
        {
            _mouseDown.X = x;
            _mouseDown.Y = y;
        }

        public static bool IsMouseMove()
        {
            var res = MouseMove;
            MouseMove = false;
            return res;
        }

        public static Point GetMouseDown()
        {
            if (_mouseDown.IsEmpty) return Point.Empty;
            var res = new Point(_mouseDown.X, _mouseDown.Y);
            _mouseDown = Point.Empty;
            return res;
        }

        public static void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
            MouseMove = true;
        }

        public static void Reset()
        {
            X = 0;
            Y = 0;
        }
    }
}
