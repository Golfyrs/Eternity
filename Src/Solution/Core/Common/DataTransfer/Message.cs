using System;

namespace Eternity.Common.DataTransfer
{
    public class Message
    {
        public object Body;
        public readonly RequestType Method;

        public Message(byte[] method, byte[] bodyBuffer)
        {
            Body = PackingService.Unpacking(bodyBuffer);
            Method = (RequestType) BitConverter.ToInt16(method, 0);
        }
    }
}
