using ImageSoundProcessing.Factories;
using ImageSoundProcessing.Helpers;
using ImageSoundProcessing.Model;
using Microsoft.VisualBasic;
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

namespace ImageSoundProcessing.Forms
{
    public partial class EffectWindow : Form
    {
        private Bitmap _bitmap;

        public EffectWindow()
        {
            InitializeComponent();
        }

        public Bitmap ImageBitmap { set => _bitmap = value; }

        private void Effect_Load(object sender, EventArgs e)
        {

        }

        private void ChooseEffectButton_Click(object sender, EventArgs e)
        {
            if (effectsList.SelectedItems.Count > 0)
            {
                ListViewItem item = effectsList.SelectedItems[0];
                switch (item.Text)
                {
                    case "Brightness":
                        {
                            Bitmap resultBitmap = new Bitmap(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("brightness");
                            form.SetControlProperties("slider", -255, 255);
                            form.SetControlProperties("sliderLabel", 0);
                            form.SetControlProperties("sliderName1", "brightness");
                            form.Show();
                            Close();
                            break;
                        }
                    case "Contrast":
                        {
                            Bitmap resultBitmap = new Bitmap(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("contrast");
                            form.SetControlProperties("slider", -255, 255);
                            form.SetControlProperties("sliderLabel", 0);
                            form.SetControlProperties("sliderName1", "contrast");
                            form.Show();
                            Close();
                            break;
                        }
                    case "Negative":
                        {
                            Bitmap resultBitmap = Effect.Negative(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("negative");
                            form.Show();
                            Close();
                            break;
                        }
                    case "GrayMode":
                        {
                            Bitmap resultBitmap = Effect.GrayMode(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("grayMode");
                            form.Show();
                            Close();
                            break;
                        }
                    case "AritmeticMiddleFilter":
                        {
                            int minMaskSize = 9, maxMaskSize = 81;
                            Bitmap resultBitmap = new Bitmap(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("aritmeticMiddleFilter");
                            form.SetControlProperties("maskButton");
                            form.SetControlProperties("maskLabel", 0);
                            form.Show();
                            Close();
                            break;
                        }
                    case "MedianFilter":
                        {
                            int minMaskSize = 9, maxMaskSize = 81;
                            Bitmap resultBitmap = new Bitmap(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("medianFilter");
                            form.SetControlProperties("maskButton");
                            form.SetControlProperties("maskLabel", 0);
                            form.Show();
                            Close();
                            break;
                        }
                    case "Histogram":
                        {
                            //H5 S4 O6//

                            int[][] value = Effect.Histogram(_bitmap);
                            Effect.ShowHistogram(value);
                            Close();
                            break;
                        }
                    case "ModifyHistogram":
                        {
                            int min = 1, max = 255;
                            Bitmap resultBitmap = Effect.ModifiHistogram(_bitmap, min, max);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("modifyHistogram");
                            form.SetControlProperties("histogramButton");
                            form.SetControlProperties("slider", min, max);
                            form.SetControlProperties("sliderLabel", 1);
                            form.SetControlProperties("slider2", min, max);
                            form.SetControlProperties("sliderLabel2", 255);

                            form.SetControlProperties("sliderName1", "min");
                            form.SetControlProperties("sliderName2", "max");
                            form.Show();

                            Close();
                            break;
                        }
                    case "SouthFilter":
                        {
                            Stopwatch time = new Stopwatch();
                            time.Start();
                            Bitmap resultBitmap = Effect.lineFilter(_bitmap, 0);
                            time.Stop();
                            MessageBox.Show(time.Elapsed.ToString());
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "SouthStatic":
                        {
                            Stopwatch time = new Stopwatch();
                            time.Start();
                            Bitmap resultBitmap = Effect.southFilter(_bitmap);
                            time.Stop();
                            MessageBox.Show(time.Elapsed.ToString());
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "SouthWestFilter":
                        {
                            Bitmap resultBitmap = Effect.lineFilter(_bitmap, 1);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "WestFilter":
                        {
                            Bitmap resultBitmap = Effect.lineFilter(_bitmap, 2);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "NorthWestFilter":
                        {
                            Bitmap resultBitmap = Effect.lineFilter(_bitmap, 3);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "UolisaFilter":
                        {
                            Bitmap resultBitmap = Effect.uolisaFilter(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "FFT":
                        {
                            Complex[][] complexData = Effect.FftTransform(_bitmap);
                            Bitmap resultBitmap = Effect.IfftTransform(complexData);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetControlProperties("powerSpectrumButtom");
                            form.SetControlProperties("phaseSpectrumButton");
                            form.SetControlProperties("lowPassFilterButton");
                            form.SetControlProperties("highPassFilterButton");
                            form.SetControlProperties("bandPassFilterButton");
                            form.SetControlProperties("bandCutFilterButton");
                            form.SetControlProperties("highPassEdgeDetectionFilterButton");
                            form.SetControlProperties("phaseSpectrumFilterButton");
                            form.SetControlProperties("regionSplittingAndMergingSegmentation");
                            form.SetControlProperties("filterRangeLabel");
                            form.SetControlProperties("filterRangeLabel2");
                            form.SetControlProperties("kComponentLabel");
                            form.SetControlProperties("lComponentLabel");
                            form.SetControlProperties("thresholdLabel");
                            form.SetControlProperties("minimumPixelsForRegionLabel");
                            form.SetControlProperties("filterRangeTextBox");
                            form.SetControlProperties("filterRangeTextBox2");
                            form.SetControlProperties("kComponentTextBox");
                            form.SetControlProperties("lComponentTextBox");
                            form.SetControlProperties("thresholdTextBox");
                            form.SetControlProperties("minPixelsTextBox");
                            form.SetOriginalComplexData(complexData);
                            form.SetProcessedComplexData(complexData);
                            form.Show();
                            Close();
                            break;
                        }
                    default: break;
                }
            }
            else
            {
                MessageBox.Show("Please choose effect", "Nothing chosen !",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
        private void effectsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
