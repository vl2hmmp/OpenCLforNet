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

        public void NDRange(CommandQueue commandQueue, int[] workSizes, params object[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var type = args[i].GetType();
                if (type.Equals(typeof(SimpleMemory)) || type.Equals(typeof(MappingMemory)))
                {
                    var argPointer = ((Memory)args[i]).Pointer;
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(long), &argPointer));
                }
                else if (type.Equals(typeof(SVMBuffer)))
                {
                    var argPointer = ((SVMBuffer)args[i]).Pointer;
                    OpenCL.CheckError(OpenCL.clSetKernelArgSVMPointer(Pointer, i, argPointer));
                }
                else if (type.Equals(typeof(byte)))
                {
                    var value = (byte)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(byte), &value));
                }
                else if (type.Equals(typeof(char)))
                {
                    var value = (char)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(char), &value));
                }
                else if (type.Equals(typeof(short)))
                {
                    var value = (short)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(short), &value));
                }
                else if (type.Equals(typeof(int)))
                {
                    var value = (int)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(int), &value));
                }
                else if (type.Equals(typeof(long)))
                {
                    var value = (long)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(long), &value));
                }
                else if (type.Equals(typeof(float)))
                {
                    var value = (float)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(float), &value));
                }
                else if (type.Equals(typeof(double)))
                {
                    var value = (double)args[i];
                    OpenCL.CheckError(OpenCL.clSetKernelArg(Pointer, i, sizeof(double), &value));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            fixed (int* workSizeArrayPointer = workSizes)
            {
                OpenCL.CheckError(OpenCL.clEnqueueNDRangeKernel(commandQueue.Pointer, Pointer, workSizes.Rank, null, workSizeArrayPointer, null, 0, null, null));
            }
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseKernel(Pointer));
        }

    }
}
