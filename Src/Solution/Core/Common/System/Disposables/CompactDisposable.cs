// ReSharper disable once CheckNamespace
namespace System
{
    public class CompactDisposable : IDisposable
    {
        private readonly Action _dispose;

        public CompactDisposable(Action initialize, Action dispose)
        {
            if (initialize == null) throw new ArgumentNullException(nameof(initialize));
            if (dispose == null) throw new ArgumentNullException(nameof(dispose));

            _dispose = dispose;

            initialize();
        }
        
        public void Dispose()
        {
            _dispose();
        }
    }
}