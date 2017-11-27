using System;
using System.Collections;
using Eternity.Promises;
using Coroutine = Eternity.Unity.Common.Coroutines.Coroutine;

namespace Eternity.Unity.Common.Promises
{
    public static class CoroutinePromiseExtensions
    {
        public static IPromise Execute(this IEnumerator self) =>
            new CoroutinePromise(self);
    }
    
    /// <summary>
    ///     Represents <see cref="IPromise"/> that is satisfied after inner coroutine is completed.
    /// </summary>
    public class CoroutinePromise : IPromise
    {
        private Action _continuation;
        
        /// <summary>
        ///     Initializes new instance of <see cref="CoroutinePromise"/> with coroutine.
        /// </summary>
        /// <param name="coroutine">Coroutine represented as <see cref="IEnumerator"/>.</param>
        public CoroutinePromise(IEnumerator coroutine)
        {
            Coroutine.Execute(RunCoroutine(coroutine));
        }

        public void Then(Action continuation) => _continuation = continuation;

        public IPromise Then(Func<IPromise> continuation)
        {
            var promise = new ManualPromise();
            
            Then(() => continuation()
                .Then(() => promise
                    .Satisfy())
            );

            return promise;
        }

        private IEnumerator RunCoroutine(IEnumerator enumerator)
        {
            yield return enumerator;
            _continuation();
        }
    }
}