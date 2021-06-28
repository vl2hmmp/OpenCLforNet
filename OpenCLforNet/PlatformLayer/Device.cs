﻿using System;
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

        public Device() : this(new Platform(0), 0) { }

        public Device(Platform platform, int index)
        {
            if (!platform.Info.IsDeviceInfoObtainable)
                throw new ArgumentException("This platform has invalid device info.");
            if (platform.Info.DeviceInfos.Count <= index)
                throw new IndexOutOfRangeException();

            Platform = platform;
            Index = index;

            // get a device
            uint count = 0;
            OpenCL.clGetDeviceIDs(platform.Pointer, cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count).CheckError();

            var devices = (void**)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            try
            {
                OpenCL.clGetDeviceIDs(platform.Pointer, cl_device_type.CL_DEVICE_TYPE_ALL, count, devices, &count).CheckError();
                Pointer = devices[index];
            }
            finally
            {
                Marshal.FreeCoTaskMem(new IntPtr(devices));
            }

            Info = platform.Info.DeviceInfos[index];
        }

        public Context CreateContext()
        {
            return new Context(new Device[] { this });
        }

    }
}
