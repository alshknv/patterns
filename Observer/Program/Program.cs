using System;

namespace Observer
{
    public class Game
    {
        // remember - no fields or properties!
        public event EventHandler RatNumberChanged; // number changed - each rat should report back
        public event EventHandler RatCountBegan; // count began - each rat should reset its attack to 0
        public event EventHandler RatReported; // report from some rat - each rat should increase its attack by 1

        public void ChangeRatNumber()
        {
            // since events are syncronous, we can be sure RatCountBegan events
            // will be processed by every rat before RatNumberChanged event fires
            RatCountBegan?.Invoke(this, EventArgs.Empty);
            RatNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RatReport()
        {
            RatReported?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Rat : IDisposable
    {
        private readonly Guid Id;
        private readonly Game game;
        public int Attack = 1;

        public Rat(Game game)
        {
            this.game = game;
            Id = Guid.NewGuid();
            Console.WriteLine($"Rat {Id} created");

            game.RatNumberChanged += OnRatNumberChanged;
            game.RatCountBegan += OnRatCountBegan;
            game.RatReported += OnRatReported;

            game.ChangeRatNumber();
        }

        public void OnRatNumberChanged(object sender, EventArgs args)
        {
            Console.WriteLine($"Rat {Id} received number changed, reports presence");
            game.RatReport();
        }

        public void OnRatCountBegan(object sender, EventArgs args)
        {
            Attack = 0;
            Console.WriteLine($"Rat {Id} received count began, reset attack to {Attack}");
        }

        public void OnRatReported(object sender, EventArgs args)
        {
            Attack++;
            Console.WriteLine($"Rat {Id} received another rat's report, set attack to {Attack}");
        }

        public void Dispose()
        {
            game.RatNumberChanged -= OnRatNumberChanged;
            game.RatCountBegan -= OnRatCountBegan;
            game.RatReported -= OnRatReported;
            game.ChangeRatNumber();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //tests are used to check exercise
            var game = new Game();
            var rat1 = new Rat(game);
            new Rat(game);
            Console.WriteLine($"Rat1 attack is {rat1.Attack}");
        }
    }
}
