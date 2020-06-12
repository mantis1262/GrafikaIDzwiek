using Sound.Model;
using Sounds.Helpers;
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
            filterData = SoundUtil.AddZerosNotCasual(windowSize - 1, filterData);
        }
    }
}
