using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public class DeviceInfo
    {

        public int Index { get; set; }
        public string Name { get; set; }
        public cl_device_type Type { get; set; }
        public int MaxComputeUnits { get; set; }
        public long[] MaxWorkItemSizes { get; set; }
        public long MaxWorkGroupSize { get; set; }
        public long MaxMemAllocSize { get; set; }
        public long MaxConstantBufferSize { get; set; }
        public string SvmCapabilities { get; set; }

    }
}
