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
        public static Form CreateProcessedImageForm(Bitmap bitmap)
        {
            ProcessedImageWindow form = new ProcessedImageWindow();
            form.SetBitmap(bitmap);
            return form;
        }

        public static Form CreateEffectForm(Bitmap bitmap)
        {
            EffectWindow form = new EffectWindow();
            form.ImageBitmap = bitmap;
            return form;
        }
    }
}
