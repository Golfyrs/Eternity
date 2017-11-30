using System;

namespace Eternity.Reactive
{
    public interface IFlow
    {
        IDisposable OnNext(Action action);
    }
    
    public interface IFlow<T>
    {
        IDisposable OnNext(Action<T> action);

        T Current { get; }
    }    
}