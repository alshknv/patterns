using System;
using Xunit;
using Proxy;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void TooYoungToDrinkAndDrive()
        {
            var person = new Person(14);
            var responsible = new ResponsiblePerson(person);
            Assert.Equal("too young", responsible.Drink());
            Assert.Equal("too young", responsible.Drive());
            Assert.Equal("dead", responsible.DrinkAndDrive());
        }

        [Fact]
        public void TooYoungToDrink()
        {
            var person = new Person(17);
            var responsible = new ResponsiblePerson(person);
            Assert.Equal("too young", responsible.Drink());
            Assert.Equal("driving", responsible.Drive());
            Assert.Equal("dead", responsible.DrinkAndDrive());
        }

        [Fact]
        public void AbleDrinkAndDrive()
        {
            var person = new Person(20);
            var responsible = new ResponsiblePerson(person);
            Assert.Equal("drinking", responsible.Drink());
            Assert.Equal("driving", responsible.Drive());
            Assert.Equal("dead", responsible.DrinkAndDrive());
        }
    }
}
