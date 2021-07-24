using System.Text;
using System;

namespace Decorator
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon
    {
        private readonly Bird bird;
        private readonly Lizard lizard;

        public Dragon(int age)
        {
            this.bird = new Bird();
            this.lizard = new Lizard();
            Age = age;
        }

        public int Age
        {
            get => bird.Age;
            set => bird.Age = lizard.Age = value;
        }

        public string Fly()
        {
            return bird.Fly();
        }

        public string Crawl()
        {
            return lizard.Crawl();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dragon = new Dragon(11);
            var sb = new StringBuilder();
            sb.AppendLine(dragon.Fly());
            sb.AppendLine(dragon.Crawl());
            Console.WriteLine(sb.ToString());
        }
    }
}
