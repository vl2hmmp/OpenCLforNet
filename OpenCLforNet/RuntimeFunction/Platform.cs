using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenCLforNet.RuntimeFunction
{
    
    using size_t = IntPtr;

    unsafe partial class OpenCL
    {
        
        [DllImport("OpenCL.dll")]
        public static extern int clGetPlatformIDs(uint num_entries, void **platforms, uint *num_platforms);

        [DllImport("OpenCL.dll")]
        public static extern int clGetPlatformInfo(void *platform, long param_name, size_t param_value_size, void *param_value, void *param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern int clGetDeviceIDs(void *platform, long device_type, uint num_entries, void **devices, uint *num_devices);

        [DllImport("OpenCL.dll")]
        public static extern int clGetDeviceInfo(void *device, long param_name, size_t param_value_size, void *param_value, void *param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateContext(void *properties, uint num_devices, void **devices, void *notify, void *user_data, int *error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseContext(void *context);

    }
}
