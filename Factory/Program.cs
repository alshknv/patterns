using System;

namespace Factory
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int index = 0;
        public Person CreatePerson(string name)
        {
            return new Person()
            {
                Id = index++,
                Name = name
            };
        }
    }

    public static class PersonExtensions
    {
        public static string StringInfo(this Person person)
        {
            var st = string.Join(" ", person.Id, person.Name);
            return st;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new PersonFactory();
            for (var i = 0; i < 5; i++)
            {
                var person = factory.CreatePerson($"PersonName_{i}");
                Console.WriteLine(person.StringInfo());
            }
        }
    }
}
