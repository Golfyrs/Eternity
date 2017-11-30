using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public class Courier : IDisposable
    {        
        private readonly TcpClient _client;
        private readonly NetworkStream _networkStream;

        public Courier(TcpClient client)
        {
            _client = client;
            _networkStream = client.GetStream();
        }
        
        public event Action<Courier, Message> MessageArrived;
        
        public void Dispose()
        {
            _client.Close();
            _networkStream.Close();
        }
        
        public async Task StartListeningStream()
        {
            while (true)
            {
                try
                {
                    var message = await Get(_networkStream);
                    
                    MessageArrived?.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }
            }
        }
        
        public async Task<bool> Send<T>(ushort code, T message)
        {
            try
            {
                var stream = _networkStream;
                var serializedMessage = Serialize(message);

                var packet = new Packet(code, serializedMessage);
                await Protocol.PackAsync(stream, packet);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        public async Task<bool> Send(ushort code)
        {
            try
            {
                var stream = _networkStream;
                var serializedMessage = new byte[0];

                var packet = new Packet(code, serializedMessage);
                await Protocol.PackAsync(stream, packet);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        private static async Task<Message> Get(Stream stream)
        {
            var packet = await Protocol.UnpackAsync(stream);
            if (packet.Body.Length == 0)
                return new Message(packet.Code, null);

            using (var ms = new MemoryStream(packet.Body, 0, packet.Body.Length))
                return new Message(packet.Code, new BinaryFormatter().Deserialize(ms));
        }
        
        private static byte[] Serialize<T>(T data)
        {
            if (data == null)
                return null;
            
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}
