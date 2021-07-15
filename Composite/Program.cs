﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Composite
{
    public interface IValueContainer : IEnumerable<int>
    {
    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var containers = new List<IValueContainer>();
            containers.Add(new SingleValue() { Value = 10 });
            containers.Add(new ManyValues() { 20, 30 });

            Console.WriteLine(containers.Sum());
        }
    }
}
