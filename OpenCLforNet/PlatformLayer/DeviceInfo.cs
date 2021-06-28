﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCLforNet.Function;
using static OpenCLforNet.Function.ClDeviceInfo;

namespace OpenCLforNet.PlatformLayer
{
    public unsafe class DeviceInfo
    {
        public int Index { get; }

        private Dictionary<string, byte[]> infos = new Dictionary<string, byte[]>();

        internal DeviceInfo(void* platform, int index)
        {
            Index = index;

            // get a device
            uint count = 0;
            OpenCL.clGetDeviceIDs(platform, cl_device_type.CL_DEVICE_TYPE_ALL, 0, null, &count).CheckError();
            var devices = (void**)Marshal.AllocCoTaskMem((int)(count * IntPtr.Size));
            try
            {
                OpenCL.clGetDeviceIDs(platform, cl_device_type.CL_DEVICE_TYPE_ALL, count, devices, &count).CheckError();

                // get device infos
                foreach (cl_device_info info in Enum.GetValues(typeof(cl_device_info)))
                {
                    var a = Enum.GetName(typeof(cl_device_info), info);
                    var size = new IntPtr();
                    var status = OpenCL.clGetDeviceInfo(devices[index], info, IntPtr.Zero, null, &size);

                    // サポートしている場合は値を取得
                    if (status != cl_status_code.CL_INVALID_VALUE)
                    {
                        status.CheckError();
                        byte[] value = new byte[(int)size];
                        fixed (byte* valuePointer = value)
                        {
                            OpenCL.clGetDeviceInfo(devices[index], info, size, valuePointer, null).CheckError();
                            infos.Add(Enum.GetName(typeof(cl_device_info), info), value);
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(new IntPtr(devices));
            }
        }

        public List<string> Keys { get => infos.Keys.ToList(); }

        public bool ContainsKey(string key) => infos.ContainsKey(key);
        public bool ContainsKey(IClDeviceInfoFor key) => infos.ContainsKey(key.Name);

        public T Get<T>(ClDeviceInfoFor<T> deviceInfo) => deviceInfo.Get(this);

        public bool TryGet(IClDeviceInfoFor deviceinfo, out object val)
        {
            if (infos.ContainsKey(deviceinfo.Name))
            {
                val = deviceinfo.Get(this);
                return true;
            }
            else
            {
                val = default(object);
                return false;
            }
        }

        public bool TryGet<T>(ClDeviceInfoFor<T> deviceInfo, out T val)
        {
            if (infos.ContainsKey(deviceInfo.Name))
            {
                val = deviceInfo.Get(this);
                return true;
            }
            else
            {
                val = default(T);
                return false;
            }
        }

        public string GetValueAsString(string key)
        {
            return Encoding.UTF8.GetString(infos[key], 0, infos[key].Length).Trim();
        }

        public bool GetValueAsBool(string key)
        {
            return BitConverter.ToBoolean(infos[key], 0);
        }

        public uint GetValueAsUInt(string key)
        {
            return BitConverter.ToUInt32(infos[key], 0);
        }

        public ulong GetValueAsULong(string key)
        {
            return BitConverter.ToUInt64(infos[key], 0);
        }

        public ulong GetValueAsSizeT(string key)
        {
            if (IntPtr.Size == 4)
                return BitConverter.ToUInt32(infos[key], 0);
            else
                return BitConverter.ToUInt64(infos[key], 0);
        }

        public ulong[] GetValueAsSizeTArray(string key)
        {
            if (IntPtr.Size == 4)
            {
                var num = infos[key].Length / 4;
                var array = new ulong[num];
                for (var i = 0; i < num; i++)
                    array[i] = BitConverter.ToUInt32(infos[key], 4 * i);
                return array;
            }
            else
            {
                var num = infos[key].Length / 8;
                var array = new ulong[num];
                for (var i = 0; i < num; i++)
                    array[i] = BitConverter.ToUInt64(infos[key], 8 * i);
                return array;
            }
        }

        public cl_device_type GetValueAsClDeviceType(string key)
        {
            return (cl_device_type)BitConverter.ToUInt64(infos[key], 0);
        }

        public cl_device_fp_config GetValueAsClDeviceFpConfig(string key)
        {
            return (cl_device_fp_config)BitConverter.ToUInt64(infos[key], 0);
        }

        public cl_device_mem_cache_type GetValueAsClDeviceMemCacheType(string key)
        {
            return (cl_device_mem_cache_type)BitConverter.ToUInt32(infos[key], 0);
        }

        public cl_device_local_mem_type GetValueAsClDeviceLocalMemType(string key)
        {
            return (cl_device_local_mem_type)BitConverter.ToUInt32(infos[key], 0);
        }

        public cl_device_exec_capabilities GetValueAsClDeviceExecCapabilities(string key)
        {
            return (cl_device_exec_capabilities)BitConverter.ToInt64(infos[key], 0);
        }

        public cl_command_queue_properties GetValueAsClCommandQueueProperties(string key)
        {
            return (cl_command_queue_properties)BitConverter.ToInt64(infos[key], 0);
        }

    }
}
