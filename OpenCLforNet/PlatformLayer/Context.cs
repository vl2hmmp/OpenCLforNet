using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Memory;
using OpenCLforNet.Runtime;
using OpenCLforNet.Function;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class Context
    {

        public Device[] Devices { get; }
        public void *Pointer { get; }

        public Context(params Device[] devices)
        {
            Devices = devices;

            int status = (int)cl_status_code.CL_SUCCESS;
            var devicePointers = (void**)Marshal.AllocCoTaskMem(devices.Length * IntPtr.Size);
            for (var i = 0; i < devices.Length; i++)
                devicePointers[i] = devices[i].Pointer;


            Pointer = OpenCL.clCreateContext(null, (uint)devices.Length, devicePointers, null, null, &status);
            status.CheckError();
        }

        public CommandQueue CreateCommandQueue(Device device)
        {
            return new CommandQueue(this, device);
        }

        public CLProgram CreateProgram(string source)
        {
            return new CLProgram(source, this);
        }

        public Kernel CreateKernel(string source, string kernelName)
        {
            var program = new CLProgram(source, this);
            return program.CreateKernel(kernelName);
        }

        public SimpleMemory CreateSimpleMemory(long size)
        {
            return new SimpleMemory(this, size);
        }

        public SimpleMemory CreateSimpleMemory(byte[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(char[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(short[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(int[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(long[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(float[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(double[] data, long size)
        {
            return new SimpleMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(long size)
        {
            return new MappingMemory(this, size);
        }

        public MappingMemory CreateMappingMemory(byte[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(char[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(short[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(int[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(long[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(float[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(double[] data, long size)
        {
            return new MappingMemory(this, data, size);
        }

        public SVMBuffer<Type> CreateSVMBuffer<Type>(int lengthX, int lengthY = 1, int lengthZ = 1, uint alignment = 0) where Type : struct
        {
            return new SVMBuffer<Type>(this, lengthX, lengthY, lengthZ, alignment);
        }

        public void Release()
        {
            OpenCL.clReleaseContext(Pointer).CheckError();
        }

    }
}
