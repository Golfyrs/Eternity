using System;

namespace Eternity.Flows
{
    public class MappedFlow<TIn, TOut> : IFlow<TOut>
    {
        private readonly IFlow<TIn> _source;
        private readonly Func<TIn, TOut> _map;

        public MappedFlow(IFlow<TIn> source, Func<TIn, TOut> map)
        {
            _source = source;
            _map = map;
        }
        
        public TOut Value() => _map(_source.Value());

        public IDisposable OnNext(Action<TOut> action) => _source.OnNext(x => action(_map(x)));
    }
}