using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public abstract unsafe class Memory : Buffer
    {

        public int Size;
        public Context Context;
        public long Pointer;

        public void Write(CommandQueue commandQueue, bool blocking, byte[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, char[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, short[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, int[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, long[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, float[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Write(CommandQueue commandQueue, bool blocking, double[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, byte[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, char[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, short[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, int[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, long[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, float[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Read(CommandQueue commandQueue, bool blocking, double[] data, int offset, int size)
        {
            fixed (void* dataPointer = data)
            {
                OpenCL.CheckError(OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, offset, size, dataPointer, 0, null, null));
            }
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseMemObject(Pointer));
        }

    }
}
