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

           float[] result = _audio.FrequencyFiltration(windowSize, filterSize, fc, hopSize, windowType.SelectedIndex, "casual");

            #region filterChar
            CharWindow resultSignalChar = new CharWindow();
            resultSignalChar.setPropert();

            resultSignalChar.Histogram.Series.Add("WindowCharm");
            resultSignalChar.Text = "SignalChar";
            resultSignalChar.Histogram.Series["WindowCharm"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            resultSignalChar.Histogram.Series["WindowCharm"].MarkerSize = 2;
            resultSignalChar.Histogram.ChartAreas[0].AxisX.Title = "Index";
            resultSignalChar.Histogram.ChartAreas[0].AxisY.Title = "signal value";

            for (int i = 0; i < result.Count(); i++)
            {
                resultSignalChar.Histogram.Series["WindowCharm"].Points.AddXY(i, result[i]);
            }

            resultSignalChar.Show();
            #endregion

            //SoundUtil.SaveSound(_audio.fileName, 44100, resultComplex.Count(), resultSignal.ToList());

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

            float[] result = _audio.FrequencyFiltration(windowSize, filterSize, fc, hopSize, windowType.SelectedIndex, "notCasual");

            #region filterChar
            CharWindow resultSignalChar = new CharWindow();
            resultSignalChar.setPropert();

            resultSignalChar.Histogram.Series.Add("WindowCharm");
            resultSignalChar.Text = "SignalChar";
            resultSignalChar.Histogram.Series["WindowCharm"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            resultSignalChar.Histogram.Series["WindowCharm"].MarkerSize = 2;
            resultSignalChar.Histogram.ChartAreas[0].AxisX.Title = "Index";
            resultSignalChar.Histogram.ChartAreas[0].AxisY.Title = "signal value";

            for (int i = 0; i < result.Count(); i++)
            {
                resultSignalChar.Histogram.Series["WindowCharm"].Points.AddXY(i, result[i]);
            }

            resultSignalChar.Show();
            #endregion

            //SoundUtil.SaveSound(_audio.fileName, 44100, resultComplex.Count(), resultSignal.ToList());
        }
    }
}
