using Eternity.Core;
using Xunit;

namespace Eternity.Tests.Core
{
    public class PlaceholderTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(2, Placeholder.Add(1, 1));
        }
    }
}