using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi_DAC_research.Code.Data
{
    public class FibonachiWeight
    {
        public List<BinaryWeight> bitWeightCombinations { get; private set; }

        //weight + error = ideal weight

        /// <summary>
        /// weight calculated by means of summing
        /// </summary>
        public double weight { get; private set; }
        public double error { get; set; }
            
        public FibonachiWeight()
        {
            weight = 0;
            error = 0;
            bitWeightCombinations = new List<BinaryWeight>();
        }

        public void AddCombinations(BinaryWeight weight)
        {
            this.weight += weight.weight;
            bitWeightCombinations.Add(weight);
        }

        public string GetWeightCombinations()
        {
            StringBuilder result = new StringBuilder();

            foreach (var combination in bitWeightCombinations)
            {
                result.AppendFormat("{0} ({1},{2})+", combination.weight,combination.numOfDac,combination.numOfBitInDac);
            }
            result.Remove(result.Length-1, 1);

            return result.ToString();
        }
        public string GetWeightBitCombinations(Inputs inputs)
        {
            var result=new StringBuilder();
            var res=new char[inputs.K][];

            for(int i=0;i<inputs.K;i++)
            {
                res[i]=Enumerable.Repeat('0', inputs.n).ToArray<char>();
            }

            foreach (var combination in bitWeightCombinations)
            {
                res[combination.numOfDac][combination.numOfBitInDac]='1';
            }

            for(int i=inputs.K-1;i>=0;i--)
            {
                result.Append(res[i].Reverse().ToArray<char>());
            }

            var resStr = result.ToString();

            return resStr + " " + Convert.ToInt64(resStr, 2) + ",";
        }
    }
}
