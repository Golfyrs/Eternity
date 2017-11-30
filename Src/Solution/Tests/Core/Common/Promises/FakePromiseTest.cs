using System;
using Eternity.Promises;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Promises
{
    public class FakePromiseTest
    {
        private readonly IPromise _promise = Promise.Fake;

        [Fact]
        public void Then_ShouldDoNothing()
        {
            var continuation = Substitute.For<Action>();

            _promise.Then(continuation);
            
            continuation.Received(0).Invoke();
        }

        [Fact]
        public void ThenWithCoroutine_ShouldDoNothing()
        {
            var continuation = Substitute.For<Func<IPromise>>();

            _promise.Then(continuation);
            
            continuation.Received(0).Invoke();
        }
    }
}