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
using Sounds.Helpers;

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
            //AudioContainer audio = new AudioContainer();

            ////Wycztywanie pliku
            string path = Path.GetSoundPath();
            //audio.OpenWav(path);
            //int[] result = SoundUtil.ChunkArray(audio.data, audio.chunkSize)[0];
            //double sampleRate = Convert.ToDouble(audio.sampleRate);
            //Histogram.Series.Clear();
            //Histogram.Series.Add("Signal");
            //this.Text = "Signal";
            //Histogram.Series["Signal"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //Histogram.Series["Signal"].MarkerSize = 2;

            //time = new double[result.Length];
            //value = new double[result.Length];
            //freq = new double[result.Length];

            //for (int i = 0; i < result.Count(); i++)
            //{
            //    value[i] = result[i] / sampleRate;
            //    time[i] = i / sampleRate;
            //    freq[i] = i * sampleRate / result.Length;
            //    Histogram.Series["Signal"].Points.AddXY(time[i], value[i]);
            //}

            ////Time domain
            //Tuple<List<long[]>, List<int>> autocorelationResult = audio.Autocorrelation();
            //long[] correlation = autocorelationResult.Item1[0];
            //int frequency = autocorelationResult.Item2[0];

            //// Rysownie wykresu autokorelacji
            //CharWindow corealationChar = new CharWindow();
            //corealationChar.button1.Visible = false;
            //corealationChar.Histogram.Series.Clear();
            //corealationChar.Histogram.Series.Add("corelation");
            //corealationChar.Name = "Corelation";
            //corealationChar.Text = "Corelation";
            //corealationChar.Histogram.Series["corelation"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //corealationChar.Histogram.Series["corelation"].MarkerSize = 2;

            //for (int i = 0; i < result.Length; i++)
            //{
            //    corealationChar.Histogram.Series["corelation"].Points.AddXY(time[i], correlation[i]);
            //}
            
            //corealationChar.Show();
            //MessageBox.Show(frequency.ToString());


            AudioContainer audioCepstrum = new AudioContainer();
            audioCepstrum.OpenWavFloat(path);
            List<int> frequenciesFreq = audioCepstrum.Cepstrum();

            //Frequency domain
            //Complex[] complex = SoundUtil.SignalToComplex(result);
            //Complex[] ComplexWindow = SoundUtil.HammingWindow(complex);

            //// Pierwszy Furier
            //Complex[] FFTComplex = SoundUtil.FftDit1d(ComplexWindow);
            //for (int i = 0; i < FFTComplex.Length / 2; ++i)
            //    FFTComplex[i] = new Complex(10.0f * (float)Math.Log10(FFTComplex[i].Modulus() + 1), 0);
            ////Pobranie połowy próbek
            //Complex[] FFTCompelxDiv2 = new Complex[FFTComplex.Length / 2];
            //for (int i = 0; i < FFTCompelxDiv2.Length; i++)
            //{
            //    FFTCompelxDiv2[i] = FFTComplex[i];
            //}

            ////Drugi Furier
            //Complex[] FFTComplex2 = SoundUtil.FftDit1d(FFTCompelxDiv2);
            ////Pobranie połowy próbek
            //Complex[] FFTCompelx2Div2 = new Complex[FFTComplex2.Length / 2];
            //for (int i = 0; i < FFTCompelx2Div2.Length; i++)
            //{
            //    FFTCompelx2Div2[i] = FFTComplex2[i];
            //}

            ////Rysowanie wykresu ceptral
            //CharWindow ceptral = new CharWindow();
            //ceptral.button1.Visible = false;
            //ceptral.Histogram.Series.Clear();
            //ceptral.Histogram.Series.Add("ceptral");
            //ceptral.Name = "ceptral";
            //ceptral.Text = "ceptral";
            //ceptral.Histogram.Series["ceptral"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //ceptral.Histogram.Series["ceptral"].MarkerSize = 2;

            //for (int i = 0; i < FFTCompelx2Div2.Count(); i++)
            //{
            //    ceptral.Histogram.Series["ceptral"].Points.AddXY(freq[i], FFTCompelx2Div2[i].Magnitude/audioHelper.SampleRate);
            //}

            //ceptral.Show();

        }
    }
}
