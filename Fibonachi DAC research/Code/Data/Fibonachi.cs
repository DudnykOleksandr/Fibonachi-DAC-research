using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Fibonachi_DAC_research.Code.Data
{
    public static class Fibonachi
    {
        static Hashtable hshTable = new Hashtable();

        public static double GetNFibonachi(int n)
        {
            if (hshTable.ContainsKey(n))
            {
                return (double)hshTable[n];
            }
            else
            {
                var res = CalFibonachi(n);
                hshTable.Add(n, res);
                return res;
            }
        }

        public static double CalculateFibonachiRange(int fibResolution)
        {
            double fibRange = 0;

            for (int i = 0; i < fibResolution; i++)
            {
                fibRange += Fibonachi.GetNFibonachi(i);
            }

            return fibRange;
        }

        private static double CalFibonachi(int n)
        {
            if ((n == 0) | (n == 1))
                return 1;
            return CalFibonachi(n - 1) + CalFibonachi(n - 2);
        }

    }
}
