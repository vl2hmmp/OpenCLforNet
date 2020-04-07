using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Function;

namespace OpenCLforNet.Runtime
{
    public unsafe class CommandQueue : IDisposable
    {
        private bool isDisposed = false;

        public Context Context { get; }
        public Device Device { get; }
        public void* Pointer { get; }

        public CommandQueue(Context context, Device device)
        {
            var status = cl_status_code.CL_SUCCESS;
            Context = context;
            Device = device;
            Pointer = OpenCL.clCreateCommandQueue(context.Pointer, device.Pointer, cl_command_queue_properties.CL_QUEUE_PROFILING_ENABLE, &status);
            status.CheckError();
        }

        ~CommandQueue() => Dispose(false);

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
        public Event NDRangeKernel(Kernel kernel, params Event[] eventWaitList)
        {
            return kernel.NDRange(this, eventWaitList);
        }

        public void WaitFinish()
        {
            OpenCL.clFinish(Pointer).CheckError();
        }

        protected void DisposeUnManaged()
        {
            OpenCL.clReleaseCommandQueue(Pointer).CheckError();
        }

        protected virtual void DisposeManaged() { }

        public virtual void Release() => Dispose();
    }
}
