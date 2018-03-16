using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.Runtime;

namespace OpenCLforNet.Framework
{
    public unsafe class DeviceInfo
    {

        public int Index { get; }

        private Dictionary<string, byte[]> infos = new Dictionary<string, byte[]>();

        internal DeviceInfo(void * platform, int index)
        {
            Index = index;

            uint count = 0;
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform, (long)cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count));
            void** devices = null;
            OpenCL.CheckError(OpenCL.clGetDeviceIDs(platform, (long)cl_device_type.CL_DEVICE_TYPE_ALL, count, platforms, &count));

            foreach (long info in Enum.GetValues(typeof(cl_platform_info)))
            {
                byte[] value = new byte[256];
                fixed (byte* valuePointer = value)
                {
                    OpenCL.CheckError(OpenCL.clGetPlatformInfo(platforms[index], info, new IntPtr(256), valuePointer, null));
                    infos.Add(Enum.GetName(typeof(cl_platform_info), info), value);
                }
            }
        }

        public List<String> Keys
        {
            get
            {
                return infos.Keys.ToList();
            }
        }

        public string GetValueAsString(string key)
        {
            return Encoding.UTF8.GetString(infos[key], 0, 256).Trim();
        }

    }
}
