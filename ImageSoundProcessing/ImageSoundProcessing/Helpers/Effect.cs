using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Helpers
{
    public static class Effect
    {
        public static Bitmap Brightness(Bitmap original, int factor)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            if (factor != 0)
            {
                LockBitmap originalBitmapLock = new LockBitmap(original);
                LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
                originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
                processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

                for (int i = 0; i < originalBitmapLock.Width; i++)
                {
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        Color temp = originalBitmapLock.GetPixel(i, j);
                        int R = temp.R + factor;
                        int G = temp.G + factor;
                        int B = temp.B + factor;
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                    }
                }

                originalBitmapLock.UnlockBits();
                processedBitmapLock.UnlockBits();
                return processedBmp;
            }
            else
            {
                return original;
            }
            
        }

        public static Bitmap Contrast(Bitmap original, int factor)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            if (factor != 0)
            {
                LockBitmap originalBitmapLock = new LockBitmap(original);
                LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
                originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
                processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

                for (int i = 0; i < originalBitmapLock.Width; i++)
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        Color temp = originalBitmapLock.GetPixel(i, j);
                        int R = factor * (temp.R - 128) + 128;
                        int G = factor * (temp.G - 128) + 128;
                        int B = factor * (temp.B - 128) + 128;
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                    }

                originalBitmapLock.UnlockBits();
                processedBitmapLock.UnlockBits();
                return processedBmp;
            }
            else
            {
                return original;
            }
        }

        public static Bitmap Negative(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                    Color temp = originalBitmapLock.GetPixel(i, j);
                    int R = 255 - temp.R;
                    int G = 255 - temp.G;
                    int B = 255 - temp.B;
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(temp.A, (byte)R, (byte)G, (byte)B));
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }  
        
        public static Bitmap GrayMode(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                    Color temp = originalBitmapLock.GetPixel(i, j);
                    int gray = (int)(0.299 * temp.R + 0.587 * temp.G + 0.114 * temp.B);
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(temp.A, (byte)gray, (byte)gray, (byte)gray));
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Bitmap AritmeticMiddleFilter(Bitmap original, int maskSize)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            int midIndex;
            if (maskSize % 2 == 0) midIndex = (maskSize + 1) / 2;
            else midIndex = maskSize / 2;
            for (int i = 0; i < originalBitmapLock.Width; i++)
                for (int j = 0; j < originalBitmapLock.Height; j++)
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
                        if (x >= originalBitmapLock.Width) x = originalBitmapLock.Width - 1;
                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            y = j + m;
                            if (y < 0)
                                y = 0;
                            if (y >= originalBitmapLock.Height)
                                y = originalBitmapLock.Height - 1;
                            tempR += originalBitmapLock.GetPixel(x, y).R;
                            tempG += originalBitmapLock.GetPixel(x, y).G;
                            tempB += originalBitmapLock.GetPixel(x, y).B;
                            o++;
                        }
                    }
                    tempR /= o;
                    tempG /= o;
                    tempB /= o;
                    if (tempR > 255) tempR = 255;
                    if (tempG > 255) tempG = 255;
                    if (tempB > 255) tempB = 255;
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, tempR, tempG, tempB));
                }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }
        public static Bitmap MedianFilter(Bitmap original, int MaskSize)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

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
                        if (x >= originalBitmapLock.Width) x = originalBitmapLock.Width - 1;
                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            y = j + m;
                            if (y < 0)
                                y = 0;
                            if (y >= originalBitmapLock.Height)
                                y = originalBitmapLock.Height - 1;
                            R.Add(originalBitmapLock.GetPixel(x, y).R);
                            G.Add(originalBitmapLock.GetPixel(x, y).G);
                            B.Add(originalBitmapLock.GetPixel(x, y).B);
                        }
                    }
                    int midleListIndex = (MaskSize * MaskSize) / 2;
                    R.Sort();
                    G.Sort();
                    B.Sort();
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, R[midleListIndex], G[midleListIndex], B[midleListIndex]));
                }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static int[][] Histogram(Bitmap original)
        {

            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            int[][] value = new int[3][]
            {
                new int[256],
                new int[256],
                new int[256]
            };           

            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                   value[0][originalBitmapLock.GetPixel(i, j).R]++;
                   value[1][originalBitmapLock.GetPixel(i, j).G]++;
                   value[2][originalBitmapLock.GetPixel(i, j).B]++;
                }
            }
            originalBitmapLock.UnlockBits();
            return value;
        }

        public static Bitmap ModifiHistogram(Bitmap original, int min, int max)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            int[][] H = Histogram(original);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);
            int N = processedBmp.Height * processedBmp.Width;
            if (H[0].SequenceEqual(H[1]) && H[0].SequenceEqual(H[2]))
            {
                int[] newH = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    double temp = 0;
                    for (int j = 0; j <= i; j++)
                    {
                        temp += H[0][j];
                    }
                    newH[i] = (int)((min * Math.Pow(((max * 1.0) / min), (temp / N))));
                }
                for (int i = 0; i < originalBitmapLock.Width; i++)
                {
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        Color color = Color.FromArgb(newH[originalBitmapLock.GetPixel(i, j).R], newH[originalBitmapLock.GetPixel(i, j).R], newH[originalBitmapLock.GetPixel(i, j).R]);
                        processedBitmapLock.SetPixel(i, j, color);
                    }
                }
            }
            else
            {
                int[] newHR = new int[256];
                int[] newHG = new int[256];
                int[] newHB = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    double[] temp = new double[3];
                    for (int j = 0; j <= i; j++)
                    {
                        temp[0] += H[0][j];
                        temp[1] += H[1][j];
                        temp[2] += H[2][j];
                    }
                    newHR[i] = (int)((min * Math.Pow(((max * 1.0) / min), (temp[0] / N))));
                    newHG[i] = (int)((min * Math.Pow(((max * 1.0) / min), (temp[1] / N))));
                    newHB[i] = (int)((min * Math.Pow(((max * 1.0) / min), (temp[2] / N))));
                }
                for (int i = 0; i < originalBitmapLock.Width; i++)
                {
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        Color color = Color.FromArgb(newHR[originalBitmapLock.GetPixel(i, j).R], newHG[originalBitmapLock.GetPixel(i, j).R], newHB[originalBitmapLock.GetPixel(i, j).R]);
                        processedBitmapLock.SetPixel(i, j, color);
                    }
                }
            }
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

    }
}
