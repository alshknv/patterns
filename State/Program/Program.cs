using System;
using System.Linq;

namespace Program
{
    public static class Extensions
    {
        public static bool In(this object obj, params string[] list)
        {
            return list.Contains(obj);
        }
    }
    public class CombinationLock
    {
        private readonly int[] combination;
        public CombinationLock(int[] combination)
        {
            this.Status = "LOCKED";
            this.combination = combination;
        }

        // you need to be changing this on user input
        public string Status;

        public bool EnterDigit(int digit)
        {
            if (Status.In("OPEN", "ERROR"))
                return false;
            if (Status == "LOCKED")
            {
                Status = string.Empty;
            }
            if (combination[Status.Length] != digit)
            {
                Status = "ERROR";
            }
            else
            {
                Status += digit.ToString();
            }
            if (Status.Length == combination.Length)
            {
                Status = "OPEN";
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
