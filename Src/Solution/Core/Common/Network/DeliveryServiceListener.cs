using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Eternity.Server.Common.DeliveryService
{
    public class DeliveryServiceListener
    {
        public event Action<TcpClient> ClientConnected;
        
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
                ClientConnected?.Invoke(client);
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}