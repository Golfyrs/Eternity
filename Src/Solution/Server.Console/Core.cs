using System;
using System.Net;
using Eternity.Server.Common.DeliveryService;

namespace Server.Console
{
    public class Core
    {
        public static void Main(string[] args)
        {
            var deliveryService = new DeliveryServiceListener();
            // Нужно подумать нужен ли классу `DeliveryServiceListener` IpAddress и порт в дальнейшем,
            // если да то перенести заполнение в конструктор.
            var _ = deliveryService.Listener( IPAddress.Broadcast, 5000 );
            System.Console.WriteLine("Start server.");

            ConsoleKeyInfo keyInfo;

            do
                keyInfo = System.Console.ReadKey(); while (keyInfo.Key != ConsoleKey.Escape);
            
            deliveryService.StopListener();
        }
    }
}
