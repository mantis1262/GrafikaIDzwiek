﻿using System;
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
using Sounds;

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

            Histogram.ChartAreas[0].AxisX.Title = "Time";
            Histogram.ChartAreas[0].AxisY.Title = "Sample value";
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
                string freqText = "";

                foreach (int freq in frequencies)
                {
                    freqText += freq.ToString() + " Hz (T = " + (1.0f / freq) + " s), ";
                }

                actionStateLabel.Text = "";
                CharWindow autoCorelation = new CharWindow();
                autoCorelation.setPropert();

                autoCorelation.Histogram.Series.Add("AutoCorelation");
                autoCorelation.Text = "AutoCorelation";
                autoCorelation.Histogram.Series["AutoCorelation"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                autoCorelation.Histogram.Series["AutoCorelation"].MarkerSize = 2;
                autoCorelation.Histogram.ChartAreas[0].AxisX.Title = "Index";
                autoCorelation.Histogram.ChartAreas[0].AxisY.Title = "AutoCorelation value";

                for (int i = 0; i < _audio.autoCorrelations[0].Count(); i++)
                {
                    autoCorelation.Histogram.Series["AutoCorelation"].Points.AddXY(i, _audio.autoCorrelations[0][i] / (32768.0f * 32768.0f * 100));
                }

                autoCorelation.Show();

                SoundUtil.SaveSound(_audio.fileName, _audio.sampleRate, _audio.chunkSize, frequencies);
                MessageBox.Show(freqText, "Autocorrelation frequencies");
            }
        }

        private void cepstrumButton_Click(object sender, EventArgs e)
        {
            if (_audio != null)
            {
                actionStateLabel.Text = "PROCESSING";
                List<int> frequencies = _audio.Cepstrum();
                string freqText = "";

                foreach (int freq in frequencies)
                {
                    freqText += freq.ToString() + " Hz (T = " + (1.0f / freq) + " s), ";
                }

                actionStateLabel.Text = "";

                CharWindow Cepstrum = new CharWindow();
                Cepstrum.setPropert();

                Cepstrum.Histogram.Series.Add("Cepstrum");
                Cepstrum.Text = "Cepstrum";
                Cepstrum.Histogram.Series["Cepstrum"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                Cepstrum.Histogram.Series["Cepstrum"].MarkerSize = 2;
                Cepstrum.Histogram.ChartAreas[0].AxisX.Title = "Index";
                Cepstrum.Histogram.ChartAreas[0].AxisY.Title = "Cepstrum value";

                for (int i = 0; i < _audio.cepstrum.Count() / 2; i++)
                {
                    Cepstrum.Histogram.Series["Cepstrum"].Points.AddXY(i, _audio.cepstrum[i].Modulus());
                }

                Cepstrum.Show();

                CharWindow Spectrum = new CharWindow();
                Spectrum.setPropert();

                Spectrum.Histogram.Series.Add("Spectrum");
                Spectrum.Text = "Spectrum";
                Spectrum.Histogram.Series["Spectrum"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                Spectrum.Histogram.Series["Spectrum"].MarkerSize = 2;
                Spectrum.Histogram.ChartAreas[0].AxisX.Title = "Index";
                Spectrum.Histogram.ChartAreas[0].AxisY.Title = "Spectrum value";


                float[] time = new float[_audio.fftDataForSpectrumChart.Length];

                for (int i = 0; i < _audio.fftDataForSpectrumChart.Count() / 2; i++)
                {
                    time[i] = i / _audio.sampleRate;

                    Spectrum.Histogram.Series["Spectrum"].Points.AddXY(i, _audio.fftDataForSpectrumChart[i].Modulus());
                }

                Spectrum.Show();

                SoundUtil.SaveSound(_audio.fileName, _audio.sampleRate, _audio.chunkSize, frequencies);
                MessageBox.Show(freqText, "Cepstrum frequencies");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm(_audio);
            filterForm.Show();
        }

        public void setPropert()
        {
            applyChunkButton.Visible = false;
            autocorrelationButton.Visible = false;
            cepstrumButton.Visible = false;
            label1.Visible = false;
            pathLabel.Visible = false;
            chunkSizeBox.Visible = false;
            loadFileButton.Visible = false;
            button1.Visible = false;


        }
    }
}
