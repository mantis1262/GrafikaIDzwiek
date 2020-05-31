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
using Sound.Model;
using Sounds.Helpers;
using System.IO;

namespace Sound
{
    public partial class CharWindow : Form
    {
        private AudioContainer _audio;
        private const int DEFAULT_CHUNK_SIZE = 44100;

        public CharWindow()
        {
            InitializeComponent();
            actionStateLabel.Text = "";
        }

        private void RefreshChart()
        {
            int[] soundData = SoundUtil.ExtractFirstChunk(_audio.data, _audio.chunkSize);
            int sampleRate = _audio.sampleRate;

            Histogram.Series.Clear();
            Histogram.Series.Add("Signal");
            this.Text = "Signal";
            Histogram.Series["Signal"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            Histogram.Series["Signal"].MarkerSize = 2;

            float[] time = new float[soundData.Length];
            float[] value = new float[soundData.Length];

            for (int i = 0; i < soundData.Count(); i++)
            {
                value[i] = (float)soundData[i] / (float)sampleRate;
                time[i] = i / sampleRate;
                Histogram.Series["Signal"].Points.AddXY(time[i], value[i]);
            }
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            actionStateLabel.Text = "PROCESSING";
            _audio = new AudioContainer();
            string path = PathHelper.GetSoundPath();
            if (path != string.Empty)
            {
                _audio.OpenWav(path);
                RefreshChart();
                pathLabel.Text = _audio.fileName;
                chunkSizeBox.Text = DEFAULT_CHUNK_SIZE.ToString();
            }
            actionStateLabel.Text = "";
        }

        private void applyChunkButton_Click(object sender, EventArgs e)
        {
           if (_audio != null)
            {
                actionStateLabel.Text = "PROCESSING";
                string chunkSizeText = chunkSizeBox.Text;
                int.TryParse(chunkSizeText, out int chunkSize);
                if (chunkSize > 0)
                {
                    _audio.chunkSize = chunkSize;
                    RefreshChart();
                }
                actionStateLabel.Text = "";
            }
        }

        private void autocorrelationButton_Click(object sender, EventArgs e)
        {
            if (_audio != null)
            {
                actionStateLabel.Text = "PROCESSING";
                List<int> frequencies = _audio.Autocorrelation();
                List<int> frequenciesFiltered = frequencies.Where(freq => freq > 0).Distinct().ToList();
                string freqText = "";

                foreach (int freq in frequenciesFiltered)
                {
                    freqText += freq.ToString() + ", ";
                }

                actionStateLabel.Text = "";
                SoundUtil.SaveSound(_audio.fileName, _audio.framesNumber, _audio.sampleRate, _audio.chunkSize, frequenciesFiltered);
                MessageBox.Show(freqText, "Autocorrelation frequencies");
            }
        }

        private void cepstrumButton_Click(object sender, EventArgs e)
        {
            if (_audio != null)
            {
                actionStateLabel.Text = "PROCESSING";
                List<int> frequencies = _audio.Cepstrum();
                List<int> frequenciesFiltered = frequencies.Where(freq => freq > 0).Distinct().ToList();
                string freqText = "";

                foreach (int freq in frequenciesFiltered)
                {
                    freqText += freq.ToString() + ", ";
                }

                actionStateLabel.Text = "";
                SoundUtil.SaveSound(_audio.fileName, _audio.framesNumber, _audio.sampleRate, _audio.chunkSize, frequenciesFiltered);
                MessageBox.Show(freqText, "Cepstrum frequencies");
            }
        }
    }
}
