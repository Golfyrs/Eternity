using System;
using Core.Promises.Core;

namespace Core.Promises.Primitives
{
    /// <summary>
    ///     Represents <see cref="IPromise"/>, that is always satisfied.
    /// </summary>
    public class InstantPromise : IPromise
    {
        public void Then(Action continuation) => continuation();
        public IPromise Then(Func<IPromise> continuation) => continuation();
    }
}