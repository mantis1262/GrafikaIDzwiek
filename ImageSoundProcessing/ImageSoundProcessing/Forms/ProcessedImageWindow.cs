using ImageSoundProcessing.Helpers;
using ImageSoundProcessing.Model;
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
        private Complex[][] _originalComplexData;
        private Complex[][] _processedComplexData;

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
                case "powerSpectrumButtom":
                    {
                        powerSpectrumButton.Visible = true;
                        break;
                    }
                case "phaseSpectrumButton":
                    {
                        phaseSpectrumButton.Visible = true;
                        break;
                    }
                case "lowPassFilterButton":
                    {
                        lowPassFilterButton.Visible = true;
                        break;
                    }
                case "highPassFilterButton":
                    {
                        highPassFilterButton.Visible = true;
                        break;
                    }
                case "bandPassFilterButton":
                    {
                        bandPassFilterButton.Visible = true;
                        break;
                    }
                case "bandCutFilterButton":
                    {
                        bandCutFilterButton.Visible = true;
                        break;
                    }
                case "highPassEdgeDetectionFilterButton":
                    {
                        highPassEdgeDetectionFilterButton.Visible = true;
                        break;
                    }
                case "phaseSpectrumFilterButton":
                    {
                        phaseSpectrumFilterButton.Visible = true;
                        break;
                    }
                case "regionSplittingAndMergingSegmentation":
                    {
                        regionSplittingAndMergingSegmentation.Visible = true;
                        break;
                    }
                case "filterRangeLabel":
                    {
                        fourierFilterLabel.Visible = true;
                        break;
                    }
                case "filterRangeLabel2":
                    {
                        fourierFilterLabel2.Visible = true;
                        break;
                    }
                case "kComponentLabel":
                    {
                        kComponentLabel.Visible = true;
                        break;
                    }
                case "lComponentLabel":
                    {
                        lComponentLabel.Visible = true;
                        break;
                    }
                case "thresholdLabel":
                    {
                        thresholdLabel.Visible = true;
                        break;
                    }
                case "minimumPixelsForRegionLabel":
                    {
                        minimumPixelsForRegionLabel.Visible = true;
                        break;
                    }
                case "filterRangeTextBox":
                    {
                        rangeTextBox.Visible = true;
                        break;
                    }
                case "filterRangeTextBox2":
                    {
                        rangeTextBox2.Visible = true;
                        break;
                    }
                case "kComponentTextBox":
                    {
                        kComponentTextBox.Visible = true;
                        break;
                    }
                case "lComponentTextBox":
                    {
                        lComponentTextBox.Visible = true;
                        break;
                    }
                case "thresholdTextBox":
                    {
                        thresholdTextBox.Visible = true;
                        break;
                    }
                case "minPixelsTextBox":
                    {
                        minPixelsTextBox.Visible = true;
                        break;
                    }
                default: break;
            }
        }

        public void SetOriginalComplexData(Complex[][] complex)
        {
            _originalComplexData = complex;
        }

        public void SetProcessedComplexData(Complex[][] complex)
        {
            _processedComplexData = complex;
        }

        private void ProcessedImageWindow_Load(object sender, EventArgs e)
        {

        }

        private void ProcessedImage_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
          string savePath = Path.GetSaveImagePath();
            if (savePath != null && savePath != " ")
                processedImage.Image.Save(savePath);
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
                                        Bitmap resultBitmap = Effect.AritmeticMiddleFilter(_originalBitmap, (int)result);
                                        SetProcessedBitmap(resultBitmap);
                                        break;
                                    }
                                case "medianFilter":
                                    {
                                        Bitmap resultBitmap = Effect.MedianFilter(_originalBitmap, (int)result);
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

        private void FourierFilterLabel_Click(object sender, EventArgs e)
        {

        }

        private void RangeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FourierFilterLabel2_Click(object sender, EventArgs e)
        {

        }

        private void RangeTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void KComponentLabel_Click(object sender, EventArgs e)
        {

        }

        private void LComponentLabel_Click(object sender, EventArgs e)
        {

        }

        private void KComponentTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LComponentTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThresholdLabel_Click(object sender, EventArgs e)
        {

        }

        private void MinimumPixelsForRegionLabel_Click(object sender, EventArgs e)
        {

        }

        private void ThresholdTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MinPixelsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PowerSpectrumButton_Click(object sender, EventArgs e)
        {
            Bitmap resultBitmap = Effect.GetSpectrumBitmap(_processedComplexData, "magnitude");
            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
            form.SetProcessedBitmap(resultBitmap);
            form.SetType("fourierPowerSpectrum");
            form.Show();
        }

        private void PhaseSpectrumButton_Click(object sender, EventArgs e)
        {
            Bitmap resultBitmap = Effect.GetSpectrumBitmap(_processedComplexData, "phase");
            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
            form.SetProcessedBitmap(resultBitmap);
            form.SetType("fourierPhaseSpectrum");
            form.Show();
        }

        private void LowPassFilterButton_Click(object sender, EventArgs e)
        {
            string rangeText = rangeTextBox.Text;
            if (!string.IsNullOrEmpty(rangeText))
            {
                int range = int.Parse(rangeText);
                if (range >= Colors.MIN_PIXEL_VALUE && range <= Colors.MAX_PIXEL_VALUE)
                {
                    Complex[][] filteredData = FourierUtil.LowPassFilter(_originalComplexData, range);
                    _processedComplexData = filteredData;
                }
                else
                {
                    _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
                }
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        

        private void HighPassFilterButton_Click(object sender, EventArgs e)
        {
            string rangeText = rangeTextBox.Text;
            if (!string.IsNullOrEmpty(rangeText))
            {
                int range = int.Parse(rangeText);
                if (range >= Colors.MIN_PIXEL_VALUE && range <= Colors.MAX_PIXEL_VALUE)
                {
                    Complex[][] filteredData = FourierUtil.HighPassFilter(_originalComplexData, range);
                    _processedComplexData = filteredData;
                }
                else
                {
                    _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
                }
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        private void BandPassFilterButton_Click(object sender, EventArgs e)
        {
            string minRangeText = rangeTextBox.Text;
            string maxRangeText = rangeTextBox2.Text;
            if (!string.IsNullOrEmpty(minRangeText) && !string.IsNullOrEmpty(maxRangeText))
            {
                int minRange = int.Parse(minRangeText);
                int maxRange = int.Parse(maxRangeText);
                if (minRange >= Colors.MIN_PIXEL_VALUE && minRange <= Colors.MAX_PIXEL_VALUE &&
                    maxRange >= Colors.MIN_PIXEL_VALUE && maxRange <= Colors.MAX_PIXEL_VALUE)
                {
                    Complex[][] filteredData = FourierUtil.BandPassFilter(_originalComplexData, minRange, maxRange);
                    _processedComplexData = filteredData;
                }
                else
                {
                    _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
                }
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        private void BandCutFilterButton_Click(object sender, EventArgs e)
        {
            string minRangeText = rangeTextBox.Text;
            string maxRangeText = rangeTextBox2.Text;
            if (!string.IsNullOrEmpty(minRangeText) && !string.IsNullOrEmpty(maxRangeText))
            {
                int minRange = int.Parse(minRangeText);
                int maxRange = int.Parse(maxRangeText);
                if (minRange >= Colors.MIN_PIXEL_VALUE && minRange <= Colors.MAX_PIXEL_VALUE &&
                    maxRange >= Colors.MIN_PIXEL_VALUE && maxRange <= Colors.MAX_PIXEL_VALUE)
                {
                    Complex[][] filteredData = FourierUtil.BandCutFilter(_originalComplexData, minRange, maxRange);
                    _processedComplexData = filteredData;
                }
                else
                {
                    _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
                }
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        private void HighPassEdgeDetectionFilterButton_Click(object sender, EventArgs e)
        {
            string rangeText = rangeTextBox.Text;
            if (!string.IsNullOrEmpty(rangeText))
            {
                int range = int.Parse(rangeText);
                if (range >= Colors.MIN_PIXEL_VALUE && range <= Colors.MAX_PIXEL_VALUE)
                {
                    Complex[][] filteredData = FourierUtil.HighPassEdgeDetectionFilter(_originalComplexData, range);
                    _processedComplexData = filteredData;
                }
                else
                {
                    _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
                }
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        private void PhaseSpectrumFilterButton_Click(object sender, EventArgs e)
        {
            string kComponentText = kComponentTextBox.Text;
            string lComponentText = lComponentTextBox.Text;
            if (!string.IsNullOrEmpty(kComponentText) && !string.IsNullOrEmpty(lComponentText))
            {
                int kComponent = int.Parse(kComponentText);
                int lComponent = int.Parse(lComponentText);

                Complex[][] filteredData = FourierUtil.PhaseSpectrumFilter(_originalComplexData, kComponent, lComponent);
                _processedComplexData = filteredData;
            }
            else
            {
                _processedComplexData = FourierUtil.CopyComplexArray(_originalComplexData);
            }

            Bitmap resultBitmap = Effect.IfftTransform(_processedComplexData);
            SetProcessedBitmap(resultBitmap);
        }

        private void RegionSplittingAndMergingSegmentation_Click(object sender, EventArgs e)
        {
            string thresholdText = thresholdTextBox.Text;
            string minPixelsForRegionText = minPixelsTextBox.Text;
            if (!string.IsNullOrEmpty(thresholdText) && !string.IsNullOrEmpty(minPixelsForRegionText))
            {
                int threshold = int.Parse(thresholdText);
                int minPixelsForRegion = int.Parse(minPixelsForRegionText);

                Bitmap resultBitmap = Effect.SegmentationRegionSplittingAndMerging(_originalBitmap, threshold, minPixelsForRegion);
                SetProcessedBitmap(resultBitmap);
            }
            else
            {
                SetProcessedBitmap(_originalBitmap);
            }

            
            
        }
    }
}
