// ReSharper disable once CheckNamespace
namespace System
{
    public class CompactDisposable : IDisposable
    {
        private readonly Action _dispose;

        public CompactDisposable(Action initialize, Action dispose)
        {
            _dispose = dispose;

            initialize();
        }
        
        public void Dispose()
        {
            _dispose();
        }
    }
}