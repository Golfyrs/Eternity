using Eternity.Core;
using Eternity.Unity.Common;
using UnityEngine;

namespace Eternity.Unity.Core
{
    // TODO: Only dependency injection, please (no %*$@#!ing public state here).
    public class EternityApp : MonoBehaviour
    {
        private static EternityApp _instance;
                
        public static World World = new World();
        public static DeliveryService.Server Server {  get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            _instance = new GameObject("_root")
            {
                hideFlags = HideFlags.HideAndDontSave
            }.AddComponent<EternityApp>();
            
            World.Spawn("Joshua", 0, 0);
            World.Spawn("XyiSobaki", 1, 1);

            Server = new DeliveryService.Server();
            var _ = Server.Start();
        }

        private void Update()
        {
            Dispatcher.Update();
        }

        private void OnApplicationQuit() => Server?.Dispose();
    }
}