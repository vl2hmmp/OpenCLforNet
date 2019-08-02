using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Runtime;
using OpenCLforNet.Function;
using System.Runtime.CompilerServices;
using System.Collections;

namespace OpenCLforNet.Memory
{
    public unsafe class SVMBuffer : AbstractBuffer
    {

        public int Size { get; }
        public Context Context { get; }
        public void* Pointer { get; }

        public SVMBuffer(Context context, int size, uint alignment = 0)
        {
            Size = size;
            Context = context;
            Pointer = OpenCL.clSVMAlloc(context.Pointer, cl_mem_flags.CL_MEM_READ_WRITE, new IntPtr(size), alignment);
        }

        public SVMBuffer(SVMBuffer origin)
        {
            Size = origin.Size;
            Context = origin.Context;
            Pointer = origin.Pointer;
        }

        public static Event Copy(CommandQueue commandQueue, void* src, int srcByteOffset, void* dst, int dstByteOffset, int byteSize, bool blocking)
        {
            void* event_ = null;
            var srcp = Unsafe.Add<byte>(src, srcByteOffset);
            var dstp = Unsafe.Add<byte>(dst, dstByteOffset);
            OpenCL.clEnqueueSVMMemcpy(commandQueue.Pointer, blocking, dstp, srcp, new IntPtr(byteSize), 0, null, &event_);
            return new Event(event_);
        }

        public static Event Copy<T>(CommandQueue commandQueue, void* src, int srcElemOffset, void* dst, int dstElemOffset, int elemSize, bool blocking) where T : struct
        {
            void* event_ = null;
            var srcp = Unsafe.Add<T>(src, srcElemOffset);
            var dstp = Unsafe.Add<T>(dst, dstElemOffset);
            var s = Unsafe.SizeOf<T>() * elemSize;
            OpenCL.clEnqueueSVMMemcpy(commandQueue.Pointer, blocking, dstp, srcp, new IntPtr(s), 0, null, &event_);
            return new Event(event_);
        }

        public static Event Copy(CommandQueue commandQueue, SVMBuffer src, int srcByteOffset, SVMBuffer dst, int dstByteOffset, int byteSize, bool blocking)
            => Copy(
                commandQueue,
                src.Pointer, srcByteOffset,
                dst.Pointer, dstByteOffset,
                byteSize,
                blocking
            );

        public static Event Copy<T>(CommandQueue commandQueue, SVMBuffer<T> src, int srcElemOffset, SVMBuffer<T> dst, int dstElemOffset, int elemSize, bool blocking) where T : struct
            => Copy<T>(
                commandQueue,
                src.Pointer, srcElemOffset,
                dst.Pointer, dstElemOffset,
                elemSize,
                blocking
            );

        public Event CopyTo(CommandQueue commandQueue, int srcByteOffset, void* dst, int dstByteOffset, int byteSize, bool blocking)
        {
            return Copy(commandQueue, Pointer, srcByteOffset, dst, dstByteOffset, byteSize, blocking);
        }

        public Event CopyFrom(CommandQueue commandQueue, void* src, int srcByteOffset, int dstByteOffset, int byteSize, bool blocking)
        {
            return Copy(commandQueue, src, srcByteOffset, Pointer, dstByteOffset, byteSize, blocking);
        }

        public Event Mapping(CommandQueue commandQueue, bool blocking)
        {
            void* event_ = null;
            OpenCL.clEnqueueSVMMap(commandQueue.Pointer, blocking, (cl_map_flags.CL_MAP_READ | cl_map_flags.CL_MAP_WRITE), Pointer, new IntPtr(Size), 0, null, &event_).CheckError();
            return new Event(event_);
        }

        public Event UnMapping(CommandQueue commandQueue)
        {
            void* event_ = null;
            OpenCL.clEnqueueSVMUnmap(commandQueue.Pointer, Pointer, 0, null, &event_).CheckError();
            return new Event(event_);
        }

        public override void Release()
        {
            OpenCL.clSVMFree(Context.Pointer, Pointer);
        }

    }

    public unsafe class SVMBuffer<T> : SVMBuffer, IEnumerable<T> where T : struct
    {

        public int UnitSize { get; }

        public int Length { get => LengthX * LengthY * LengthZ; }

        public int LengthX { get; }

        public int LengthY { get; }

        public int LengthZ { get; }

        public SVMBuffer(Context context, int lengthX, int lengthY = 1, int lengthZ = 1, uint alignment = 0)
            : base(context, lengthX * lengthY * lengthZ * Unsafe.SizeOf<T>(), alignment)
        {
            UnitSize = Unsafe.SizeOf<T>();
            LengthX = lengthX;
            LengthY = lengthY;
            LengthZ = lengthZ;
        }

        public SVMBuffer(SVMBuffer origin, int lengthX, int lengthY = 1, int lengthZ = 1) : base(origin)
        {
            UnitSize = Unsafe.SizeOf<T>();
            LengthX = lengthX;
            LengthY = lengthY;
            LengthZ = lengthZ;
        }

        public T this[int index]
        {
            get => Unsafe.Read<T>(Unsafe.Add<T>(Pointer, index));
            set => Unsafe.Write<T>(Unsafe.Add<T>(Pointer, index), value);
        }

        public T this[int indexX, int indexY]
        {
            get => this[indexX + LengthX * indexY];
            set => this[indexX + LengthX * indexY] = value;
        }

        public T this[int indexX, int indexY, int indexZ]
        {
            get => this[indexX + LengthX * indexY + LengthX * LengthY * indexZ];
            set => this[indexX + LengthX * indexY + LengthX * LengthY * indexZ] = value;
        }

        public new Event CopyTo(CommandQueue commandQueue, int srcElemOffset, void* dst, int dstElemOffset, int elemSize, bool blocking)
        {
            return Copy<T>(commandQueue, Pointer, srcElemOffset, dst, dstElemOffset, elemSize, blocking);
        }

        public new Event CopyFrom(CommandQueue commandQueue, void* src, int srcElemOffset, int dstElemOffset, int elemSize, bool blocking)
        {
            return Copy<T>(commandQueue, src, srcElemOffset, Pointer, dstElemOffset, elemSize, blocking);
        }

        public SVMBuffer<CastT> CastTo<CastT>(int lengthX, int lengthY = 1, int lengthZ = 1) where CastT : struct 
            => new SVMBuffer<CastT>(this, lengthX, lengthY, lengthZ);

        public IEnumerator<T> GetEnumerator() => new SVMEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public class SVMEnumerator<T> : IEnumerator<T> where T : struct
    {

        public int Index { get; private set; }
        public int Length { get; private set; }
        public SVMBuffer<T> Buffer { get; private set; }

        public SVMEnumerator(SVMBuffer<T> buffer)
        {
            Index = -1;
            Buffer = buffer;
            Length = buffer.LengthX * buffer.LengthY * buffer.LengthZ;
        }

        public T Current { get => Buffer[Index]; }

        object IEnumerator.Current => this.Current;

        public bool MoveNext() =>  ++Index < Length;

        public void Reset() => Index = -1;

        public void Dispose() { }
    }
}
