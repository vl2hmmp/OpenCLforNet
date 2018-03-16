using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.RuntimeFunction;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class Device
    {

        public Platform Platform { get; }
        public int Index { get; }
        public void *Pointer { get; }
        public DeviceInfo Info { get; }

        public Device(Platform platform, int index)
        {
            Platform = platform;
            Index = index;

            // get a platform
            uint count = 0;
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count));
            var devices = (void **)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, count, devices, &count));
            Pointer = devices[index];

            Info = Platform.PlatformInfos[platform.Index].DeviceInfos[index];

            Marshal.FreeCoTaskMem(new IntPtr(devices));
        }

        public Context CreateContext()
        {
            return new Context(new Device[] { this });
        }

    }
}
