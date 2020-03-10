using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Helpers
{
    public static class Effect
    {
        public static Bitmap Brightness(Bitmap original, int factor)
        {
            Bitmap result = new Bitmap(original.Width,original.Height);

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    Color temp = original.GetPixel(i, j);
                    int R = temp.R + factor;
                    int G = temp.G + factor;
                    int B = temp.B + factor;
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    result.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                }

                    return result;
        }

        public static Bitmap Contrast(Bitmap original, int factor)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    Color temp = original.GetPixel(i, j);
                    int R = factor * (temp.R - 128) + 128;
                    int G = factor * (temp.G - 128) + 128;
                    int B = factor * (temp.B - 128) + 128;
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    result.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                }

            return result;
        }

        public static Bitmap Negative(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width,original.Height);

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    Color temp = original.GetPixel(i, j);
                    int R = 255 - temp.R;
                    int G = 255 - temp.G;
                    int B = 255 - temp.B;
                    result.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                }

            return result;
        }  
        
        public static Bitmap GrayMode(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width,original.Height);

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    Color temp = original.GetPixel(i, j);
                    int gray = (int)(0.299 * temp.R + 0.587 * temp.G + 0.114 * temp.B);
                    result.SetPixel(i, j, Color.FromArgb(temp.A, (byte)gray, (byte)gray, (byte)gray));
                }

            return result;
        }

        public static Bitmap ArtmeticMiddleFilter(Bitmap original, int MaskSize)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            int midIndex;
            if (MaskSize % 2 == 0) midIndex = (MaskSize + 1) / 2;
            else midIndex = MaskSize / 2;


            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    int tempR = 0;
                    int tempG = 0;
                    int tempB = 0;
                    int x, y;
                    int o = 0;

                    for (int n = -midIndex; n <= midIndex; n++)
                    {
                        x = i + n;
                        if (x < 0) x = 0;
                        if (x >= original.Width) x = original.Width - 1;

                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            y = j + m;
                            if (y < 0) y = 0;
                            if (y >= original.Height) y = original.Height - 1;

                            tempR += original.GetPixel(x, y).R;
                            tempG += original.GetPixel(x, y).G;
                            tempB += original.GetPixel(x, y).B;

                            o++;
                        }
                    }

                    tempR /= o;
                    tempG /= o;
                    tempB /= o;

                    if (tempR > 255) tempR = 255;
                    if (tempG > 255) tempG = 255;
                    if (tempB > 255) tempB = 255;
                    result.SetPixel(i, j, Color.FromArgb(original.GetPixel(i, j).A, tempR, tempG, tempB));
                }

            return result;
        }
        public static Bitmap MedianFilter(Bitmap original, int MaskSize)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            int midIndex;
            if (MaskSize % 2 == 0) midIndex = (MaskSize + 1) / 2;
            else midIndex = MaskSize / 2;

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    int x, y;
                    List<int> R = new List<int>();
                    List<int> G = new List<int>();
                    List<int> B = new List<int>();

                    for (int n = -midIndex; n <= midIndex; n++)
                    {
                        x = i + n;
                        if (x < 0) x = 0;
                        if (x >= original.Width) x = original.Width - 1;

                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            y = j + m;
                            if (y < 0) y = 0;
                            if (y >= original.Height) y = original.Height - 1;

                            R.Add(original.GetPixel(x, y).R);
                            G.Add(original.GetPixel(x, y).G);
                            B.Add(original.GetPixel(x, y).B);

                        }
                    }
                    int midleListIndex = (MaskSize * MaskSize) / 2;

                    R.Sort();
                    G.Sort();
                    B.Sort();
                    result.SetPixel(i, j, Color.FromArgb(original.GetPixel(i, j).A, R[midleListIndex], G[midleListIndex], B[midleListIndex]));
                }

                return result;
        }

    }
}
