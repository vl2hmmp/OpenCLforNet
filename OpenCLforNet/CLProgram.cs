using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class CLProgram
    {

        public readonly Context Context;
        public readonly long Pointer;

        public CLProgram(string source, Context context)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            var sourceArray = Encoding.UTF8.GetBytes(source);
            var lengthArray = new int[] { source.Length };
            fixed (byte* sourceArrayPointer = sourceArray)
            {
                byte*[] sourcesArray = new byte*[] { sourceArrayPointer };
                fixed (byte** sourcesArrayPointer = sourcesArray)
                {
                    fixed (int* lengthArrayPointer = lengthArray)
                    {
                        Pointer = OpenCL.clCreateProgramWithSource(context.Pointer, 1, sourcesArrayPointer, lengthArrayPointer, &status);
                        OpenCL.CheckError(status);
                    }
                }
            }

            try
            {
                var deviceArray = new List<long>();
                foreach (var device in context.Devices)
                    deviceArray.Add(device.Pointer);
                fixed (long* deviceArrayPointer = deviceArray.ToArray())
                {
                    OpenCL.CheckError(OpenCL.clBuildProgram(Pointer, deviceArray.Count, deviceArrayPointer, null, null, null));
                    Context = context;
                }
            }
            catch (Exception e)
            {
                OpenCL.clReleaseProgram(Pointer);
                throw e;
            }
        }

        public Kernel CreateKernel(string kernelName)
        {
            return new Kernel(this, kernelName);
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseProgram(Pointer));
        }

    }
}
