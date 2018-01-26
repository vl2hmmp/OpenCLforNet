using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class CommandQueue
    {

        public readonly Context Context;
        public readonly Device Device;
        public readonly long Pointer;

        public CommandQueue(Context context, Device device)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Context = context;
            Device = device;
            Pointer = OpenCL.clCreateCommandQueue(context.Pointer, device.Pointer, null, &status);
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
