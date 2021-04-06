using System;
using Autofac;

namespace autofacDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<Context>();
      builder.RegisterType<User>();
      var container = builder.Build();
      container.Resolve<User>();

      using var scope = container.BeginLifetimeScope("Scoped", builder =>
      {
        builder.RegisterInstance(new Context() { Name = "Scoped" });
      });
      scope.Resolve<User>();
    }
  }
}
