using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class SVMBuffer : Buffer
    {

        public readonly int Size;
        public readonly Context Context;
        public readonly void* Pointer;

        public SVMBuffer(Context context, int size, int alignment)
        {
            Pointer = OpenCL.clSVMAlloc(context.Pointer, (long)cl_mem_flags.CL_MEM_READ_WRITE, size, alignment);
            Size = size;
            Context = context;
        }

        public void* GetSVMPointer()
        {
            return Pointer;
        }

        public void Mapping(CommandQueue commandQueue, bool blocking)
        {
            OpenCL.CheckError(OpenCL.clEnqueueSVMMap(commandQueue.Pointer, blocking, (long)(cl_map_flags.CL_MAP_READ | cl_map_flags.CL_MAP_WRITE), Pointer, Size, 0, null, null));
        }

        public void UnMapping(CommandQueue commandQueue)
        {
            OpenCL.CheckError(OpenCL.clEnqueueSVMUnmap(commandQueue.Pointer, Pointer, 0, null, null));
        }

        public void Release()
        {
            OpenCL.clSVMFree(Context.Pointer, Pointer);
        }

    }
}
