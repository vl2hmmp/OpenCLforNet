using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Function;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class Device
    {

        public Platform Platform { get; }
        public int Index { get; }
        public void* Pointer { get; }
        public DeviceInfo Info { get; }

        public Device(Platform platform, int index)
        {
            Platform = platform;
            Index = index;

            // get a device
            uint count = 0;
            OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count).CheckError();

            var devices = (void **)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            try
            {
                OpenCL.clGetDeviceIDs(platform.Pointer, (long)cl_device_type.CL_DEVICE_TYPE_ALL, count, devices, &count).CheckError();
                Pointer = devices[index];
            }
            finally
            {
                Marshal.FreeCoTaskMem(new IntPtr(devices));
            }

            Info = Platform.PlatformInfos[platform.Index].DeviceInfos[index];
        }

        public Context CreateContext()
        {
            return new Context(new Device[] { this });
        }

    }
}
