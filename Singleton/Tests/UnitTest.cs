using System;
using Xunit;
using Singleton;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void RegularObject()
        {
            Assert.False(
                SingletonTester.IsSingleton(() => new object()));
        }

        [Fact]
        public void SingletonObject()
        {
            Assert.True(SingletonTester.IsSingleton(() => SingletonObject1.Instance));
        }
    }
}
