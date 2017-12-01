using System;

namespace Eternity.Reactive
{
    public class DebouncedFlow : IFlow
    {
        private readonly IFlow _flow;
        private readonly int _timeoutMs;

        private int _elapsedTicks;

        public DebouncedFlow(IFlow flow, int timeoutMs)
        {
            _flow = flow;
            _timeoutMs = timeoutMs;
        }

        public IDisposable OnNext(Action action) => _flow.OnNext(() =>
        {
            var timeout = Environment.TickCount - _elapsedTicks;
            if (timeout < _timeoutMs)
                return;

            action();
            _elapsedTicks = Environment.TickCount;
        });
    }
}