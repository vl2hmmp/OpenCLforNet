using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.Function;

namespace OpenCLforNet.Runtime
{
    public unsafe class Event: IDisposable
    {

        private long? executionTime = null;
        public long ExecutionTime
        {
            get
            {
                if (executionTime == null)
                {
                    Wait();

                    long start, end;
                    OpenCL.clGetEventProfilingInfo(Pointer, cl_profiling_info.CL_PROFILING_COMMAND_START, new IntPtr(sizeof(long)), &start, null);
                    OpenCL.clGetEventProfilingInfo(Pointer, cl_profiling_info.CL_PROFILING_COMMAND_END, new IntPtr(sizeof(long)), &end, null);
                    executionTime = end - start;
                }
                return (long)executionTime;
            }
        }

        readonly void* _Pointer;
        public void* Pointer 
        { 
            get 
            {
                if (disposed) { throw new ObjectDisposedException(nameof(Event)); }
                else { return _Pointer; }
            }
        }

        internal Event(void* pointer)
        {
            _Pointer = pointer;
        }

        public void Wait()
        {
            var event_ = Pointer;
            OpenCL.clWaitForEvents(1, &event_);
        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed) { return; }
            OpenCL.clReleaseEvent(Pointer);
            disposed = true;
            GC.SuppressFinalize(this);
        }
        ~Event() 
        {
            Dispose(); 
        }
    }
}
