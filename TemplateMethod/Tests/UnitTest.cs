using System;
using Xunit;
using TemplateMethod;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void TemporaryNoKill()
        {
            var game = new TemporaryCardDamageGame(new Creature[] { new Creature(1, 2), new Creature(1, 3) });
            Assert.Equal(-1, game.Combat(1, 0));
            Assert.Equal(-1, game.Combat(1, 0));
        }

        [Fact]
        public void PermanentKill()
        {
            var game = new PermanentCardDamage(new Creature[] { new Creature(1, 2), new Creature(1, 3) });
            Assert.Equal(-1, game.Combat(1, 0));
            Assert.Equal(1, game.Combat(1, 0));
        }

        [Fact]
        public void BothKilled()
        {
            var game1 = new TemporaryCardDamageGame(new Creature[] { new Creature(2, 2), new Creature(2, 2) });
            Assert.Equal(-1, game1.Combat(0, 1));
            var game2 = new PermanentCardDamage(new Creature[] { new Creature(2, 2), new Creature(2, 2) });
            Assert.Equal(-1, game2.Combat(1, 0));
        }
    }
}
