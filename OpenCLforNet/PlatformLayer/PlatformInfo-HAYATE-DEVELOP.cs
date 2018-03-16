using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLforNet.Runtime;
using static OpenCLforNet.Utils;

namespace OpenCLforNet.Framework
{
    public unsafe class PlatformInfo
    {

        public int Index { get; }

        private Dictionary<string, byte[]> infos = new Dictionary<string, byte[]>();

        internal PlatformInfo(int index)
        {
            Index = index;

            uint count = 0;
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(0, null, &count));
            void** platforms = null;
            if (Is32Bit())
            {
                platforms = new 
            }
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(count, platforms, &count));
            
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
