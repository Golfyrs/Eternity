using System;

namespace Eternity.Reactive
{
    public class MappedFlow<TIn, TOut> : IFlow<TOut>
    {
        private readonly IFlow<TIn> _source;
        private readonly Func<TIn, TOut> _map;

        public MappedFlow(IFlow<TIn> source, Func<TIn, TOut> map)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (map == null)
                throw new ArgumentNullException(nameof(map));
            
            _source = source;
            _map = map;
        }
        
        public TOut Current => _map(_source.Current);

        public IDisposable OnNext(Action<TOut> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            return _source.OnNext(x => action(_map(x)));
        }
    }
}