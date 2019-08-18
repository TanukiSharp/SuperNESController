using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMediaPlayer
{
    public class VolumeDownExtension : ExtensionBase
    {
        public VolumeDownExtension()
            : base("Volume Down", 1, AppCommands.VolumeDown)
        {
        }
    }
}
