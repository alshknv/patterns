using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Mediator
{
    class Program
    {
        public class Message
        {
            public int Value { get; set; }
            public Guid Source { get; set; }

            public Message(int value, Guid source)
            {
                Value = value;
                Source = source;
            }
        }

        public class Participant
        {
            private readonly Guid guid = Guid.NewGuid();
            public int Value { get; set; }

            private readonly Mediator mediator;

            public Participant(Mediator mediator)
            {
                this.mediator = mediator;
                mediator.Subscribe(msg =>
                {
                    if (msg.Source != guid)
                    {
                        Value = msg.Value;
                    }
                });
            }

            public void Say(int n)
            {
                this.mediator.Publish(new Message(n, guid));
            }

            public override string ToString()
            {
                return $"Participant {guid}: value {Value}";
            }
        }

        public class Mediator : IObservable<Message>
        {
            private readonly List<Subscription> subscribers = new List<Subscription>();
            public IDisposable Subscribe(IObserver<Message> subscriber)
            {
                var sub = new Subscription(this, subscriber);
                if (!subscribers.Any(s => s.Subscriber == subscriber))
                {
                    subscribers.Add(sub);
                }
                return sub;
            }

            public void Unsubscribe(IObserver<Message> subscriber)
            {
                subscribers.RemoveAll(s => s.Subscriber == subscriber);
            }

            public void Publish(Message value)
            {
                foreach (var s in subscribers)
                {
                    s.Subscriber.OnNext(value);
                }
            }

            private class Subscription : IDisposable
            {
                private readonly Mediator mediator;
                public readonly IObserver<Message> Subscriber;
                public Subscription(Mediator mediator, IObserver<Message> subscriber)
                {
                    this.mediator = mediator;
                    Subscriber = subscriber;
                }
                public void Dispose()
                {
                    mediator.Unsubscribe(Subscriber);
                }
            }
        }
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
