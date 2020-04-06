using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenCLforNet.Function
{

    using size_t = IntPtr;

    unsafe static class OpenCL
    {

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetPlatformIDs(uint num_entries, void** platforms, uint* num_platforms);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetPlatformInfo(void* platform, cl_platform_info param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetDeviceIDs(void* platform, cl_device_type device_type, uint num_entries, void** devices, uint* num_devices);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetDeviceInfo(void* device, cl_device_info param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateContext(void* properties, uint num_devices, void** devices, void* notify, void* user_data, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clReleaseContext(void* context);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateCommandQueue(void* context, void* device, cl_command_queue_properties properties, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clReleaseCommandQueue(void* command_queue);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateProgramWithSource(void* context, int count, byte** strings, void* lengths, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clBuildProgram(void* program, uint num_devices, void** device_list, byte* options, void* notify, void* user_data);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetProgramBuildInfo(void* program, void* device, cl_program_build_info param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clReleaseProgram(void* program);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateKernel(void* program, byte* kernel_name, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clReleaseKernel(void* kernel);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateBuffer(void* context, cl_mem_flags flags, size_t size, void* host_ptr, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueReadBuffer(void* command_queue, void* buffer, bool blocking_read, size_t offset, size_t cb, void* ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueWriteBuffer(void* command_queue, void* buffer, bool blocking_write, size_t offset, size_t cb, void* ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern void* clEnqueueMapBuffer(void* command_queue, void* buffer, bool blocking_map, cl_map_flags map_flags, size_t offset, size_t size, uint num_events_in_wait_list, void* event_wait_list, void** event_, cl_status_code* error_code);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueUnmapMemObject(void* command_queue, void* memobj, void* mapped_ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clReleaseMemObject(void* mem);

        [DllImport("OpenCL.dll")]
        public static extern void* clSVMAlloc(void* context, cl_mem_flags flags, size_t size, uint alignment);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueSVMMap(void* command_queue, bool blocking_map, cl_map_flags map_flags, void* svm_ptr, size_t size, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueSVMUnmap(void* command_queue, void* svm_ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern void clSVMFree(void* context, void* svm_pointer);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clSetKernelArg(void* kernel, int arg_index, int arg_size, void* arg_value);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clSetKernelArgSVMPointer(void* kernel, int arg_index, void* arg_value);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clEnqueueNDRangeKernel(void* command_queue, void* kernel, uint work_dim, size_t* global_work_offset, size_t* gloal_work_size, size_t* local_work_size, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clWaitForEvents(uint num_events, void** event_list);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clGetEventProfilingInfo(void* event_, cl_profiling_info param_name, size_t param_value_size, void* param_value, size_t* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern cl_status_code clFinish(void* command_queue);

    }
}
