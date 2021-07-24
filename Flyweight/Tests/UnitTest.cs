using System;
using Xunit;
using Flyweight;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void CapitalizeWord()
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Assert.Equal("hello WORLD", sentence.ToString());
        }

        [Fact]
        public void WordStorage()
        {
            var sentence = new Sentence("hello world hello world");
            Assert.Equal(2, sentence.WordsStored);
        }
    }
}
