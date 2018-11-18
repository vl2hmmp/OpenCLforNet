using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Runtime;
using OpenCLforNet.Function;

namespace OpenCLforNet.Memory
{
    public abstract unsafe class AbstractMemory : AbstractBuffer
    {

        public long Size { get; protected set; }
        public Context Context { get; protected set; }
        public void *Pointer { get; protected set; }

        public Event Write(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, IntPtr data, long offset, long size)
        {
            return Write(commandQueue, blocking, (void*)data, offset, size);
        }

        public Event Write(CommandQueue commandQueue, bool blocking, void* data, long offset, long size)
        {
            void* event_ = null;
            OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), data, 0, null, &event_).CheckError();
            return new Event(event_);
        }

        public Event Read(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, IntPtr data, long offset, long size)
        {
            return Read(commandQueue, blocking, (void*)data, offset, size);
        }

        public Event Read(CommandQueue commandQueue, bool blocking, void *data, long offset, long size)
        {
            void* event_ = null;
            OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), data, 0, null, &event_).CheckError();
            return new Event(event_);
        }

        public void Release()
        {
            OpenCL.clReleaseMemObject(Pointer).CheckError();
        }

    }
}
