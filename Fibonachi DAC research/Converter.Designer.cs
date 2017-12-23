namespace Fibonachi_DAC_research
{
    partial class Converter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFibonachiRes = new System.Windows.Forms.TextBox();
            this.txtBinaryRes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFibonachiToBinary = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fibonachi DAC resolution";
            // 
            // txtFibonachiRes
            // 
            this.txtFibonachiRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFibonachiRes.Location = new System.Drawing.Point(207, 13);
            this.txtFibonachiRes.Name = "txtFibonachiRes";
            this.txtFibonachiRes.Size = new System.Drawing.Size(107, 26);
            this.txtFibonachiRes.TabIndex = 1;
            this.txtFibonachiRes.Text = "12";
            // 
            // txtBinaryRes
            // 
            this.txtBinaryRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBinaryRes.Location = new System.Drawing.Point(207, 58);
            this.txtBinaryRes.Name = "txtBinaryRes";
            this.txtBinaryRes.Size = new System.Drawing.Size(107, 26);
            this.txtBinaryRes.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Binary DAC resolution";
            // 
            // btnFibonachiToBinary
            // 
            this.btnFibonachiToBinary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFibonachiToBinary.Location = new System.Drawing.Point(341, 16);
            this.btnFibonachiToBinary.Name = "btnFibonachiToBinary";
            this.btnFibonachiToBinary.Size = new System.Drawing.Size(91, 29);
            this.btnFibonachiToBinary.TabIndex = 2;
            this.btnFibonachiToBinary.Text = "Calculate";
            this.btnFibonachiToBinary.UseVisualStyleBackColor = true;
            this.btnFibonachiToBinary.Click += new System.EventHandler(this.btnFibonachiToBinary_Click);
            // 
            // Converter
            // 
            this.AcceptButton = this.btnFibonachiToBinary;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 116);
            this.Controls.Add(this.btnFibonachiToBinary);
            this.Controls.Add(this.txtBinaryRes);
            this.Controls.Add(this.txtFibonachiRes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Converter";
            this.Text = "Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFibonachiRes;
        private System.Windows.Forms.TextBox txtBinaryRes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFibonachiToBinary;
    }
}