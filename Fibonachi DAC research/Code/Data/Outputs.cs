using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fibonachi_DAC_research.Code.Logic;

namespace Fibonachi_DAC_research.Code.Data
{
    class Outputs
    {
        public List<DacSet> dacs { get; private set; }
        public Inputs inputs { get; private set; }

        public Outputs(List<DacSet> dacs, Inputs inputs)
        {
            this.dacs = dacs;
            this.inputs = inputs;
        }

        public string GetStringStatisticsRepresentation()
        {
            var result = Statistics.GetStatisticToString(dacs,inputs);

            return result;
        }
    }
}
