using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class Device
    {

        public static DeviceInfo[] GetDeviceInfos(Platform platform)
        {
            int deviceCount = 0;
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &deviceCount));
            var deviceInfos = new DeviceInfo[deviceCount];

            byte[] value = new byte[256];
            int size = 0;
            
            for (int i = 0; i < deviceCount; i++)
            {
                var device = new Device(platform, i);
                fixed (byte* valuePointer = value)
                    OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_NAME, 256, valuePointer, &size));
                var name = Encoding.UTF8.GetString(value, 0, size);
                var maxComputeUnits = 0;
                OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_MAX_COMPUTE_UNITS, 4, &maxComputeUnits, &size));
                var maxWorkItemSizes = new long[3];
                fixed (long* maxWorkItemSizesPointer = maxWorkItemSizes)
                    OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_MAX_WORK_ITEM_SIZES, 24, maxWorkItemSizesPointer, &size));
                var maxWorkGroupSize = 0L;
                OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_MAX_WORK_GROUP_SIZE, 8, &maxWorkGroupSize, &size));
                var maxMemAllocSize = 0L;
                OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_MAX_MEM_ALLOC_SIZE, 8, &maxMemAllocSize, &size));
                var maxConstantBufferSize = 0L;
                OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE, 8, &maxConstantBufferSize, &size));
                var svmCapabilities = 0L;
                OpenCL.CheckError(OpenCL.clGetDeviceInfo(device.Pointer, (int)cl_device_info.CL_DEVICE_SVM_CAPABILITIES, 8, &svmCapabilities, &size));
                var svmCapabilitiesString = "None";
                if ((svmCapabilities & (long)cl_device_svm_capabilities.CL_DEVICE_SVM_COARSE_GRAIN_BUFFER) != 0) svmCapabilitiesString = "Coarse Grain Buffer";
                if ((svmCapabilities & (long)cl_device_svm_capabilities.CL_DEVICE_SVM_FINE_GRAIN_BUFFER) != 0) svmCapabilitiesString = "Fine Grain Buffer";
                if ((svmCapabilities & (long)cl_device_svm_capabilities.CL_DEVICE_SVM_FINE_GRAIN_SYSTEM) != 0) svmCapabilitiesString = "Fine Grain System";
                if ((svmCapabilities & (long)cl_device_svm_capabilities.CL_DEVICE_SVM_ATOMICS) != 0) svmCapabilitiesString = "Atomics";

                deviceInfos[i] = new DeviceInfo
                {
                    Index = i,
                    Name = name,
                    MaxComputeUnits = maxComputeUnits,
                    MaxWorkItemSizes = maxWorkItemSizes,
                    MaxWorkGroupSize = maxWorkGroupSize,
                    MaxMemAllocSize = maxMemAllocSize,
                    MaxConstantBufferSize = maxConstantBufferSize,
                    SvmCapabilities = svmCapabilitiesString
                };
            }
            return deviceInfos;
        }

        public readonly int Index;
        public readonly long Pointer;

        public Device(Platform platform, int index)
        {
            int deviceCount = 0;
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &deviceCount));

            var devices = new long[deviceCount];
            fixed (long* devicesPointer = devices)
            {
                OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, deviceCount, devicesPointer, &deviceCount));
            }
            Index = index;
            Pointer = devices[index];
        }

        public Context CreateContext()
        {
            return new Context(new Device[] { this });
        }

    }
}
