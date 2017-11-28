using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;

namespace Eternity.Unity.Core.Patterns
{
    public class PlayerPattern : Pattern<Player>
    {
        public Player Player;

        protected override Player Idea() => Player;
    }
}