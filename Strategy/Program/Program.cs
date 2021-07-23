using System;
using System.Numerics;

namespace Program
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public virtual double CalculateDiscriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : OrdinaryDiscriminantStrategy
    {
        public override double CalculateDiscriminant(double a, double b, double c)
        {
            var d = base.CalculateDiscriminant(a, b, c);
            if (d < 0)
            {
                return double.NaN;
            }
            return d;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        private readonly static OrdinaryDiscriminantStrategy ordinaryDiscriminantStrategy
            = new OrdinaryDiscriminantStrategy();

        private readonly static RealDiscriminantStrategy realDiscriminantStrategy
            = new RealDiscriminantStrategy();

        public static IDiscriminantStrategy OrdinaryDiscriminantStrategy
        {
            get
            {
                return ordinaryDiscriminantStrategy;
            }
        }

        public static IDiscriminantStrategy RealDiscriminantStrategy
        {
            get
            {
                return realDiscriminantStrategy;
            }
        }

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var d = strategy.CalculateDiscriminant(a, b, c);
            return new Tuple<Complex, Complex>(
                (-b + Complex.Sqrt(d)) / 2 / a, (-b - Complex.Sqrt(d)) / 2 / a
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver(QuadraticEquationSolver.RealDiscriminantStrategy);
            var result = solver.Solve(0.5, 0.125, 0);
        }
    }
}
