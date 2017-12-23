using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fibonachi_DAC_research.Code.Data;

namespace Fibonachi_DAC_research.Code.Logic
{
    public static class Statistics
    {

        public static string GetStatisticToString(List<DacSet> list, Inputs inputs)
        {
            StringBuilder result = new StringBuilder();

            var sortedList = list.OrderByDescending(x => x.n).ThenBy(y => y.dividerArray[0]).ThenBy(y => y.error).Take(1000);

            foreach (var dac in sortedList)
            {
                result.AppendFormat("Resulution {0}, Error {1}, Residue {2},Dividers {3} ", dac.n, dac.error, dac.residue, dac.DividerArrayToString());
                result.Append("Bit weight: ");
                foreach (var weight in dac.weights)
                {
                    result.AppendFormat("\n w {0}:combination {1}, weightBitCombination {2} ", weight.weight * inputs.scaleResistor, weight.GetWeightCombinations(),
                        weight.GetWeightBitCombinations(inputs));
                }
                result.Remove(result.Length-2,2);
                result.Append("\n");
            }

            return result.ToString();
        }
        public static string GetStatisticOfSimpleWeightsLineToString(Inputs inputs,List<FibonachiWeight> weights)
        {
            StringBuilder result = new StringBuilder();

            result.Append("Bit weight: ");
            foreach (var weight in weights)
            {
                result.AppendFormat("\n w {0}:combination {1}, weightBitCombination {2} ", weight.weight * inputs.scaleResistor, weight.GetWeightCombinations(),
                    weight.GetWeightBitCombinations(inputs));
            }
            result.Remove(result.Length - 2, 2);
            result.Append("\n");

            return result.ToString();
        }
    }
}
