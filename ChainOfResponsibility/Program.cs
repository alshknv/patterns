using System.Reflection;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainOfResponsibility
{
    /* A kind of card game example
        Goblin gains +1 defence for every other goblin in game (including GoblinKing)
        Each goblin gains +1 attak if GoblinKing is in game
    */

    public abstract class Creature
    {
        private int attack;
        private int defence;
        public int Attack
        {
            get
            {
                return this.HandleModifiers<AttackModifier>(attack);
            }
            set
            {
                this.attack = value;
            }
        }
        public int Defence
        {
            get
            {
                return this.HandleModifiers<DefenceModifier>(defence);
            }
            set
            {
                this.defence = value;
            }
        }

        private readonly CreatureModifier modifier = new CreatureModifier(); // root basic modifier

        private int HandleModifiers<T>(int initialValue)
            where T : CreatureModifier
        {
            var value = initialValue;
            this.modifier.Handle<T>(ref value);
            return value;
        }

        public void AddModifier(CreatureModifier modifier)
        {
            this.modifier.Add(modifier);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: attack {Attack}, defence {Defence}";
        }
    }

    public class Goblin : Creature
    {
        public Goblin(Game game)
        {
            Attack = Defence = 1;
            foreach (var creature in game.Creatures.Where(c =>
                !c.Equals(this) &&
                c.GetType() == typeof(Goblin)))
            {
                creature.AddModifier(new DefenceModifier(1)); // + defence for others
                if (this.GetType() == typeof(Goblin))
                {
                    this.AddModifier(new DefenceModifier(1)); // + defence for self
                }
            }
            game.Creatures.Add(this);
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack = Defence = 3;
            foreach (var creature in game.Creatures.Where(c =>
                !c.Equals(this) &&
                c.GetType() == typeof(Goblin)))
            {
                creature.AddModifier(new AttackModifier(1)); // + attack for others only
            }
        }
    }

    public class CreatureModifier
    {
        protected int Value;
        public CreatureModifier Next;
        public CreatureModifier() { }
        public CreatureModifier(int value)
        {
            Value = value;
        }

        public void Add(CreatureModifier modifier)
        {
            if (Next == null)
            {
                Next = modifier;
            }
            else
            {
                Next.Add(modifier);
            }
        }

        public virtual void Handle<T>(ref int value)
            where T : CreatureModifier
        {
            Next?.Handle<T>(ref value);
        }
    }

    public class AttackModifier : CreatureModifier
    {
        public AttackModifier(int value) : base(value) { }
        public override void Handle<T>(ref int value)
        {
            if (typeof(T) == typeof(AttackModifier))
            {
                value += Value;
            }
            base.Handle<T>(ref value);
        }
    }

    public class DefenceModifier : CreatureModifier
    {
        public DefenceModifier(int value) : base(value) { }
        public override void Handle<T>(ref int value)
        {
            if (typeof(T) == typeof(DefenceModifier))
            {
                value += Value;
            }
            base.Handle<T>(ref value);
        }
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin1 = new Goblin(game);
            var goblin2 = new Goblin(game);
            var goblin3 = new Goblin(game);
            var king = new GoblinKing(game);

            Console.WriteLine($"Goblin1 - {goblin1}");
            Console.WriteLine($"Goblin2 - {goblin2}");
            Console.WriteLine($"Goblin3 - {goblin3}");
            Console.WriteLine($"GoblinKing - {king}");

            Console.WriteLine($"Total {game.Creatures.Count} creatures in game");
        }
    }
}
