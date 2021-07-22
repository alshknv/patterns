using System.Dynamic;
using System;
using ImpromptuInterface;

namespace Program
{
    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    // Null log class to avoid any exceptions
    public class NullLog : ILog
    {
        public int RecordCount { get; set; } = 0;
        public int RecordLimit
        {
            get => RecordCount + 1;
            set { }
        }
        public void LogInfo(string message)
        {
            RecordCount++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILog log = new NullLog()
            {
                RecordLimit = 2
            };

            var acc = new Account(log);
            for (var i = 0; i < 5; i++)
            {
                acc.SomeOperation();
            }
        }
    }
}
