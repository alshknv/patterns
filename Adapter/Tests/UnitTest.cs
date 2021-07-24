using System;
using Xunit;
using Adapter;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void Rectangle()
        {
            var square = new Square(5);
            IRectangle rect = new SquareToRectangleAdapter(square);
            Assert.Equal(5, rect.Width);
            Assert.Equal(5, rect.Height);
            Assert.Equal(25, rect.Area());
        }
    }
}
