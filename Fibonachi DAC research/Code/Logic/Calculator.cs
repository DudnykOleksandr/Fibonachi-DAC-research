using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fibonachi_DAC_research.Code.Data;

namespace Fibonachi_DAC_research.Code.Logic
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(string percentageProgress)
        {
            this.percentageProgress = percentageProgress;
        }
        public string percentageProgress;
    }

    class Calculator
    {
        Inputs inp;

        double currentProgress;
        double maxProgress;
        public double percentageProgress;

        const int BorderNumber = 100000;
        const int ListNumber = 1000;

        List<DacSet> result;
        DacSet bestDacSet;

        float[] dividers;

        public delegate void CalculatorChangedEventHandler(object sender, ProgressEventArgs e);

        public event CalculatorChangedEventHandler CalculatorChanged;

        public Calculator(Inputs inp)
        {
            this.inp = inp;

            dividers = PrepareDividers();

            var value = dividers.Length;

            // для перебору без першого цап
            // maxProgress = (double)(Factorial.FSharpFactorial(value + inp.K - 1-1) / (Factorial.FSharpFactorial(value - 1) * Factorial.FSharpFactorial(inp.K-1)));

            maxProgress = (double)(Factorial.FSharpFactorial(value + inp.K -1)
                / (Factorial.FSharpFactorial(value - 1) * Factorial.FSharpFactorial(inp.K)));

            currentProgress = 0;
            percentageProgress = 0;
        }

        public Outputs Calculate()
        {
            result = new List<DacSet>();
            bestDacSet = new DacSet();

            int msbDivider = dividers.Length - 1;
            int step = inp.step;

            ApproximateOneDac(inp.K, msbDivider, step);

            return new Outputs(result,inp);
        }

        #region Private

        private void ApproximateOneDac(int numOfDac, int startDivider, int step)
        {
            numOfDac--;

            for (int divider = startDivider; divider >= 0; divider--)
            {
                inp.RestoreAllBelowEqualDacs(numOfDac);

                inp.Divide(numOfDac, dividers[divider]);

                if (numOfDac != 0)
                {
                    ApproximateOneDac(numOfDac, divider, step);

                    //звільняємо память
                    if (result.Count > BorderNumber)
                    {
                        result = result.OrderByDescending(x => x.n).ThenBy(y => y.error).Take(ListNumber).ToList<DacSet>();
                        bestDacSet = result[0];
                    }
                }
                else
                {
                    //рахуэмо відсоток виконаної роботи
                    currentProgress++;

                    var weights = inp.GetWeightsArray();

                    DacSet dacSet = new DacSet();

                    int iOneWeight;
                    for (iOneWeight = 0; true; iOneWeight++)
                    {
                        if ((0.5 - weights[iOneWeight].weight) <= 0.25)
                            break;
                    }
                    double weightToApproximate = weights[iOneWeight].weight;

                    for (int i = 0; true; i++)
                    {
                        if (i >= 2)
                        {
                            weightToApproximate = dacSet.weights[i - 1].weight + dacSet.weights[i - 2].weight;
                        }
                        else if (i == 1)
                        {
                            weightToApproximate = dacSet.weights[i - 1].weight;
                        }

                        var aproximatedWeights = ApproximateOneWeight(weightToApproximate, weights);

                        if (aproximatedWeights == null)
                        {
                            break;
                        }

                        dacSet.AddWeight(aproximatedWeights);
                    }
                    if (dacSet.n >= bestDacSet.n || bestDacSet.n - dacSet.n < 3)
                    {
                        dacSet.dividerArray = (float[])inp.dividerArray.Clone();

                        //comment in PROD
                        //dacSet.residue = inp.GetRange() -Fibonachi.CalculateFibonachiRange(dacSet.n);

                        result.Add(dacSet);
                    }

                    var newPercentageProgress = Math.Round(currentProgress * 100 / maxProgress, 2);

                    //якшо відсоток виконаної роботи змінився до четвертої цифри після коми - виводимо
                    if (newPercentageProgress != percentageProgress)
                    {
                        percentageProgress = newPercentageProgress;
                        //fire an event
                        if (CalculatorChanged != null)
                            CalculatorChanged(this, new ProgressEventArgs(percentageProgress.ToString()));
                    }
                }
            }
        }

        private FibonachiWeight ApproximateOneWeight(double weightToApproximate, List<BinaryWeight> inputWeights)
        {

            FibonachiWeight resultWeight = new FibonachiWeight();

            double sum = 0;

            for (int i = inputWeights.Count - 1; i >= 0; i--)
            {
                var tempV = sum + inputWeights[i].weight;
                if (weightToApproximate >= tempV)
                {
                    sum = tempV;
                    resultWeight.AddCombinations(inputWeights[i]);
                }
            }

            if (Math.Abs((weightToApproximate - sum)/weightToApproximate)*100 > inp.d || resultWeight.bitWeightCombinations.Count==0)
            {
                return null;
            }

            SubtractWeightCombinationFromInputArray(resultWeight, inputWeights);

            resultWeight.error = weightToApproximate - sum;

            return resultWeight;
        }

        private void SubtractWeightCombinationFromInputArray(FibonachiWeight resultWeight, List<BinaryWeight> inputWeights)
        {
            foreach (var item in resultWeight.bitWeightCombinations)
            {
                foreach (var weight in inputWeights)
                {
                    if (weight.numOfDac == item.numOfDac &&
                        weight.numOfBitInDac == item.numOfBitInDac)
                    {
                        inputWeights.Remove(weight);
                        break;
                    }
                }
            }
        }

        private float[] PrepareDividers()
        {
            if (inp.dividers != null)
            {
                return inp.dividers.ToArray<float>();
            }

            var dividers = new List<float>();

            int numberOfBitWhichIsCurrent = inp.n - 1;
            int currentBitDivider = (int)inp.GetBitWeight(numberOfBitWhichIsCurrent); // MSB
            int nextBitDivider = (int)inp.GetBitWeight(numberOfBitWhichIsCurrent - 1);

            do
            {
                if (currentBitDivider <= nextBitDivider)
                {
                    dividers.Add(nextBitDivider);

                    currentBitDivider = nextBitDivider;
                    numberOfBitWhichIsCurrent--;

                    if (numberOfBitWhichIsCurrent == 0)
                    {
                        break;
                    }

                    nextBitDivider = (int)inp.GetBitWeight(numberOfBitWhichIsCurrent - 1);
                }
                else
                {
                    dividers.Add(currentBitDivider);
                }

                currentBitDivider -= inp.step;
            }
            while (true);

            return dividers.ToArray<float>();
        }

        #endregion Private


        internal string CalculateLineOfWeights()
        {
            string result;

            DacSet dacSet = new DacSet();

            dividers = PrepareDividers();

            for (int i = 0; i < inp.K; i++)
            {
                inp.Divide(i, dividers[i]);
            }

            var weights = inp.GetWeightsArray();

            for (int i = weights.Count - 1; i >= 0; i--)
            {
                var resultWeight = new FibonachiWeight();

                resultWeight.AddCombinations(weights[i]);

                dacSet.AddWeight(resultWeight);
            }

            var dacSetWeights = dacSet.weights.OrderBy(x => x.weight).ToList();

            result=Statistics.GetStatisticOfSimpleWeightsLineToString(inp, dacSetWeights);

            return result;

        }
    }
}