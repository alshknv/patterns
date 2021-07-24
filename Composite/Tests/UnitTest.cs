using System.Collections.Generic;
using System;
using Xunit;
using Composite;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void SingleValue()
        {
            IValueContainer value = new SingleValue() { Value = 10 };
            Assert.Equal(10, value.Sum());
        }

        [Fact]
        public void MultipleValue()
        {
            IValueContainer value = new ManyValues() { 20, 30 };
            Assert.Equal(50, value.Sum());
        }

        [Fact]
        public void SingleAndMultiple()
        {
            var containers = new List<IValueContainer>
            {
                new SingleValue() { Value = 10 },
                new ManyValues() { 20, 30 }
            };
            Assert.Equal(60, containers.Sum());
        }
    }
}
