using System;
using Autofac;

namespace Singleton
{
    class Program
    {
        public class SingletonTester
        {
            public static bool IsSingleton(Func<object> func)
            {
                return func().Equals(func());
            }
        }

        public class GuidObject
        {
            public Guid Id { get; set; }

            public GuidObject()
            {
                Id = Guid.NewGuid();
            }
        }

        // lazy initialized singleton1 with static Instance
        public class SingletonObject1 : GuidObject
        {
            public SingletonObject1() : base()
            {
            }

            private static readonly Lazy<SingletonObject1> instance =
                new Lazy<SingletonObject1>(
                    () => new SingletonObject1()
                );

            public static SingletonObject1 Instance => instance.Value;
        }

        //singleton2
        public class SingletonObject2 : GuidObject
        {
            public SingletonObject2() : base()
            {
            }
        }

        //singleton3
        public class SingletonObject3 : GuidObject
        {
            public SingletonObject3() : base()
            {
            }
        }

        static void Main(string[] args)
        {
            //autofac container builder
            var cb = new ContainerBuilder();
            //singleton2 - single instance
            cb.RegisterType<SingletonObject2>()
                .SingleInstance();
            //singleton3 - multiple instances
            cb.RegisterType<SingletonObject3>();


            IContainer container = cb.Build();

            //prints False - not a singleton
            Console.WriteLine(
                SingletonTester.IsSingleton(() =>
                    new object()
                ).ToString()
            );

            //prints True - singleton
            Console.WriteLine(
                SingletonTester.IsSingleton(() =>
                    SingletonObject1.Instance
                ).ToString()
            );

            //prints True - singleton
            Console.WriteLine(
                SingletonTester.IsSingleton(() =>
                    container.Resolve<SingletonObject2>()
                ).ToString()
            );

            //prints False - not a singleton
            Console.WriteLine(
                SingletonTester.IsSingleton(() =>
                    container.Resolve<SingletonObject3>()
                ).ToString()
            );
        }
    }
}
