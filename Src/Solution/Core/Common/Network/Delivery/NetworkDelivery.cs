using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public class NetworkDelivery : IDisposable
    {
        public event Action<TcpClient, byte[]> PackageArrived;

        private readonly TcpClient _client;

        public NetworkDelivery(TcpClient client)
        {
            _client = client;
            Listen();
        }
        
        public void Dispose() => _client.Close();
        
        private async Task Listen()
        {
            var stream = _client.GetStream();

            while (stream.CanRead)
            {
                try
                {
                    var isConnected = !(_client.Client.Poll(1, SelectMode.SelectRead) && _client.Client.Available == 0);
                    if (!isConnected)
                    {
                        Console.WriteLine("Client has disconnected.");
                        break;
                    }

                    var headBytes = new byte[4];
                    await stream.ReadAsync(headBytes, 0, headBytes.Length);

                    var bodyLength = BitConverter.ToInt32(headBytes, 0);
                    var bodyBytes = new byte[bodyLength];

                    var readLength = 0;
                    while (readLength != bodyLength)
                        readLength += await stream.ReadAsync(bodyBytes, readLength, bodyBytes.Length - readLength);
                    
                    PackageArrived?.Invoke(_client, bodyBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }
            }
        }
    }
}
