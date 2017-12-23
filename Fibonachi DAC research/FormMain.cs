using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Fibonachi_DAC_research.Code.Logic;
using Fibonachi_DAC_research.Code.Data;
using System.Threading;
using System.IO;
using System.Data.OleDb;

namespace Fibonachi_DAC_research
{
    public partial class FormMain : Form
    {
        MainLogic mainLogic;

        Thread calculationThread;

        delegate void SetTextCallback(string text);
        delegate void ChangeButtonStateCallback();

        public FormMain()
        {
            InitializeComponent();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            btnCalculate.Enabled = false;

            this.lblProg.Text = "00.0000";

            var inputs=GetInputs();

            if (inputs != null)
            {
                mainLogic = new MainLogic(inputs);

                mainLogic.calculator.CalculatorChanged += new Calculator.CalculatorChangedEventHandler(calculator_CalculatorChanged);
                mainLogic.CalculatorFinished+=new MainLogic.CalculatorFinishedEventHandler(mainLogic_CalculatorFinished);

                calculationThread=new Thread(new ThreadStart(mainLogic.Calculate));
                calculationThread.Priority = ThreadPriority.Normal;
                calculationThread.Start();
                
            }
        }

        private void calculator_CalculatorChanged(object sender, ProgressEventArgs e)
        {
            SetPercentageProgressText(e.percentageProgress);
        }

        private void SetPercentageProgressText(string text)
        {
            if (this.lblProg.InvokeRequired)
            {
                var d = new SetTextCallback(SetPercentageProgressText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblProg.Text= text;
            }
        }

        private void mainLogic_CalculatorFinished(object sender, EventArgs e)
        {
            SetResultsText(mainLogic.StringStatisticsRepresentation);

            ChangeButtonState();
        }

        private void SetResultsText(string text)
        {
            if (this.rtxtRes.InvokeRequired)
            {
                var d = new SetTextCallback(SetResultsText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.rtxtRes.Text = text;
            }
        }
        private void ChangeButtonState()
        {
            if (this.btnCalculate.InvokeRequired)
            {
                var d = new ChangeButtonStateCallback(ChangeButtonState);
                this.Invoke(d);
            }
            else
            {
                this.btnCalculate.Enabled = true;
            }
        }

        private Inputs GetInputs()
        {
            int K;
            int n;
            double d;
            int step;
            double scaleResistor;
            List<float> dividers = null;

            try
            {
                K = int.Parse(txtK.Text);
                n = int.Parse(txtn.Text);
                d = double.Parse(txtd.Text);
                step = int.Parse(txtStep.Text);
                scaleResistor = double.Parse(txtScaleRes.Text);

                if (chkFromExcel.Checked)
                {
                    dividers = ReadeDividersFromExcel();

                    dividers = Loose(dividers,step);
                }

                var result = new Inputs(n, K, d, step, scaleResistor, dividers);

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in inputs" + ex.Message);
            }

            return null;
        }

        private List<float> Loose(List<float> dividers, int step)
        {
            List<float> res=new List<float>();
            for (int i = 0; i < dividers.Count; i++)
            {
                if (i % step == 0)
                {
                    res.Add(dividers[i]);
                }
            }
            return res;
        }

        private List<float> ReadeDividersFromExcel()
        {
            var res = new List<float>();

            var fileName = string.Format("{0}\\ExcelBookDividers.xlsx", Directory.GetCurrentDirectory());
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\"", fileName);


            var adapter = new OleDbDataAdapter("SELECT * FROM [worksheet1$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "dividers");
            DataTable data = ds.Tables["dividers"];

            foreach (DataRow row in data.Rows)
            {
                if(!string.IsNullOrEmpty(row[0].ToString()))
                {
                res.Add(float.Parse(row[0].ToString()));
                }
            }

            // Сортируем.
            res.Sort();

            return res;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (calculationThread != null)
            {
                calculationThread.Abort();
                calculationThread.Join();
            }
        }

        private void converterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Converter().Show();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (calculationThread != null)
            {
                calculationThread.Abort();
                calculationThread.Join();
            }

            this.lblProg.Text = "00.0000";
            this.btnCalculate.Enabled = true;
        }

        private void chkFromExcel_CheckedChanged(object sender, EventArgs e)
        {
            //txtStep.Enabled = !chkFromExcel.Checked;
        }

        private void btnCalSimply_Click(object sender, EventArgs e)
        {
            btnCalculate.Enabled = false;

            this.lblProg.Text = "00.0000";

            var inputs = GetInputs();

            if (inputs != null)
            {
                mainLogic = new MainLogic(inputs);

                var result=mainLogic.CalculateLineOfWeights();

                SetResultsText(result);

            }
        }
    }
}
