using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflectionDemo
{
    class Maths
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }
        public static int Multiply(int a, int b)
        {
            return a * b;
        }
        private int Divide(int a, int b, string name)
        {
            return a / b;

        }

        public virtual int Percentage(int a, int b)
        {
            return a / b * 100;

        }
    }
}
