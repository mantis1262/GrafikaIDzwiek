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
                            string textValue = Interaction.InputBox("Enter value", "Factor", "", 100, 100);
                            if (!textValue.Equals(""))
                            {
                                int parseResult;
                                if (int.TryParse(textValue, out parseResult))
                                {
                                    Bitmap resultBitmap = Effect.Brightness(_bitmap, parseResult);
                                    Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                                    form.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter correct value", "Incorrect value !",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        }
                    case "Contrast":
                        {
                            string textValue = Interaction.InputBox("Enter value", "Factor", "", 100, 100);
                            if (!textValue.Equals(""))
                            {
                                int parseResult;
                                if (int.TryParse(textValue, out parseResult))
                                {
                                    Bitmap resultBitmap = Effect.Contrast(_bitmap, parseResult);
                                    Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                                    form.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter correct value", "Incorrect value !",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        }
                    case "Negative":
                        {
                            Bitmap resultBitmap = Effect.Negative(_bitmap);
                            Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        }
                    case "GrayMode":
                        {
                            Bitmap resultBitmap = Effect.GrayMode(_bitmap);
                            Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                            form.Show();
                            Close();
                            break;
                        } 
                    case "ArtmeticMiddleFilter":
                        {
                            string textValue = Interaction.InputBox("Enter value", "Factor", "", 100, 100);
                            if (!textValue.Equals(""))
                            {
                                int parseResult;
                                if (int.TryParse(textValue, out parseResult))
                                {
                                    Bitmap resultBitmap = Effect.ArtmeticMiddleFilter(_bitmap, parseResult);
                                    Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                                    form.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter correct value", "Incorrect value !",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        }
                    case "MedianFilter":
                        {

                            string textValue = Interaction.InputBox("Enter value", "Factor", "", 100, 100);
                            if (!textValue.Equals(""))
                            {
                                int parseResult;
                                if (int.TryParse(textValue, out parseResult))
                                {
                                    Bitmap resultBitmap = Effect.ArtmeticMiddleFilter(_bitmap, parseResult);
                                    Form form = FormFactory.CreateProcessedImageForm(resultBitmap);
                                    form.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter correct value", "Incorrect value !",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
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
