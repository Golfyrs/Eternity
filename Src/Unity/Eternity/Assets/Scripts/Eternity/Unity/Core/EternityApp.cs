using Eternity.Core;
using UnityEngine;

namespace Eternity.Unity.Core
{
    // TODO: Only dependency injection, please (no %*$@#!ing public state here).
    public static class EternityApp
    {
        public static World World = new World();
        public static DeliveryService.Server Server {  get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            World.Spawn("TestPlayer", 0, 0);
            World.Spawn("XyiSobaki", 1, 1);

            Server = new DeliveryService.Server();
            var _ = Server.Start();
        }
    }
}