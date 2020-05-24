using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Dsp;
using Sound.Helpers;

namespace Sound
{
    public partial class CharWindow : Form
    {
        public CharWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AudioHelper audioHelper = new AudioHelper();

            short[] left;

            Tuple<double[], int, TimeSpan> wave = audioHelper.openWav(Path.GetSoundPath(), out left);
            double[] result = wave.Item1;
            double sampleRate = Convert.ToDouble(wave.Item2);
            Histogram.Series.Clear();
            Histogram.Series.Add("Value");
            Histogram.Series["Value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            Histogram.Series["Value"].MarkerSize = 2;

            double[] time = new double[result.Length];
            double[] value = new double[result.Length];
            double[] freq = new double[result.Length];

            for (int i = 0; i < result.Count(); i++)
            {
                value[i] = result[i] / sampleRate;
                time[i] = i / sampleRate;
                freq[i] = i * sampleRate / result.Length;
                Histogram.Series["Value"].Points.AddXY(freq[i], value[i]);
            }

            double[] autocorelation = audioHelper.Autocorrelation(result);
            double localMaxIndex = audioHelper.findLocalMax(autocorelation);
            double frequenties = audioHelper.sampleRate / localMaxIndex;

            MessageBox.Show(frequenties.ToString("0.0"));


        }
    }
}
