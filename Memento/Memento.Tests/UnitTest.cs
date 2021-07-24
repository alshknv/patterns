using System;
using Xunit;

namespace Memento.Tests
{
    public class UnitTest
    {
        [Fact]
        public void SimpleTest()
        {
            var tm = new TokenMachine();
            var m = tm.AddToken(123);
            tm.AddToken(456);
            tm.Revert(m);
            Assert.Collection(tm.Tokens, t1 => Assert.Equal(123, t1.Value));
        }

        [Fact]
        public void TwoTokenTest()
        {
            var tm = new TokenMachine();
            tm.AddToken(1);
            var m = tm.AddToken(2);
            tm.AddToken(3);
            tm.Revert(m);
            Assert.Collection(tm.Tokens,
                t1 => Assert.Equal(1, t1.Value),
                t2 => Assert.Equal(2, t2.Value));
        }

        [Fact]
        public void ComplexTest()
        {
            var tm = new TokenMachine();
            var m1 = tm.AddToken(1);
            var token2 = new Token(10);
            var m2 = tm.AddToken(token2);
            var m3 = tm.AddToken(20);
            Assert.Collection(tm.Tokens,
                t1 => Assert.Equal(1, t1.Value),
                t2 => Assert.Equal(10, t2.Value),
                t3 => Assert.Equal(20, t3.Value));
            token2.Value = 5;
            Assert.Collection(tm.Tokens,
                t1 => Assert.Equal(1, t1.Value),
                t2 => Assert.Equal(5, t2.Value),
                t3 => Assert.Equal(20, t3.Value));
            tm.Revert(m2);
            Assert.Collection(tm.Tokens,
                t1 => Assert.Equal(1, t1.Value),
                t2 => Assert.Equal(5, t2.Value));
            tm.Revert(m1);
            Assert.Collection(tm.Tokens,
                t1 => Assert.Equal(1, t1.Value));
        }
    }
}
