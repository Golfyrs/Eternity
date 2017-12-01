using System;
using System.Net.Sockets;

namespace Eternity.Network
{
    public interface IDelivery : IDisposable
    {
        event Action<TcpClient, byte[]> PackageArrived;
    }
}