namespace LIBS2018
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDetectSpectro = new System.Windows.Forms.Button();
            this.lblSpectroInfo = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCurrentDirectory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxFileRoot = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlot)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPlot
            // 
            chartArea1.AxisX.Title = "Wavelength [nm]";
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.Title = "Intensity [arb. unit]";
            chartArea1.Name = "ChartArea1";
            this.chartPlot.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPlot.Legends.Add(legend1);
            this.chartPlot.Location = new System.Drawing.Point(13, 13);
            this.chartPlot.Name = "chartPlot";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPlot.Series.Add(series1);
            this.chartPlot.Size = new System.Drawing.Size(1125, 383);
            this.chartPlot.TabIndex = 0;
            this.chartPlot.Text = "chartPlot";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1063, 433);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDetectSpectro
            // 
            this.btnDetectSpectro.Location = new System.Drawing.Point(13, 403);
            this.btnDetectSpectro.Name = "btnDetectSpectro";
            this.btnDetectSpectro.Size = new System.Drawing.Size(148, 36);
            this.btnDetectSpectro.TabIndex = 2;
            this.btnDetectSpectro.Text = "Start Acquisition";
            this.btnDetectSpectro.UseVisualStyleBackColor = true;
            this.btnDetectSpectro.Click += new System.EventHandler(this.btnDetectSpectro_Click);
            // 
            // lblSpectroInfo
            // 
            this.lblSpectroInfo.AutoSize = true;
            this.lblSpectroInfo.Location = new System.Drawing.Point(12, 446);
            this.lblSpectroInfo.Name = "lblSpectroInfo";
            this.lblSpectroInfo.Size = new System.Drawing.Size(10, 13);
            this.lblSpectroInfo.TabIndex = 3;
            this.lblSpectroInfo.Text = "-";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(353, 402);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(105, 36);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Choose Directory";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblState.Location = new System.Drawing.Point(167, 410);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(39, 20);
            this.lblState.TabIndex = 5;
            this.lblState.Text = "Idle";
            // 
            // lblCurrentDirectory
            // 
            this.lblCurrentDirectory.AutoSize = true;
            this.lblCurrentDirectory.Location = new System.Drawing.Point(354, 446);
            this.lblCurrentDirectory.Name = "lblCurrentDirectory";
            this.lblCurrentDirectory.Size = new System.Drawing.Size(90, 13);
            this.lblCurrentDirectory.TabIndex = 6;
            this.lblCurrentDirectory.Text = "Current directory: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(494, 415);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filename root :";
            // 
            // txtBoxFileRoot
            // 
            this.txtBoxFileRoot.Location = new System.Drawing.Point(576, 411);
            this.txtBoxFileRoot.Name = "txtBoxFileRoot";
            this.txtBoxFileRoot.Size = new System.Drawing.Size(251, 20);
            this.txtBoxFileRoot.TabIndex = 8;
            this.txtBoxFileRoot.TextChanged += new System.EventHandler(this.txtBoxFileRoot_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 468);
            this.Controls.Add(this.txtBoxFileRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCurrentDirectory);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblSpectroInfo);
            this.Controls.Add(this.btnDetectSpectro);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.chartPlot);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartPlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPlot;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDetectSpectro;
        private System.Windows.Forms.Label lblSpectroInfo;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCurrentDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxFileRoot;
    }
}

