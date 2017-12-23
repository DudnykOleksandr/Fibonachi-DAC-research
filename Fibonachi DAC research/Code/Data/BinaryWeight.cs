using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi_DAC_research.Code.Data
{
    public class BinaryWeight
    {
        public double weight { get; private set; }
        public int numOfDac { get; set; }
        public int numOfBitInDac { get; set; }

        public BinaryWeight(double weight, int numOfDac, int numOfBitInDac)
        {
            this.weight = weight;
            this.numOfDac = numOfDac;
            this.numOfBitInDac = numOfBitInDac;
        }
    }
}
