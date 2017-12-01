using Eternity.Game;
using Eternity.Unity.Common;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Eternity.Unity.Core
{
    // TODO: Only dependency injection, please (no %*$@#!ing public state here).
    public class EternityApp : MonoBehaviour
    {
        public static World World = new World();
        public static DeliveryService.Server Server {  get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Log.Message("Initialized.");
            
            new GameObject("_root")
            {
                hideFlags = HideFlags.HideAndDontSave
            }.AddComponent<EternityApp>();
            
            World.Spawn("Joshua", 0, 0);
            World.Spawn("XyiSobaki", 1, 1);

            Application.logMessageReceived += (condition, stacktrace, logType) =>
            {
                if (logType == LogType.Exception)
                {
                    
                }
            };

            InitializeAsync();
        }

        private static async void InitializeAsync()
        {
            Server = new DeliveryService.Server();
            await Server.Start();
        }

        private void OnApplicationQuit() => Server?.Dispose();
    }
}