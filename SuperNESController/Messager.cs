using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperNESController.Core;

namespace SuperNESController
{
    public class Messager : IMessager
    {
        private readonly NotifyIcon notification;
        private readonly SynchronizationContext context;

        public Messager(NotifyIcon notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            context = SynchronizationContext.Current;

            if (context == null)
                throw new InvalidOperationException("Impossible to get SynchronizationContext");

            this.notification = notification;
        }

        public void ShowMessage(int timeout, string title, string message, MessageIcon icon)
        {
            timeout = Math.Max(1000, Math.Min(timeout, 15000));
            context.Send(_ => notification.ShowBalloonTip(timeout, title, message, (ToolTipIcon)icon), null);
        }
    }
}
