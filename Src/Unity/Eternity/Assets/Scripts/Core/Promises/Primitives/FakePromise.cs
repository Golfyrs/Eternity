using System;
using Core.Promises.Core;

namespace Core.Promises.Primitives
{
    /// <summary>
    ///     Represents <see cref="IPromise"/> that will never be satisfied.
    /// </summary>
    public class FakePromise : IPromise
    {
        public void Then(Action continuation) { }
        public IPromise Then(Func<IPromise> continuation) => this;
    }
}