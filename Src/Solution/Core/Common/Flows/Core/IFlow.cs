using System;

namespace Eternity.Flows
{
    public interface IFlow
    {
        // TODO: Unsubscribe.
        IDisposable OnNext(Action action);
    }
    
    public interface IFlow<T>
    {
        // TODO: Unsubscribe.
        IDisposable OnNext(Action<T> action);

        T Value();
    }    
}