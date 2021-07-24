using System.Linq;
using System;
using System.Collections.Generic;

namespace Flyweight
{
    public class Sentence
    {
        private readonly List<string> words = new List<string>();
        private readonly List<(int, WordToken)> content = new List<(int, WordToken)>();

        public Sentence(string plainText)
        {
            foreach (var word in plainText.Split(' '))
            {
                var idx = words.IndexOf(word);
                if (idx < 0)
                {
                    words.Add(word);
                    idx = words.Count - 1;
                }
                content.Add((idx, new WordToken()));
            }
        }

        public int WordsStored
        {
            get => words.Count;
        }

        public WordToken this[int index]
        {
            get
            {
                return content[index].Item2;
            }
        }

        public override string ToString()
        {
            return string.Join(
                " ",
                content.Select(i =>
                {
                    var word = words[i.Item1];
                    return i.Item2.Capitalize ? word.ToUpperInvariant() : word;
                })
            );
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence);
        }
    }
}
