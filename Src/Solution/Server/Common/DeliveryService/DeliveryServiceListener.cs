using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;

namespace Eternity.Server.Common.DeliveryService
{
    public class DeliveryServiceListener
    {
        public static List<TcpClient> Clients = new List<TcpClient>();

        private readonly IPAddress _ipAddress;
        private readonly int _port;

        private bool _running = true;

        public DeliveryServiceListener(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task Start()
        {
            var listener = new TcpListener(_ipAddress, _port);
            listener.Start();

            while (_running)
            {
                var client = await listener.AcceptTcpClientAsync();
                ThreadPool.QueueUserWorkItem(ProcessDepartment, client);
            }
        }

        public void Stop()
        {
            _running = false;
        }

        private void ProcessDepartment(object state)
        {
            var client = state as TcpClient;

            Console.WriteLine("Client connected, IP:"); // Нужно разобраться как идентифицировать клиента. 
            Clients.Add(client);

            var department = new DeliveryServiceDepartment(client);
            var _ = department.StartListeningStream();
        }
    }
}