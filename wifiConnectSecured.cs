using System;
using System.Diagnostics;

namespace ConnectToWifi
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ConnectToWifi <SSID> <password>");
                return;
            }

            string ssid = args[0];
            string password = args[1];

            Process process = new Process();
            process.StartInfo.FileName = "netsh.exe";
            process.StartInfo.Arguments = $"wlan connect name=\"{ssid}\" ssid=\"{ssid}\" keyMaterial=\"{password}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine($"Successfully connected to {ssid}");
            }
            else
            {
                Console.WriteLine($"Failed to connect to {ssid}: {error}");
            }
        }
    }
}
