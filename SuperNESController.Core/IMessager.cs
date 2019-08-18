using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNESController.Core
{
    public enum MessageIcon
    {
        None,
        Info,
        Warning,
        Error,
    }

    public interface IMessager
    {
        void ShowMessage(int timeout, string title, string message, MessageIcon icon);
    }
}
