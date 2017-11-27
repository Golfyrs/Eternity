using Eternity.Core;
using UnityEngine;

namespace Eternity.Unity.Core
{
    // TODO: Only dependency injection, please (no %*$@#!ing public state here).
    public static class EternityApp
    {
        public static World World = new World();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            World.Spawn("TestPlayer", 0, 0);
        }
    }
}