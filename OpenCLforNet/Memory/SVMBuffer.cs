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

        public long Size { get; }
        public Context Context { get; }
        public void* Pointer { get; }

        public SVMBuffer(Context context, long size, uint alignment = 0)
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

        public long UnitSize { get; }
        public long Length { get; }

        public SVMBuffer(Context context, long length, uint alignment = 0) : base(context, length * Unsafe.SizeOf<T>(), alignment)
        {
            UnitSize = Unsafe.SizeOf<T>();
            Length = length;
        }

        public SVMBuffer(SVMBuffer origin) : base(origin)
        {
            UnitSize = Unsafe.SizeOf<T>();
            Length = origin.Size / UnitSize;
        }

        public T this[int index]
        {
            get => Unsafe.Read<T>(Unsafe.Add<T>(Pointer, index));
            set => Unsafe.Write<T>(Unsafe.Add<T>(Pointer, index), value);
        }

        public SVMBuffer<CastT> CastTo<CastT>() where CastT : struct => new SVMBuffer<CastT>(this);

        public IEnumerator<T> GetEnumerator() => new SVMEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public class SVMEnumerator<T> : IEnumerator<T> where T : struct
    {

        public int Index { get; private set; }
        public SVMBuffer<T> Buffer { get; private set; }

        public SVMEnumerator(SVMBuffer<T> buffer)
        {
            Index = 0;
            Buffer = buffer;
        }

        public T Current { get => Buffer[Index]; }

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            Index++;

            return Index < Buffer.Length;
        }

        public void Reset() => Index = 0;

        public void Dispose() { }
    }
}
