using System;
using Eternity.Reactive;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Reactive.Fluxes
{
    public class PureFluxTest
    {
        public class VoidFlux
        {
            private readonly IFlux _flux = new PureFlux();

            [Fact]
            public void OnNext_ShouldBeCalledOnPulse()
            {
                var action = Substitute.For<Action>();

                _flux.OnNext(action);
                _flux.Pulse();
                _flux.Pulse();
                
                action.Received( 2 ).Invoke();
            }

            [Fact]
            public void OnNext_ShouldThrowArgumentNullException_IfActionIsNull()
            {
                Assert.Throws<ArgumentNullException>(() => _flux.OnNext(null));
            }
        }

        public class IntFlux
        {
            private readonly IFlux<int> _flux = new PureFlux<int>(10);

            [Fact]
            public void Current_ShouldReturnValueFromConstructor()
            {
                Assert.Equal(10, _flux.Current);
            }
            
            [Fact]
            public void Current_ShouldReturnNewValueAfterPulse()
            {
                _flux.Pulse(15);

                Assert.Equal(15, _flux.Current);
            }

            [Fact]
            public void OnNext_ShouldBeCalledOnPulse()
            {
                var action = Substitute.For<Action<int>>();

                _flux.OnNext(action);
                _flux.Pulse( 10 );
                _flux.Pulse( 10 );
                
                action.Received( 2 ).Invoke( 10 );
            }
        }
    }
}