using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPattern
{

    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;

        public Command(Action action, int amount)
        {
            TheAction = action;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{TheAction} {Amount}$: " + (Success ? "Success" : "Failure");
        }
    }

    public class Account
    {
        public int Balance { get; set; }

        public List<Command> CommandHistory = new List<Command>();

        public void Process(Command c)
        {
            c.Success = false;
            switch (c.TheAction)
            {
                case Command.Action.Deposit:
                    Balance += c.Amount;
                    c.Success = true;
                    break;
                case Command.Action.Withdraw:
                    if (Balance - c.Amount >= 0)
                    {
                        Balance -= c.Amount;
                        c.Success = true;
                    }
                    break;
            }
            var log = new StringBuilder();
            log.Append(c);
            log.Append(", balance: ").Append(Balance).Append('$');
            Console.WriteLine(log);
            CommandHistory.Add(c);
        }

        public void UndoLast()
        {
            var cmd = CommandHistory.Last();
            if (cmd.Success)
            {
                switch (cmd.TheAction)
                {
                    case Command.Action.Deposit:
                        Balance -= cmd.Amount;
                        break;
                    case Command.Action.Withdraw:
                        Balance += cmd.Amount;
                        break;
                }
            }
            var log = new StringBuilder();
            log.Append("Undo ").Append(cmd);
            if (!cmd.Success)
            {
                log.Append(", no actual action");
            }
            log.Append(", balance: ").Append(Balance).Append('$');
            Console.WriteLine(log);
            CommandHistory.Remove(cmd);
        }

        public override string ToString()
        {
            return $"Balance: {Balance}$";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Account();

        }
    }
}
