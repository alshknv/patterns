using System.Numerics;
using System;
using Xunit;
using Program;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TwoRealRoots()
        {
            var solver = new QuadraticEquationSolver(QuadraticEquationSolver.RealDiscriminantStrategy);
            var result = solver.Solve(0.5, 0.125, 0);
            Assert.Equal(0, result.Item1.Real);
            Assert.Equal(-0.25, result.Item2.Real);
        }

        [Fact]
        public void OneRealRoot()
        {
            var solver = new QuadraticEquationSolver(QuadraticEquationSolver.RealDiscriminantStrategy);
            var result = solver.Solve(-4, 28, -49);
            Assert.Equal(3.5, result.Item1.Real);
            Assert.Equal(3.5, result.Item2.Real);
        }

        [Fact]
        public void NoRealRoots()
        {
            var solver = new QuadraticEquationSolver(QuadraticEquationSolver.RealDiscriminantStrategy);
            var result = solver.Solve(1, 2, 5);
            Assert.Equal(double.NaN, result.Item1.Real);
            Assert.Equal(double.NaN, result.Item2.Real);
        }

        [Fact]
        public void ComplexRoots()
        {
            var solver = new QuadraticEquationSolver(QuadraticEquationSolver.OrdinaryDiscriminantStrategy);
            var result = solver.Solve(1, 2, 5);
            Assert.Equal(new Complex(-1, 2), result.Item1);
            Assert.Equal(new Complex(-1, -2), result.Item2);
        }
    }
}
