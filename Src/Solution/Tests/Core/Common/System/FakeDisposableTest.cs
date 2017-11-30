using System;
using Xunit;

namespace Eternity.Tests.Core.Common.System
{
    public class FakeDisposableTest
    {
        [Fact]
        public void Dispose_DoNothing() =>
            Disposable.Fake.Dispose();
    }
}