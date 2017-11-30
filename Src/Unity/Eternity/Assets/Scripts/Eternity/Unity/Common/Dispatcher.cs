using System;

namespace Eternity.Unity.Common
{
    public static class Dispatcher
    {
        private static readonly object Lock = new object();
        
        private static readonly Action[] Queue1 = new Action[1024];
        private static readonly Action[] Queue2 = new Action[1024];
        
        private static Action[] _currentQueue = Queue1;
        private static int _length;
        
        public static void Update()
        {
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

        public static void Invoke(Action action)
        {
            lock (Lock)
                _currentQueue[_length++] = action;
        }
    }
}