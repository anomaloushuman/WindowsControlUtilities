using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("setupapi.dll", SetLastError = true)]
    static extern int CM_Locate_DevNode(out IntPtr pdnDevInst, string pDeviceID, int ulFlags);

    [DllImport("setupapi.dll", SetLastError = true)]
    static extern int CM_Reenumerate_DevNode(IntPtr pdnDevInst, int ulFlags);

    static void Main(string[] args)
    {
        IntPtr pdnDevInst = IntPtr.Zero;

        // Locate the root device node
        int result = CM_Locate_DevNode(out pdnDevInst, null, 0);
        if (result != 0)
        {
            Console.WriteLine("Failed to locate root device node: error {0}", result);
            return;
        }

        // Re-enumerate the device node to scan for hardware changes and install new devices
        result = CM_Reenumerate_DevNode(pdnDevInst, 0);
        if (result != 0)
        {
            Console.WriteLine("Failed to re-enumerate device node: error {0}", result);
            return;
        }

        Console.WriteLine("Hardware scan complete");
    }
}
