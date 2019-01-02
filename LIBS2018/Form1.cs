using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using OmniDriver;

namespace LIBS2018
{
    public partial class Form1 : Form
    {
        OmniDriver.CCoWrapper wrapper = new OmniDriver.CCoWrapper();
        List<Spectrometer> spList = new List <Spectrometer>();
        List<double[]> wholeSignal = new List<double[]>();
        String filePath = @"C:\Users\yvanbrabant\Desktop\LIBS\YvesTests\";
        String root = "Test_sp";
        //String initialIncrement = "0000";
        int incrementValue = 0;
        List<AcquisitionThread> acquisitionThreadList = new List<AcquisitionThread>();
        List<Thread> threadList = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
            filePath = Directory.GetCurrentDirectory();
            lblCurrentDirectory.Text = "Current directory: " + filePath;
            txtBoxFileRoot.Text = root;
            emptyChart();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            wrapper.closeAllSpectrometers();
            Application.Exit();
        }
        private void emptyChart()
        {
            chartPlot.Series.Add("Default");
            chartPlot.Series["Default"].ChartType = SeriesChartType.Line;
            chartPlot.Series["Default"].Points.AddXY(200.0, 1000.0);
            chartPlot.Series["Default"].Points.AddXY(400.0, 1500.0);
            chartPlot.Series["Default"].Points.AddXY(600.0, 1100.0);
            chartPlot.ChartAreas[0].AxisX.Maximum = 600.0;
            chartPlot.ChartAreas[0].AxisX.Minimum = 200.0;
            chartPlot.ChartAreas[0].AxisY.Maximum = 1600.0;
            chartPlot.ChartAreas[0].AxisY.Minimum = 900.0;
            chartPlot.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartPlot.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

        }

        private void btnDetectSpectro_Click(object sender, EventArgs e)
        {
            detectSpectro();
        }
        private void testu()
        {
            detectSpectro();
        }

        private void detectSpectro()
        {
            
            int nSpectro = wrapper.openAllSpectrometers();

            for (int i = 0; i < nSpectro; i++)
            {
                wrapper.setIntegrationTime(i, 4000);
                wrapper.setExternalTriggerMode(i,mode: 4);
            }

            double wavelengthMax = -1000.0;
            double wavelengthMin = 9999999999.0;
            double[] tmp;
            double[] wholeSpectralWindow = { 0.0 };
            double[] wholeSignalArray = { 0.0 };
            //int array1OriginalLength = 0;
            List<double[]> wholeSignal = new List<double[]>();
            lblState.Text = "In acquisition";
            lblState.ForeColor = System.Drawing.Color.Red;

            for (int i = 0; i < nSpectro; i++)
            {
                tmp = wrapper.getWavelengths(i);
                if (tmp.Max() > wavelengthMax) { wavelengthMax = tmp.Max(); }
                if (tmp.Min() < wavelengthMin) { wavelengthMin = tmp.Min(); }
                if(i==0)
                {
                    wholeSpectralWindow = tmp;
                }
                else
                {
                    int array1OriginalLength = wholeSpectralWindow.Length;
                    Array.Resize<double>(ref wholeSpectralWindow, array1OriginalLength + tmp.Length);
                    tmp.CopyTo(wholeSpectralWindow,array1OriginalLength);
                }
            }

            if (nSpectro != 0)
            {
                lblSpectroInfo.Text = "Number of spectro : " + nSpectro.ToString() + "   Spect. window : [" + wavelengthMin.ToString("F") + " -> " + wavelengthMax.ToString("F") + "]";
            }

            //Prepare the thread
            if(acquisitionThreadList.Count>1) acquisitionThreadList.Clear();
            if(threadList.Count>1) threadList.Clear();

            for (int i = 0; i < nSpectro; i++)
            {
                AcquisitionThread tmpAcquisitionThread = new AcquisitionThread(ref wrapper, i);
                acquisitionThreadList.Add(tmpAcquisitionThread);
                Thread tmpThread = new Thread(new ThreadStart(acquisitionThreadList[i].doWork));
                threadList.Add(tmpThread);
            }
            //AcquisitionThread acquisitionThread1 = new AcquisitionThread(ref wrapper, 0);
            //Thread oThreadone = new Thread(new ThreadStart(acquisitionThread1.doWork));
            //AcquisitionThread acquisitionThread2 = new AcquisitionThread(ref wrapper, 1);
            //Thread oThreadtwo = new Thread(new ThreadStart(acquisitionThread2.doWork));
            for (int i = 0; i < nSpectro; i++)
            {
                threadList[i].Start();
            }
            //oThreadone.Start();
            //oThreadtwo.Start();

            for (int i = 0; i < nSpectro; i++)
            {
                threadList[i].Join();
            }
            //oThreadone.Join();
            //oThreadtwo.Join();

            for (int i = 0; i < nSpectro; i++)
            {
                if (i == 0)
                {
                    wholeSignalArray = acquisitionThreadList[i].signal;
                }
                else
                {
                    int array1OriginalLength2 = wholeSignalArray.Length;
                    Array.Resize<double>(ref wholeSignalArray, array1OriginalLength2 + acquisitionThreadList[i].signal.Length);
                    acquisitionThreadList[i].signal.CopyTo(wholeSignalArray, array1OriginalLength2);
                }

            }

            upDatePlot(wholeSpectralWindow, wholeSignalArray);



            WhatNext wn = new WhatNext();
            wn.ShowDialog();

            //Case of re-acquired data
            if (wn.data==1)
            {
                testu();
            }

            //Case of saving data
            if(wn.data==2)
            {
                saveData(nSpectro,acquisitionThreadList);
                testu();

            }

            //Last spectrum -> Save and Exit
            if(wn.data==3)
            {
                saveData(nSpectro,acquisitionThreadList);
                
                lblState.ForeColor = System.Drawing.Color.Purple;
                lblState.Text = "Idle";
                wrapper.closeAllSpectrometers();
            }
        }
        private void saveData(int nSp, List<AcquisitionThread> aThreadList)
        {
            var csv = new StringBuilder();
            string cha;
            string intensity;
            string fullPath;
            int spectroIndex;
            incrementValue++;

            for (int i = 0; i < nSp; i++)
            {
                csv.Clear();
                spectroIndex = i + 1;
                double[] wwww0 = wrapper.getWavelengths(i);
                for (int j = 0; j < acquisitionThreadList[i].signal.Length; j++)
                {
                    cha = wwww0[j].ToString();
                    intensity = acquisitionThreadList[i].signal[j].ToString();
                    var newline = string.Format("{0},{1}\n", cha, intensity);
                    csv.Append(newline);
                }
                fullPath = filePath + root + spectroIndex.ToString()+"_" + incrementValue.ToString() + ".csv";
                File.WriteAllText(fullPath, csv.ToString());
            }
        }

        private void upDatePlot(double[] waveL, double[] signal)
        {
            double waveMin = waveL.Min();
            double waveMax = waveL.Max();
            double signalMin = signal.Min();
            double signalMax = signal.Max();
            chartPlot.ChartAreas[0].AxisX.Minimum = waveMin;
            chartPlot.ChartAreas[0].AxisX.Maximum = waveMax;
            chartPlot.ChartAreas[0].AxisY.Minimum = signalMin;
            chartPlot.ChartAreas[0].AxisY.Maximum = signalMax;
            chartPlot.Series["Default"].Points.Clear();
            for(int i = 0; i<waveL.Length;i++)
            {
                chartPlot.Series["Default"].Points.AddXY(waveL[i], signal[i]);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test function");
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    filePath = fbd.SelectedPath;
                    String stmp = filePath.Substring(filePath.Length);
                    
                    //Adding trailing slash if necessary
                    if (stmp != "\\")
                    {
                        filePath = filePath + "\\";
                    }
                    lblCurrentDirectory.Text = "Current directory: " + filePath;
                }
            }
        }

        private void txtBoxFileRoot_TextChanged(object sender, EventArgs e)
        {
            root = txtBoxFileRoot.Text + "_sp";
        }
    }

    public class Spectrometer
    {
        public int spectrometerIndex { get; set; }
        //public double[] spectrometerWaveLengths { get; set; }
        public double waveMin { get; set; }
        public double waveMax { get; set; }
        public double[] wavelengths { get; set; }
        public int nPixel { get; set; }

        public string wave2String(double wav)
        {
            string tmp = "";
            tmp = String.Format("{0:0.0}", wav);
            return tmp;
        }

    }
    internal class AcquisitionThread
    {
        private int spectroNumber;
        private int acquisitionTime;
        private CCoWrapper w;
        public double[] signal;

        public AcquisitionThread(ref CCoWrapper w, int spN)
        {
            this.spectroNumber = spN;
            this.w = w;
            this.acquisitionTime = w.getIntegrationTime(this.spectroNumber);
        }
        public void doWork()
        {
            double[] tmp = w.getSpectrum(this.spectroNumber);
            this.signal = tmp;
        }
    }
}
