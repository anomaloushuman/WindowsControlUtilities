using System;
using System.Net.NetworkInformation;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces()
            .Where(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211);

        var interfaceList = interfaces.Select(ni => new {
            Name = ni.Name,
            Description = ni.Description,
            DeviceId = ni.Id,
            Manufacturer = ni.GetPhysicalAddress()?.ToString(),
            Speed = ni.Speed,
            OperationalStatus = ni.OperationalStatus.ToString(),
            SupportsMulticast = ni.SupportsMulticast,
            IPv4Addresses = ni.GetIPProperties().UnicastAddresses
                .Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(a => a.Address.ToString())
        });

        var json = JsonConvert.SerializeObject(interfaceList, Formatting.Indented);

        Console.WriteLine(json);
    }
}
