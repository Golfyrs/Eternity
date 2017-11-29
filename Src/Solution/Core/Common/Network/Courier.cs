using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public class Courier
    {
        public event Action<Courier, Message> MessageArrived;
        
        private readonly NetworkStream _networkStream;

        public Courier(TcpClient client)
        {
            _networkStream = client.GetStream();
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
        
        public Task Send<T>(ushort code, T message)
        {
            var stream = _networkStream;
            var serializedMessage = Serialize(message);
            
            var packet = new Packet(code, serializedMessage);
            return Protocol.PackAsync(stream, packet);
        }
        
        public Task Send(ushort code)
        {
            var stream = _networkStream;
            var serializedMessage = new byte[0];
            
            var packet = new Packet(code, serializedMessage);
            return Protocol.PackAsync(stream, packet);
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
