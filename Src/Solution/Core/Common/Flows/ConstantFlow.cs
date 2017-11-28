using System;

namespace Eternity.Flows
{
    public class ConstantFlow<T> : IFlow<T>
    {
        private readonly T _value;

        public ConstantFlow(T value)
        {
            _value = value;
        }
        
        public T Value() => _value;
        
        public IDisposable OnNext(Action<T> action) =>
            new CompactDisposable(
                () => action(_value),
                () => { });
    }
}