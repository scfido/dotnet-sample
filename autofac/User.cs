using System;
using Autofac;

namespace autofacDemo
{
    class User
    {
        ILifetimeScope scope;
        Context context;
        public User(ILifetimeScope scope, Context context)
        {
            this.scope = scope;
            this.context = context;
        }

        public ILogger Logger { get; set; }

        public void Print()
        {
            Logger.Log($"Name: {context.Name}");
            Logger.Log($"Scope.Tag: {scope.Tag}");
        }
    }
}