using System;

namespace Eternity.Reactive.Extensions
{
    public static class FlowExtensions
    {
        public static IFlow<TOut> As<TIn, TOut>(this IFlow<TIn> self, Func<TIn, TOut> map) =>
            new MappedFlow<TIn, TOut>(self, map);

        public static IFlow<T> DispatchedOn<T>(this IFlow<T> self, Action<Action> dispatcher) =>
            new DispatchedFlow<T>(self, dispatcher);

        public static IFlow Debounced(this IFlow self, int timeoutMs) =>
            new DebouncedFlow(self, timeoutMs);
    }
}