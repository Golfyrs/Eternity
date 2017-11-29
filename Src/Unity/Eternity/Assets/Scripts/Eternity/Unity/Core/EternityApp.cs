using Eternity.Core;
using UnityEngine;

namespace Eternity.Unity.Core
{
    // TODO: Only dependency injection, please (no %*$@#!ing public state here).
    public static class EternityApp
    {
        public static World World = new World();
        public static Common.DeliveryService.Server Server {  get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            World.Spawn("TestPlayer", 0, 0);

            Server = new Common.DeliveryService.Server();
            var _ = Server.Start();
        }
    }
}