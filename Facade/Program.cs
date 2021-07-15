using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facade
{
    # region Unchangeable part of exercise

    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    #endregion

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            var generator = new Generator();
            var splitter = new Splitter();
            var verifier = new Verifier();

            var square = new List<List<int>>();
            do
            {
                square.Clear();
                for (var i = 0; i < size; i++)
                {
                    square.Add(generator.Generate(size));
                }
            } while (!verifier.Verify(splitter.Split(square)));
            return square;
        }
    }

    public static class ExtensionMethods
    {
        public static void WriteSquare(this List<List<int>> square)
        {
            Console.WriteLine(new string('-', square.Count * 4 + 1));
            foreach (var row in square)
            {
                Console.WriteLine($"| {string.Join(" | ", row)} |");
                Console.WriteLine(new string('-', square.Count * 4 + 1));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var magicGenerator = new MagicSquareGenerator();
            var magicSquare = magicGenerator.Generate(3);
            magicSquare.WriteSquare();
        }
    }
}
