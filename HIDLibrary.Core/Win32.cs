using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace HIDLibrary.Core
{
	public class Win32
	{
		[StructLayout(LayoutKind.Sequential)]
		internal struct POINT
		{
			public int X;
			public int Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Message
		{
			public IntPtr hWnd;
			public uint msg;
			public IntPtr hParam;
			public IntPtr lParam;
			public uint time;
			public POINT pt;
		}

        public const uint DBT_DEVNODES_CHANGED = 0x0007; // A device has been added to or removed from the system.

		public const uint DBT_DEVTYP_DEVICEINTERFACE = 0x00000005; // Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.
		public const uint DBT_DEVTYP_HANDLE = 0x00000006; // File system handle. This structure is a DEV_BROADCAST_HANDLE structure.
		public const uint DBT_DEVTYP_OEM = 0x00000000; // OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.
		public const uint DBT_DEVTYP_PORT = 0x00000003; // Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.
		public const uint DBT_DEVTYP_VOLUME = 0x00000002; // Logical volume. This structure is a DEV_BROADCAST_VOLUME structure.

		public struct DEV_BROADCAST_HDR
		{
			public uint dbch_size;
			public uint dbch_devicetype;
			public uint dbch_reserved;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct DEV_BROADCAST_DEVICEINTERFACE
		{
			public int dbcc_size;
			public int dbcc_devicetype;
			public int dbcc_reserved;
			public Guid dbcc_classguid;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
			public string dbcc_name;
		} 

		public delegate void HDEVNOTIFY(IntPtr hRecipient, IntPtr NotificationFilter, uint Flags);

		public struct DEV_BROADCAST_HANDLE
		{
			public uint dbch_size;
			public uint dbch_devicetype;
			public uint dbch_reserved;
			public IntPtr dbch_handle;
			public HDEVNOTIFY dbch_hdevnotify;
			public Guid dbch_eventguid;
			public uint dbch_nameoffset;
			[MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]
			public byte[] dbch_data;
		}

		public struct DEV_BROADCAST_OEM
		{
			public uint dbco_size;
			public uint dbco_devicetype;
			public uint dbco_reserved;
			public uint dbco_identifier;
			public uint dbco_suppfunc;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		struct DEV_BROADCAST_PORT
		{
			public uint dbcp_size;
			public uint dbcp_devicetype;
			public uint dbcp_reserved;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
			public string dbcp_name;
		}

		public struct DEV_BROADCAST_VOLUME
		{
			public uint dbcv_size;
			public uint dbcv_devicetype;
			public uint dbcv_reserved;
			public uint dbcv_unitmask;
			public ushort dbcv_flags;
		}


		internal const uint PM_NOREMOVE = 0x0000;
		internal const uint PM_REMOVE = 0x0001;
		internal const uint PM_NOYIELD = 0x0002;

		internal const uint WM_QUIT = 0x0012;

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool GetMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern short PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

		[DllImport("user32.dll")]
		internal static extern bool TranslateMessage(ref Message lpMsg);

		[DllImport("user32.dll")]
		internal static extern IntPtr DispatchMessage(ref Message lpmsg);
	}
}
