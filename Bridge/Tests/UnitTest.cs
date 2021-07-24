using System;
using Xunit;
using Bridge;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void VectorTriangle()
        {
            var renderer = new VectorRenderer();
            var triangle = new Triangle(renderer);
            Assert.Matches(".*Triangle.*lines", triangle.ToString());
        }

        [Fact]
        public void RasterSquare()
        {
            var renderer = new RasterRenderer();
            var triangle = new Square(renderer);
            Assert.Matches(".*Square.*pixels", triangle.ToString());
        }
    }
}
