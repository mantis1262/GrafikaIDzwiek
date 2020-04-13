using ImageSoundProcessing.Forms;
using ImageSoundProcessing.Model;
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
                double a = 0;
                for (int i = 0; i < originalBitmapLock.Width; i++)
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        if (factor <= 0) a = 1.0 + factor / 256.0;
                        else a = 256.0 / Math.Pow(2, Math.Log(257 - factor, 2));
                        Color temp = originalBitmapLock.GetPixel(i, j);
                        int R = (int)(a * (temp.R - 128) + 128);
                        int G = (int)(a * (temp.G - 128) + 128);
                        int B = (int)(a * (temp.B - 128) + 128);
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
                        Color color = Color.FromArgb(newHR[originalBitmapLock.GetPixel(i, j).R], newHG[originalBitmapLock.GetPixel(i, j).G], newHB[originalBitmapLock.GetPixel(i, j).B]);
                        processedBitmapLock.SetPixel(i, j, color);
                    }
                }
            }
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static void ShowHistogram(int[][] value)
        {
            if (value[0].SequenceEqual(value[1]) == false && value[0].SequenceEqual(value[2]) == false)
            {
                CharWindow formR = FormFactory.CreateCharForm(value[0]);
                formR.Name = "ValueR";
                formR.Histogram.Series[0].Name = "valueR";
                formR.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                CharWindow formG = FormFactory.CreateCharForm(value[1]);
                formG.Name = "ValueG";
                formG.Histogram.Series[0].Name = "valueG";
                formG.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                CharWindow formB = FormFactory.CreateCharForm(value[2]);
                formB.Name = "ValueB";
                formB.Histogram.Series[0].Name = "valueB";
                formB.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                formR.Show();
                formG.Show();
                formB.Show();
            }
            else
            {
                CharWindow form = FormFactory.CreateCharForm(value[2]);
                form.Name = "ValueLight";
                form.Histogram.Series[0].Name = "ValueLight";
                form.Histogram.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                form.Show();
            }
        }

        public static Bitmap lineFilter(Bitmap original,int k) 
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            int maskSize = 3;
            int midIndex;
            if (maskSize % 2 == 0) midIndex = (maskSize + 1) / 2;
            else midIndex = maskSize / 2;

            int[][] mask = new int[4][]
            {
              new int[] {-1, -1, -1,
                          1, -2,  1,
                          1,  1,  1},

              new int[] { 1,  1, 1,
                         -1, -2, 1,
                         -1, -1, 1},

              new int[] {-1,  1, 1,
                         -1, -2, 1,
                         -1,  1, 1},

              new int[] {-1, -1, 1,
                         -1, -2, 1,
                          1,  1, 1}
            };
            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {

                    int tempR = 0;
                    int tempG = 0;
                    int tempB = 0;
                    int x, y;
                    int o = 0;
                    for (int n = -midIndex; n <= midIndex; n++)
                    {
                        y = j + n;
                        if (y < 0)
                            y = 0;
                        if (y >= originalBitmapLock.Height)
                            y = originalBitmapLock.Height - 1;
                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            
                            x = i + m;
                            if (x < 0) x = 0;
                            if (x >= originalBitmapLock.Width) x = originalBitmapLock.Width - 1;

                            tempR += originalBitmapLock.GetPixel(x, y).R * mask[k][o];
                            tempG += originalBitmapLock.GetPixel(x, y).G * mask[k][o];
                            tempB += originalBitmapLock.GetPixel(x, y).B * mask[k][o];
                            o++;
                        }

                    }

                    if (tempR > 255) tempR = 255;
                    if (tempG > 255) tempG = 255;
                    if (tempB > 255) tempB = 255;
                    if (tempR < 0) tempR = 0;
                    if (tempG < 0) tempG = 0;
                    if (tempB < 0) tempB = 0;
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, tempR, tempG, tempB));

                }
            }
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Bitmap southFilter(Bitmap original)
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

                    int tempR;
                    int tempG;
                    int tempB;

                    int x1 = i - 1;
                    int x2 = i + 1;
                    if (x1 < 0) x1 = 0;
                    if (x2 >= originalBitmapLock.Width) x2 = originalBitmapLock.Width - 1;

                    int y1 = j - 1;
                    int y2 = j + 1;
                    if (y1 < 0) y1 = 0;
                    if (y2 >= originalBitmapLock.Height) y2 = originalBitmapLock.Height - 1;

                    tempR = originalBitmapLock.GetPixel(x1, j).R -2 * originalBitmapLock.GetPixel(i, j).R + originalBitmapLock.GetPixel(x2, j).R
                            - originalBitmapLock.GetPixel(x1, y1).R - originalBitmapLock.GetPixel(i, y1).R - originalBitmapLock.GetPixel(x2, y1).R
                            + originalBitmapLock.GetPixel(x1, y2).R + originalBitmapLock.GetPixel(i, y2).R + originalBitmapLock.GetPixel(x2, y2).R;
                    tempG = originalBitmapLock.GetPixel(x1, j).G - 2 * originalBitmapLock.GetPixel(i, j).G + originalBitmapLock.GetPixel(x2, j).G
                            - originalBitmapLock.GetPixel(x1, y1).G - originalBitmapLock.GetPixel(i, y1).G - originalBitmapLock.GetPixel(x2, y1).G
                            + originalBitmapLock.GetPixel(x1, y2).G + originalBitmapLock.GetPixel(i, y2).G + originalBitmapLock.GetPixel(x2, y2).G;
                    tempB = originalBitmapLock.GetPixel(x1, j).B - 2 * originalBitmapLock.GetPixel(i, j).B + originalBitmapLock.GetPixel(x2, j).B
                            - originalBitmapLock.GetPixel(x1, y1).B - originalBitmapLock.GetPixel(i, y1).B - originalBitmapLock.GetPixel(x2, y1).B
                            + originalBitmapLock.GetPixel(x1, y2).B + originalBitmapLock.GetPixel(i, y2).B + originalBitmapLock.GetPixel(x2, y2).B;

                    if (tempR > 255) tempR = 255;
                    if (tempG > 255) tempG = 255;
                    if (tempB > 255) tempB = 255;
                    if (tempR < 0) tempR = 0;
                    if (tempG < 0) tempG = 0;
                    if (tempB < 0) tempB = 0;
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, tempR, tempG, tempB));

                }
            }
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Bitmap uolisaFilter(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                int x1 = i + 1;
                int x0 = i - 1;
                if (x0 < 0) x0 = 0;
                if (x1 >= originalBitmapLock.Width)    x1 = originalBitmapLock.Width - 1;

                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                    int y0 = j - 1;
                    int y1 = j + 1;
                    if (y0 < 0) y0 = 0;
                    if (y1 >= originalBitmapLock.Height) y1 = originalBitmapLock.Height - 1;


                    double px = (originalBitmapLock.GetPixel(i, y0).R * originalBitmapLock.GetPixel(i, y1).R *
                                  originalBitmapLock.GetPixel(x0, j).R * originalBitmapLock.GetPixel(x1, j).R);
                    double pxx = (Math.Pow(originalBitmapLock.GetPixel(i, j).R, 4));
                    double log = Math.Log(pxx / px);
                    int R = (int)(255  * (log / 4.0));

                    int G = (int)(255 * (Math.Log(
                                  ((Math.Pow(originalBitmapLock.GetPixel(i, j).G, 4) * 1.0) /
                                  (originalBitmapLock.GetPixel(i, y0).G * originalBitmapLock.GetPixel(i, y1).G *
                                  originalBitmapLock.GetPixel(x0, j).G * originalBitmapLock.GetPixel(x1, j).G)
                                  )) / 4.0));
                    int B = (int)(255 * (Math.Log(
                                  ((Math.Pow(originalBitmapLock.GetPixel(i, j).B, 4) * 1.0) /
                                  (originalBitmapLock.GetPixel(i, y0).B * originalBitmapLock.GetPixel(i, y1).B *
                                  originalBitmapLock.GetPixel(x0, j).B * originalBitmapLock.GetPixel(x1, j).B)
                                  )) / 4.0));


                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    if (B < 0) B = 0;
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, (byte)R, (byte)G, (byte)B)) ;
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Complex[,] FftTransform(Bitmap original)
        {
            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);

            Complex[,] pixelsData = FourierUtil.TransformImgToComplex2DTable(originalBitmapLock);
            Complex[,] afterForwardComplex = FourierUtil.FftDit2d(pixelsData);
            FourierUtil.SwapQuadrants(afterForwardComplex);

            originalBitmapLock.UnlockBits();
            return afterForwardComplex;
        }

        public static Bitmap IfftTransform(Complex[,] complex)
        {
            Bitmap processedBmp = new Bitmap(complex.GetLength(0), complex.GetLength(1));
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            Complex[,] afterInverseComplex = FourierUtil.IfftDit2d(complex);
            double[,] normalisedResult = FourierUtil.GetPixelValues(afterInverseComplex, false, "none");

            for (int x = 0; x < processedBitmapLock.Width; x++)
            {
                for (int y = 0; y < processedBitmapLock.Height; y++)
                {
                    processedBitmapLock.SetPixel(x, y, Color.FromArgb((int)normalisedResult[x, y], (int)normalisedResult[x, y], (int)normalisedResult[x, y]));
                }
            }

            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Bitmap GetSpectrumBitmap(Complex[,] complexImage, string spectrum)
        {
            double[,] pixelValues = FourierUtil.GetPixelValues(complexImage, true, spectrum);
            int width = complexImage.GetLength(0);
            int height = complexImage.GetLength(1);
            Bitmap resultBitmap = new Bitmap(width, height);
            LockBitmap resultBitmapLock = new LockBitmap(resultBitmap);
            resultBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pixelValues[x, y] < Colors.MIN_PIXEL_VALUE)
                    {
                        pixelValues[x, y] = Colors.MIN_PIXEL_VALUE;
                    }
                    int pixelColor = (int)pixelValues[x, y];
                    resultBitmapLock.SetPixel(x, y, Color.FromArgb(255, pixelColor, pixelColor, pixelColor));
                }
            }

            resultBitmapLock.UnlockBits();
            return resultBitmap;
        }
    }
}
