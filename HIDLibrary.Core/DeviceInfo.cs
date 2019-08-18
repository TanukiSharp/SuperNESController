using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;

namespace HIDLibrary.Core
{
    public class DeviceInfo : IEquatable<DeviceInfo>, IDisposable
    {
        private string originalDevicePath;

        public ushort VendorID { get; private set; }
        public ushort ProductID { get; private set; }
        public uint Identifier { get; private set; }
        public string DevicePath { get; private set; }

        public Stream Stream { get; private set; }

        public short InputReportLength { get; private set; }
        public short OutputReportLength { get; private set; }

        private SafeFileHandle handle;

        public DeviceInfo(string devicePath)
        {
            if (string.IsNullOrWhiteSpace(devicePath))
                throw new ArgumentException("Invalid Device Path. (it must not be null or empty)", nameof(devicePath));

            if (InitializeDevicePath(devicePath) == false)
                throw new ArgumentException($"Invalid Device Path. ({devicePath})");

            HID.DevicePlugged += HID_DevicePlugged;
            HID.DeviceUnplugged += HID_DeviceUnplugged;
        }

        private void HID_DevicePlugged(object sender, DeviceEventsArgs e)
        {
            if (e.VendorID == VendorID && e.ProductID == ProductID)
                OnDevicePlugged();
        }

        private void HID_DeviceUnplugged(object sender, DeviceEventsArgs e)
        {
            if (e.VendorID == VendorID && e.ProductID == ProductID)
                OnDeviceUnplugged();
        }

        public event EventHandler DevicePlugged;
        public event EventHandler DeviceUnplugged;

        protected virtual void OnDevicePlugged()
        {
            DevicePlugged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDeviceUnplugged()
        {
            DeviceUnplugged?.Invoke(this, EventArgs.Empty);
        }

        public bool Reset()
        {
            return InitializeDevicePath(DevicePath);
        }

        private bool InitializeDevicePath(string devicePath)
        {
            VendorID = 0;
            ProductID = 0;
            Identifier = 0;
            DevicePath = null;

            InputReportLength = -1;
            OutputReportLength = -1;

            if (ParseDevicePath(devicePath, out ushort vid, out ushort pid, out uint id))
            {
                VendorID = vid;
                ProductID = pid;
                Identifier = id;
            }

            originalDevicePath = devicePath;
            DevicePath = devicePath;

            return true;
        }

        public bool Start()
        {
            Dispose();

            handle = Win32USB.CreateFile(
                DevicePath,
                Win32USB.GENERIC_READ | Win32USB.GENERIC_WRITE,
                Win32USB.FILE_SHARE_READ | Win32USB.FILE_SHARE_WRITE,
                IntPtr.Zero,
                Win32USB.OPEN_EXISTING,
                Win32USB.FILE_FLAG_OVERLAPPED,
                IntPtr.Zero);

            if (handle == null)
                return false;

            if (GetCaps() == false)
                return false;

            Stream = new FileStream(handle, FileAccess.ReadWrite, Math.Max(InputReportLength, OutputReportLength), true);

            return true;
        }

        private bool GetCaps()
        {
            InputReportLength = -1;
            OutputReportLength = -1;

            if (Win32USB.HidD_GetPreparsedData(handle, out IntPtr preparsedData))
            {
                try
                {
                    Win32USB.HidP_GetCaps(preparsedData, out Win32USB.HidCaps caps);

                    InputReportLength = caps.InputReportByteLength;
                    OutputReportLength = caps.OutputReportByteLength;
                }
                finally
                {
                    Win32USB.HidD_FreePreparsedData(ref preparsedData);
                }

                return true;
            }
            else
            {
                Win32USB.CloseHandle(handle);
                handle = null;

                return false;
            }
        }

        public static bool ParseDevicePath(string devicePath, out ushort vendorID, out ushort productID, out uint identifier)
        {
            if (string.IsNullOrWhiteSpace(devicePath))
                throw new ArgumentException("Invalid argument.", nameof(devicePath));

            vendorID = 0;
            productID = 0;
            identifier = 0;

            string[] elements = devicePath.Split('#');

            if (elements.Length < 2)
                return false;

            if (elements[0] != @"\\?\hid")
                return false;

            string[] subElements = elements[1].Split('&');
            if (subElements.Length < 2)
                return false;

            string pidStr = subElements.FirstOrDefault(se => se.StartsWith("pid_"));
            if (pidStr == null)
                return false;

            productID = Convert.ToUInt16(pidStr.Substring(4), 16);

            string vidStr = subElements.FirstOrDefault(se => se.StartsWith("vid_"));
            if (vidStr == null)
                return false;

            vendorID = Convert.ToUInt16(vidStr.Substring(4), 16);

            if (elements.Length > 2)
            {
                subElements = elements[2].Split('&');
                if (subElements.Length >= 2)
                    identifier = Convert.ToUInt32(subElements[1], 16);
            }

            return true;
        }

        public override string ToString()
        {
            return $"VendorID: 0x{VendorID:X4}, ProductID: 0x{ProductID:X4}";
        }

        public override bool Equals(object obj)
        {
            var deviceInfo = obj as DeviceInfo;

            if (deviceInfo == null)
                return false;

            return Equals(deviceInfo);
        }

        public bool Equals(DeviceInfo other)
        {
            if (other == null)
                return false;

            return string.Equals(other.DevicePath, DevicePath, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            if (DevicePath == null)
                return 0;

            return DevicePath.GetHashCode();
        }

        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }
        }
    }

    public class DeviceInfoEqualityComparer : IEqualityComparer<DeviceInfo>
    {
        public bool Equals(DeviceInfo x, DeviceInfo y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(DeviceInfo obj)
        {
            if (obj == null)
                return 0;

            return obj.GetHashCode();
        }
    }
}
