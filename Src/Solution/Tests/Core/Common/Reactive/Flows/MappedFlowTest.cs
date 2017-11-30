using System;
using Eternity.Reactive;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Reactive.Flows
{
    public class MappedFlowTest
    {
        private readonly IFlow<int> _source = Substitute.For<IFlow<int>>();

        public MappedFlowTest()
        {
            _source.Current.Returns(10);

            _source
                .When(x => x.OnNext(Arg.Any<Action<int>>()))
                .Do(x => x.ArgAt<Action<int>>(0).Invoke(15));
        }
        
        [Fact]
        public void Current_ShouldReturnConvertedValue()
        {
            var flow = new MappedFlow<int, string>(_source, x => x.ToString() + "d");

            Assert.Equal("10d", flow.Current);
        }

        [Fact]
        public void OnNext_ShouldFireWithConvertedValue()
        {
            var flow = new MappedFlow<int, string>(_source, x => x.ToString() + "d");

            flow.OnNext(x => Assert.Equal("15d", x));
        }
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullExceptionIfSourceIsNull() =>
            Assert.Throws<ArgumentNullException>(
                () => new MappedFlow<int, string>(null, x => x.ToString() + "d"));
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullExceptionIfFuncIsNull() =>
            Assert.Throws<ArgumentNullException>(
                () => new MappedFlow<int, string>(_source, null));

        [Fact]
        public void OnNext_ShouldThrowArgumentNullExceptionIfActionIsNull() =>
            Assert.Throws<ArgumentNullException>(
                () => new MappedFlow<int, string>(_source, x => x.ToString() + "d").OnNext(null));
    }
}