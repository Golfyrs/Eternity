using System.Collections;
using UnityEngine;

namespace Eternity.Unity.Common.Coroutines
{
    /// <summary>
    ///     Represents static type object that helps to perform coroutine operations.
    /// </summary>
    public static class Coroutine
    {
        private class CoroutineComponent : MonoBehaviour { }
        
        private static readonly CoroutineComponent Component = new GameObject("_coroutine")
        {
            hideFlags = HideFlags.HideAndDontSave
        }.AddComponent<CoroutineComponent>();
        
        /// <summary>
        ///     Executes specified coroutine.
        /// </summary>
        /// <param name="coroutine">Coroutine that needs to be executed.</param>
        public static void Execute(IEnumerator coroutine) => Component.StartCoroutine(coroutine);
    }
}