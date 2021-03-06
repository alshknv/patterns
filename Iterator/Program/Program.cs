using System.Collections.Generic;
using System;

namespace Iterator
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            if (left != null)
                left.Parent = this;
            if (right != null)
                right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                yield return Value;
                if (Left != null)
                {
                    foreach (var node in Left?.PreOrder)
                        yield return node;
                }
                if (Right != null)
                {
                    foreach (var node in Right?.PreOrder)
                        yield return node;
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var node1 = new Node<string>("0",
            new Node<string>("0l",
                new Node<string>("0l1l"), null), null);
            Console.WriteLine(string.Join(", ", node1.PreOrder));
        }
    }
}
