using System;
using Xunit;
using Iterator;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void PreOrder()
        {
            var node1 = new Node<string>("0",
            new Node<string>("0l",
                new Node<string>("0l1l"), new Node<string>("0l1r")),
            new Node<string>("0r",
                new Node<string>("0r1l"), new Node<string>("0r1r")));

            Assert.Collection(node1.PreOrder,
                node1 => Assert.Equal("0", node1),
                node2 => Assert.Equal("0l", node2),
                node3 => Assert.Equal("0l1l", node3),
                node4 => Assert.Equal("0l1r", node4),
                node5 => Assert.Equal("0r", node5),
                node6 => Assert.Equal("0r1l", node6),
                node7 => Assert.Equal("0r1r", node7)
            );
        }
    }
}
