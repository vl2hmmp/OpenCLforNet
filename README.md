# OpenCLforNet

This is a OpenCL wrapper library for .NET Standard.

# Usage

## Dependecies

- OpenCL.dll/OpenCL.so

## Coding

Please see OpenCLforNetSample for details.

### Initialize

``` csharp

var platformIndex = 0;
var platform = new Platform(platformIndex);

var deviceIndices = new int[] { 0 };
var device = platform.CreateDevices(deviceIndices).First();

var context = device.CreateContext();
var commandQueue = context.CreateCommandQueue(device);

```

### Build kernel

``` csharp

var source = "kernel void ...";
var program = context.CreateProgram(source);

var kernelName = "...";
var kernel = program.CreateKernel(kernelName);

```

### Allocate memory object or SVM pointer

``` csharp

var size = 100;
var initialData = new byte[] { 0, 1, 2, ... };

var memObject = context.CreateSimpleMemory(size);
var initializedMemObject = context.CreateSimpleMemory(initialData, size);

var mappableMemObject = context.CreateMappingMemory(size);
var initializedMappableMemObject = context.CreateMappingMemory(initialData, size);

var alignment = 0;
var svmPointer = context.CreateSVMBuffer(size, alignment);

```

### Executing and Profiling

``` csharp

var workSizes = new long[] { 4 };
var arg1 = memObject;
var arg2 = svmPointer;
var arg3 = 3F;

var event_ = kernel.NDRange(commandQueue, workSizes, arg1, arg2);
event_.Wait();

Console.WriteLine($"exec time : {event_.ExecutionTime} ns");

```

