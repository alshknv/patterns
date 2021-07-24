using System;

namespace Proxy
{
    public class Person
    {
        public int Age { get; set; }

        public Person(int age)
        {
            Age = age;
        }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private readonly Person person;

        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get
            {
                return person.Age;
            }
            set
            {
                person.Age = value;
            }
        }

        public string Drink()
        {
            return Age >= 18 ?
                person.Drink() :
                "too young";
        }

        public string Drive()
        {
            return Age >= 16 ?
                person.Drive() :
                "too young";
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(17);
            var responsible = new ResponsiblePerson(person);
            Console.WriteLine($"trying to drink: {responsible.Drink()}");
            Console.WriteLine($"trying to drive: {responsible.Drive()}");
            Console.WriteLine($"trying to drink and drive: {responsible.DrinkAndDrive()}");
        }
    }
}
