using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet.Memory
{
    public abstract class AbstractBuffer : IDisposable
    {
        private bool isDisposed = false;

        ~AbstractBuffer() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool includeManaged)
        {
            if (!isDisposed)
            {
                if (includeManaged)
                    DisposeManaged();

                DisposeUnManaged();
                isDisposed = true;
            }
        }

        protected abstract void DisposeUnManaged();

        protected abstract void DisposeManaged();

        public virtual void Release() => Dispose();
    }
}
