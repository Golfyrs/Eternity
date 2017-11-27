using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;

namespace Eternity.Unity.Core.Patterns
{
    public class TestPlayerPattern : Pattern<Player>
    {
        protected override Player Idea() => EternityApp.World.Player("TestPlayer");
    }
}