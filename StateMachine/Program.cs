using System;

namespace StateMachine
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
           var app =new AppStateMachine();
            app.InstallApp();
            await app.StartApp();
            app.StopApp();
            app.UninstallApp();
        }
    }
}
