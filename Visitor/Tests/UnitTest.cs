using System;
using Xunit;
using Visitor;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void SimpleAddition()
        {
            var exp = new AdditionExpression(
                new Value(1), new Value(1)
            );
            var expPrinter = new ExpressionPrinter();
            expPrinter.Visit(exp);
            Assert.Equal("(1+1)", expPrinter.ToString());
        }

        [Fact]
        public void AdditionOfMultiplications()
        {
            var exp = new AdditionExpression(
                new MultiplicationExpression(
                    new Value(3), new Value(7)
                ), new MultiplicationExpression(
                    new Value(2), new Value(3)
                )
            );
            var expPrinter = new ExpressionPrinter();
            expPrinter.Visit(exp);
            Assert.Equal("(3*7+2*3)", expPrinter.ToString());
        }

        [Fact]
        public void MultiplicationOfAdditions()
        {
            var exp = new MultiplicationExpression(
                new AdditionExpression(
                    new Value(3), new Value(7)
                ), new AdditionExpression(
                    new Value(2), new Value(3)
                )
            );
            var expPrinter = new ExpressionPrinter();
            expPrinter.Visit(exp);
            Assert.Equal("(3+7)*(2+3)", expPrinter.ToString());
        }
    }
}
