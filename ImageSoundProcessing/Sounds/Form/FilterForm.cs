using NAudio.Wave;
using Sound;
using Sound.Model;
using Sounds.Helpers;
using Sounds.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sounds
{
    public partial class FilterForm : Form
    {
        AudioContainer _audio;

        public FilterForm()
        {
            InitializeComponent();
        }

        public FilterForm(AudioContainer audio)
        {
            _audio = audio;
            InitializeComponent();
        }

        private void causal_Click(object sender, EventArgs e)
        {
            int windowSize;
            int hopSize;
            int filterSize;
            int fc;

            int.TryParse(this.WindowSize.Text, out windowSize);
            int.TryParse(this.R.Text, out hopSize);
            int.TryParse(this.L.Text, out filterSize);
            int.TryParse(this.Fc.Text, out fc);
            double[] windowData = new double[windowSize];

            //okno dla sygnału
            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFactors(windowSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFactors(windowSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFactors(windowSize);
                    break;
            }


            double[][] parts = SoundUtil.ChunkArrayWithHop(_audio.dataNormalized, windowSize, hopSize);
            double[][] winParts = new double[parts.Length][];
            for (int i = 0; i < parts.Length; i++)
            {
                winParts[i] = new double[parts[0].Length];
                for (int j = 0; j < parts[i].Length; j++)
                {
                    winParts[i][j] = parts[i][j] * windowData[j];

                }
               // winParts[i] = SoundUtil.MovedSignal(winParts[i], hopSize);
                winParts[i] = SoundUtil.AddZerosCasual(filterSize - 1, winParts[i]);
            }

            // okno dla filtru
            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFactors(filterSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFactors(filterSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFactors(filterSize);
                    break;
            }

            double[] filterData = SoundUtil.lowPassFilter(fc, 44100, windowData);
            filterData = SoundUtil.AddZerosCasual(windowSize - 1, filterData);
            #region filterChar
            CharWindow WindowCharm = new CharWindow();
            WindowCharm.setPropert();

            WindowCharm.Histogram.Series.Add("WindowCharm");
            WindowCharm.Text = "WindowCharm";
            WindowCharm.Histogram.Series["WindowCharm"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            WindowCharm.Histogram.Series["WindowCharm"].MarkerSize = 2;
            WindowCharm.Histogram.ChartAreas[0].AxisX.Title = "Index";
            WindowCharm.Histogram.ChartAreas[0].AxisY.Title = "Window value";

            for (int i = 0; i < filterData.Count(); i++)
            {
                WindowCharm.Histogram.Series["WindowCharm"].Points.AddXY(i, filterData[i]);
            }

            WindowCharm.Show();
            #endregion

            Complex[] filterComplex = SoundUtil.FFT(SoundUtil.SignalToComplex(filterData));
            Complex[] resultComplex = new Complex[filterComplex.Length];
            List<float[]> resultIFurier = new List<float[]>();
            foreach (double[] part in winParts)
            {
                List<float> resultPart = new List<float>();
                Complex[] signalPartComplex = SoundUtil.FFT(SoundUtil.SignalToComplex(part));

                for (int i = 0; i < part.Length; i++)
                    resultComplex[i] = signalPartComplex[i] * filterComplex[i];
                resultComplex = SoundUtil.IFFT(resultComplex);
                foreach (Complex complex in resultComplex)
                {
                    resultPart.Add((int)(complex.Real));
                }
                resultIFurier.Add(resultPart.ToArray());
            }


            int[] resultSignal = new int[_audio.dataNormalized.Length];
            int totalStep = 0;

            foreach (float[] window in resultIFurier)
            {
                for (int i = 0; i < window.Length; i++)
                {
                    if (i + totalStep < resultSignal.Length)
                        resultSignal[i + totalStep] += (int)window[i];
                }

                totalStep += hopSize;
            }

            SoundUtil.SaveSound(_audio.fileName, 44100, resultComplex.Count(), resultSignal.ToList());

        }

        private void notCausal_Click(object sender, EventArgs e)
        {
            int windowSize;
            int hopSize;
            int filterSize;
            int fc;

            int.TryParse(this.WindowSize.Text, out windowSize);
            int.TryParse(this.R.Text, out hopSize);
            int.TryParse(this.L.Text, out filterSize);
            int.TryParse(this.Fc.Text, out fc);
            double[] windowData = new double[windowSize];

            //okno dla sygnału
            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFactors(windowSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFactors(windowSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFactors(windowSize);
                    break;
            }


            double[][] parts = SoundUtil.ChunkArrayWithHop(_audio.dataNormalized, windowSize, hopSize);
            double[][] winParts = new double[parts.Length][];
            for (int i = 0; i < parts.Length; i++)
            {
                winParts[i] = new double[parts[0].Length];
                for (int j = 0; j < parts[i].Length; j++)
                {
                    winParts[i][j] = parts[i][j] * windowData[j];

                }
                // winParts[i] = SoundUtil.MovedSignal(winParts[i], hopSize);
                winParts[i] = SoundUtil.AddZerosNotCasual(filterSize - 1, winParts[i]);
            }

            // okno dla filtru
            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFactors(filterSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFactors(filterSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFactors(filterSize);
                    break;
            }

            double[] filterData = SoundUtil.lowPassFilter(fc, 44100, windowData);
            filterData = SoundUtil.AddZerosNotCasual(windowSize - 1, filterData);
            #region filterChar
            CharWindow WindowCharm = new CharWindow();
            WindowCharm.setPropert();

            WindowCharm.Histogram.Series.Add("WindowCharm");
            WindowCharm.Text = "WindowCharm";
            WindowCharm.Histogram.Series["WindowCharm"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            WindowCharm.Histogram.Series["WindowCharm"].MarkerSize = 2;
            WindowCharm.Histogram.ChartAreas[0].AxisX.Title = "Index";
            WindowCharm.Histogram.ChartAreas[0].AxisY.Title = "Window value";

            for (int i = 0; i < filterData.Count(); i++)
            {
                WindowCharm.Histogram.Series["WindowCharm"].Points.AddXY(i, filterData[i]);
            }

            WindowCharm.Show();
            #endregion

            Complex[] filterComplex = SoundUtil.FFT(SoundUtil.SignalToComplex(filterData));
            Complex[] resultComplex = new Complex[filterComplex.Length];
            List<float[]> resultIFurier = new List<float[]>();
            foreach (double[] part in winParts)
            {
                List<float> resultPart = new List<float>();
                Complex[] signalPartComplex = SoundUtil.FFT(SoundUtil.SignalToComplex(part));

                for (int i = 0; i < part.Length; i++)
                    resultComplex[i] = signalPartComplex[i] * filterComplex[i];
                resultComplex = SoundUtil.IFFT(resultComplex);
                foreach (Complex complex in resultComplex)
                {
                    resultPart.Add((int)(complex.Real));
                }
                resultIFurier.Add(resultPart.ToArray());
            }


            float[] resultSignal = new float[_audio.dataNormalized.Length];
            int totalStep = 0;

            foreach (float[] window in resultIFurier)
            {
                for (int i = 0; i < window.Length; i++)
                {
                    if (i + totalStep < resultSignal.Length)
                        resultSignal[i + totalStep] += window[i];
                }

                totalStep += hopSize;
            }



            #region filterChar
            CharWindow resultSignalChar = new CharWindow();
            resultSignalChar.setPropert();

            resultSignalChar.Histogram.Series.Add("WindowCharm");
            resultSignalChar.Text = "SignalChar";
            resultSignalChar.Histogram.Series["WindowCharm"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            resultSignalChar.Histogram.Series["WindowCharm"].MarkerSize = 2;
            resultSignalChar.Histogram.ChartAreas[0].AxisX.Title = "Index";
            resultSignalChar.Histogram.ChartAreas[0].AxisY.Title = "signal value";

            for (int i = 0; i < resultSignal.Count(); i++)
            {
                resultSignalChar.Histogram.Series["WindowCharm"].Points.AddXY(i, resultSignal[i]);
            }

            resultSignalChar.Show();
            #endregion

            SoundUtil.SaveSound(_audio.fileName, _audio.sampleRate, resultSignal.ToList());
        }
    }
}
