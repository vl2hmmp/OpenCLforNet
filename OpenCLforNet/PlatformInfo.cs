using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    public class PlatformInfo
    {

        public int Index { get; set; }
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Profile { get; set; }
        public DeviceInfo[] DeviceInfos { get; set; }

    }
}
