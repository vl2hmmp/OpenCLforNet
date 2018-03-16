using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Memory;
using OpenCLforNet.RuntimeFunction;

namespace OpenCLforNet.Runtime
{
    public unsafe class Kernel
    {

        public string KernelName { get; }
        public CLProgram Program { get; }
        public void *Pointer { get; }

        public Kernel(CLProgram program, string kernelName)
        {
            KernelName = kernelName;
            Program = program;

            int status = (int)cl_status_code.CL_SUCCESS;
            var kernelNameArray = Encoding.UTF8.GetBytes(kernelName);
            fixed (byte* kernelNameArrayPointer = kernelNameArray)
            {
                Pointer = OpenCL.clCreateKernel(program.Pointer, kernelNameArrayPointer, &status);
                OpenCL.CheckError(status);
            }
        }

        public void SetArgs(params object[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] is AbstractMemory mem)
                {
                    var argPointer = mem.Pointer;
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(long), &argPointer));
                }
                else if (args[i] is SVMBuffer buf)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArgSVMPointer(Pointer, i, buf.Pointer));
                }
                else if (args[i] is byte barg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(byte), &barg));
                }
                else if (args[i] is char carg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(char), &carg));
                }
                else if (args[i] is short sarg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(short), &sarg));
                }
                else if (args[i] is int iarg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(int), &iarg));
                }
                else if (args[i] is long larg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(long), &larg));
                }
                else if (args[i] is float farg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(float), &farg));
                }
                else if (args[i] is double darg)
                {
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(double), &darg));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private void **WorkSizes = (void**)Marshal.AllocCoTaskMem(3 * IntPtr.Size);

        public void SetWorkSize(long[] workSizes)
        {
            if (3 != workSizes.Length)
                throw new ArgumentException("workSizes length must be 3");
            for (var i = 0; i < 3; i++)
                WorkSizes[i] = (void *)(new IntPtr(workSizes[i]));
        }

        public void NDRange(CommandQueue commandQueue)
        {
            OpenCL.CheckError(OpenCL.clEnqueueNDRangeKernel(commandQueue.Pointer, Pointer, 3, null, WorkSizes, null, 0, null, null));
        }

        public void NDRange(CommandQueue commandQueue, long[] workSizes)
        {
            SetWorkSize(workSizes);
            NDRange(commandQueue);
        }

        public void NDRange(CommandQueue commandQueue, long[] workSizes, params object[] args)
        {
            SetWorkSize(workSizes);
            SetArgs(args);
            NDRange(commandQueue);
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseKernel(Pointer));
        }

    }
}
