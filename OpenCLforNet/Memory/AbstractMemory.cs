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
    public abstract unsafe class AbstractMemory : Buffer
    {

        public long Size { get; protected set; }
        public Context Context { get; protected set; }
        public void *Pointer { get; protected set; }

        public void Write(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), dataPointer, 0, null, null));
            }
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseMemObject(Pointer));
        }

    }
}
