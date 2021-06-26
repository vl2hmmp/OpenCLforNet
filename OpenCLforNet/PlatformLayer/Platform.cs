using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Function;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class Platform
    {

        public static List<PlatformInfo> PlatformInfos { get; } = new List<PlatformInfo>();

        static Platform()
        {
            // get platforms
            uint count = 0;
            OpenCL.clGetPlatformIDs(0, null, &count).CheckError();

            // create platform infos
            for (int i = 0; i < count; i++)
                PlatformInfos.Add(new PlatformInfo(i));

            PlatformInfos.Sort((a, b) =>
            {
                if (a.IsDeviceInfoObtainable && !b.IsDeviceInfoObtainable) return -1;
                if (!a.IsDeviceInfoObtainable && b.IsDeviceInfoObtainable) return 1;
                return 0;
            });

        }

        public int Index { get; }
        public void* Pointer { get; }
        public PlatformInfo Info { get; }

        public Platform(int index)
        {
            Index = index;

            // get a platform
            uint count = 0;
            OpenCL.clGetPlatformIDs(0, null, &count).CheckError();
            void** platforms = (void**)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));

            try
            {
                OpenCL.clGetPlatformIDs(count, platforms, &count).CheckError();
                Pointer = platforms[index];
            }
            finally
            {
                Marshal.FreeCoTaskMem(new IntPtr(platforms));
            }

            Info = PlatformInfos[index];
        }

        public Device[] CreateDevices(params int[] indices)
        {
            return indices
                .Select(i => new Device(this, i))
                .ToArray();
        }

    }
}
