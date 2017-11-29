using System.Net;
using Eternity.Core.Dto;
using Eternity.Network;
using Xunit;

namespace Eternity.Tests
{
    public class TestClient
    {
        [Fact]
        public void Run()
        {
            var task = new DeliveryServiceConnection().Connect(IPAddress.Parse("25.70.57.150"), 5555);
            task.Wait();

            var tuple = task.Result;
            var courier = new Courier(tuple.Item1);

            courier.Send((ushort) RequestCode.Move, new MoveMessage
            {
                Name = "XUnitPlayer",
                X = 1,
                Y = 1
            });
        }
    }
}