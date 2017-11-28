using System;

namespace Eternity.Reactive
{
    public sealed class PureFlux : IFlux
    {
        private event Action Next;

        public IDisposable OnNext(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            
            return new CompactDisposable(
                () => Next += action,
                () => Next -= action);
        }

        public void Pulse() => Next?.Invoke();
    }

    public sealed class PureFlux<T> : IFlux<T>
    {
        private event Action<T> Next;
        private T _current;
        
        public PureFlux(T current = default(T))
        {
            _current = current;
        }

        public T Current => _current;
        
        public IDisposable OnNext(Action<T> action) =>
            new CompactDisposable(
                () => Next += action,
                () => Next -= action);

        public void Pulse(T value)
        {
            _current = value;
            Next?.Invoke(value);
        }
    }
}