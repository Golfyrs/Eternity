using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Eternity.Common.DataTransfer
{
    public class DeliveryServiceDepartment : IDisposable
    {
        private readonly NetworkStream _networkStream;

        public DeliveryServiceDepartment(TcpClient client)
        {
            _networkStream = client.GetStream();
        }

        public async Task Send(byte[] bytes)
        {
            await _networkStream.WriteAsync(bytes, 0, bytes.Length);
        }

        public async Task<Message> Get()
        {
            Console.WriteLine("Reading from client.");

            var message = await ReadFromStreamAsync();
            
            Console.WriteLine("Reading ended.");
            
            return message;
        }
        
        public void Dispose()
        {
            _networkStream.Dispose();
        }

        private async Task<Message> ReadFromStreamAsync()
        {
            // Это правила протокола. Было бы круто вынести правило ( 4 - header, 2 - method, n - body ) в другой класс. 
            // Есть идея с каким-то подобием итератора, дополнительный класс будет задавать правила. 
            // Пример: считывает 4 байта и передает обратно в класс, потом 2 байта и обратно, n байтов и обратно, а в конце возвращает message.
            var header = await ReadFromStreamAsync(4);

            var method = await ReadFromStreamAsync(2);

            var bodyLenght = BitConverter.ToInt16(header, 0);
            var bodyBuffer = await ReadFromStreamAsync(bodyLenght);
            
            var message = new Message(method, bodyBuffer);

            return message;
        }

        private async Task<byte[]> ReadFromStreamAsync(int nbytes)
        {
            var buf = new byte[nbytes];
            var readpos = 0;

            while (readpos < nbytes)
                readpos += await _networkStream.ReadAsync(buf, readpos, nbytes - readpos);

            return buf;
        }
    }
}
