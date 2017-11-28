using System;

namespace Eternity.Flows
{
    public class MutableFlow : IFlow
    {
        private event Action Next;
        
        public IDisposable OnNext(Action action) =>
            new CompactDisposable(
                () => Next += action,
                () => Next -= action);
        
        public void Push() => Next?.Invoke();
    }
    
    public class MutableFlow<T> : IFlow<T>
    {
        private event Action<T> Next;
        
        private T _value;

        public T Value() => _value;
        
        public IDisposable OnNext(Action<T> action) =>
            new CompactDisposable(
                () => Next += action,
                () => Next -= action);
        
        public void Push(T value)
        {
            _value = value;
            Next?.Invoke(value);
        }
    }
}