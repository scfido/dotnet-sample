using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            new ExceptDemo().Test();
            Console.WriteLine("测试完成");
            Console.Read();
        }


    }
}
