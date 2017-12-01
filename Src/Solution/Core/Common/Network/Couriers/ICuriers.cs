using System;
using System.Threading.Tasks;

namespace Eternity.Network
{
    public interface ICourier : IDisposable
    {
        Task<bool> Send(ILetter letter);
    }
}
