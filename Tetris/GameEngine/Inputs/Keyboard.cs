using System.Collections.Generic;
using System.Windows.Input;

namespace Tetris.GameEngine.Inputs
{
    class Keyboard
    {
        private static readonly HashSet<Key> Kbd = new HashSet<Key>();

        public static void KeyDown(Key key)
        {
            Kbd.Add(key);
        }

        public static void KeyUp(Key key)
        {
            Kbd.Remove(key);
        }

        public static bool IsKeyDown(Key key)
        {
            return Kbd.Contains(key);
        }

        public static void ClearState()
        {
            Kbd.Clear();
        }

    }
}
