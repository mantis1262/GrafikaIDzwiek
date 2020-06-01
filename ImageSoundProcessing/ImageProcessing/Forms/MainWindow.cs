using ImageSoundProcessing.Factories;
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
    public partial class MainWindow : Form
    {
        private string _imagePath;
        private Bitmap _bitmap;

        public string ImagePath { get => _imagePath; set => _imagePath = value; }

        public Bitmap Bitmap { get => _bitmap; set => _bitmap = value; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void OriginalImage_Click(object sender, EventArgs e)
        {

        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            string imagePath = Path.GetImagePath();
            if (!imagePath.Equals(""))
            {
                _imagePath = imagePath;
                _bitmap = BitmapFactory.CreateBitmap(imagePath);
                _originalImage.Image = _bitmap;
            }
        }

        private void EffectButton_Click(object sender, EventArgs e)
        {
            if (_bitmap != null)
            {
                Form form = FormFactory.CreateEffectForm(_bitmap);
                form.Show();
            }
        }
    }
}
