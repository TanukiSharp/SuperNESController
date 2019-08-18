using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIDLibrary.Core
{
	public class AnonymousDisposable : IDisposable
	{
		private bool isDisposed;
		private readonly Action onDispose;

		public AnonymousDisposable(Action onDispose)
		{
			if (onDispose == null)
				throw new ArgumentNullException(nameof(onDispose));

			this.onDispose = onDispose;
		}

        ~AnonymousDisposable()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                if (isDisposed)
                    return;

                isDisposed = true;

                onDispose();
            }
        }

		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}
	}
}
