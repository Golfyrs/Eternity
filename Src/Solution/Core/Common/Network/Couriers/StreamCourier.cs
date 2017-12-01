using System;
using System.IO;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public class StreamCourier : ICourier
    {
        private readonly Stream _stream;

        public StreamCourier(Stream stream)
        {
            _stream = stream;
        }

        public void Dispose() => _stream.Dispose();

        public async Task<bool> Send(ILetter letter)
        {
            try
            {
                var body = letter.Pack();
                await _stream.WriteAsync(body, 0, body.Length);

                return true;
            }
            catch (ObjectDisposedException e)
            {
                return false;
            }
        }
    }
}
