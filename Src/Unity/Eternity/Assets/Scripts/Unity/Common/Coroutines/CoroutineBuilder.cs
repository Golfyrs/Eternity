using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Common.Coroutines
{
    /// <summary>
    ///     Represents coroutine builder,
    ///     which allows to create Unity coroutines without additional methods.
    /// </summary>
    public class CoroutineBuilder
    {
        private class Coroutine : IEnumerator
        {
            private readonly IList<object> _steps;
            private int _index = -1;

            public Coroutine(IList<object> steps)
            {
                _steps = steps;
            }

            public object Current => _steps[_index];
            
            public void Reset() => _index = -1;
            
            public bool MoveNext()
            {
                _index++;
                
                if (_index == _steps.Count)
                    return false;
                
                var next = _steps[_index];
                if (next is Action)
                {
                    ((Action) next).Invoke();
                    return MoveNext();
                }

                return true;
            }
        }

        private readonly IList<object> _steps = new List<object>();

        /// <summary>
        ///     Finishes building process and represents <see cref="CoroutineBuilder"/> as Unity coroutine.
        /// </summary>
        /// <returns><see cref="IEnumerator"/> object that represents Unity coroutine.</returns>
        public IEnumerator AsCoroutine() => new Coroutine(_steps);

        /// <summary>
        ///     Invokes specified function at this time of coroutine.
        /// </summary>
        /// <param name="action">Action to be invoked.</param>
        /// <returns><code>this</code> builder object.</returns>
        public CoroutineBuilder Do(Action action) => Add(action);
        
        /// <summary>
        ///     Skips one frame at this time of coroutine.
        /// </summary>
        /// <returns><code>this</code> builder object.</returns>
        public CoroutineBuilder SkipFrame() => Add(null);
        
        /// <summary>
        ///     Skips specified number of seconds at this time of coroutine.
        /// </summary>
        /// <param name="seconds">Number of seconds to be skipped.</param>
        /// <returns><code>this</code> builder object.</returns>
        public CoroutineBuilder SkipSeconds(float seconds) => Add(new WaitForSeconds(seconds));

        private CoroutineBuilder Add(object step)
        {
            _steps.Add(step);
            return this;
        }
    }
}