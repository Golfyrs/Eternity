using System;
using NSubstitute;
using Xunit;

namespace Eternity.Tests.Core.Common.System
{
    public class CompactDisposableTest
    {
        [Fact]
        public void Constructor_ShouldCallInitialize()
        {
            var initialize = Substitute.For<Action>();

            new CompactDisposable(initialize, () => { });
            
            initialize.Received(1).Invoke();
        }

        [Fact]
        public void Dispose_ShouldCallDispose()
        {
            var dispose = Substitute.For<Action>();

            using (new CompactDisposable(() => { }, dispose)) { }
            
            dispose.Received(1).Invoke();
        }
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfInitializeIsNull() =>
            Assert.Throws<ArgumentNullException>(() => new CompactDisposable(null, () => { }));
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfDisposeIsNull() =>
            Assert.Throws<ArgumentNullException>(() => new CompactDisposable(() => { }, null));
    }
}