using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenCLforNet.RuntimeFunction
{

    unsafe partial class OpenCL
    {

        public static void CheckError(int status)
        {
            var code = (cl_status_code)Enum.ToObject(typeof(cl_status_code), status);
            if (code != cl_status_code.CL_SUCCESS)
            {
                throw new Exception(Enum.GetName(typeof(cl_status_code), code));
            }
        }

    }

}
