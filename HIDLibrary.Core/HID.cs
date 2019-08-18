using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace HIDLibrary.Core
{
    public class DeviceEventsArgs : EventArgs
    {
        public ushort VendorID { get; }
        public ushort ProductID { get; }
        public uint Identifier { get; }
        public string DevicePath { get; }

        public DeviceEventsArgs(ushort vendorID, ushort productID, uint identifier, string devicePath)
        {
            VendorID = vendorID;
            ProductID = productID;
            Identifier = identifier;
            DevicePath = devicePath;
        }
    }

    public static class HID
    {
        static HID()
        {
            Win32USB.HidD_GetHidGuid(out Guid hidGuid);
            Guid = hidGuid;
        }

        public static Guid Guid { get; private set; }

        private static IMessagePump globalMessagePump;
        private static IDisposable messagePumpSubscription;

        public static void Initialize(IMessagePump messagePump)
        {
            if (messagePump == null)
                throw new ArgumentNullException(nameof(messagePump));

            globalMessagePump = messagePump;
            globalMessagePump.MessageReceived += globalMessagePump_MessageReceived;
            messagePumpSubscription = globalMessagePump.Start();
        }

        private static void globalMessagePump_MessageReceived(object sender, MessagePumpEventArgs e)
        {
            if (e.Message == Win32USB.WM_DEVICECHANGE)
            {
                int type = e.HighParam.ToInt32();

                if (type == Win32USB.DEVICE_ARRIVAL || type == Win32USB.DEVICE_REMOVECOMPLETE)
                {
                    Win32.DEV_BROADCAST_HDR hdr = PtrToStructure<Win32.DEV_BROADCAST_HDR>(e.LowParam);

                    if (hdr.dbch_devicetype == Win32.DBT_DEVTYP_DEVICEINTERFACE)
                    {
                        Win32.DEV_BROADCAST_DEVICEINTERFACE devitf = PtrToStructure<Win32.DEV_BROADCAST_DEVICEINTERFACE>(e.LowParam);

                        if (DeviceInfo.ParseDevicePath(devitf.dbcc_name, out ushort vendorID, out ushort productID, out uint identifier))
                        {
                            bool plugged = type == Win32USB.DEVICE_ARRIVAL;
                            EventHandler<DeviceEventsArgs> handler = plugged ? DevicePlugged : DeviceUnplugged;

                            handler?.Invoke(null, new DeviceEventsArgs(vendorID, productID, identifier, devitf.dbcc_name));
                        }
                    }
                }
            }
        }

        private static T PtrToStructure<T>(IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static event EventHandler<DeviceEventsArgs> DevicePlugged;
        public static event EventHandler<DeviceEventsArgs> DeviceUnplugged;

        public static void Terminate()
        {
            if (messagePumpSubscription != null)
                messagePumpSubscription.Dispose();
        }

        public static IEnumerable<DeviceInfo> GetDevices()
        {
            return GetDevices(0, 0);
        }

        public static IEnumerable<DeviceInfo> GetDevices(ushort vendorID, ushort productID)
        {
            Guid gHid = HID.Guid;

            IntPtr infoSetPointer = Win32USB.SetupDiGetClassDevs(ref gHid, null, IntPtr.Zero, Win32USB.DIGCF_DEVICEINTERFACE | Win32USB.DIGCF_PRESENT);

            try
            {
                var deviceInterfaceData = new Win32USB.DeviceInterfaceData();
                deviceInterfaceData.Size = Marshal.SizeOf(deviceInterfaceData);

                int index = 0;

                while (Win32USB.SetupDiEnumDeviceInterfaces(infoSetPointer, 0, ref gHid, (uint)index, ref deviceInterfaceData))
                {
                    string devicePath = GetDevicePath(infoSetPointer, ref deviceInterfaceData);

                    if (devicePath != null)
                    {
                        if (vendorID == 0 && productID == 0)
                            yield return new DeviceInfo(devicePath);
                        else
                        {
                            if (DeviceInfo.ParseDevicePath(devicePath, out ushort localVendorID, out ushort localProductID, out uint localIdentifier) &&
                                localVendorID == vendorID && localProductID == productID)
                                yield return new DeviceInfo(devicePath);
                        }
                    }

                    index++;
                }
            }
            finally
            {
                Win32USB.SetupDiDestroyDeviceInfoList(infoSetPointer);
            }
        }

        /// <summary>
        /// Helper method to return the device path given a DeviceInterfaceData structure and an InfoSet handle.
        /// Used in 'FindDevice' so check that method out to see how to get an InfoSet handle and a DeviceInterfaceData.
        /// </summary>
        /// <param name="infoSetPointer">Handle to the InfoSet</param>
        /// <param name="deviceInterfaceData">DeviceInterfaceData structure</param>
        /// <returns>The device path or null if there was some problem</returns>
        internal static string GetDevicePath(IntPtr infoSetPointer, ref Win32USB.DeviceInterfaceData deviceInterfaceData)
        {
            uint requiredSize = 0;

            // Get the device interface details
            if (Win32USB.SetupDiGetDeviceInterfaceDetail(infoSetPointer, ref deviceInterfaceData, IntPtr.Zero, 0, ref requiredSize, IntPtr.Zero) == false)
            {
                var deviceInterfaceDetailData = new Win32USB.DeviceInterfaceDetailData();

                if (IntPtr.Size == 8)
                    deviceInterfaceDetailData.Size = 8;
                else
                    deviceInterfaceDetailData.Size = 5;

                if (Win32USB.SetupDiGetDeviceInterfaceDetail(infoSetPointer, ref deviceInterfaceData, ref deviceInterfaceDetailData, requiredSize, ref requiredSize, IntPtr.Zero))
                    return deviceInterfaceDetailData.DevicePath;
            }

            return null;
        }
    }
}
