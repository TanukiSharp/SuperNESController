using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMediaPlayer
{
    public class PlayPauseExtension : ExtensionBase
    {
        public PlayPauseExtension()
            : base("Play/Pause", 1, AppCommands.MediaPlayPause)
        {
        }
    }
}
