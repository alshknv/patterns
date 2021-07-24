using System;
using Xunit;
using CommandPattern;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void Deposit()
        {
            var a = new Account();
            a.Process(new Command(Command.Action.Deposit, 100));
            Assert.Equal(100, a.Balance);
        }

        [Fact]
        public void DepositAndWithdraw()
        {
            var a = new Account();
            a.Process(new Command(Command.Action.Deposit, 100));
            a.Process(new Command(Command.Action.Withdraw, 80));
            Assert.Equal(20, a.Balance);
        }

        [Fact]
        public void DepositAndRollback()
        {
            var a = new Account();
            a.Process(new Command(Command.Action.Deposit, 100));
            a.Process(new Command(Command.Action.Deposit, 20));
            a.UndoLast();
            Assert.Equal(100, a.Balance);
        }

        [Fact]
        public void ImpossibleWithdraw()
        {
            var a = new Account();
            a.Process(new Command(Command.Action.Deposit, 100));
            a.Process(new Command(Command.Action.Withdraw, 200));
            Assert.Equal(100, a.Balance);
        }

        [Fact]
        public void ComplexExample()
        {
            var a = new Account();
            a.Process(new Command(Command.Action.Deposit, 100));
            a.Process(new Command(Command.Action.Withdraw, 200));
            a.Process(new Command(Command.Action.Withdraw, 20));
            a.Process(new Command(Command.Action.Withdraw, 10));
            Assert.Equal(70, a.Balance);
            for (int i = 0; i < 3; ++i)
                a.UndoLast();
            Assert.Equal(100, a.Balance);
            a.Process(new Command(Command.Action.Withdraw, 40));
            Assert.Equal(60, a.Balance);
            a.UndoLast();
            Assert.Equal(100, a.Balance);
        }
    }
}
