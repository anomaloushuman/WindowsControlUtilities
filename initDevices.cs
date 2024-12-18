using System;
using System.Management;

namespace DeviceInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Device Information:");
            Console.WriteLine();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE ClassGuid='{6bdd1fc6-810f-11d0-bec7-08002be2092f}' AND Description LIKE '%cam%'"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                Console.WriteLine("Device Name: {0}", (string)device["Name"]);
                Console.WriteLine("DeviceID: {0}", (string)device["DeviceID"]);
                Console.WriteLine("Device Class: {0}", (string)device["ClassGuid"]);
                Console.WriteLine("Device Status: {0}", (string)device["Status"]);
                Console.WriteLine("Device Manufacturer: {0}", (string)device["Manufacturer"]);
                Console.WriteLine("Device Description: {0}", (string)device["Description"]);
                Console.WriteLine();
            }
            collection.Dispose();
        }
    }
}
