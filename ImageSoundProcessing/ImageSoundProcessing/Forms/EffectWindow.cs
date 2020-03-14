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
                            Bitmap resultBitmap = new Bitmap(Effect.AritmeticMiddleFilter(_bitmap, minMaskSize));
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("aritmeticMiddleFilter");
                            form.SetControlProperties("slider", minMaskSize, maxMaskSize);
                            form.SetControlProperties("sliderLabel", minMaskSize);
                            form.Show();
                            Close();
                            break;
                        }
                    case "MedianFilter":
                        {
                            int minMaskSize = 9, maxMaskSize = 81;
                            Bitmap resultBitmap = new Bitmap(Effect.MedianFilter(_bitmap, minMaskSize));
                            ProcessedImageWindow form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.SetType("medianFilter");
                            form.SetControlProperties("slider", minMaskSize, maxMaskSize);
                            form.SetControlProperties("sliderLabel", minMaskSize);
                            form.Show();
                            Close();
                            break;
                        }
                    default:
                        {
                            break;
                        }
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
