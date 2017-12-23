using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fibonachi_DAC_research.Code.Data;
using Fibonachi_DAC_research.Code.Logic;

namespace Fibonachi_DAC_research.Code.Logic
{
    class MainLogic
    {
        public Calculator calculator { get; private set; }
        public string StringStatisticsRepresentation { get; private set; }

        public delegate void CalculatorFinishedEventHandler(object sender, EventArgs e);
        public event CalculatorFinishedEventHandler CalculatorFinished;
    
        public MainLogic(Inputs inp)
        {
            this.calculator = new Calculator(inp);
        }

        public void Calculate()
        {
            var statData = this.calculator.Calculate();

            StringStatisticsRepresentation=statData.GetStringStatisticsRepresentation();

            if (CalculatorFinished != null)
                CalculatorFinished(this, new EventArgs());
        }

        internal string CalculateLineOfWeights()
        {
            return this.calculator.CalculateLineOfWeights();
        }
    }
}
