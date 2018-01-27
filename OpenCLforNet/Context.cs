using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class Context
    {

        public readonly Device[] Devices;
        public readonly long Pointer;

        public Context(params Device[] devices)
        {
            int status = (int)cl_status_code.CL_SUCCESS;
            var deviceArray = devices.Select(device => device.Pointer).ToList();
            fixed (long* deviceArrayPointer = deviceArray.ToArray())
            {
                Devices = devices;
                Pointer = OpenCL.clCreateContext(null, devices.Length, deviceArrayPointer, null, null, &status);
            }
            OpenCL.CheckError(status);
        }

        public CommandQueue CreateCommandQueue(Device device)
        {
            return new CommandQueue(this, device);
        }

        public CommandQueue[] CreateCommandQueues()
        {
            var commandQueues = new List<CommandQueue>();
            foreach (var device in Devices)
                commandQueues.Add(CreateCommandQueue(device));
            return commandQueues.ToArray();
        }

        public CLProgram CreateProgram(string source)
        {
            return new CLProgram(source, this);
        }

        public SimpleMemory CreateSimpleMemory(int size)
        {
            return new SimpleMemory(this, size);
        }

        public SimpleMemory CreateSimpleMemory(byte[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(char[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(short[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(int[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(long[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(float[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public SimpleMemory CreateSimpleMemory(double[] data, int size)
        {
            return new SimpleMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(int size)
        {
            return new MappingMemory(this, size);
        }

        public MappingMemory CreateMappingMemory(byte[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(char[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(short[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(int[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(long[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(float[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public MappingMemory CreateMappingMemory(double[] data, int size)
        {
            return new MappingMemory(this, data, size);
        }

        public SVMBuffer CreateSVMBuffer(int size, int alignment)
        {
            return new SVMBuffer(this, size, alignment);
        }

        public void Release()
        {
            OpenCL.CheckError(OpenCL.clReleaseContext(Pointer));
        }

    }
}
