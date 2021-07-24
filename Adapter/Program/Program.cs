using System.Drawing;
using System;

namespace Adapter
{
    public class Square
    {
        public int Side;

        public Square(int side)
        {
            Side = side;
        }
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public SquareToRectangleAdapter(Square square)
        {
            Width = Height = square.Side;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sq = new Square(5);
            var adapter = new SquareToRectangleAdapter(sq);
            Console.WriteLine($"IRectangle width: {adapter.Width}, height: {adapter.Height}");
        }
    }
}
