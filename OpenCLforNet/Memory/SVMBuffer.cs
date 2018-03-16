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
    public unsafe class SVMBuffer : Buffer
    {

        public long Size { get; }
        public Context Context { get; }
        public void *Pointer { get; }

        public SVMBuffer(Context context, long size, uint alignment)
        {
            Size = size;
            Context = context;
            Pointer = OpenCL.clSVMAlloc(context.Pointer, cl_mem_flags.CL_MEM_READ_WRITE, new IntPtr(size), alignment);
        }

        public void* GetSVMPointer()
        {
            return Pointer;
        }

        public void Mapping(CommandQueue commandQueue, bool blocking)
        {
            OpenCL.CheckError(OpenCL.clEnqueueSVMMap(commandQueue.Pointer, blocking, (cl_map_flags.CL_MAP_READ | cl_map_flags.CL_MAP_WRITE), Pointer, new IntPtr(Size), 0, null, null));
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
