using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIDLibrary.Core
{
	public class Win32MessagePump : IMessagePump
	{
		public event EventHandler<MessagePumpEventArgs> MessageReceived;

        public IntPtr Handle { get; private set; }

        private IntPtr usbEventsSubscription = IntPtr.Zero;

    	public IDisposable Start()
		{
            Form form = null;

            Handle = IntPtr.Zero;

            var mre = new ManualResetEventSlim(false);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                form = new InternalForm(this);
                mre.Set();
                Application.Run(form);
            });

            mre.Wait();

            return new AnonymousDisposable(() => form.Invoke((Action)delegate { form.Close(); }, null));
        }

		private class InternalForm : Form
		{
			private readonly Win32MessagePump messagePump;

			public InternalForm(Win32MessagePump messagePump)
			{
				this.messagePump = messagePump;
            }

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

				ControlBox = false;
				FormBorderStyle = FormBorderStyle.None;
				ShowIcon = false;
				ShowInTaskbar = false;
				Visible = false;
			}

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);

                messagePump.Handle = Handle;
                messagePump.usbEventsSubscription = Win32USB.RegisterForUsbEvents(Handle, HID.Guid);
            }

            protected override void OnHandleDestroyed(EventArgs e)
            {
                base.OnHandleDestroyed(e);

                Win32USB.UnregisterDeviceNotification(messagePump.usbEventsSubscription);
                messagePump.Handle = IntPtr.Zero;
            }

			protected override void WndProc(ref Message m)
			{
				messagePump.MessageReceived?.Invoke(messagePump, new MessagePumpEventArgs(Handle, m.Msg, m.WParam, m.LParam));

				base.WndProc(ref m);
			}
		}
	}
}
