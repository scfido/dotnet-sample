using System;
using Refit;

namespace RefitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = RestService.For<IClient>("https://www.baidu.com");
            var ret = client.Search().Result;
            Console.WriteLine(ret.StatusCode);
        }
    }
}
