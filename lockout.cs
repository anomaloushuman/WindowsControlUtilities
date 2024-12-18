using System;
using System.Runtime.InteropServices;

class Program
{
    // Define GUID for keyboard class
    static readonly Guid GUID_DEVCLASS_KEYBOARD = new Guid(0x4D36E96B, 0xE325, 0x11CE, 0xBF, 0xC1, 0x08, 0x00, 0x2B, 0xE1, 0x03, 0x18);

    // Define native API functions
    [DllImport("setupapi.dll", SetLastError = true)]
    static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, uint Flags);

    [DllImport("setupapi.dll", SetLastError = true)]
    static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

    [DllImport("setupapi.dll", SetLastError = true)]
    static extern bool SetupDiRemoveDevice(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    static extern bool SetupDiRestartDevices(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, IntPtr DeviceInstanceId, uint DeviceInstanceIdSize, out uint RequiredSize);

    static void Main(string[] args)
    {
        // Get device information set for keyboard devices
        Guid keyboardGuid = new Guid(GUID_DEVCLASS_KEYBOARD.ToByteArray());
        IntPtr hDevInfo = SetupDiGetClassDevs(ref keyboardGuid, IntPtr.Zero, IntPtr.Zero, 0x00000002);

        if (hDevInfo.ToInt32() == -1)
        {
            Console.WriteLine("Error: Could not get device information set for keyboard devices");
            return;
        }

        SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
        DeviceInfoData.cbSize = (uint)Marshal.SizeOf(DeviceInfoData);

        // Remove all keyboard devices
        for (uint i = 0; SetupDiEnumDeviceInfo(hDevInfo, i, ref DeviceInfoData); i++)
        {
            if (SetupDiRemoveDevice(hDevInfo, ref DeviceInfoData))
            {
                Console.WriteLine("Removed keyboard device with device instance ID: {0}", GetDeviceInstanceId(DeviceInfoData));
            }
            else
            {
                Console.WriteLine("Error: Could not remove keyboard device with device instance ID: {0}", GetDeviceInstanceId(DeviceInfoData));
            }
        }

        // Clean up
        SetupDiDestroyDeviceInfoList(hDevInfo);
    }

    // Helper function to get the device instance ID of a device
    static string GetDeviceInstanceId(SP_DEVINFO_DATA DeviceInfoData)
    {
        uint
            RequiredSize = 0,
            DataT = 0;

        if (!SetupDiGetDeviceInstanceId(IntPtr.Zero, ref DeviceInfoData, IntPtr.Zero, 0, out RequiredSize))
        {
            IntPtr DeviceInstanceId = Marshal.AllocHGlobal((int)RequiredSize * 2);
            if (SetupDiGetDeviceInstanceId(IntPtr.Zero, ref DeviceInfoData, DeviceInstanceId, RequiredSize, out DataT))
            {
                return Marshal.PtrToStringAuto(DeviceInstanceId);
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }

    // Structure for device information
    [StructLayout(LayoutKind.Sequential)]
    struct SP_DEVINFO_DATA
    {
        public uint cbSize;
        public Guid ClassGuid;
        public uint DevInst;
        public IntPtr Reserved;
    }
}