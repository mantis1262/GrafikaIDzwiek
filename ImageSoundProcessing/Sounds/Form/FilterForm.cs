﻿using Sound;
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

            int.TryParse(this.WindowSize.Text,out windowSize);
            int.TryParse(this.R.Text,out hopSize);
            int.TryParse(this.L.Text,out filterSize);
            int.TryParse(this.Fc.Text,out fc);

            double[] windowData = new double[windowSize];
            int[][] parts = SoundUtil.ChunkArray(_audio.data, windowSize);



            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFunc(filterSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFunc(filterSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFunc(filterSize);
                    break;
            }
            
            double[] filterData = SoundUtil.lowPassFilter(fc, 44100, windowData);
            filterData = SoundUtil.AddZerosCasual(windowSize - 1, filterData);

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
                    windowData = SoundUtil.RectangularFunc(windowSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFunc(windowSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFunc(windowSize);
                    break;
            }


            int[][] parts = SoundUtil.ChunkArray(_audio.data, windowSize);
            double[][] winParts = new double[parts.Length][];
            for(int i = 0; i< parts.Length; i++)
            {
                winParts[i] = new double[parts[i].Length];
                for (int j = 0; j < parts[i].Length; j++)
                {
                    winParts[i][j] = parts[i][j] * windowData[j];
                    
                }
                winParts[i] = SoundUtil.AddZerosCasual(filterSize - 1, winParts[i]);
            }
   //         double[][] movedParts = SoundUtil.MovedSignal(winParts, hopSize);

            // okno dla filtru
            switch (windowType.SelectedIndex)
            {
                case 0:
                    windowData = SoundUtil.RectangularFunc(filterSize);
                    break;
                case 1:
                    windowData = SoundUtil.HannFunc(filterSize);
                    break;
                case 2:
                    windowData = SoundUtil.HammingFunc(filterSize);
                    break;
            }

            double[] filterData = SoundUtil.lowPassFilter(fc, 44100, windowData);
            filterData = SoundUtil.AddZerosNotCasual(windowSize - 1, filterData);

            CharWindow autoCorelation = new CharWindow();
            autoCorelation.setPropert();

            autoCorelation.Histogram.Series.Add("AutoCorelation");
            autoCorelation.Text = "Window";
            autoCorelation.Histogram.Series["AutoCorelation"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            autoCorelation.Histogram.Series["AutoCorelation"].MarkerSize = 2;
            autoCorelation.Histogram.ChartAreas[0].AxisX.Title = "Index";
            autoCorelation.Histogram.ChartAreas[0].AxisY.Title = "Window value";

            for (int i = 0; i < filterData.Count(); i++)
            {
                autoCorelation.Histogram.Series["AutoCorelation"].Points.AddXY(i,filterData[i]);
            }

            autoCorelation.Show();

            List<int> result = new List<int>();
            foreach(double[] part in winParts)
            {
                double  [] freq = SoundUtil.STFT(hopSize, filterData, part);
                Complex [] com = SoundUtil.ISTFT(SoundUtil.RectangularFunc(1), freq);
                foreach (Complex complex in com)
               {
                   result.Add((int)complex.Real);
               }
            }

            SoundUtil.SaveSound(_audio.fileName, _audio.framesNumber, _audio.sampleRate , windowSize, result);
        }
    }
}
