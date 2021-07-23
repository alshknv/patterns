using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using Xunit;
using Program;

namespace Tests
{
    public static class Extensions
    {
        public static List<string> EnterDigits(this CombinationLock cl, Queue<int> digits)
        {
            var statusList = new List<string>();
            while (digits.TryDequeue(out int digit))
            {
                if (cl.EnterDigit(digit))
                {
                    statusList.Add(cl.Status);
                }
            }
            return statusList;
        }
    }
    public class UnitTest1
    {
        [Fact]
        public void OpenTest()
        {
            var cl = new CombinationLock(new[] { 1, 2, 3, 4 });
            Assert.Equal("LOCKED", cl.Status);
            var statusList = cl.EnterDigits(new Queue<int>(new[] { 1, 2, 3, 4 }));
            Assert.Collection(statusList,
                s1 => Assert.Equal("1", s1),
                s2 => Assert.Equal("12", s2),
                s3 => Assert.Equal("123", s3),
                s4 => Assert.Equal("OPEN", s4));
        }

        [Fact]
        public void LateErrorTest()
        {
            var cl = new CombinationLock(new[] { 1, 2, 3, 4 });
            Assert.Equal("LOCKED", cl.Status);
            var statusList = cl.EnterDigits(new Queue<int>(new[] { 1, 2, 3, 1 }));
            Assert.Collection(statusList,
                s1 => Assert.Equal("1", s1),
                s2 => Assert.Equal("12", s2),
                s3 => Assert.Equal("123", s3),
                s4 => Assert.Equal("ERROR", s4));
        }

        [Fact]
        public void EarlyErrorTest()
        {
            var cl = new CombinationLock(new[] { 1, 2, 3, 4 });
            Assert.Equal("LOCKED", cl.Status);
            var statusList = cl.EnterDigits(new Queue<int>(new[] { 1, 1, 3, 4 }));
            Assert.Collection(statusList,
                s1 => Assert.Equal("1", s1),
                s2 => Assert.Equal("ERROR", s2));
        }
    }
}
