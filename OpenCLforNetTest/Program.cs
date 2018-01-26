using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet;

namespace OpenCLforNetTest
{
    unsafe class Program
    {
        static void Main(string[] args)
        {
            foreach (var platformInfo in Platform.GetPlatformInfos())
            {
                Console.WriteLine($"platform index   : {platformInfo.Index}");
                Console.WriteLine($"platform vendor  : {platformInfo.Vendor}");
                Console.WriteLine($"platform name    : {platformInfo.Name}");
                Console.WriteLine($"platform verison : {platformInfo.Version}");
                foreach (var deviceInfo in platformInfo.DeviceInfos)
                {
                    Console.WriteLine($"  device index                    : {deviceInfo.Index}");
                    Console.WriteLine($"  device name                     : {deviceInfo.Name}");
                    Console.WriteLine($"  device max conpute units        : {deviceInfo.MaxComputeUnits}");
                    Console.WriteLine($"  device max work item sizes      : {deviceInfo.MaxWorkItemSizes[0]}, {deviceInfo.MaxWorkItemSizes[1]}, {deviceInfo.MaxWorkItemSizes[2]}");
                    Console.WriteLine($"  device max work group size      : {deviceInfo.MaxWorkGroupSize}");
                    Console.WriteLine($"  device mem alloc size           : {deviceInfo.MaxMemAllocSize}");
                    Console.WriteLine($"  device max constant buffer size : {deviceInfo.MaxConstantBufferSize}");
                    Console.WriteLine($"  device svm capabilities         : {deviceInfo.SvmCapabilities}");
                }
            }


            var platform = new Platform(0);
            var device = platform.CreateDevice(0);
            var context = device.CreateContext();
            var commandQueue = context.CreateCommandQueue(device);


            var program = context.CreateProgram(source);
            var kernel = program.CreateKernel("testKernel");


            // CL_MEM_READ_WRITE
            var data = new float[] { 3F, 4.5F, 0F, -4.4F };
            var dataSize = sizeof(float) * 4;
            var simpleMemory = context.CreateSimpleMemory(dataSize);
            simpleMemory.Write(commandQueue, true, data, 0, dataSize);
            kernel.NDRange(commandQueue, new long[] { 4 }, simpleMemory, 3F);
            commandQueue.WaitFinish();
            simpleMemory.Read(commandQueue, true, data, 0, dataSize);
            ShowArray(data);
            simpleMemory.Release();


            // CL_MEM_COPY_HOST_PTR
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            simpleMemory = context.CreateSimpleMemory(data, dataSize);
            kernel.NDRange(commandQueue, new long[] { 4 }, simpleMemory, 3F);
            commandQueue.WaitFinish();
            simpleMemory.Read(commandQueue, true, data, 0, dataSize);
            ShowArray(data);
            simpleMemory.Release();


            // CL_MEM_ALLOC_HOST_PTR
            dataSize = sizeof(float) * 4;
            var mappingMemory = context.CreateMappingMemory(dataSize);
            var pointer = (float *)mappingMemory.Mapping(commandQueue, true, 0, dataSize);
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            mappingMemory.UnMapping(commandQueue, pointer);
            kernel.NDRange(commandQueue, new long[] { 4 }, mappingMemory, 3F);
            commandQueue.WaitFinish();
            mappingMemory.Read(commandQueue, true, data, 0, dataSize);
            ShowArray(data);
            mappingMemory.Release();


            // CL_MEM_USE_HOST_PTR
            data = new float[] { 3F, 4.5F, 0F, -4.4F };
            dataSize = sizeof(float) * 4;
            mappingMemory = context.CreateMappingMemory(data, dataSize);
            kernel.NDRange(commandQueue, new long[] { 4 }, mappingMemory, 3F);
            commandQueue.WaitFinish();
            mappingMemory.Read(commandQueue, true, data, 0, dataSize);
            ShowArray(data);
            mappingMemory.Release();


            // SVM
            dataSize = sizeof(float) * 4;
            var svmBuffer = context.CreateSVMBuffer(dataSize, 0);
            pointer = (float *)svmBuffer.GetSVMPointer();
            pointer[0] = 3F;
            pointer[1] = 4.5F;
            pointer[2] = 0F;
            pointer[3] = -4.4F;
            kernel.NDRange(commandQueue, new long[] { 4 }, svmBuffer, 3F);
            commandQueue.WaitFinish();
            Console.Write("{  ");
            for (var i = 0; i < 4; i++)
                Console.Write($"{pointer[i]}  ");
            Console.WriteLine("}");
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
            Console.Write("{  ");
            foreach (var value in array)
                Console.Write($"{value}  ");
            Console.WriteLine("}");
        }

        private static string source = @"
__kernel void testKernel(__global float* array, float rate){
    int idx = get_global_id(0);
    array[idx] *= rate;
}
";
    }
}
