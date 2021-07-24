using System;
using Xunit;
using Program;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void NullLogTest()
        {
            ILog log = new NullLog()
            {
                RecordLimit = 2
            };

            var acc = new Account(log);
            // we try to null log 5 operations with limit set to 2
            for (var i = 0; i < 5; i++)
            {
                acc.SomeOperation();
            }
        }
    }
}
