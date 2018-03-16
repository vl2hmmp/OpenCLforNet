using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Runtime;
using OpenCLforNet.RuntimeFunction;

namespace OpenCLforNet.Memory
{
    public unsafe class MappingMemory : AbstractMemory
    {

        public MappingMemory(Context context, long size)
        {
            CreateMappingMemory(context, size);
        }

        public MappingMemory(Context context, byte[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, char[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, short[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, int[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, long[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, float[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        public MappingMemory(Context context, double[] data, long size)
        {
            fixed (void* dataPointer = data)
                CreateMappingMemory(context, dataPointer, size);
        }

        private void CreateMappingMemory(Context context, long size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (cl_mem_flags.CL_MEM_ALLOC_HOST_PTR | cl_mem_flags.CL_MEM_READ_WRITE), new IntPtr(size), null, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

        private void CreateMappingMemory(Context context, void* dataPointer, long size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            Pointer = OpenCL.clCreateBuffer(context.Pointer, (cl_mem_flags.CL_MEM_USE_HOST_PTR | cl_mem_flags.CL_MEM_READ_WRITE), new IntPtr(size), dataPointer, &status);
            Size = size;
            Context = context;
            OpenCL.CheckError(status);
        }

        public void* Mapping(CommandQueue commandQueue, bool blocking, long offset, long size)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            void* pointer = OpenCL.clEnqueueMapBuffer(commandQueue.Pointer, Pointer, blocking, (cl_map_flags.CL_MAP_READ | cl_map_flags.CL_MAP_WRITE), new IntPtr(offset), new IntPtr(size), 0, null, null, &status);
            OpenCL.CheckError(status);
            return pointer;
        }

        public void UnMapping(CommandQueue commandQueue, void* pointer)
        {
            OpenCL.CheckError(OpenCL.clEnqueueUnmapMemObject(commandQueue.Pointer, Pointer, pointer, 0, null, null));
        }

    }
}
