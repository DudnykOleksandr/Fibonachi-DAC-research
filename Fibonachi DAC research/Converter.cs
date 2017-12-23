using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Fibonachi_DAC_research.Code.Data;

namespace Fibonachi_DAC_research
{
    public partial class Converter : Form
    {
        public Converter()
        {
            InitializeComponent();
        }

        private void btnFibonachiToBinary_Click(object sender, EventArgs e)
        {
            int fibResolution = Convert.ToInt32(txtFibonachiRes.Text);

            double fibRange = Fibonachi.CalculateFibonachiRange(fibResolution);
            
            txtBinaryRes.Text = ((int)Math.Log(fibRange, 2)).ToString();
        }
    }
}
