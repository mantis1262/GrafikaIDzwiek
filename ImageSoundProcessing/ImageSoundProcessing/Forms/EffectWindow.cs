using ImageSoundProcessing.Factories;
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
                switch(item.Text)
                {
                    case "Brightness":
                        {
                            Bitmap resultBitmap = new Bitmap(_bitmap);
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("brightness");
                            form.SetControlProperties("slider", -255, 255);
                            form.SetControlProperties("sliderLabel", 0);
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

                                int[][] value = Effect.Histogram(_bitmap, 0);
                                if(value[0].SequenceEqual(value[1]) == false && value[0].SequenceEqual(value[2]) == false)
                            {
                                CharWindow formR = FormFactory.CreateCharForm(value[0]);
                                formR.Name = "ValueR";
                                formR.Histogram.Series[0].Name = "valueR";
                                formR.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                                CharWindow formG = FormFactory.CreateCharForm(value[1]);
                                formG.Name = "ValueG";
                                formG.Histogram.Series[0].Name = "valueG";
                                formG.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                                CharWindow formB = FormFactory.CreateCharForm(value[2]);
                                formB.Name = "ValueB";
                                formB.Histogram.Series[0].Name = "valueB";
                                formB.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                                formR.Show();
                                formG.Show();
                                formB.Show();
                            }
                            else
                            {
                                CharWindow form = FormFactory.CreateCharForm(value[2]);
                                form.Name = "ValueLight";
                                form.Histogram.Series[0].Name = "ValueLight";
                                form.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                                form.Show();
                            }
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
