using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eternity.Network
{
    public class ResponseLetter : ILetter
    {
        private readonly ResponseCode _code;

        public ResponseLetter(ResponseCode code)
        {
            _code = code;
        }

        public byte[] Pack()
        {
            var body = Serialize(new Message((ushort) _code, null));
            var headBytes = BitConverter.GetBytes(body.Length);

            return headBytes.Concat(body).ToArray(); // TODO: Optimize with buffer.
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
