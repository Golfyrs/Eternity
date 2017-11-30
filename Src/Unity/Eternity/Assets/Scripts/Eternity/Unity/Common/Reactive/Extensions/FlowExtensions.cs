using Eternity.Reactive;

namespace Eternity.Unity.Common.Reactive.Extensions
{
    public static class FlowExtensions
    {
        public static IFlow<T> OnMainThread<T>(this IFlow<T> self) =>
            new DispatchedFlow<T>(self, Dispatcher.Invoke);
    }
}