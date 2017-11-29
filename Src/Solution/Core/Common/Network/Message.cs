namespace Eternity.Network
{
    public class Message
    {
        public readonly ushort Code;
        public readonly object Body;

        public Message(ushort code, object body)
        {
            Code = code;
            Body = body;
        }
    }
}
