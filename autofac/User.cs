using System;
using Autofac;

namespace autofacDemo
{
  class User
  {
    public User(ILifetimeScope scope, Context context)
    {
      Console.WriteLine($"Name: {context.Name}");
      Console.WriteLine($"Scope.Tag: {scope.Tag}");
    }

    public string Name { get; set; }
  }
}