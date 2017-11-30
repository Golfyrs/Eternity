using System;
using Eternity.Promises;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.Promises
{
    public class ManualPromiseTest
    {
        public class ThenBeforeSatisfy
        {
            private readonly ManualPromise _promise = new ManualPromise();

            [Fact]
            public void Then()
            {
                var action = Substitute.For<Action>();

                _promise.Then(action);
                _promise.Satisfy();
                
                action.Received(1).Invoke();
            }

            [Fact]
            public void ThenWithCoroutine()
            {
                var promise = Promise.Instant;
                var action = Substitute.For<Action>();
                var continuation = Substitute.For<Func<IPromise>>();
                continuation().Returns(promise);

                _promise.Then(continuation).Then(action);
                _promise.Satisfy();

                action.Received(1).Invoke();
                continuation.Received(1).Invoke();
            }
        }

        public class ThenAfterSatisfy
        {
            private readonly ManualPromise _promise = new ManualPromise();

            [Fact]
            public void Then()
            {
                var action = Substitute.For<Action>();

                _promise.Satisfy();
                _promise.Then(action);
                
                action.Received(1).Invoke();
            }

            [Fact]
            public void ThenWithCoroutine()
            {
                var promise = Promise.Instant;
                var action = Substitute.For<Action>();
                var continuation = Substitute.For<Func<IPromise>>();
                continuation().Returns(promise);

                _promise.Satisfy();
                _promise.Then(continuation).Then(action);
                
                action.Received(1).Invoke();
                continuation.Received(1).Invoke();
            }
        }

        public class DoubleSatisfy
        {
            private readonly ManualPromise _promise = new ManualPromise();

            [Fact]
            public void Then()
            {
                var action = Substitute.For<Action>();

                _promise.Then(action);
                _promise.Satisfy();
                _promise.Satisfy();
                _promise.Satisfy();
                
                action.Received(1).Invoke();
            }

            [Fact]
            public void ThenWithCoroutine()
            {
                var promise = Promise.Instant;
                var action = Substitute.For<Action>();
                var continuation = Substitute.For<Func<IPromise>>();
                continuation().Returns(promise);

                _promise.Then(continuation).Then(action);
                _promise.Satisfy();
                _promise.Satisfy();
                _promise.Satisfy();

                action.Received(1).Invoke();
                continuation.Received(1).Invoke();
            }
        }
    }
}