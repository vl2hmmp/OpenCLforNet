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
        public static extern int clGetPlatformIDs(uint num_entries, void** platforms, uint* num_platforms);

        [DllImport("OpenCL.dll")]
        public static extern int clGetPlatformInfo(void* platform, long param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern int clGetDeviceIDs(void* platform, long device_type, uint num_entries, void** devices, uint* num_devices);

        [DllImport("OpenCL.dll")]
        public static extern int clGetDeviceInfo(void* device, long param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateContext(void* properties, uint num_devices, void** devices, void* notify, void* user_data, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseContext(void* context);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateCommandQueue(void* context, void* device, cl_command_queue_properties properties, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseCommandQueue(void* command_queue);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateProgramWithSource(void* context, int count, byte** strings, void* lengths, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clBuildProgram(void* program, uint num_devices, void* *device_list, byte* options, void* notify, void* user_data);

        [DllImport("OpenCL.dll")]
        public static extern int clGetProgramBuildInfo(void* program, void* device, cl_program_build_info param_name, size_t param_value_size, void* param_value, void* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseProgram(void* program);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateKernel(void* program, byte* kernel_name, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseKernel(void* kernel);

        [DllImport("OpenCL.dll")]
        public static extern void* clCreateBuffer(void* context, cl_mem_flags flags, size_t size, void* host_ptr, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueReadBuffer(void* command_queue, void* buffer, bool blocking_read, size_t offset, size_t cb, void* ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueWriteBuffer(void* command_queue, void* buffer, bool blocking_write, size_t offset, size_t cb, void* ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern void* clEnqueueMapBuffer(void* command_queue, void* buffer, bool blocking_map, cl_map_flags map_flags, size_t offset, size_t size, uint num_events_in_wait_list, void* event_wait_list, void** event_, int* error_code);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueUnmapMemObject(void* command_queue, void* memobj, void* mapped_ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueCopyBuffer(void* command_queue, void* src_buffer, void* dst_buffer, size_t src_offset, size_t dst_offset, size_t cb, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern int clReleaseMemObject(void* mem);

        [DllImport("OpenCL.dll")]
        public static extern void* clSVMAlloc(void* context, cl_mem_flags flags, size_t size, uint alignment);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueSVMMap(void* command_queue, bool blocking_map, cl_map_flags map_flags, void* svm_ptr, size_t size, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueSVMUnmap(void* command_queue, void* svm_ptr, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern void clSVMFree(void* context, void* svm_pointer);

        [DllImport("OpenCL.dll")]
        public static extern int clSetKernelArg(void* kernel, int arg_index, int arg_size, void* arg_value);

        [DllImport("OpenCL.dll")]
        public static extern int clSetKernelArgSVMPointer(void* kernel, int arg_index, void* arg_value);

        [DllImport("OpenCL.dll")]
        public static extern int clEnqueueNDRangeKernel(void* command_queue, void* kernel, uint work_dim, size_t* global_work_offset, size_t* gloal_work_size, size_t* local_work_size, uint num_events_in_wait_list, void* event_wait_list, void** event_);

        [DllImport("OpenCL.dll")]
        public static extern int clWaitForEvents(uint num_events, void** event_list);

        [DllImport("OpenCL.dll")]
        public static extern int clGetEventProfilingInfo(void* event_, cl_profiling_info param_name, size_t param_value_size, void* param_value, size_t* param_value_size_ret);

        [DllImport("OpenCL.dll")]
        public static extern int clFinish(void* command_queue);

    }
}
