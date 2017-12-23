using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Fibonachi_DAC_research.Code.Logic
{
    public static class Factorial
    {
        public static double Get(int x)
        {
            double f = 1;
            for (int i = 1; i <= x; i++)
            {
                f *= i;
            }
            return f;
        }

        public static BigInteger FSharpFactorial(int number)
        {
            BigInteger f = new BigInteger(1);
            for (int i = 1; i <= number; i++)
            {
                f *= new BigInteger(i);
            }
            return f;
        } 

        public static double PartialFactorialGet(int x, int y)
        {
            double f = 1;
            for (int i = x; i <= y; i++)
            {
                f *= i;
            }
            return f;
        }
    }
}
