using System;
using System.Net;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;
using Eternity.Core;
using Eternity.Server.Common.DeliveryService;
using Xunit;

namespace Eternity.Tests.Server
{
    public class BasicServerTest : IDisposable
    {
        public PostOffice PostOffice;
        public BasicServerTest()
        {
            //var deliveryService = new DeliveryServiceListener(IPAddress.Broadcast, 5000);
            //var _ = deliveryService.Start();
            //Console.WriteLine("Start server.");

            var deliveryServiceConnection = new DeliveryServiceConnection();
            var tuple = deliveryServiceConnection.Connect(IPAddress.Loopback, 5000);

            if (!tuple.Result.Item2)
                throw new Exception("The client could not connect to the server! ;( ");

            var deliveryServiceDepartment = new DeliveryServiceDepartment(tuple.Result.Item1);
            PostOffice = new PostOffice(deliveryServiceDepartment);

            //deliveryServiceConnection.StartListening(deliveryServiceDepartment);
        }

        public void Dispose()
        {
        }


        [Fact]
        public async Task Test()
        {
            var position = new Position(5, 5);
            PostOffice.Send(position, RequestType.Move);
        }
    }
}
