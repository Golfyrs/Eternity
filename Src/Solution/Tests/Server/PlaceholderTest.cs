using Eternity.Server;
using Xunit;

namespace Eternity.Tests.Server
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