using System;
using Xunit;
using Decorator;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void TooYoungToCrawl()
        {
            var dragon = new Dragon(0);
            Assert.Equal("too young", dragon.Crawl());
            Assert.Equal("flying", dragon.Fly());
        }

        [Fact]
        public void CrawlingAndFlying()
        {
            var dragon = new Dragon(5);
            Assert.Equal("crawling", dragon.Crawl());
            Assert.Equal("flying", dragon.Fly());
        }

        [Fact]
        public void TooOldToFly()
        {
            var dragon = new Dragon(11);
            Assert.Equal("crawling", dragon.Crawl());
            Assert.Equal("too old", dragon.Fly());
        }
    }
}
