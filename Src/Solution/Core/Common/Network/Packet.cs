namespace Eternity.Network
{
    public struct Packet
    {
        public readonly ushort Code;
        public readonly byte[] Body;

        public Packet(ushort code, byte[] body)
        {
            Code = code;
            Body = body;
        }
    }
}