using System;
using System.Threading;
using Eternity.Reactive;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Reactive.Flows
{
    public class DebouncedFlowTest
    {
        public class VoidFlow
        {
            [Fact]
            public void OnNext_ShouldBeCalledOnlyAfterTimeout()
            {
                var flux = new PureFlux();
                var flow = new DebouncedFlow(flux, 250);
                var action = Substitute.For<Action>();

                flow.OnNext(action);
                
                flux.Pulse();
                action.Received(1).Invoke();
                
                flux.Pulse();
                flux.Pulse();
                flux.Pulse();
                flux.Pulse();
                action.Received(1).Invoke();

                Thread.Sleep(250);
                
                flux.Pulse();
                flux.Pulse();
                flux.Pulse();
                flux.Pulse();
                action.Received(2).Invoke();
            }
        }
    }
}