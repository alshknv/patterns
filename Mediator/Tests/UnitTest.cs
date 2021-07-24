using System.Collections.Generic;
using System;
using Xunit;
using MediatorPattern;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void Initialization()
        {
            var mediator = new Mediator();
            var participants = new List<Participant>() {
                new Participant(mediator),
                new Participant(mediator),
                new Participant(mediator)
            };
            Assert.Collection(participants,
                p1 => Assert.Equal(0, p1.Value),
                p2 => Assert.Equal(0, p2.Value),
                p3 => Assert.Equal(0, p3.Value)
            );
        }

        [Fact]
        public void Saying()
        {
            var mediator = new Mediator();
            var participants = new List<Participant>() {
                new Participant(mediator),
                new Participant(mediator),
                new Participant(mediator)
            };
            participants[0].Say(1);
            participants[1].Say(5);

            Assert.Collection(participants,
                p1 => Assert.Equal(5, p1.Value),
                p2 => Assert.Equal(1, p2.Value),
                p3 => Assert.Equal(5, p3.Value)
            );
        }

        [Fact]
        public void AddMoreParticipants()
        {
            var mediator = new Mediator();
            var participants = new List<Participant>() {
                new Participant(mediator),
                new Participant(mediator),
                new Participant(mediator)
            };
            participants[0].Say(1);
            participants[1].Say(5);

            var newParticipant = new Participant(mediator);
            participants.Add(newParticipant);
            newParticipant.Say(1);

            Assert.Collection(participants,
                p1 => Assert.Equal(1, p1.Value),
                p2 => Assert.Equal(1, p2.Value),
                p3 => Assert.Equal(1, p3.Value),
                p4 => Assert.Equal(0, p4.Value)
            );
        }
    }
}
