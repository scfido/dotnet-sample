using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunTasks();
            Console.ReadKey();
        }


        void RunTasks()
        {
            var source = new CancellationTokenSource(3000);
            var task = Task.Run(() => Do(source.Token));
           
            task.Wait();
        }



        private async Task Do(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("Action");
                await Task.Delay(1000, token);
            }
        }
    }
}
