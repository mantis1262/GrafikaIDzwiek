using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageSoundProcessing.Helpers
{
    public static class Path
    {
        public static string GetImagePath()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Image Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "bmp",
                Filter =
                    "BMP (*.bmp)|*.bmp|" +
                    "JPG (*.jpg)|*.jpg|" +
                    "JPEG (*.jpeg)|*.jpeg|" +
                    "JPE (*.jpe)|*.jpe|" +
                    "TIFF (*.tiff)|*.tiff|" +
                    "PNG (*.png)|*.png|" +
                    "All images (*.bmp, *.jpg, *.jpeg, *.jpe, *.tiff,*.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *tiff; *.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

        public static string GetSaveImagePath()
        {
            string path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Choose destination",
                CheckPathExists = true,
                DefaultExt = "bmp",
                Filter =
                    "BMP (*.bmp)|*.bmp|" +
                    "JPG (*.jpg)|*.jpg|" +
                    "JPEG (*.jpeg)|*.jpeg|" +
                    "JPE (*.jpe)|*.jpe|" +
                    "TIFF (*.tiff)|*.tiff|" +
                    "PNG (*.png)|*.png|" +
                    "All images (*.bmp, *.jpg, *.jpeg, *.jpe, *.tiff,*.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *tiff; *.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }
            return path;
        }
    }
}
