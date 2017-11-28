using System;

namespace Eternity.Flows
{
    public static class FlowExtensions
    {
        public static IFlow<TResult> As<TInput, TResult>(this IFlow<TInput> self, Func<TInput, TResult> map) =>
            new MappedFlow<TInput, TResult>(self, map);
    }
}