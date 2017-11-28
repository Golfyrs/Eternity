using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;

namespace Eternity.Unity.Core.Patterns
{
    public class WorldPattern : Pattern<World>
    {
        protected override World Idea() => EternityApp.World;
    }
}