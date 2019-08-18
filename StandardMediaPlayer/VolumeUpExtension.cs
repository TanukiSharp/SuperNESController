using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMediaPlayer
{
    public class VolumeUpExtension : ExtensionBase
    {
        public VolumeUpExtension()
            : base("Volume Up", 1, AppCommands.VolumeUp)
        {
        }
    }
}
