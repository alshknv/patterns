using System;
using System.Diagnostics.CodeAnalysis;

namespace Prototype
{
    public class Point : IEquatable<Point>
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals([AllowNull] Point other)
        {
            return X == other?.X && Y == other?.Y;
        }
    }

    public class Line : IEquatable<Line>
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line(int startX, int startY, int endX, int endY)
        {
            Start = new Point(startX, startY);
            End = new Point(endX, endY);
        }

        public Line DeepCopy()
        {
            return new Line(Start.X, Start.Y, End.X, End.Y);
        }

        public bool Equals([AllowNull] Line other)
        {
            return Start.Equals(other?.Start) && End.Equals(other?.End);
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
            var line1 = new Line(0, 0, 5, 8);
            Console.WriteLine(line1);
        }
    }
}
