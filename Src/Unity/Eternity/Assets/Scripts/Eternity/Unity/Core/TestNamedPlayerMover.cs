using Eternity.Unity.Common.Components;

namespace Eternity.Unity.Core
{
    public class TestNamedPlayerMover : EternityComponent
    {
        public string Name;
        public int X;
        public int Y;
        
        protected override void Initialize()
        {
            var player = EternityApp.World.Player(Name);

            Updates.OnNext(() => player.Move(X, Y));
        }
    }
}