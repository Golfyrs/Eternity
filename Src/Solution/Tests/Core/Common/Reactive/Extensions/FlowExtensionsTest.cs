using Eternity.Reactive;
using Eternity.Reactive.Extensions;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Reactive.Extensions
{
    public class FlowExtensionsTest
    {
        private readonly IFlow<int> _source = Substitute.For<IFlow<int>>();
        
        [Fact]
        public void As_ShouldReturnMappedFlow()
        {
            var flow = _source.As(x => x.ToString() + "d");

            Assert.IsType<MappedFlow<int, string>>(flow);
        }
    }
}