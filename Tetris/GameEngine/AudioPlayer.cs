using System.Collections.Generic;
using IrrKlang;

namespace Tetris.GameEngine
{
    public class AudioPlayer
    {
        private static readonly List<ISound> MusicList = new List<ISound>();
        private static readonly List<ISound> EffectList = new List<ISound>();
        private static readonly ISoundEngine SoundEngine;

        public static float MusicVolume = 0.4f;
        public static float EffectVolume = 0.5f;

        public static bool SoundOn = true;

        public const string EffectRotate = "sound\\rotate.ogg";
        public const string EffectDown = "sound\\down.ogg";
        public const string EffectDrop = "sound\\drop.ogg";
        public const string EffectLine = "sound\\line.ogg";
        public const string EffectBite = "sound\\bite.ogg";
        public const string EffectDirection = "sound\\direction.ogg";

        public const string MusicTheme = "sound\\theme.ogg";
        public const string SnakeTheme = "sound\\snakeTheme.ogg";

        static AudioPlayer()
        {
            SoundEngine = new ISoundEngine();
        }

        public static void PlayEffect(string effect)
        {
            var se = new ISoundEngine();
            var sound = se.Play2D(effect, false, true);
            sound.Volume = SoundOn ? EffectVolume : 0;
            EffectList.Add(sound);
            sound.Paused = false;
        }

        public static void PlayMusic(string sound, bool loop = true)
        {
            var music = SoundEngine.Play2D(sound, loop, true);
            music.Volume = SoundOn ? MusicVolume : 0;
            MusicList.Add(music);
            music.Paused = false;
        }

        public static void Pause(bool state)
        {
            foreach (var item in MusicList)
            {
                item.Paused = state;
            }
        }

        public static void Update()
        {
            foreach (ISound t in MusicList)
            {
                t.Volume = !SoundOn ? 0 : MusicVolume;
            }
            foreach (ISound t in EffectList)
            {
                t.Volume = !SoundOn ? 0 : EffectVolume;
            }
            Clear();
        }

        private static void Clear()
        {
            for (int i = 0; i < MusicList.Count; i++)
            {
                if (MusicList[i].Finished)
                {
                    MusicList.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < EffectList.Count; i++)
            {
                if (EffectList[i].Finished)
                {
                    EffectList.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
