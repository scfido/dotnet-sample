using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionScopeAndDisposableDemo.Services
{

    public interface IOrderService
    {
        void StartTimer();
        void StopTimer();
    }


    public class DisposableOrderService : IOrderService , IDisposable
    {
        int count = 0;
        Timer timer;

        public DisposableOrderService()
        {
            timer = new Timer(3000);
            timer.Elapsed += (e, s) =>
            {
                Console.WriteLine($"Timer: {this.GetHashCode()} - {count++}");
            };

        }

        ~DisposableOrderService()
        {
            Console.WriteLine($"~DisposableOrderService: {this.GetHashCode()}");
        }

        public void StartTimer()
        {
            timer.Start();
            Console.WriteLine($"Start DisposableOrderService {this.GetHashCode()}");
        }

        public void StopTimer()
        {
            timer.Stop();
            Console.WriteLine($"Stop DisposableOrderService {this.GetHashCode()}");
        }

        public void Dispose()
        {
            timer.Stop();
            Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
        }
    }
}