using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Dto;
using Eternity.Network;
using Eternity.Unity.Common;

namespace Eternity.Unity.Core.DeliveryService
{
    public class Server : IDisposable
    {
        private static StreamCourier _courier;

        public void Dispose()
        {
            _courier?.Dispose();
        }
        
        public async Task Start()
        {
            var tuple = await new DeliveryServiceConnection().Connect(IPAddress.Parse("25.70.105.88"), 5555);
            
            if (tuple.Item2)
                Log.Message("The client has connected to the server");
            else
                Log.Error("The client could not connect to the server! ;( ");
            
            _courier = new StreamCourier(tuple.Item1.GetStream());

            var delivery = new NetworkDelivery(tuple.Item1);
            delivery.PackageArrived += OnMessageArrive;
        }

        private void OnMessageArrive(TcpClient tcpClient, byte[] bytes)
        {
            var message = GetMessage(bytes);

            var code = (ResponseCode) message.Code;
            if (code == ResponseCode.PlayerMoved)
            {
                Log.Message("Player moved!");
                
                var moveMessage = (MoveMessage) message.Body;
                var world = EternityApp.World;
                var player = world.Player(moveMessage.Name);

                if (player != null)
                    player.Move((int) moveMessage.X, (int) moveMessage.Y);
                else
                    world.Spawn(moveMessage.Name, (int) moveMessage.X, (int) moveMessage.Y);
            }
        }

        private Message GetMessage(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
                return new BinaryFormatter().Deserialize(ms) as Message;
        }

        public void Send<T>(RequestCode code, T message)
        {
            var letter = new ProtocolLetterWithObject<T>((ushort) code, message);

            ThreadPool.QueueUserWorkItem(_ => _courier.Send(letter));
        }
    }
}
