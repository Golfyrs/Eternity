using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public class DeliveryServiceConnection
    {
        private const int ConnectTimeoutMilliseconds = 1000;

        private TcpClient _client;

        public async Task<Tuple<TcpClient, bool>> Connect(IPAddress address, int port)
        {
            _client = new TcpClient();

            var task = _client.ConnectAsync(address, port);
            await Task.WhenAny(task, Task.Delay(ConnectTimeoutMilliseconds));

            return new Tuple<TcpClient, bool>(_client, !task.IsFaulted);
        }
    }
}
