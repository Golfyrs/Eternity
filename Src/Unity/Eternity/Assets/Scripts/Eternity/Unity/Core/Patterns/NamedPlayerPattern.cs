using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;

namespace Eternity.Unity.Core.Patterns
{
    public class NamedPlayerPattern : Pattern<Player>
    {
        public string Name;

        protected override Player Idea() => EternityApp.World.Player(Name);
    }
}