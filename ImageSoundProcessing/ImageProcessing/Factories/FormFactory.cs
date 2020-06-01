using ImageSoundProcessing.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageSoundProcessing
{
    public static class FormFactory
    {
        public static ProcessedImageWindow CreateProcessedImageForm(Bitmap bitmap)
        {
            ProcessedImageWindow form = new ProcessedImageWindow();
            form.SetOriginalBitmap(bitmap);
            form.SetProcessedBitmap(bitmap);
            return form;
        }

        public static EffectWindow CreateEffectForm(Bitmap bitmap)
        {
            EffectWindow form = new EffectWindow();
            form.ImageBitmap = bitmap;
            return form;
        }

        public static CharWindow CreateCharForm(int[] value)
        {
            CharWindow form = new CharWindow();
            form.Histogram.Series.Clear();
            form.Histogram.Series.Add("Value");
            for (int i = 0; i < 256; i++)
                form.Histogram.Series["Value"].Points.AddXY(i,value[i]);
            return form;
        }
    }
}
