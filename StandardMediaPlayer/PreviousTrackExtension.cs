using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMediaPlayer
{
    public class PreviousTrackExtension : ExtensionBase
    {
        public PreviousTrackExtension()
            : base("Previous Track", 1, AppCommands.MediaPrevious)
        {
        }
    }
}
