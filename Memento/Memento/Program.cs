using System;
using System.Collections.Generic;

namespace Memento
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        private readonly List<Token> tokens;
        public Memento(List<Token> tokens)
        {
            this.tokens = new List<Token>();
            foreach (var token in tokens)
                this.tokens.Add(token);
        }

        public List<Token> GetState()
        {
            return tokens;
        }
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            return AddToken(new Token(value));
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            return new Memento(Tokens);
        }

        public void Revert(Memento m)
        {
            Tokens = m.GetState();
        }

        public override string ToString()
        {
            return string.Join(",", Tokens.ConvertAll(x => x.Value));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var tm = new TokenMachine();
            var token = new Token(111);
            tm.AddToken(token);
            var m = tm.AddToken(222);
            token.Value = 333;
            tm.Revert(m);
            Console.WriteLine(tm); // got 333,222
        }
    }
}
