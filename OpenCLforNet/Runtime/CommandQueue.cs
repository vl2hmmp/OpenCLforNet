using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.RuntimeFunction;

namespace OpenCLforNet.Runtime
{
    public unsafe class CommandQueue
    {

        public Context Context { get; }
        public Device Device { get; }
        public void *Pointer { get; }

        public CommandQueue(Context context, Device device)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Context = context;
            Device = device;
            Pointer = OpenCL.clCreateCommandQueue(context.Pointer, device.Pointer, cl_command_queue_properties.CL_QUEUE_ON_DEVICE, &status);
            OpenCL.CheckError(status);
        }

        public void NDRangeKernel(Kernel kernel, long[] workSizes, params object[] args)
        {
            kernel.NDRange(this, workSizes, args);
        }

        public void WaitFinish()
        {
            OpenCL.CheckError(OpenCL.clFinish(Pointer));
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseCommandQueue(Pointer));
        }

    }
}
