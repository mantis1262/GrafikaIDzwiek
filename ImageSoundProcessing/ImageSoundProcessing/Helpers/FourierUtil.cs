using ImageSoundProcessing.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Helpers
{
    public static class FourierUtil
    {
        public static Complex[] GetComplexRow(Complex[,] complex, int dimNum)
        {
            int colsOrWidth = complex.GetLength(0);
            Complex[] result = new Complex[colsOrWidth];

            for (int i = 0; i < colsOrWidth; i++)
            {
                result[i] = complex[dimNum, i];
            }

            return result;
        }

        public static Complex[] GetComplexCol(Complex[,] complex, int dimNum)
        {
            int rowsOrHeight = complex.GetLength(1);
            Complex[] result = new Complex[rowsOrHeight];

            for (int i = 0; i < rowsOrHeight; i++)
            {
                result[i] = complex[i, dimNum];
            }

            return result;
        }

        public static void SetComplexRow(Complex[,] complex, Complex[] complexRow, int dimNum)
        {
            for (int i = 0; i < complexRow.Length; i++)
            {
                complex[dimNum, i] = complexRow[i];
            }
        }

        public static void SetComplexCol(Complex[,] complex, Complex[] complexCol, int dimNum)
        {
            for (int i = 0; i < complexCol.Length; i++)
            {
                complex[i, dimNum] = complexCol[i];
            }
        }

        //public static Complex[,] Swap2dDimensions(Complex[,] complex)
        //{
        //    int rows = complex.GetLength(0);
        //    int cols = complex.GetLength(1);
        //    Complex[,] swappedTab = new Complex[rows, cols];

        //    for (int x = 0; x < rows; x++)
        //    {
        //        for (int y = 0; y < cols; y++)
        //        {
        //            swappedTab[x, y] = complex[y, x];
        //        }
        //    }

        //    return swappedTab;
        //}

        public static Complex[] FftDit1d(Complex[] complex)
        {
            int N = complex.Length;

            // base case
            if (N == 1)
            {
                return new Complex[] { complex[0] };
            }

            // radix 2 Cooley-Tukey FFT
            if (N % 2 != 0)
            {
                throw new ArgumentException("N has to be the power of 2.");
            }

            // fft of even terms
            Complex[] even = new Complex[N / 2];
            for (int k = 0; k < N / 2; k++)
            {
                even[k] = complex[2 * k];
            }
            Complex[] q = FftDit1d(even);

            // fft of odd terms
            Complex[] odd = even;  // reuse the array
            for (int k = 0; k < N / 2; k++)
            {
                odd[k] = complex[2 * k + 1];
            }
            Complex[] r = FftDit1d(odd);

            // combine
            Complex[] y = new Complex[N];
            for (int k = 0; k < N / 2; k++)
            {
                double kth = -2 * k * Math.PI / N;
                Complex wk = new Complex(Math.Cos(kth), Math.Sin(kth));
                y[k] = q[k].Plus(wk.Times(r[k]));
                y[k + N / 2] = q[k].Minus(wk.Times(r[k]));
            }
            return y;
        }

        public static Complex[] IfftDit1d(Complex[] complex)
        {
            int N = complex.Length;
            Complex[] y = new Complex[N];

            // take conjugate
            for (int i = 0; i < N; i++)
            {
                y[i] = complex[i].Conjugate();
            }

            // compute forward FFT
            y = FftDit1d(y);

            // take conjugate again
            for (int i = 0; i < N; i++)
            {
                y[i] = y[i].Conjugate();
            }

            // divide by N
            for (int i = 0; i < N; i++)
            {
                y[i] = y[i].Times(1.0 / N);
            }

            return y;
        }

        public static Complex[,] FftDit2d(Complex[,] complex2d)
        {
            int rows = complex2d.GetLength(0);
            int cols = complex2d.GetLength(1);

            Complex[,] afterTransformComplex = new Complex[rows, cols];

            for (int x = 0; x < rows; x++)
            {
                Complex[] complex = GetComplexRow(complex2d, x);
                SetComplexRow(afterTransformComplex, FftDit1d(complex), x);
            }

            for (int y = 0; y < cols; y++)
            {
                Complex[] complex = GetComplexCol(complex2d, y);
                SetComplexCol(afterTransformComplex, FftDit1d(complex), y);
            }

            return afterTransformComplex;
        }

        public static Complex[,] IfftDit2d(Complex[,] complex2d)
        {
            int rows = complex2d.GetLength(0);
            int cols = complex2d.GetLength(1);

            Complex[,] afterTransformComplex = new Complex[rows, cols];

            for (int x = 0; x < rows; x++)
            {
                Complex[] complex = GetComplexRow(complex2d, x);
                SetComplexRow(afterTransformComplex, IfftDit1d(complex), x);
            }

            for (int y = 0; y < rows; y++)
            {
                Complex[] complex = GetComplexCol(complex2d, y);
                SetComplexCol(afterTransformComplex, FftDit1d(complex), y);
            }

            return afterTransformComplex;
        }

        public static void SwapQuadrants(Complex[,] complexImage)
        {
            int size = complexImage.GetLength(0);

            for (int x = 0; x < size / 2; x++)
            {
                for (int y = 0; y < size / 2; y++)
                {
                    Complex temp = complexImage[x, y];
                    complexImage[x, y] = complexImage[x + size / 2, y + size / 2];
                    complexImage[x + size / 2, y + size / 2] = temp;
                }
            }

            for (int x = size / 2; x < size; x++)
            {
                for (int y = 0; y < size / 2; y++)
                {
                    Complex temp = complexImage[x, y];
                    complexImage[x, y] = complexImage[x - size / 2, y + size / 2];
                    complexImage[x - size / 2, y + size / 2] = temp;
                }
            }
        }

        public static double[,] GetPixelValues(Complex[,] complexImage, bool normalize, string spectrum)
        {
            int rows = complexImage.GetLength(0);
            int cols = complexImage.GetLength(1);

            double[,] res = new double[rows, cols];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    if (spectrum == "none")
                    {
                        res[x, y] = complexImage[x, y].Real;
                        if (res[x, y] < Colors.MIN_PIXEL_VALUE)
                        {
                            res[x, y] = Colors.MIN_PIXEL_VALUE;
                        }
                        else if (res[x, y] > Colors.MAX_PIXEL_VALUE)
                        {
                            res[x, y] = Colors.MAX_PIXEL_VALUE;
                        }
                    }
                    else if (spectrum == "phase")
                    {
                        res[x, y] = complexImage[x, y].Phase();
                    }
                    else if (spectrum == "abs")
                    {
                        res[x, y] = complexImage[x, y].Abs();
                    }
                }
            }

            if (normalize)
            {
                res = Normalise(res);
            }

            return res;
        }

        public static double[,] Normalise(double[,] values)
        {
            int rows = values.GetLength(0);
            int cols = values.GetLength(1);

            double curMin = values[0, 0];
            double curMax = values[0, 0];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    if (values[x, y] > curMax)
                    {
                        curMax = values[x, y];
                    }
                    if (values[x, y] < curMin)
                    {
                        curMin = values[x, y];
                    }
                }
            }

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    values[x, y] = Colors.MAX_PIXEL_VALUE * Math.Log(values[x, y] + 1) / Math.Log(curMax + 1);
                }
            }

            return values;
        }

        public static Complex[,] TransformImgToComplex2DTable(LockBitmap lockBitmap)
        {
            Complex[,] complex2DTable = new Complex[lockBitmap.Width, lockBitmap.Height];
          
            for (int x = 0; x < lockBitmap.Width; x++)
            {
                for (int y = 0; y < lockBitmap.Height; y++)
                {
                    complex2DTable[x, y] = new Complex(lockBitmap.GetPixel(x, y).R, 0.0d);
                }
            }

            return complex2DTable;
        }

        public static Complex[,] CopyComplexArray(Complex[,] complex)
        {
            Complex[,] result = new Complex[complex.GetLength(0), complex.GetLength(1)];
            return result;
        }
    }
}
