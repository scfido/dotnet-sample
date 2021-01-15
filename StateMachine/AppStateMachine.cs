using System;
using System.Threading.Tasks;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;

namespace StateMachine
{
    public class AppStateMachine
    {
        private enum States
        {
            PendingInstall,
            PendingUninstall,
            Stoped,
            Running,
        }

        private enum Events
        {
            Install,
            Uninstall,
            Start,
            Stop,
        }

        private readonly AsyncPassiveStateMachine<States, Events> app;

        public AppStateMachine()
        {
            var builder = new StateMachineDefinitionBuilder<States, Events>();


            //builder
            //    .In(States.PendingInstall)
            //    .On(Events.Install)
            //    .Goto(States.Stoped)
            //    .Execute(Install);


            builder
                .In(States.PendingInstall)
                .On(Events.Install)
                .Goto(States.Stoped)
                .Execute(Install);

            builder
                .In(States.PendingInstall)
                .On(Events.Uninstall)
                .Goto(States.PendingUninstall)
                .Execute(Uninstall);

            builder
               .In(States.Stoped)
               .On(Events.Start)
               .Goto(States.Running)
               .Execute(Start)
               ;

            builder
               .In(States.Stoped)
               .On(Events.Uninstall)
               .Goto(States.PendingUninstall)
               .Execute(Uninstall);

            builder
               .In(States.Running)
               .On(Events.Stop)
               .Goto(States.Stoped)
               .Execute(Stop);

            builder
                .WithInitialState(States.PendingInstall);

            app = builder
                .Build()
                .CreatePassiveStateMachine();

            app.Start();
        }

        public void InstallApp()
        {
            
            app.Fire(Events.Install);
        }

        public void UninstallApp()
        {
            app.Fire(Events.Uninstall);
        }

        public async Task StartApp()
        {
            await app.Fire(Events.Start);
        }

        public void StopApp()
        {
            app.Fire(Events.Stop);
        }

        private void Install()
        {
            Console.WriteLine("安装APP");
        }

        private void Uninstall()
        {
            Console.WriteLine("卸载APP");
        }


        private Task Start()
        {
            Console.WriteLine("启动APP");
            return Task.FromException(new Exception("Start Error"));
        }

        private void Stop()
        {
            Console.WriteLine("停止APP");
        }
    }
}
