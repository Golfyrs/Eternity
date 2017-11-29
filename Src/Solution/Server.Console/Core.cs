using System;
using System.Net;
using Eternity.Server.Common.DeliveryService;

namespace Server.Console
{
    public class Core
    {
        public static void Main(string[] args)
        {
            var deliveryService = new DeliveryServiceListener(IPAddress.Parse("127.0.0.1"), 5000);
            var _ = deliveryService.Start();

            System.Console.WriteLine("Start server.");

            ConsoleKeyInfo keyInfo;
            do
                keyInfo = System.Console.ReadKey(); while (keyInfo.Key != ConsoleKey.Escape);
            
            deliveryService.Stop();
        }
    }
}
