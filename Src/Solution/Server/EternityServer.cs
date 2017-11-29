﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Eternity.Core.Dto;
using Eternity.Network;
using Eternity.Server.Common.DeliveryService;

namespace Eternity.Server
{
    public class EternityServer
    {
        private readonly List<Courier> _couriers;
        private readonly DeliveryServiceListener _listener;
        
        public EternityServer(string ip, int port)
        {  
            _couriers = new List<Courier>();
            
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
            var department = new Courier(client);
            _couriers.Add(department);
            
            ThreadPool.QueueUserWorkItem(_ => Process(department));
        }
        
        private void Process(Courier courier)
        {
            courier.MessageArrived += OnMessageArrive;
            var _ = courier.StartListeningStream();
        }

        private void OnMessageArrive(Courier courier, Message message)
        {
            var requestType = (RequestCode) message.Code;
            
            switch (requestType)
            {
                case RequestCode.Move:
                    var moveMessage = message.Body as MoveMessage;

                    Console.WriteLine($"X: {moveMessage.X}, Y: {moveMessage.Y}");

                    Respond(courier, ResponseCode.Ok);
                    
                    foreach (var otherCourier in _couriers.Where(x => x != courier))
                        Respond(otherCourier, ResponseCode.Ok, moveMessage);
                        
                    break;
            }
        }

        private static async void Respond(Courier courier, ResponseCode code) =>
            await courier.Send((ushort) code);
        
        private static async void Respond<T>(Courier courier, ResponseCode code, T message) =>
            await courier.Send((ushort) code, message);
    }
}