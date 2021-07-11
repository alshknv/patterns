using System;

namespace Prototype
{
    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return new Line()
            {
                Start = new Point()
                {
                    X = Start.X,
                    Y = Start.Y
                },
                End = new Point()
                {
                    X = End.X,
                    Y = End.Y
                }
            };
        }

        public override string ToString()
        {
            return $"({Start.X},{Start.Y}) - ({End.X},{End.Y})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var line1 = new Line()
            {
                Start = new Point()
                {
                    X = 0,
                    Y = 0
                },
                End = new Point()
                {
                    X = 5,
                    Y = 8
                }
            };
            var line2 = line1.DeepCopy();
            line2.Start.X = 1;
            line2.Start.Y = 1;
            line2.End.X = 50;
            line2.End.Y = 80;

            Console.WriteLine(line1);
            Console.WriteLine(line2);
        }
    }
}
