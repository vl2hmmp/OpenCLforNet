using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLforNet
{
    class Utils
    {

        public static bool Is32Bit()
        {
            return IntPtr.Size == 4;
        }

    }
}
