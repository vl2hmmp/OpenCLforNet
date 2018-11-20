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
            kernel.SetWorkSize(4);

            
            Console.WriteLine("\nCL_MEM_READ_WRITE");
            var data = new float[] { 3F, 4.5F, 0F, -4.4F };
            var dataSize = (long)(sizeof(float) * 4);
            var simpleMemory = context.CreateSimpleMemory(dataSize);
            kernel.SetArgs(simpleMemory, 1F);

            var event11 = simpleMemory.Write(commandQueue, true, data, 0, dataSize);
            var event12 = kernel.NDRange(commandQueue, event11);
            var event13 = simpleMemory.Read(commandQueue, true, data, 0, dataSize, event12);

            Console.WriteLine($"write time : {event11.ExecutionTime} ns");
            Console.WriteLine($"exec time : {event12.ExecutionTime} ns");
            Console.WriteLine($"read time : {event13.ExecutionTime} ns");
            ShowArray(data);
            simpleMemory.Release();

            
            Console.WriteLine("\nCL_MEM_COPY_HOST_PTR");
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            simpleMemory = context.CreateSimpleMemory(data, dataSize);
            kernel.SetArgs(simpleMemory, 2F);

            var event21 = kernel.NDRange(commandQueue);
            var event22 = simpleMemory.Read(commandQueue, true, data, 0, dataSize, event21);

            Console.WriteLine($"exec time : {event21.ExecutionTime} ns");
            Console.WriteLine($"read time : {event22.ExecutionTime} ns");
            ShowArray(data);
            simpleMemory.Release();

            
            Console.WriteLine("\nCL_MEM_ALLOC_HOST_PTR");
            dataSize = sizeof(float) * 4;
            var mappingMemory = context.CreateMappingMemory(dataSize);
            kernel.SetArgs(mappingMemory, 3F);

            var event31 = mappingMemory.Mapping(commandQueue, true, 0, dataSize, out var p);
            event31.Wait();
            var pointer = (float*) p;
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            var event32 = mappingMemory.UnMapping(commandQueue, pointer);
            var event33 = kernel.NDRange(commandQueue, event32);
            var event34 = mappingMemory.Read(commandQueue, true, data, 0, dataSize, event33);

            Console.WriteLine($"mapping time : {event31.ExecutionTime} ns");
            Console.WriteLine($"unmapping time : {event32.ExecutionTime} ns");
            Console.WriteLine($"exec time : {event33.ExecutionTime} ns");
            Console.WriteLine($"read time : {event34.ExecutionTime} ns");
            ShowArray(data);
            mappingMemory.Release();

            
            Console.WriteLine("\nCL_MEM_USE_HOST_PTR");
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            mappingMemory = context.CreateMappingMemory(data, dataSize);
            kernel.SetArgs(mappingMemory, 4F);

            var event41 = kernel.NDRange(commandQueue);
            var event42 = mappingMemory.Read(commandQueue, true, data, 0, dataSize, event41);

            Console.WriteLine($"exec time : {event41.ExecutionTime} ns");
            Console.WriteLine($"read time : {event42.ExecutionTime} ns");
            ShowArray(data);
            mappingMemory.Release();

            
            Console.WriteLine("\nSVM");
            dataSize = sizeof(float) * 4;
            var svmBuffer = context.CreateSVMBuffer(dataSize, 0);
            kernel.SetArgs(svmBuffer, 5F);

            pointer = (float*)svmBuffer.GetSVMPointer();
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            var event51 = kernel.NDRange(commandQueue);

            Console.WriteLine($"exec time : {event51.ExecutionTime} ns");
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
