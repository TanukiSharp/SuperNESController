using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMediaPlayer
{
    public class NextTrackExtension : ExtensionBase
    {
        public NextTrackExtension()
            : base("Next Track", 1, AppCommands.MediaNext)
        {
        }
    }
}
