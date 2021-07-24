using System;
using Xunit;
using Interpreter;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void SimpleExpression()
        {
            var ep = new ExpressionProcessor();
            Assert.Equal(5, ep.Calculate("10-5"));
        }

        [Fact]
        public void Variable()
        {
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 3);
            Assert.Equal(9, ep.Calculate("10+2-x"));
        }

        [Fact]
        public void UnknownVariable()
        {
            var ep = new ExpressionProcessor();
            Assert.Equal(0, ep.Calculate("5-4+z"));
        }

        [Fact]
        public void InvalidVariable()
        {
            var ep = new ExpressionProcessor();
            Assert.Equal(0, ep.Calculate("1+2+xy"));
        }

        [Fact]
        public void Parenthesis()
        {
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 3);
            ep.Variables.Add('y', 5);
            Assert.Equal(12, ep.Calculate("10-(x-y)"));
        }

        [Fact]
        public void NestedParenthesis()
        {
            var ep = new ExpressionProcessor();
            Assert.Equal(5, ep.Calculate("12-(1+4-(2-1))-3"));
        }

        [Fact]
        public void ExpressionWithWhitespaces()
        {
            var ep = new ExpressionProcessor();
            Assert.Equal(5, ep.Calculate("12 - (1+ 4-( 2-1 ) ) -3"));
        }
    }
}
