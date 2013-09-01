using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace Tetris.GameEngine
{
    public class GameLoop : DispatcherObject
    {
        private readonly Game _game;
        private readonly Thread _loop;
        private bool _run;
        private const int FrameRate = 60;
        private const int UpdateTime = 1000 / FrameRate;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public static int Fps = 0;

        public GameLoop(Game game)
        {
            _game = game;
            _run = false;
            _loop = new Thread(Run) {IsBackground = true};
        }

        public void Start()
        {
            _run = true;
            _stopwatch.Start();
            _loop.Start();
        }

        public void Stop()
        {
            _run = false;
        }

        private void Run()
        {
            long lastTime = _stopwatch.ElapsedMilliseconds;
            long ellapsedTime = 0;
            long fpsEllapsed = 0;
            int fps = 0;
            while (_run)
            {
                fpsEllapsed += _stopwatch.ElapsedMilliseconds - lastTime;
                ellapsedTime += _stopwatch.ElapsedMilliseconds - lastTime;
                if (ellapsedTime >= UpdateTime)
                {
                    //_game.Update(UpdateTime);
                    //Dispatcher.Invoke((Action)(() => _game.Redraw()));
                    Dispatcher.Invoke((Action)(() => _game.Update(UpdateTime)));
                    fps++;
                    ellapsedTime -= UpdateTime;
                }
                if (fpsEllapsed >= 1000)
                {
                    Fps = fps;
                    fps = 0;
                    fpsEllapsed = 0;
                }
                lastTime = _stopwatch.ElapsedMilliseconds;
                while (_stopwatch.ElapsedMilliseconds - lastTime < UpdateTime)
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}
