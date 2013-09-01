using System;
using System.Drawing;
using System.Runtime.InteropServices;
using BitmapSource = System.Windows.Media.Imaging.BitmapSource;

namespace Tetris.GameEngine
{
    public class Images
    {
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private static readonly Bitmap Green = Properties.Resources.green;
        private static readonly Bitmap Red = Properties.Resources.red;
        private static readonly Bitmap Blue = Properties.Resources.blue;
        private static readonly Bitmap Light = Properties.Resources.light;
        private static readonly Bitmap Orange = Properties.Resources.orange;
        private static readonly Bitmap Violet = Properties.Resources.violet;
        private static readonly Bitmap Yellow = Properties.Resources.yellow;
        
        private static readonly Bitmap SGreen = Properties.Resources.green_small;
        private static readonly Bitmap SRed = Properties.Resources.red_small;
        private static readonly Bitmap SBlue = Properties.Resources.blue_small;
        private static readonly Bitmap SLight = Properties.Resources.light_small;
        private static readonly Bitmap SOrange = Properties.Resources.orange_small;
        private static readonly Bitmap SViolet = Properties.Resources.violet_small;
        private static readonly Bitmap SYellow = Properties.Resources.yellow_small;

        public static Bitmap[] Colors =
        {
            Green, Red, Blue, Light, Orange, Violet, Yellow
        };

        public static Bitmap[] SmallColors =
        {
            SGreen, SRed, SBlue, SLight, SOrange, SViolet, SYellow
        };

        public static Bitmap Feature = Properties.Resources.feature;

        public static Bitmap Grid = Properties.Resources.grid;

        public static Bitmap Pause = Properties.Resources.pause;

        public static Bitmap GoBackground = Properties.Resources.gobackground;
        public static Bitmap GoRepeat = Properties.Resources.gorepeat;
        public static Bitmap GoRepeatHover = Properties.Resources.gorepeat_hover;
        public static Bitmap GoMenu = Properties.Resources.gomenu;
        public static Bitmap GoMenuHover = Properties.Resources.gomenu_hover;
        public static Bitmap GoExit = Properties.Resources.goexit;
        public static Bitmap GoExitHover = Properties.Resources.goexit_hover;

        public static BitmapSource ToBitmapSource(Bitmap source)
        {
            var hBitmap = source.GetHbitmap();
            var result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap, IntPtr.Zero, System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hBitmap);
            return result;
        }

    }
}
