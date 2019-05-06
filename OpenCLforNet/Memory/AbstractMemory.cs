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

        public Event Write(CommandQueue commandQueue, byte[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, char[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, short[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, int[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, long[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, float[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, double[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Write(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Write(CommandQueue commandQueue, IntPtr data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            return Write(commandQueue, (void*)data, blocking, offset, size ?? Size, eventWaitList);
        }

        public Event Write(CommandQueue commandQueue, void* data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueWriteBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size ?? Size), data, num, listPointer, &event_).CheckError();
            }
            
            return new Event(event_);
        }

        public Event Read(CommandQueue commandQueue, byte[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, char[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, short[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, int[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, long[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, float[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, double[] data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            fixed (void* dataPointer = data)
            {
                return Read(commandQueue, dataPointer, blocking, offset, size ?? Size, eventWaitList);
            }
        }

        public Event Read(CommandQueue commandQueue, IntPtr data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            return Read(commandQueue, (void*)data, blocking, offset, size ?? Size, eventWaitList);
        }

        public Event Read(CommandQueue commandQueue, void *data, bool blocking = true, long offset = 0, long? size = null, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueReadBuffer(commandQueue.Pointer, Pointer, blocking, new IntPtr(offset), new IntPtr(size ?? Size), data, num, listPointer, &event_).CheckError();
            }
            
            return new Event(event_);
        }

        public Event Copy(CommandQueue commandQueue, long srcOffset = 0, long dstOffset = 0, long? size = null, params Event[] eventWaitList)
        {
            return CopyFrom(commandQueue, this, srcOffset, dstOffset, size, eventWaitList);
        }

        public Event CopyFrom(CommandQueue commandQueue, AbstractMemory memory, long srcOffset = 0, long dstOffset = 0, long? size = null, params Event[] eventWaitList)
        {
            void* event_ = null;

            var num = (uint)eventWaitList.Length;
            var list = eventWaitList.Select(e => new IntPtr(e.Pointer)).ToArray();
            fixed (void* listPointer = list)
            {
                OpenCL.clEnqueueCopyBuffer(commandQueue.Pointer, Pointer, memory.Pointer, new IntPtr(srcOffset), new IntPtr(dstOffset), new IntPtr(size ?? Size), num, listPointer, &event_).CheckError();
            }

            return new Event(event_);
        }

        public override void Release()
        {
            OpenCL.clReleaseMemObject(Pointer).CheckError();
        }

    }
}
