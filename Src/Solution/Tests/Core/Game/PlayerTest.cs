using Eternity.Game;
using Xunit;

namespace Eternity.Tests.Core.Game
{
    public class PlayerTest
    {
        [Theory]
        [InlineData("Test")]
        [InlineData("Test1")]
        [InlineData("Test12")]
        [InlineData("Test123")]
        public void Name_ShouldReturnValueFromConstructor(string name)
        {
            Assert.Equal(name, new Player(name).Name);
        }
        
        [Fact]
        public void Move_ShouldChangePosition()
        {
            var player = new Player("Test");

            player.Move(1, 2);
            Assert.Equal(1, player.X.Current);
            Assert.Equal(2, player.Y.Current);
            
            player.Move(15, 12);
            Assert.Equal(15, player.X.Current);
            Assert.Equal(12, player.Y.Current);
        }
    }
}