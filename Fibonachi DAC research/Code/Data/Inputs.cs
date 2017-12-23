using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi_DAC_research.Code.Data
{
    public class Inputs
    {
        public int n { get; private set; }
        public int K { get; private set; }
        public double d { get; private set; }
        public double scaleResistor { get; private set; }
        public int step { get; private set; }

        public List<float> dividers { get; set; }

        public float[] dividerArray;
        public List<List<BinaryWeight>> inputDacs { get; private set; }

        public Inputs(int n, int K, double d, int step, double scaleResistor, List<float> dividers)
            : this(n, K, d, step, scaleResistor)
        {
            this.dividers = dividers;
        }
        public Inputs(int n, int K, double d, int step,double scaleResistor)
        {
            this.n = n;
            this.K = K;
            this.d = d;
            this.step = step;
            this.scaleResistor = scaleResistor;

            dividerArray = new float[K];

            for(int i=0; i < dividerArray.Length; i++)
            {
                dividerArray[i] = 1;
            }

            inputDacs = new List<List<BinaryWeight>>();

            for (int k = 0; k < K; k++)
            {
                inputDacs.Add(new List<BinaryWeight>());

                for (int r = 0; r < n; r++)
                {
                    inputDacs[k].Add(new BinaryWeight(Math.Pow(2, r),k,r));
                }
            }
        }

        public Inputs Divide(int numOfDac, float divider)
        {
            dividerArray[numOfDac] = divider;

            var dividedDac = inputDacs[numOfDac].Select(item => 
                new BinaryWeight(item.weight / divider, item.numOfDac, item.numOfBitInDac)).ToList<BinaryWeight>();
            inputDacs[numOfDac] = dividedDac;

           return this;
        }

        public List<BinaryWeight> GetWeightsArray()
        {
            return GetOneArrayFromDacsArrays();
        }

        public double GetMSB()
        {
            return inputDacs[0][inputDacs[0].Count - 1].weight;
        }
        public double GetBitWeight(int n)
        {
            return inputDacs[0][n].weight;
        }

        public void RestoreAllBelowEqualDacs(int numOfDac)
        {
            for (int k = numOfDac; k >= 0; k--)
            {
                for (int r = 0; r < n; r++)
                {
                    inputDacs[k][r] = new BinaryWeight(Math.Pow(2, r), k, r);
                }
            }
        }

        public double GetRange()
        {
            double res=0;

            foreach (var dacItem in inputDacs)
            {
                foreach (var weight in dacItem)
                {
                    res += weight.weight;
                }
            }

            return res;
        }

        #region Privates

        List<BinaryWeight> GetOneArrayFromDacsArrays()
        {
            List<BinaryWeight> result = new List<BinaryWeight>();

            for (int k = 0; k < K; k++)
            {
                result.AddRange(inputDacs[k]);
            }

            result = result.OrderBy(x => x.weight).ToList<BinaryWeight>();

            return result;
        }

        #endregion Privates
    }
}
