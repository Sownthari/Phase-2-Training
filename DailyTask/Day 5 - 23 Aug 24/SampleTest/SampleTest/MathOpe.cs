using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTest
{
    public class MathOpe
    {
        public int add(int a, int b)
        {
            return a + b;
        }
        public int Sub(int x, int y)
        {
            return x - y;
        }
        public int Pro(int x, int y)
        {
            return x * y;
        }
        public int Div(int x, int y)
        {
            return x / y;
        }
        public virtual bool CheckValues()
        {
            return false;
        }
    }
}
