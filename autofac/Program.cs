using System;
using Autofac;

namespace autofacDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultLogger>().As<ILogger>();
            builder.RegisterType<Context>();
            builder.RegisterType<User>().PropertiesAutowired();
            var container = builder.Build();
            container.Resolve<User>().Print();

            using var scope = container.BeginLifetimeScope("Scoped", builder =>
            {
                builder.RegisterInstance(new Context() { Name = "Scoped" });
            });
            scope.Resolve<User>().Print();
        }
    }
}
