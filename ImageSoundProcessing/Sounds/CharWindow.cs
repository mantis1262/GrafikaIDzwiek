using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sound.Helpers;
using System.Numerics;

namespace Sound
{
    public partial class CharWindow : Form
    {
        double[] time;
        double[] value;
        double[] freq;

        public CharWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AudioHelper audioHelper = new AudioHelper();


            Tuple<double[], int, TimeSpan> wave = audioHelper.openWav(Path.GetSoundPath());
            double[] result = wave.Item1;
            double sampleRate = Convert.ToDouble(wave.Item2);
            Histogram.Series.Clear();
            Histogram.Series.Add("Signal");
            this.Text = "Signal";
            Histogram.Series["Signal"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            Histogram.Series["Signal"].MarkerSize = 2;

            time = new double[result.Length];
            value = new double[result.Length];
            freq = new double[result.Length];

            for (int i = 0; i < result.Count(); i++)
            {
                value[i] = result[i] / sampleRate;
                time[i] = i / sampleRate;
                freq[i] = i * sampleRate / result.Length;
                Histogram.Series["Signal"].Points.AddXY(freq[i], value[i]);
            }

            //Time domain

            double[] autocorelation = audioHelper.Autocorrelation(result);
            double localMaxIndex = audioHelper.findLocalMax(autocorelation);
            double frequenties = audioHelper.sampleRate / localMaxIndex;


            CharWindow corealationChar = new CharWindow();
            corealationChar.button1.Visible = false;
            corealationChar.Histogram.Series.Clear();
            corealationChar.Histogram.Series.Add("corelation");
            corealationChar.Name = "Corelation";
            corealationChar.Text = "Corelation";
            corealationChar.Histogram.Series["corelation"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            corealationChar.Histogram.Series["corelation"].MarkerSize = 2;

            for (int i = 0; i < result.Count(); i++)
            {
                corealationChar.Histogram.Series["corelation"].Points.AddXY(i, autocorelation[i]);
            }

            corealationChar.Show();
            MessageBox.Show(frequenties.ToString("0.0"));
            //


            //Frqudency domian
            Complex[] complex = audioHelper.SignalToComplex(result);
            Complex[] ComplexWindow = audioHelper.HammingWindow(complex);

            Complex[] FFTComplex = audioHelper.FFT(ComplexWindow);

            for (int i = 0; i < FFTComplex.Length; ++i)
                FFTComplex[i] = new Complex(10.0 * Math.Log10(FFTComplex[i].Magnitude + 1), 0);

            Complex[] FFTComplex2 = audioHelper.FFT(FFTComplex);


             CharWindow ceptral = new CharWindow();
            ceptral.button1.Visible = false;
            ceptral.Histogram.Series.Clear();
            ceptral.Histogram.Series.Add("ceptral");
            ceptral.Name = "ceptral";
            ceptral.Text = "ceptral";
            ceptral.Histogram.Series["ceptral"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            ceptral.Histogram.Series["ceptral"].MarkerSize = 2;

            for (int i = 0; i < result.Count(); i++)
            {
                ceptral.Histogram.Series["ceptral"].Points.AddXY(i, FFTComplex2[i].Magnitude);
            }

            ceptral.Show();


            double[][] d = new double[2][];
            d[0] = new double[result.Length];
            d[1] = new double[result.Length];
            for (int i = 0; i < FFTComplex2.Length; ++i)
            {
                //power cepstrum
                //d[0][i]=Math.pow(csignal[i].abs(),2);
                d[0][i] = FFTComplex2[i].Magnitude;
            }

        }
    }
}
