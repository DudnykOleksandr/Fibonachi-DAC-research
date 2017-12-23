using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi_DAC_research.Code.Data
{
    public class DacSet
    {
        public double residue { get; set; }
        public double error { get; private set; }
        public int n { get;private set; }

        public float[] dividerArray { get; set; }

        public List<FibonachiWeight> weights { get; private set; }

        public DacSet()
        {
            error = 0;
            n = 0;

            weights = new List<FibonachiWeight>();
        }

        public void AddWeight(FibonachiWeight item)
        {
            weights.Add(item);
            error += item.error;
            n++;
        }

        public string DividerArrayToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var div in dividerArray)
            {
                result.AppendFormat(" {0}", div);
            }

            return result.ToString();
        }
    }
}
