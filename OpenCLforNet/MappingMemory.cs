using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class MappingMemory : Memory
    {

        public MappingMemory(Context context, int size)
        {
            CreateMappingMemory(context, size);
        }

        public MappingMemory(Context context, byte[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, char[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, short[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, int[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, long[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, float[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, double[] data, int size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        private void CreateMappingMemory(Context context, int size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (long)(cl_mem_flags.CL_MEM_ALLOC_HOST_PTR | cl_mem_flags.CL_MEM_READ_WRITE), size, null, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

        private void CreateMappingMemory(Context context, void* dataPointer, int size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (long)(cl_mem_flags.CL_MEM_USE_HOST_PTR | cl_mem_flags.CL_MEM_READ_WRITE), size, dataPointer, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

        public void* Mapping(CommandQueue commandQueue, bool blocking, int offset, int size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            void* pointer = OpenCL.clEnqueueMapBuffer(commandQueue.Pointer, Pointer, blocking, (long)(cl_map_flags.CL_MAP_READ | cl_map_flags.CL_MAP_WRITE), offset, size, 0, null, null, &status);
            OpenCL.CheckError(status);
            return pointer;
        }

        public void UnMapping(CommandQueue commandQueue, void* pointer)
        {
            OpenCL.CheckError(OpenCL.clEnqueueUnmapMemObject(commandQueue.Pointer, Pointer, pointer, 0, null, null));
        }

    }
}
