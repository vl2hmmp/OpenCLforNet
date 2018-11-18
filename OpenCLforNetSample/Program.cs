using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.PlatformLayer;
using OpenCLforNet.Runtime;

namespace OpenCLforNetTest
{
    unsafe class Program
    {
        static void Main(string[] args)
        {

            Event event_;

            foreach (var platformInfo in Platform.PlatformInfos)
            {
                foreach (var key in platformInfo.Keys)
                {
                    Console.WriteLine($"platform {key} : {platformInfo.GetValueAsString(key)}");
                }
                foreach (var deviceInfo in platformInfo.DeviceInfos)
                {
                    foreach (var key in deviceInfo.Keys)
                    {
                        Console.WriteLine($"  device {key} : {deviceInfo.GetValueAsString(key)}");
                    }
                }
            }


            var platform = new Platform(0);
            var device = platform.CreateDevices(0).First();
            var context = device.CreateContext();
            var commandQueue = context.CreateCommandQueue(device);


            var program = context.CreateProgram(source);
            var kernel = program.CreateKernel("testKernel");

            
            Console.WriteLine("\nCL_MEM_READ_WRITE");
            var data = new float[] { 3F, 4.5F, 0F, -4.4F };
            var dataSize = (long)(sizeof(float) * 4);
            var simpleMemory = context.CreateSimpleMemory(dataSize);
            event_ = simpleMemory.Write(commandQueue, true, data, 0, dataSize);
            event_.Wait();
            Console.WriteLine($"write time : {event_.ExecutionTime} ns");
            event_ = kernel.NDRange(commandQueue, new long[] { 4 }, simpleMemory, 3F);
            event_.Wait();
            Console.WriteLine($"exec time : {event_.ExecutionTime} ns");
            var result = new float[4];
            event_ = simpleMemory.Read(commandQueue, true, result, 0, dataSize);
            event_.Wait();
            Console.WriteLine($"read time : {event_.ExecutionTime} ns");
            ShowArray(result);
            simpleMemory.Release();

            
            Console.WriteLine("\nCL_MEM_COPY_HOST_PTR");
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            simpleMemory = context.CreateSimpleMemory(data, dataSize);
            event_ = kernel.NDRange(commandQueue, new long[] { 4 }, simpleMemory, 3F);
            event_.Wait();
            Console.WriteLine($"exec time : {event_.ExecutionTime} ns");
            event_ = simpleMemory.Read(commandQueue, true, data, 0, dataSize);
            event_.Wait();
            Console.WriteLine($"read time : {event_.ExecutionTime} ns");
            ShowArray(data);
            simpleMemory.Release();

            
            Console.WriteLine("\nCL_MEM_ALLOC_HOST_PTR");
            dataSize = sizeof(float) * 4;
            var mappingMemory = context.CreateMappingMemory(dataSize);
            event_ = mappingMemory.Mapping(commandQueue, true, 0, dataSize, out var p);
            event_.Wait();
            Console.WriteLine($"mapping time : {event_.ExecutionTime} ns");
            var pointer = (float*) p;
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            event_ = mappingMemory.UnMapping(commandQueue, pointer);
            event_.Wait();
            Console.WriteLine($"unmapping time : {event_.ExecutionTime} ns");
            event_ = kernel.NDRange(commandQueue, new long[] { 4 }, mappingMemory, 3F);
            event_.Wait();
            Console.WriteLine($"exec time : {event_.ExecutionTime} ns");
            event_ = mappingMemory.Read(commandQueue, true, data, 0, dataSize);
            event_.Wait();
            Console.WriteLine($"read time : {event_.ExecutionTime} ns");
            ShowArray(data);
            mappingMemory.Release();

            
            Console.WriteLine("\nCL_MEM_USE_HOST_PTR");
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            mappingMemory = context.CreateMappingMemory(data, dataSize);
            event_ = kernel.NDRange(commandQueue, new long[] { 4 }, mappingMemory, 3F);
            event_.Wait();
            Console.WriteLine($"exec time : {event_.ExecutionTime} ns");
            event_ = mappingMemory.Read(commandQueue, true, data, 0, dataSize);
            event_.Wait();
            Console.WriteLine($"read time : {event_.ExecutionTime} ns");
            ShowArray(data);
            mappingMemory.Release();

            
            Console.WriteLine("\nSVM");
            dataSize = sizeof(float) * 4;
            var svmBuffer = context.CreateSVMBuffer(dataSize, 0);
            pointer = (float*)svmBuffer.GetSVMPointer();
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            event_ = kernel.NDRange(commandQueue, new long[] { 4, 0, 0 }, svmBuffer, 3F);
            event_.Wait();
            Console.WriteLine($"exec time : {event_.ExecutionTime} ns");
            Console.Write("result : [  ");
            for (var i = 0; i < 4; i++)
                Console.Write($"{pointer[i]}  ");
            Console.WriteLine("]");
            svmBuffer.Release();


            kernel.Release();
            program.Release();


            commandQueue.Release();
            context.Release();
            Console.WriteLine("program end ...");
            Console.Read();
            
        }

        private static void ShowArray(float[] array)
        {
            Console.Write("result : [  ");
            foreach (var value in array)
                Console.Write($"{value}  ");
            Console.WriteLine("]");
        }

        private static string source = @"
kernel void testKernel(global float* array, float rate){
    int idx = get_global_id(0);
    array[idx] *= rate;
}
";
    }
}
