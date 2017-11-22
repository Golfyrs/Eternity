using System;
using Common.Promises.Core;

namespace Common.Promises.Primitives
{
    /// <summary>
    ///     Represents <see cref="IPromise"/> that will be applied after manual <see cref="Satisfy"/> call.
    /// </summary>
    public class ManualPromise : IPromise
    {
        private Action _continuation;
        private bool _isSatisfied;
        
        public void Then(Action continuation)
        {
            _continuation = continuation;
            
            if (_isSatisfied)
                _continuation();
        }

        public IPromise Then(Func<IPromise> continuation)
        {
            var promise = new ManualPromise();
            
            Then(() => continuation()
                .Then(() => promise
                    .Satisfy())
            );

            return promise;
        }

        /// <summary>
        ///     Manually satisfies <see cref="IPromise"/>.
        /// </summary>
        /// <returns><code>this</code> object.</returns>
        public void Satisfy()
        {
            if (_isSatisfied)
                return;
            
            _isSatisfied = true;
            _continuation();
        }
    }
}