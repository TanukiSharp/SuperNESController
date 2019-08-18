using System;

namespace HIDLibrary.Core
{
	public class MessagePumpEventArgs : EventArgs
	{
		public MessagePumpEventArgs(IntPtr handle, int message, IntPtr hParam, IntPtr lParam)
		{
			Handle = handle;
			Message = message;
			HighParam = hParam;
			LowParam = lParam;
		}

		public IntPtr Handle { get; private set; }
		public int Message { get; private set; }
		public IntPtr HighParam { get; private set; }
		public IntPtr LowParam { get; private set; }
	}

	public interface IMessagePump
	{
        IntPtr Handle { get; }
		event EventHandler<MessagePumpEventArgs> MessageReceived;
		IDisposable Start();
	}
}
