using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Function;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class PlatformInfo
    {

        public int Index { get; }
        public List<DeviceInfo> DeviceInfos { get; } = new List<DeviceInfo>();

        private Dictionary<string, byte[]> infos = new Dictionary<string, byte[]>();

        internal PlatformInfo(int index)
        {
            Index = index;

            // get a platform
            uint count = 0;
            OpenCL.clGetPlatformIDs(0, null, &count).CheckError();
            var platforms = (void **)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            void* platform;
            try
            {
                OpenCL.clGetPlatformIDs(count, platforms, &count).CheckError();
                platform = platforms[index];
            }
            finally
            {
                Marshal.FreeCoTaskMem(new IntPtr(platforms));
            }

            // get platform infos
            foreach (cl_platform_info info in Enum.GetValues(typeof(cl_platform_info)))
            {
                var size = new IntPtr();
                OpenCL.clGetPlatformInfo(platform, info, IntPtr.Zero, null, &size).CheckError();
                byte[] value = new byte[(int)size];
                fixed (byte* valuePointer = value)
                {
                    OpenCL.clGetPlatformInfo(platform, info, size, valuePointer, null).CheckError();
                    infos.Add(Enum.GetName(typeof(cl_platform_info), info), value);
                }
            }
            
            // get devices
            OpenCL.clGetDeviceIDs(platform, cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count).CheckError();

            // create device infos
            for (int i = 0; i < count; i++)
                DeviceInfos.Add(new DeviceInfo(platform, i));
        }

        public List<string> Keys { get => infos.Keys.ToList(); }

        public string this[string key]
        {
            get => Encoding.UTF8.GetString(infos[key], 0, infos[key].Length).Trim();
        }

        public string GetValueAsString(string key)
        {
            return Encoding.UTF8.GetString(infos[key], 0, infos[key].Length).Trim();
        }

    }
}
