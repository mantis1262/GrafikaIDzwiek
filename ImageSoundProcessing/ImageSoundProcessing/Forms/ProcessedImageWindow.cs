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
        private Bitmap _bitmap;

        public ProcessedImageWindow()
        {
            InitializeComponent();
        }

        public void SetBitmap(Bitmap bitmap)
        {
            _bitmap = new Bitmap(bitmap);
            processedImage.Image = _bitmap;
        }

        private void ProcessedImageWindow_Load(object sender, EventArgs e)
        {

        }

        private void ProcessedImage_Click(object sender, EventArgs e)
        {

        }
    }
}
