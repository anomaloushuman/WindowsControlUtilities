using Microsoft.Win32;

namespace EnableTaskbarDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryKey taskbarKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3", true);

            if (taskbarKey != null)
            {
                // Enable the taskbar by setting the "Settings" registry key back to its original value
                taskbarKey.SetValue("Settings", new byte[] { 0x2c, 0x00, 0x00, 0x00 });

                // Close the registry key
                taskbarKey.Close();
            }
        }
    }
}
