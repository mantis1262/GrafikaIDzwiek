using ImageSoundProcessing.Helpers;
using ImageSoundProcessing.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Factories
{
    public static class BitmapFactory
    {
        public static Bitmap CreateBitmap(string path)
        {
            Bitmap image = (Bitmap)Image.FromFile(path);
            return image;
        }

        public static Complex[,] BitmapToComplex2D(Bitmap bmp)
        {
            if (Image.GetPixelFormatSize(bmp.PixelFormat) != 8)
            {
                throw new ArgumentException("Only 8 bpp images are supported.");
            }

            LockBitmap bitmapLock = new LockBitmap(bmp);
            Complex[,] comp = new Complex[bmp.Width, bmp.Height];

            bitmapLock.LockBits(ImageLockMode.ReadOnly);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    comp[x, y] = new Complex(bitmapLock.GetPixel(x, y).R, 0);
                }
            }
            bitmapLock.UnlockBits();

            return comp;
        }
    }
}
