using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Observer;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SimpleTwoRats()
        {
            var game = new Game();
            List<Rat> rats = new List<Rat>() {
                new Rat(game),
                new Rat(game)
            };
            Assert.Collection(rats,
                rat1 => Assert.Equal(2, rat1.Attack),
                rat2 => Assert.Equal(2, rat2.Attack));
        }

        [Fact]
        public void ThreRatsThenTwoGone()
        {
            var game = new Game();
            List<Rat> rats = new List<Rat>() {
                new Rat(game),
                new Rat(game),
                new Rat(game)
            };
            Assert.Collection(rats,
                rat1 => Assert.Equal(3, rat1.Attack),
                rat2 => Assert.Equal(3, rat2.Attack),
                rat3 => Assert.Equal(3, rat3.Attack));
            rats[1].Dispose();
            rats[2].Dispose();
            rats.RemoveRange(1, 2);
            Assert.Collection(rats,
                rat1 => Assert.Equal(1, rat1.Attack));
        }

    }
}
