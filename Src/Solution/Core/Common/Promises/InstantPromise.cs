using System;

namespace Eternity.Promises
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