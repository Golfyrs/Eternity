using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public static class Protocol
    {
        public static Task PackAsync(Stream stream, Packet packet)
        {
            var headBytes = BitConverter.GetBytes(packet.Body.Length);
            var codeBytes = BitConverter.GetBytes(packet.Code);
            var packetBytes = headBytes.Concat(codeBytes).Concat(packet.Body).ToArray(); // TODO: Optimize with buffer.

            return stream.WriteAsync(packetBytes, 0, packetBytes.Length);
        }

        //public static async Task<byte[]> UnpackAsync(Stream stream)
        //{
        //    var headBytes = new byte[4];
        //    await stream.ReadAsync(headBytes, 0, headBytes.Length);
            
        //    var codeBytes = new byte[2];
        //    await stream.ReadAsync(codeBytes, 0, codeBytes.Length);

        //    var bodyBytes = new byte[BitConverter.ToInt32(headBytes, 0)];
        //    await stream.ReadAsync(bodyBytes, 0, bodyBytes.Length);

        //    return headBytes.Concat(bodyBytes).Concat(bodyBytes).ToArray();
        //}
    }
}