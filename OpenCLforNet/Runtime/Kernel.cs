using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Memory;
using OpenCLforNet.Function;

namespace OpenCLforNet.Runtime
{
    public unsafe class Kernel
    {

        public string KernelName { get; }
        public CLProgram Program { get; }
        public void* Pointer { get; }

        public Kernel(CLProgram program, string kernelName)
        {
            KernelName = kernelName;
            Program = program;

            int status = (int)cl_status_code.CL_SUCCESS;
            var kernelNameArray = Encoding.UTF8.GetBytes(kernelName);
            fixed (byte* kernelNameArrayPointer = kernelNameArray)
            {
                Pointer = OpenCL.clCreateKernel(program.Pointer, kernelNameArrayPointer, &status);
                status.CheckError();
            }
        }

        private void*[] Args = new void*[0];

        public void SetArgs(params object[] args)
        {
            foreach (var arg in Args)
            {
                Marshal.FreeCoTaskMem(new IntPtr(arg));
            }
            Args = new void*[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    throw new NullReferenceException();
                }
                else if (args[i] is AbstractMemory mem)
                {
                    var argPointer = (void*)Marshal.AllocCoTaskMem(IntPtr.Size);
                    Marshal.WriteIntPtr(new IntPtr(argPointer), new IntPtr(mem.Pointer));
                    OpenCL.clSetKernelArg(Pointer, i, IntPtr.Size, argPointer).CheckError();
                    Args[i] = argPointer;
                }
                else if (args[i] is SVMBuffer buf)
                {
                    OpenCL.clSetKernelArgSVMPointer(Pointer, i, buf.Pointer).CheckError();
                }
                else if (args[i] is byte barg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(byte), &barg).CheckError();
                }
                else if (args[i] is char carg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(char), &carg).CheckError();
                }
                else if (args[i] is short sarg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(short), &sarg).CheckError();
                }
                else if (args[i] is int iarg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(int), &iarg).CheckError();
                }
                else if (args[i] is long larg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(long), &larg).CheckError();
                }
                else if (args[i] is float farg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(float), &farg).CheckError();
                }
                else if (args[i] is double darg)
                {
                    OpenCL.clSetKernelArg(Pointer, i, sizeof(double), &darg).CheckError();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private uint Dimention = 1;
        private IntPtr* WorkSizes = (IntPtr*)Marshal.AllocCoTaskMem(3 * IntPtr.Size);

        public void SetWorkSize(params long[] workSizes)
        {
            if (workSizes.Length <= 0 || 4 <= workSizes.Length)
                throw new ArgumentException("workSizes length is invalid.");

            Dimention = (uint)workSizes.Rank;
            for (var i = 0; i < Dimention; i++)
                WorkSizes[i] = new IntPtr(workSizes[i]);
        }

        public Event NDRange(CommandQueue commandQueue, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueNDRangeKernel(commandQueue.Pointer, Pointer, Dimention, null, WorkSizes, null, num, listPointer, &event_).CheckError();
            }
            
            return new Event(event_);
        }

        public Event NDRange(CommandQueue commandQueue, long[] workSizes)
        {
            SetWorkSize(workSizes);
            return NDRange(commandQueue);
        }

        public Event NDRange(CommandQueue commandQueue, long[] workSizes, params object[] args)
        {
            SetWorkSize(workSizes);
            SetArgs(args);
            return NDRange(commandQueue);
        }

        public void Release()
        {
            foreach (var arg in Args)
            {
                Marshal.FreeCoTaskMem(new IntPtr(arg));
            }
            OpenCL.clReleaseKernel(Pointer).CheckError();
        }

    }
}
