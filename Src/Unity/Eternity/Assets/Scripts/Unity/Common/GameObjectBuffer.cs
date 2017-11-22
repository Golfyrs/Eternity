using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.Common
{
    /// <summary>
    ///     Represents temporary <see cref="GameObject"/>, which will be destroyed after disposing.
    /// </summary>
    public class GameObjectBuffer : IDisposable
    {
        /// <summary>
        ///     Instance of temporary created <see cref="UnityEngine.GameObject"/>.
        /// </summary>
        public GameObject GameObject { get; }
        
        private GameObjectBuffer()
        {
            GameObject = new GameObject
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }
        
        /// <summary>
        ///     Creates temporary game object.
        /// </summary>
        /// <returns><see cref="GameObjectBuffer"/> object.</returns>
        public static GameObjectBuffer Use()
        {
            return new GameObjectBuffer();
        }

        /// <summary>
        ///     Destroys temporary game object.
        /// </summary>
        public void Dispose()
        {
            Object.Destroy(GameObject);
        }
    }
}