using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eternity.Common.DataTransfer
{
    public static class PackingService
    {
        public static byte[] Packing<T>(T data, RequestType method)
        {
            var body = Serialization(data);
            var head = BitConverter.GetBytes(body.Length);

            var methodBytes = BitConverter.GetBytes((ushort) method);

            return head.Concat(methodBytes).Concat(body).ToArray();
        }

        public static object Unpacking(byte[] bytes)
        {
            var memStream = new MemoryStream(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            return new BinaryFormatter().Deserialize(memStream);
        }

        private static byte[] Serialization<T>(T data)
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