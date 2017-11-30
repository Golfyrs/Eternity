using System;

namespace Eternity.Reactive.Extensions
{
    public static class FlowExtensions
    {
        public static IFlow<TOut> As<TIn, TOut>(this IFlow<TIn> self, Func<TIn, TOut> map) =>
            new MappedFlow<TIn, TOut>(self, map);
    }
}