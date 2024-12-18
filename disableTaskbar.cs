using Microsoft.Win32;

namespace DisableTaskbarDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryKey taskbarKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3", true);

            if (taskbarKey != null)
            {
                // Disable the taskbar
                taskbarKey.SetValue("Settings", new byte[] { 0x03, 0x00, 0x00, 0x00 });

                // Close the registry key
                taskbarKey.Close();
            }
        }
    }
}
