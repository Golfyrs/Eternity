using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Eternity.Dto;
using Eternity.Network;
using Eternity.Server.Common.DeliveryService;

namespace Eternity.Server
{
    public class EternityServer
    {
        private readonly Dictionary<TcpClient, ICourier> _clients;
        private readonly DeliveryServiceListener _listener;
        
        public EternityServer(string ip, int port)
        {  
            _clients = new Dictionary<TcpClient, ICourier>();
            
            _listener = new DeliveryServiceListener(IPAddress.Parse(ip), port);
            _listener.ClientConnected += OnClientConnect;
        }
        
        public void Start()
        {
            var _ = _listener.Start();

            Console.WriteLine("Start server.");
        }

        private void OnClientConnect(TcpClient client)
        {
            _clients[client] = new StreamCourier(client.GetStream());
            
            var delivery = new NetworkDelivery(client);
            delivery.PackageArrived += OnMessageArrive;
        }
        
        private void OnMessageArrive(TcpClient tcpClient, byte[] bytes)
        {
            var message = GetMessage(bytes);

            var requestType = (RequestCode) message.Code;
            
            switch (requestType)
            {
                case RequestCode.Move:
                    var moveMessage = message.Body as MoveMessage;

                    Console.WriteLine($"Name: {moveMessage.Name} X: {moveMessage.X}, Y: {moveMessage.Y}");

                    Respond(_clients[tcpClient], ResponseCode.Ok);

                    foreach (var client in _clients.Where(x => x.Key != tcpClient).ToList())
                        Respond(client.Value, ResponseCode.PlayerMoved, moveMessage);
                    
                    break;
            }
        }

        private Message GetMessage(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
                return new BinaryFormatter().Deserialize(ms) as Message;
        }
        
        private async void Respond(ICourier courier, ResponseCode code)
        {
            var letter = new ResponseLetter(code);
            var success = await courier.Send(letter);

            if (!success)
                _clients.Remove(_clients.FirstOrDefault(x => x.Value == courier).Key);
        }

        private async void Respond<T>(ICourier courier, ResponseCode code, T message)
        {
            var letter = new ProtocolLetterWithObject<T>((ushort) code, message);
            var success = await courier.Send(letter);

            if (!success)
                _clients.Remove(_clients.FirstOrDefault(x => x.Value == courier).Key);
        }
    }
}