using System;
using Eternity.Promises;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Promises
{
    public class InstantPromise
    {
        private readonly IPromise _promise = Promise.Instant;

        [Fact]
        public void Then_ShouldContinueInstantly()
        {
            var continuation = Substitute.For<Action>();

            _promise.Then(continuation);
            
            continuation.Received(1).Invoke();
        }

        [Fact]
        public void ThenWithCoroutine_ShouldContinueInstantly()
        {
            var promise = Promise.Instant;
            var action = Substitute.For<Action>();
            var continuation = Substitute.For<Func<IPromise>>();
            continuation().Returns(promise);

            _promise.Then(continuation).Then(action);
            
            action.Received(1).Invoke();
            continuation.Received(1).Invoke();
        }
    }
}