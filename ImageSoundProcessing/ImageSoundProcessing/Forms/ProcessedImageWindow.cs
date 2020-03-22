using ImageSoundProcessing.Helpers;
using Microsoft.VisualBasic;
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

        public void SetControlProperties(string name, params Object[] args)
        {
            switch (name)
            {
                case "slider":
                    {
                        factorSlider.Visible = true;
                        factorSlider.Minimum = (int)args[0];
                        factorSlider.Maximum = (int)args[1];
                        break;
                    }
                case "sliderLabel":
                    {
                        factorLabel.Visible = true;
                        factorLabel.Text = args[0].ToString();
                        break;
                    }

                case "slider2":
                    {
                        factorSlider2.Visible = true;
                        factorSlider2.Minimum = (int)args[0];
                        factorSlider2.Maximum = (int)args[1];
                        break;
                    }
                case "sliderLabel2":
                    {
                        factorLabel2.Visible = true;
                        factorLabel2.Text = args[0].ToString();
                        break;
                    }

                case "sliderName1":
                    {

                        SliderName1.Visible = true;
                        SliderName1.Text = args[0].ToString();
                        break;
                    }
                case "sliderName2":
                    {

                        SliderName2.Visible = true;
                        SliderName2.Text = args[0].ToString();
                        break;
                    }
                case "histogramButton":
                    {
                        histogram.Visible = true;
                        break;
                    }

                case "maskButton":
                    {
                        maskButton.Visible = true;
                        break;
                    }
                case "maskLabel":
                    {
                        maskLabel.Visible = true;
                        if ((int)args[0] == 0)
                        {
                            maskLabel.Text = "None";
                        }
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
                case "modifyHistogram":
                    {
                        bitmap = Effect.ModifiHistogram(_originalBitmap, factorSlider.Value, factorSlider2.Value);
                        this.SetControlProperties("slider", 1, this.factorSlider2.Value);
                        this.SetControlProperties("slider2", this.factorSlider.Value, 255);
                        SetControlProperties("sliderLabel", factorSlider.Value);
                        SetControlProperties("sliderLabel2", factorSlider2.Value);

                        break;
                    }
                default: break;
            }
            if (bitmap != null)
            {
                SetProcessedBitmap(bitmap);
            }
        }

        private void MaskButton_Click(object sender, EventArgs e)
        {
            string previousMaskLabelText = maskLabel.Text;
            maskLabel.Text = "...";
            string textValue = Interaction.InputBox("Enter value", "Mask size", "", 400, 400);
            if (!textValue.Equals(""))
            {
                int parseResult;
                if (int.TryParse(textValue, out parseResult))
                {
                    if (parseResult == 0)
                    {
                        maskLabel.Text = "None";
                        SetProcessedBitmap(_originalBitmap);
                    }
                    else
                    {
                        double result = Math.Sqrt(parseResult);
                        bool isSquare = result % 1 == 0;

                        if (parseResult > 1 && isSquare)
                        {
                            switch (_type)
                            {
                                case "aritmeticMiddleFilter":
                                    {
                                        Bitmap resultBitmap = Effect.AritmeticMiddleFilter(_originalBitmap, parseResult);
                                        SetProcessedBitmap(resultBitmap);
                                        break;
                                    }
                                case "medianFilter":
                                    {
                                        Bitmap resultBitmap = Effect.MedianFilter(_originalBitmap, parseResult);
                                        SetProcessedBitmap(resultBitmap);
                                        break;
                                    }
                               default: break;
                            }

                            maskLabel.Text = parseResult.ToString();
                        }
                        else
                        {
                            maskLabel.Text = previousMaskLabelText;
                            MessageBox.Show("Only perfect squares higher than 1 are allowed", "Incorrect value !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                maskLabel.Text = previousMaskLabelText;
                MessageBox.Show("Please enter the value", "Incorrect value !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MaskLabel_Click(object sender, EventArgs e)
        {

        }

        private void histogram_Click(object sender, EventArgs e)
        {
            Effect.ShowHistogram(Effect.Histogram(_processedBitmap));
        }
    }
}
