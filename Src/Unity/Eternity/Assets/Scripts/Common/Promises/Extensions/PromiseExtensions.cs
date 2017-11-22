using System;
using Common.Promises.Core;
using Common.Promises.Primitives;

namespace Common.Promises.Extensions
{
    /// <summary>
    ///     Represents extensions applied on <see cref="IPromise"/>.
    /// </summary>
    public static class PromiseExtensions
    {
        /// <summary>
        ///     Continues promise after it's satisfied.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="getPromise">Continuation promise creaiton function.</param>
        /// <returns>Promise continued after the <paramref name="getPromise"/>.</returns>
        public static IPromise Then(this IPromise self, Func<IPromise> getPromise)
        {
            var manual = new ManualPromise();

            self.Then(() => getPromise().Then(() => manual.Satisfy()));
            
            return manual;
        }
    }
}