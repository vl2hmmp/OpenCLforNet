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

        public Event Write(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, bool blocking, IntPtr data, long offset, long size, params Event[] eventWaitList)
        {
            return Write(commandQueue, blocking, (void*)data, offset, size, eventWaitList);
        }

        public Event Write(CommandQueue commandQueue, bool blocking, void* data, long offset, long size, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), data, num, listPointer, &event_).CheckError();
            }
            
            return new Event(event_);
        }

        public Event Read(CommandQueue commandQueue, bool blocking, byte[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, char[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, short[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, int[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, long[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, float[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, double[] data, long offset, long size, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, blocking, dataPointer, offset, size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, bool blocking, IntPtr data, long offset, long size, params Event[] eventWaitList)
        {
            return Read(commandQueue, blocking, (void*)data, offset, size, eventWaitList);
        }

        public Event Read(CommandQueue commandQueue, bool blocking, void *data, long offset, long size, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size), data, num, listPointer, &event_).CheckError();
            }
            
            return new Event(event_);
        }

        public void Release()
        {
            OpenCL.clReleaseMemObject(Pointer).CheckError();
        }

    }
}
