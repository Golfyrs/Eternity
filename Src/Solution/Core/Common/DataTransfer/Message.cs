using System;

namespace Eternity.Common.DataTransfer
{
    public class Message
    {
        public readonly ServerMethods Method;

        //private readonly byte[] _header;
        //private readonly byte[] _method;
        private readonly byte[] _bodyBuffer;

        public Message(byte[] method, byte[] bodyBuffer)
        {
            _bodyBuffer = bodyBuffer;

            Method = (ServerMethods) BitConverter.ToInt16(method, 0);
        }

        public T GetData<T>()
        {
            // PackingService - можно задавать через dependency injection.
            return PackingService.Unpacking<T>(_bodyBuffer);
        }
    }
}
