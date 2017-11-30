using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Core.Dto;
using Eternity.Network;
using Eternity.Unity.Common;

namespace Eternity.Unity.Core.DeliveryService
{
    public class Server : IDisposable
    {
        private static Courier _courier;

        public void Dispose()
        {
            _courier?.Dispose();
        }
        
        public async Task Start()
        {
            var tuple = await new DeliveryServiceConnection().Connect(IPAddress.Parse("25.70.57.150"), 5555);
            
            if (tuple.Item2)
                Log.Message("The client has connected to the server");
            else
                Log.Error("The client could not connect to the server! ;( ");
            
            _courier = new Courier(tuple.Item1);
            ThreadPool.QueueUserWorkItem(_ => ProcessCourier(_courier));
        }
        
        private void ProcessCourier(Courier courier)
        {
            courier.MessageArrived += OnMessageArrive;
            var _ = courier.StartListeningStream();
        }

        private void OnMessageArrive(Courier courier, Message message)
        {           
            var code = (ResponseCode) message.Code;
            if (code == ResponseCode.PlayerMoved)
            {
                Log.Message("Player moved!");
                
                var moveMessage = message.Body as MoveMessage;
                var world = EternityApp.World;
                var player = world.Player(moveMessage.Name);

                if (player != null)
                    player.Move((int) moveMessage.X, (int) moveMessage.Y);
                else
                    world.Spawn(moveMessage.Name, (int) moveMessage.X, (int) moveMessage.Y);
            }
        }

        public void Send<T>(RequestCode code, T message)
        {
            ThreadPool.QueueUserWorkItem(_ => _courier.Send((ushort) code, message));
        }
    }
}
