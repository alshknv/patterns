using System.Net.Mail;
using System.Collections.Generic;
using System;
using Xunit;
using Prototype;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void CopiedLine()
        {
            var line1 = new Line(0, 0, 5, 5);
            var line2 = line1.DeepCopy();
            line2.Start.X = 1;
            line2.Start.Y = 1;
            line2.End.X = 2;
            line2.End.Y = 2;

            Assert.Equal(new Point(1, 1), line2.Start);
            Assert.Equal(new Point(2, 2), line2.End);
            Assert.Equal(new Line(0, 0, 5, 5), line1);
        }
    }
}
