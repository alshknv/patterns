using System;
using Xunit;
using Builder;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void BuildClassPerson()
        {
            var builder = new CodeBuilder("Person")
                .AddField("Age", "int")
                .AddField("Name", "string");
            Assert.Collection(builder.ToString().Split('\n', StringSplitOptions.RemoveEmptyEntries),
                l1 => Assert.Equal("public class Person", l1),
                l2 => Assert.Equal("{", l2),
                l3 => Assert.Equal("  public int Age;", l3),
                l4 => Assert.Equal("  public string Name;", l4),
                l5 => Assert.Equal("}", l5)
            );
        }
    }
}
