using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.RuntimeFunction;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class Platform
    {

        public static List<PlatformInfo> PlatformInfos { get; } = new List<PlatformInfo>();

        static Platform()
        {
            // get platforms
            uint count = 0;
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(0, null, &count));

            // create platform infos
            for (int i = 0; i < count; i++)
                PlatformInfos.Add(new PlatformInfo(i));
        }

        public int Index { get; }
        public void *Pointer { get; }
        public PlatformInfo Info { get; }

        public Platform(int index)
        {
            Index = index;

            // get a platform
            uint count = 0;
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(0, null, &count));
            void **platforms = (void**)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            OpenCL.CheckError(OpenCL.clGetPlatformIDs(count, platforms, &count));
            Pointer = platforms[index];

            Info = PlatformInfos[index];

            Marshal.FreeCoTaskMem(new IntPtr(platforms));
        }

        public Device[] CreateDevices(params int[] index)
        {
            return index
                .Select(i => new Device(this, i))
                .ToArray();
        }

    }
}
