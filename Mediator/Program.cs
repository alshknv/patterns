using System.Collections.Generic;
using System;
using System.Linq;

namespace Mediator
{
    public class Participant
    {
        private readonly Mediator mediator;
        public Guid Guid = Guid.NewGuid();

        public int Value { get; set; }

        public Participant(Mediator mediator)
        {
            this.mediator = mediator;
            this.mediator.RegisterParticipant(this);
        }

        public void Say(int n)
        {
            mediator.Publish(Guid, n);
        }

        public void Receive(int n)
        {
            Value = n;
        }

        public override string ToString()
        {
            return $"Participant {Guid}, value {Value}";
        }
    }

    public class Mediator
    {
        private readonly List<Participant> participants = new List<Participant>();

        public void RegisterParticipant(Participant participant)
        {
            if (!participants.Any(p => p.Guid == participant.Guid))
            {
                participants.Add(participant);
            }
        }

        public void Publish(Guid guid, int value)
        {
            foreach (var p in participants.Where(p => p.Guid != guid))
            {
                p.Receive(value);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mediator = new Mediator();
            var part1 = new Participant(mediator);
            var part2 = new Participant(mediator);
            part1.Say(3);
            part2.Say(2);
            Console.WriteLine(part1);
            Console.WriteLine(part2);
        }
    }
}
