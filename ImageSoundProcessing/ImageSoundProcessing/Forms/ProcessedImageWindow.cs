using ImageSoundProcessing.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageSoundProcessing
{
    public partial class ProcessedImageWindow : Form
    {
        private string _type;
        private Bitmap _originalBitmap;
        private Bitmap _processedBitmap;

        public ProcessedImageWindow()
        {
            InitializeComponent();
        }

        public void SetType(string type)
        {
            _type = type;
        }

        public void SetOriginalBitmap(Bitmap bitmap)
        {
            _originalBitmap = new Bitmap(bitmap);
        }

        public void SetProcessedBitmap(Bitmap bitmap)
        {
            _processedBitmap = new Bitmap(bitmap);
            processedImage.Image = _processedBitmap;
        }

        public void SetControlProperties(string name, params int[] args)
        {
            switch(name)
            {
                case "slider":
                    {
                        factorSlider.Visible = true;
                        factorSlider.Minimum = args[0];
                        factorSlider.Maximum = args[1];
                        break;
                    }
                case "sliderLabel":
                    {
                        factorLabel.Visible = true;
                        factorLabel.Text = args[0].ToString();
                        break;
                    }
                default: break;
            }
        }

        private void ProcessedImageWindow_Load(object sender, EventArgs e)
        {

        }

        private void ProcessedImage_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void FactorSlider_Scroll(object sender, EventArgs e)
        {
            SetControlProperties("sliderLabel", factorSlider.Value);
            Bitmap bitmap = null;
            switch (_type)
            {
                case "brightness":
                    {
                        bitmap = Effect.Brightness(_originalBitmap, factorSlider.Value);
                        break;
                    }
                case "contrast":
                    {
                        bitmap = Effect.Contrast(_originalBitmap, factorSlider.Value);
                        break;
                    }
                case "aritmeticMiddleFilter":
                    {
                        bitmap = Effect.AritmeticMiddleFilter(_originalBitmap, factorSlider.Value);
                        break;
                    }
                case "medianFilter":
                    {
                        bitmap = Effect.MedianFilter(_originalBitmap, factorSlider.Value);
                        break;
                    }
                default: break;
            }
            if (bitmap != null)
            {
                SetProcessedBitmap(bitmap);
            }
        }
    }
}
