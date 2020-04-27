using OpenCLforNet.PlatformLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCLforNet.Function
{
    public static class ClDeviceInfo
    {
        public static readonly ReadOnlyCollection<IClDeviceInfoFor> All;

        public static readonly ClDeviceInfoFor<uint> DeviceAddressBits;
        public static readonly ClDeviceInfoFor<bool> DeviceAvailable;
        public static readonly ClDeviceInfoFor<bool> DeviceCompilerAvailable;
        public static readonly ClDeviceInfoFor<cl_device_fp_config> DeviceDoubleFpConfig;
        public static readonly ClDeviceInfoFor<bool> DeviceEndianLittle;
        public static readonly ClDeviceInfoFor<bool> DeviceErrorCorrectionSupport;
        public static readonly ClDeviceInfoFor<cl_device_exec_capabilities> DeviceExecutionCapabilities;
        public static readonly ClDeviceInfoFor<string> DeviceExtensions;
        public static readonly ClDeviceInfoFor<ulong> DeviceGlobalMemCacheSize;
        public static readonly ClDeviceInfoFor<cl_device_mem_cache_type> DeviceGlobalMemCacheType;
        public static readonly ClDeviceInfoFor<uint> DeviceGlobalMemCachelineSize;
        public static readonly ClDeviceInfoFor<ulong> DeviceGlobalMemSize;
        public static readonly ClDeviceInfoFor<cl_device_fp_config> DeviceHalfFpConfig;
        public static readonly ClDeviceInfoFor<bool> DeviceImageSupport;
        public static readonly ClDeviceInfoFor<ulong> DeviceImage2dMaxHeight;
        public static readonly ClDeviceInfoFor<ulong> DeviceImage2dMaxWidth;
        public static readonly ClDeviceInfoFor<ulong> DeviceImage3dMaxDepth;
        public static readonly ClDeviceInfoFor<ulong> DeviceImage3dMaxHeight;
        public static readonly ClDeviceInfoFor<ulong> DeviceImage3dMaxWidth;
        public static readonly ClDeviceInfoFor<ulong> DeviceLocalMemSize;
        public static readonly ClDeviceInfoFor<cl_device_local_mem_type> DeviceLocalMemType;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxClockFrequency;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxComputeUnits;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxConstantArgs;
        public static readonly ClDeviceInfoFor<ulong> DeviceMaxConstantBufferSize;
        public static readonly ClDeviceInfoFor<ulong> DeviceMaxMemAllocSize;
        public static readonly ClDeviceInfoFor<ulong> DeviceMaxParameterSize;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxReadImageArgs;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxSamplers;
        public static readonly ClDeviceInfoFor<ulong> DeviceMaxWorkGroupSize;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxWorkItemDimensions;
        public static readonly ClDeviceInfoFor<ulong[]> DeviceMaxWorkItemSizes;
        public static readonly ClDeviceInfoFor<uint> DeviceMaxWriteImageArgs;
        public static readonly ClDeviceInfoFor<uint> DeviceMemBaseAddrAlign;
        public static readonly ClDeviceInfoFor<uint> DeviceMinDataTypeAlignSize;
        public static readonly ClDeviceInfoFor<string> DeviceName;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthChar;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthShort;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthInt;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthLong;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthFloat;
        public static readonly ClDeviceInfoFor<uint> DevicePreferredVectorWidthDouble;
        public static readonly ClDeviceInfoFor<string> DeviceProfile;
        public static readonly ClDeviceInfoFor<ulong> DeviceProfilingTimerResolution;
        public static readonly ClDeviceInfoFor<cl_command_queue_properties> DeviceQueueProperties;
        public static readonly ClDeviceInfoFor<cl_device_fp_config> DeviceSingleFpConfig;
        public static readonly ClDeviceInfoFor<cl_device_type> DeviceType;
        public static readonly ClDeviceInfoFor<string> DeviceVendor;
        public static readonly ClDeviceInfoFor<uint> DeviceVendorId;
        public static readonly ClDeviceInfoFor<string> DeviceVersion;
        public static readonly ClDeviceInfoFor<string> DriverVersion;

        static ClDeviceInfo()
        {
            DeviceAddressBits = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_ADDRESS_BITS);
            DeviceAvailable = new CLDeviceInfoBool(cl_device_info.CL_DEVICE_AVAILABLE);
            DeviceCompilerAvailable = new CLDeviceInfoBool(cl_device_info.CL_DEVICE_COMPILER_AVAILABLE);
            DeviceDoubleFpConfig = new CLDeviceInfoClDeviceFpConfig(cl_device_info.CL_DEVICE_DOUBLE_FP_CONFIG);
            DeviceEndianLittle = new CLDeviceInfoBool(cl_device_info.CL_DEVICE_ENDIAN_LITTLE);
            DeviceErrorCorrectionSupport = new CLDeviceInfoBool(cl_device_info.CL_DEVICE_ERROR_CORRECTION_SUPPORT);
            DeviceExecutionCapabilities = new CLDeviceInfoClDeviceExecCapabilities(cl_device_info.CL_DEVICE_EXECUTION_CAPABILITIES);
            DeviceExtensions = new CLDeviceInfoString(cl_device_info.CL_DEVICE_EXTENSIONS);
            DeviceGlobalMemCacheSize = new CLDeviceInfoULong(cl_device_info.CL_DEVICE_GLOBAL_MEM_CACHE_SIZE);
            DeviceGlobalMemCacheType = new CLDeviceInfoClDeviceMemCacheType(cl_device_info.CL_DEVICE_GLOBAL_MEM_CACHE_TYPE);
            DeviceGlobalMemCachelineSize = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_GLOBAL_MEM_CACHELINE_SIZE);
            DeviceGlobalMemSize = new CLDeviceInfoULong(cl_device_info.CL_DEVICE_GLOBAL_MEM_SIZE);
            DeviceHalfFpConfig = new CLDeviceInfoClDeviceFpConfig(cl_device_info.CL_DEVICE_HALF_FP_CONFIG);
            DeviceImageSupport = new CLDeviceInfoBool(cl_device_info.CL_DEVICE_IMAGE_SUPPORT);
            DeviceImage2dMaxHeight = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_IMAGE2D_MAX_HEIGHT);
            DeviceImage2dMaxWidth = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_IMAGE2D_MAX_WIDTH);
            DeviceImage3dMaxDepth = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_IMAGE3D_MAX_DEPTH);
            DeviceImage3dMaxHeight = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_IMAGE3D_MAX_HEIGHT);
            DeviceImage3dMaxWidth = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_IMAGE3D_MAX_WIDTH);
            DeviceLocalMemSize = new CLDeviceInfoULong(cl_device_info.CL_DEVICE_LOCAL_MEM_SIZE);
            DeviceLocalMemType = new CLDeviceInfoClDeviceLocalMemType(cl_device_info.CL_DEVICE_LOCAL_MEM_TYPE);
            DeviceMaxClockFrequency = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_CLOCK_FREQUENCY);
            DeviceMaxComputeUnits = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_COMPUTE_UNITS);
            DeviceMaxConstantArgs = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_CONSTANT_ARGS);
            DeviceMaxConstantBufferSize = new CLDeviceInfoULong(cl_device_info.CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE);
            DeviceMaxMemAllocSize = new CLDeviceInfoULong(cl_device_info.CL_DEVICE_MAX_MEM_ALLOC_SIZE);
            DeviceMaxParameterSize = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_MAX_PARAMETER_SIZE);
            DeviceMaxReadImageArgs = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_READ_IMAGE_ARGS);
            DeviceMaxSamplers = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_SAMPLERS);
            DeviceMaxWorkGroupSize = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_MAX_WORK_GROUP_SIZE);
            DeviceMaxWorkItemDimensions = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_WORK_ITEM_DIMENSIONS);
            DeviceMaxWorkItemSizes = new CLDeviceInfoSizeTArray(cl_device_info.CL_DEVICE_MAX_WORK_ITEM_SIZES);
            DeviceMaxWriteImageArgs = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MAX_WRITE_IMAGE_ARGS);
            DeviceMemBaseAddrAlign = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MEM_BASE_ADDR_ALIGN);
            DeviceMinDataTypeAlignSize = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_MIN_DATA_TYPE_ALIGN_SIZE);
            DeviceName = new CLDeviceInfoString(cl_device_info.CL_DEVICE_NAME);
            DevicePreferredVectorWidthChar = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_CHAR);
            DevicePreferredVectorWidthShort = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_SHORT);
            DevicePreferredVectorWidthInt = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_INT);
            DevicePreferredVectorWidthLong = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_LONG);
            DevicePreferredVectorWidthFloat = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT);
            DevicePreferredVectorWidthDouble = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE);
            DeviceProfile = new CLDeviceInfoString(cl_device_info.CL_DEVICE_PROFILE);
            DeviceProfilingTimerResolution = new CLDeviceInfoSizeT(cl_device_info.CL_DEVICE_PROFILING_TIMER_RESOLUTION);
            DeviceQueueProperties = new CLDeviceInfoClCommandQueueProperties(cl_device_info.CL_DEVICE_QUEUE_PROPERTIES);
            DeviceSingleFpConfig = new CLDeviceInfoClDeviceFpConfig(cl_device_info.CL_DEVICE_SINGLE_FP_CONFIG);
            DeviceType = new CLDeviceInfoClDeviceType(cl_device_info.CL_DEVICE_TYPE);
            DeviceVendor = new CLDeviceInfoString(cl_device_info.CL_DEVICE_VENDOR);
            DeviceVendorId = new CLDeviceInfoUInt(cl_device_info.CL_DEVICE_VENDOR_ID);
            DeviceVersion = new CLDeviceInfoString(cl_device_info.CL_DEVICE_VERSION);
            DriverVersion = new CLDeviceInfoString(cl_device_info.CL_DRIVER_VERSION);

            All = new ReadOnlyCollection<IClDeviceInfoFor>(new List<IClDeviceInfoFor>{
                DeviceAddressBits, DeviceAvailable, DeviceCompilerAvailable, DeviceDoubleFpConfig, DeviceEndianLittle,
                DeviceErrorCorrectionSupport, DeviceExecutionCapabilities, DeviceExtensions, DeviceGlobalMemCacheSize,
                DeviceGlobalMemCacheType, DeviceGlobalMemCachelineSize, DeviceGlobalMemSize, DeviceHalfFpConfig,
                DeviceImageSupport, DeviceImage2dMaxHeight, DeviceImage2dMaxWidth, DeviceImage3dMaxDepth,
                DeviceImage3dMaxHeight, DeviceImage3dMaxWidth, DeviceLocalMemSize, DeviceLocalMemType,
                DeviceMaxClockFrequency, DeviceMaxComputeUnits, DeviceMaxConstantArgs, DeviceMaxConstantBufferSize,
                DeviceMaxMemAllocSize, DeviceMaxParameterSize, DeviceMaxReadImageArgs, DeviceMaxSamplers,
                DeviceMaxWorkGroupSize, DeviceMaxWorkItemDimensions, DeviceMaxWorkItemSizes, DeviceMaxWriteImageArgs,
                DeviceMemBaseAddrAlign, DeviceMinDataTypeAlignSize, DeviceName, DevicePreferredVectorWidthChar,
                DevicePreferredVectorWidthShort, DevicePreferredVectorWidthInt, DevicePreferredVectorWidthLong,
                DevicePreferredVectorWidthFloat, DevicePreferredVectorWidthDouble, DeviceProfile,
                DeviceProfilingTimerResolution, DeviceQueueProperties, DeviceSingleFpConfig, DeviceType, DeviceVendor,
                DeviceVendorId, DeviceVersion, DriverVersion,
            });
        }

        public interface IClDeviceInfoFor
        {
            cl_device_info Code { get; }
            string Name { get; }

            object Get(DeviceInfo device);
            string GetAsString(DeviceInfo device);
        }

        public abstract class ClDeviceInfoFor<T> : IClDeviceInfoFor
        {
            public cl_device_info Code { get; }
            public string Name { get; }

            protected internal ClDeviceInfoFor(cl_device_info info)
            {
                Code = info;
                Name = Enum.GetName(typeof(cl_device_info), info);
            }


            object IClDeviceInfoFor.Get(DeviceInfo device) => Get(device);
            public abstract T Get(DeviceInfo device);
            public abstract string GetAsString(DeviceInfo device);

        }

        private class CLDeviceInfoString : ClDeviceInfoFor<string>
        {
            public CLDeviceInfoString(cl_device_info info) : base(info) { }
            public override string Get(DeviceInfo device) => device.GetValueAsString(Name);
            public override string GetAsString(DeviceInfo device) => Get(device);
        }

        private class CLDeviceInfoBool : ClDeviceInfoFor<bool>
        {
            public CLDeviceInfoBool(cl_device_info info) : base(info) { }
            public override bool Get(DeviceInfo device) => device.GetValueAsBool(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoUInt : ClDeviceInfoFor<uint>
        {
            public CLDeviceInfoUInt(cl_device_info info) : base(info) { }
            public override uint Get(DeviceInfo device) => device.GetValueAsUInt(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoULong : ClDeviceInfoFor<ulong>
        {
            public CLDeviceInfoULong(cl_device_info info) : base(info) { }
            public override ulong Get(DeviceInfo device) => device.GetValueAsULong(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoSizeT : ClDeviceInfoFor<ulong>
        {
            public CLDeviceInfoSizeT(cl_device_info info) : base(info) { }
            public override ulong Get(DeviceInfo device) => device.GetValueAsSizeT(Name);
            public override string GetAsString(DeviceInfo device) => "size " + Get(device).ToString();
        }
        private class CLDeviceInfoSizeTArray : ClDeviceInfoFor<ulong[]>
        {
            public CLDeviceInfoSizeTArray(cl_device_info info) : base(info) { }
            public override ulong[] Get(DeviceInfo device) => device.GetValueAsSizeTArray(Name);
            public override string GetAsString(DeviceInfo device) => "size " + String.Join(", ", Get(device));
        }
        private class CLDeviceInfoClDeviceType : ClDeviceInfoFor<cl_device_type>
        {
            public CLDeviceInfoClDeviceType(cl_device_info info) : base(info) { }
            public override cl_device_type Get(DeviceInfo device) => device.GetValueAsClDeviceType(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoClDeviceFpConfig : ClDeviceInfoFor<cl_device_fp_config>
        {
            public CLDeviceInfoClDeviceFpConfig(cl_device_info info) : base(info) { }
            public override cl_device_fp_config Get(DeviceInfo device) => device.GetValueAsClDeviceFpConfig(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoClDeviceMemCacheType : ClDeviceInfoFor<cl_device_mem_cache_type>
        {
            public CLDeviceInfoClDeviceMemCacheType(cl_device_info info) : base(info) { }
            public override cl_device_mem_cache_type Get(DeviceInfo device) => device.GetValueAsClDeviceMemCacheType(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoClDeviceLocalMemType : ClDeviceInfoFor<cl_device_local_mem_type>
        {
            public CLDeviceInfoClDeviceLocalMemType(cl_device_info info) : base(info) { }
            public override cl_device_local_mem_type Get(DeviceInfo device) => device.GetValueAsClDeviceLocalMemType(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoClDeviceExecCapabilities : ClDeviceInfoFor<cl_device_exec_capabilities>
        {
            public CLDeviceInfoClDeviceExecCapabilities(cl_device_info info) : base(info) { }
            public override cl_device_exec_capabilities Get(DeviceInfo device) => device.GetValueAsClDeviceExecCapabilities(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }
        private class CLDeviceInfoClCommandQueueProperties : ClDeviceInfoFor<cl_command_queue_properties>
        {
            public CLDeviceInfoClCommandQueueProperties(cl_device_info info) : base(info) { }
            public override cl_command_queue_properties Get(DeviceInfo device) => device.GetValueAsClCommandQueueProperties(Name);
            public override string GetAsString(DeviceInfo device) => Get(device).ToString();
        }

    }
}
