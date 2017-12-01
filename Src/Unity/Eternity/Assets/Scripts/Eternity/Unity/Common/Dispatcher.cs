using System;
using Eternity.Reactive;
using UnityEngine;

namespace Eternity.Unity.Common
{
    public class Dispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            new GameObject("_dispatcher")
            {
                hideFlags = HideFlags.HideAndDontSave
            }.AddComponent<Dispatcher>();
        }
        
        private static IFlux _updates = new PureFlux();
        
        private static readonly object Lock = new object();
        
        private static readonly Action[] Queue1 = new Action[1024];
        private static readonly Action[] Queue2 = new Action[1024];
        
        private static Action[] _currentQueue = Queue1;
        private static int _length;
        
        private void Update()
        {
            _updates.Pulse();
            
            Action[] queue;
            int length;

            lock (Lock)
            {
                queue = _currentQueue;
                length = _length;
                
                _currentQueue = queue == Queue1 ? Queue2 : Queue1;
                _length = 0;
            }
            
            for (var i = 0; i < length; i++)
                queue[i].Invoke();
        }
        
        #region API

        public static IFlow Updates => _updates;

        public static void Invoke(Action action)
        {
            lock (Lock)
                _currentQueue[_length++] = action;
        }
        
        #endregion
    }
}