using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public unsafe class Platform
    {

        public static PlatformInfo[] GetPlatformInfos()
        {
            int platformCount = 0;
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(0, null, &platformCount));
            var platformInfos = new PlatformInfo[platformCount];

            byte[] value = new byte[256];
            int size = 0;
            fixed (byte* valuePointer = value)
            {
                for (int i = 0; i < platformCount; i++)
                {
                    var platform = new Platform(i);
                    OpenCL.CheckError(OpenCL.clGetPlatformInfo(platform.Pointer, (int)cl_platform_info.CL_PLATFORM_VENDOR, 256, valuePointer, &size));
                    var vendor = Encoding.UTF8.GetString(value, 0, size);
                    OpenCL.CheckError(OpenCL.clGetPlatformInfo(platform.Pointer, (int)cl_platform_info.CL_PLATFORM_NAME, 256, valuePointer, &size));
                    var name = Encoding.UTF8.GetString(value, 0, size);
                    OpenCL.CheckError(OpenCL.clGetPlatformInfo(platform.Pointer, (int)cl_platform_info.CL_PLATFORM_VERSION, 256, valuePointer, &size));
                    var version = Encoding.UTF8.GetString(value, 0, size);
                    OpenCL.CheckError(OpenCL.clGetPlatformInfo(platform.Pointer, (int)cl_platform_info.CL_PLATFORM_PROFILE, 256, valuePointer, &size));
                    var profile = Encoding.UTF8.GetString(value, 0, size);
                    platformInfos[i] = new PlatformInfo
                    {
                        Index = i,
                        Vendor = vendor,
                        Name = name,
                        Version = version,
                        Profile = profile,
                        DeviceInfos = Device.GetDeviceInfos(platform)
                    };
                }
            }
            return platformInfos;
        }

        public readonly int Index;
        public readonly long Pointer;

        public Platform(int index)
        {
            int platformCount = 0;
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(0, null, &platformCount));

            var platforms = new long[platformCount];
            fixed (long* platformsPointer = platforms)
            {
                OpenCL.CheckError(OpenCL.clGetPlatformIDs(platformCount, platformsPointer, &platformCount));
            }
            Index = index;
            Pointer = platforms[index];
        }

        public Device CreateDevice(int index)
        {
            return new Device(this, index);
        }

    }
}
