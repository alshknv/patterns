using System.Collections.Generic;
using System;
using System.Linq;
using Xunit;
using Facade;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void MagicSquareTest()
        {
            var generator = new MagicSquareGenerator();
            var square = generator.Generate(3);
            Assert.Collection(square,
                l1 => Assert.Equal(3, l1.Count),
                l2 => Assert.Equal(3, l2.Count),
                l3 => Assert.Equal(3, l3.Count)
            );
            var sum = square[0].Sum();
            Assert.True(square.All(row => row.Sum() == sum));

            var cols = new List<List<int>>() {
                square.ConvertAll(row => row[0]),
                square.ConvertAll(row => row[1]),
                square.ConvertAll(row => row[2])
            };
            Assert.True(cols.All(col => col.Sum() == sum));

            var diags = new List<List<int>>() {
                new List<int>(),
                new List<int>()
            };
            for (int i = 0; i < 3; i++)
            {
                diags[0].Add(square[i][i]);
                diags[1].Add(square[2 - i][i]);
            }
            Assert.True(diags.All(diag => diag.Sum() == sum));
        }
    }
}
