using System.Collections.Generic;
using System;
using Xunit;
using ChainOfResponsibility;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void ThreeGoblinsWithKing()
        {
            var game = new Game();
            List<Goblin> goblins = new List<Goblin>();
            for (int i = 0; i < 3; i++)
            {
                goblins.Add(new Goblin(game));
            }
            var gk = new GoblinKing(game);
            Assert.Collection(goblins,
                g1 => { Assert.Equal(2, g1.Attack); Assert.Equal(4, g1.Defence); },
                g2 => { Assert.Equal(2, g2.Attack); Assert.Equal(4, g2.Defence); },
                g3 => { Assert.Equal(2, g3.Attack); Assert.Equal(4, g3.Defence); }
            );
            Assert.Equal(3, gk.Attack);
            Assert.Equal(3, gk.Defence);
        }
    }
}
