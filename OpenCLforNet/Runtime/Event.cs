using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.Function;

namespace OpenCLforNet.Runtime
{
    public unsafe class Event
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
        public void* Pointer { get; }

        internal Event(void* pointer)
        {
            Pointer = pointer;
        }

        public Event Wait()
        {
            var event_ = Pointer;
            OpenCL.clWaitForEvents(1, &event_);
            return this;
        }

    }
}
