using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Linq
{
    public class ExceptDemo
    {
        public ExceptDemo()
        {
            
        }

        public void Test()
        {
            var a = new List<string>() { "A", "B" };
            var b = new List<string>() { "A", "B","C" };

            var c = b.Except(a);
        }
    }
}