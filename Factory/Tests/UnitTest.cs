using System.Collections.Generic;
using System;
using Xunit;
using Factory;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void CreatingPersons()
        {
            var factory = new PersonFactory();
            for (var i = 0; i < 5; i++)
            {
                var person = factory.CreatePerson($"PersonName_{i}");
                Assert.Equal(i, person.Id);
                Assert.Equal($"PersonName_{i}", person.Name);
            }
        }
    }
}
