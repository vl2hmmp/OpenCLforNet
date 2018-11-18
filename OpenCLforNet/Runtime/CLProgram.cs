using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Function;

namespace OpenCLforNet.Runtime
{
    public unsafe class CLProgram
    {

        public Context Context { get; }
        public void* Pointer { get; }

        public CLProgram(string source, Context context)
        {
            Context = context;

            int status = (int)cl_status_code.CL_SUCCESS;
            var sourceArray = Encoding.UTF8.GetBytes(source);
            var lengths = (void *)(new IntPtr(source.Length));
            fixed (byte* sourceArrayPointer = sourceArray)
            {
                byte*[] sourcesArray = new byte*[] { sourceArrayPointer };
                fixed (byte** sourcesArrayPointer = sourcesArray)
                {
                    Pointer = OpenCL.clCreateProgramWithSource(context.Pointer, 1, sourcesArrayPointer, &lengths, &status);
                    status.CheckError();
                }
            }

            try
            {
                var devices = context.Devices;
                var devicePointers = (void**)Marshal.AllocCoTaskMem((devices.Length * IntPtr.Size));
                for (var i = 0; i < devices.Length; i++)
                    devicePointers[i] = devices[i].Pointer;
                OpenCL.clBuildProgram(Pointer, (uint)devices.Length, devicePointers, null, null, null).CheckError();
                
            }
            catch (Exception e)
            {
                long logSize;
                OpenCL.clGetProgramBuildInfo(Pointer, context.Devices[0].Pointer, cl_program_build_info.CL_PROGRAM_BUILD_LOG, IntPtr.Zero, null, &logSize).CheckError();
                byte[] log = new byte[logSize + 1];
                fixed (byte* logPointer = log)
                {
                    OpenCL.clGetProgramBuildInfo(Pointer, context.Devices[0].Pointer, cl_program_build_info.CL_PROGRAM_BUILD_LOG, new IntPtr(logSize), logPointer, null).CheckError();
                }
                OpenCL.clReleaseProgram(Pointer);
                throw new Exception(e.Message + Environment.NewLine + Encoding.UTF8.GetString(log, 0, (int)logSize));
            }
        }

        public Kernel CreateKernel(string kernelName)
        {
            return new Kernel(this, kernelName);
        }

        public void Release()
        {
            OpenCL.clReleaseProgram(Pointer).CheckError();
        }

    }
}
