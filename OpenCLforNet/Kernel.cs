using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class Kernel
    {

        public readonly string KernelName;
        public readonly CLProgram Program;
        public readonly long Pointer;

        public Kernel(CLProgram program, string kernelName)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            var kernelNameArray = Encoding.UTF8.GetBytes(kernelName);
            fixed (byte* kernelNameArrayPointer = kernelNameArray)
            {
                Pointer = OpenCL.clCreateKernel(program.Pointer, kernelNameArrayPointer, &status);
                KernelName = kernelName;
                Program = program;
                OpenCL.CheckError(status);
            }
        }

        public void SetArgs(params object[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] is Memory mem)
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

        private long[] WorkSizes = new long[] { 0, 0, 0 };

        public void SetWorkSize(long[] workSizes)
        {
            WorkSizes = (long[])workSizes.Clone();
        }

        public void NDRange(CommandQueue commandQueue)
        {
            fixed (long* workSizeArrayPointer = WorkSizes)
            {
                OpenCL.CheckError(OpenCL.clEnqueueNDRangeKernel(commandQueue.Pointer, Pointer, WorkSizes.Rank, null, workSizeArrayPointer, null, 0, null, null));
            }
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
