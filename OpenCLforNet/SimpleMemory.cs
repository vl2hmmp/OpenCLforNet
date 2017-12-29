using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class SimpleMemory : Memory
    {

        public SimpleMemory(Context context, int size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (long)cl_mem_flags.CL_MEM_READ_WRITE, size, null, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

        public SimpleMemory(Context context, byte[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, char[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, short[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, int[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, long[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, float[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        public SimpleMemory(Context context, double[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateSimpleMemory(context, dataPointer, size);
        }

        private void CreateSimpleMemory(Context context, void* dataPointer, int size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (long)(cl_mem_flags.CL_MEM_COPY_HOST_PTR | cl_mem_flags.CL_MEM_READ_WRITE), size, dataPointer, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

    }
}
