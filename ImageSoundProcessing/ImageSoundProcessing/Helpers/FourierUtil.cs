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
        public static Complex[][] CopyComplexArray(Complex[][] complex)
        {
            int size = complex.Length;
            Complex[][] result = new Complex[size][];

            for (int i = 0; i < size; i++)
            {
                result[i] = new Complex[size];

                for (int j = 0; j < size; j++)
                {
                    Complex singleComplex = complex[i][j];
                    result[i][j] = new Complex(singleComplex.Real, singleComplex.Imaginary);
                }
            }

            return result;
        }

        public static Complex[][] BitmapToComplex(LockBitmap lockBitmap)
        {
            int size = lockBitmap.Width;
            Complex[][] result = new Complex[size][];

            for (int x = 0; x < size; x++)
            {
                result[x] = new Complex[size];

                for (int y = 0; y < size; y++)
                {
                    result[x][y] = new Complex(lockBitmap.GetPixel(x, y).R, 0.0f);
                }
            }

            return result;
        }

        public static Bitmap TransformResultToBitmap(float[,] transform)
        {
            int size = transform.GetLength(1);
            Bitmap resultBitmap = new Bitmap(size, size);
            LockBitmap resultBitmapLock = new LockBitmap(resultBitmap);
            resultBitmapLock.LockBits(ImageLockMode.WriteOnly);

            float max = 0.0f;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (max < transform[i, j])
                        max = transform[i, j];
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!float.IsNaN(transform[i, j]))
                    {
                        float pixelValue = transform[i, j];
                        Color color = Color.FromArgb(
                            (int)(pixelValue / max * Colors.MAX_PIXEL_VALUE),
                            (int)(pixelValue / max * Colors.MAX_PIXEL_VALUE),
                            (int)(pixelValue / max * Colors.MAX_PIXEL_VALUE));
                        resultBitmapLock.SetPixel(i, j, color);
                    }
                }
            }

            resultBitmapLock.UnlockBits();
            return resultBitmap;
        }

        public static Complex[] FftDit1d(Complex[] input)
        {
            int N = input.Length;
            float omega = (float)(-2.0 * Math.PI / N);
            Complex[] result = new Complex[input.Length];

            // base case
            if (N == 1)
            {
                result[0] = input[0];

                if (Complex.IsNaN(result[0]))
                {
                    return new[] { new Complex(0, 0) };
                }
                return result;
            }

            // radix 2 Cooley-Tukey FFT
            if (N % 2 != 0)
            {
                throw new ArgumentException("N has to be the power of 2.");
            }

            Complex[] evenInput = new Complex[N / 2];
            Complex[] oddInput = new Complex[N / 2];

            for (int i = 0; i < N / 2; i++)
            {
                evenInput[i] = input[2 * i];
                oddInput[i] = input[2 * i + 1];
            }

            Complex[] even = FftDit1d(evenInput);
            Complex[] odd = FftDit1d(oddInput);

            for (int k = 0; k < N / 2; k++)
            {
                int phase = k;
                odd[k] *= Complex.FromPolar(1, omega * phase);
            }

            for (int k = 0; k < N / 2; k++)
            {
                result[k] = even[k] + odd[k];
                result[k + N / 2] = even[k] - odd[k];
            }

            return result;
        }

        public static Complex[] IfftDit1d(Complex[] input)
        {
            int N = input.Length;
            Complex[] result = new Complex[N];

            // take conjugate
            for (int i = 0; i < N; i++)
            {
                result[i] = input[i].Conjugate();
            }

            // compute forward FFT
            result = FftDit1d(result);

            // take conjugate again
            for (int i = 0; i < N; i++)
            {
                result[i] = result[i].Conjugate();
            }

            return result;
        }

        public static Complex[][] FftDit2d(Complex[][] complexImage)
        {
            int size = complexImage.GetLength(0);
            Complex[][] p = new Complex[size][];
            Complex[][] f = new Complex[size][];
            Complex[][] t = new Complex[size][];

            //CALCULATE P
            for (int l = 0; l < size; l++)
            {
                p[l] = FftDit1d(complexImage[l]);
            }

            //TANSPOSE AND COMPUTE
            for (int l = 0; l < size; l++)
            {
                t[l] = new Complex[size];

                for (int k = 0; k < size; k++)
                {
                    t[l][k] = p[k][l];
                }

                f[l] = FftDit1d(t[l]);
            }

            return f;
        }

        public static float[,] IfftDit2d(Complex[][] inputComplex)
        {
            int size = inputComplex.GetLength(0);
            Complex[][] p = new Complex[size][];
            Complex[][] f = new Complex[size][];
            Complex[][] t = new Complex[size][];

            float[,] floatImage = new float[size, size];

            //CALCULATE P
            for (int l = 0; l < size; l++)
            {
                p[l] = IfftDit1d(inputComplex[l]);
            }

            //TRANSPOSE AND COMPUTE
            for (int l = 0; l < size; l++)
            {
                t[l] = new Complex[size];

                for (int k = 0; k < size; k++)
                {
                    t[l][k] = p[k][l] / (size * size);
                }

                f[l] = IfftDit1d(t[l]);
            }

            for (int k = 0; k < size; k++)
            {
                for (int l = 0; l < size; l++)
                {
                    floatImage[k, l] = f[k][l].Modulus();
                }
            }
            return floatImage;
        }

        public static void SwapQuadrants(Complex[][] complexImage)
        {
            int size = complexImage.GetLength(0);

            for (int x = 0; x < size / 2; x++)
            {
                for (int y = 0; y < size / 2; y++)
                {
                    Complex temp = complexImage[x][y];
                    complexImage[x][y] = complexImage[x + size / 2][y + size / 2];
                    complexImage[x + size / 2][y + size / 2] = temp;
                }
            }

            for (int x = size / 2; x < size; x++)
            {
                for (int y = 0; y < size / 2; y++)
                {
                    Complex temp = complexImage[x][y];
                    complexImage[x][y] = complexImage[x - size / 2][y + size / 2];
                    complexImage[x - size / 2][y + size / 2] = temp;
                }
            }
        }

        public static float[,] Normalise(float[,] transform)
        {
            int size = transform.GetLength(0);
            float[,] floatTransform = new float[size, size];
            float max = 0.0f;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (max < transform[i, j])
                        max = transform[i, j];
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double pixelValue = Math.Log10(1 + transform[i, j]) * 255 / Math.Log10(1 + max);
                    floatTransform[i, j] = (float)pixelValue;
                }
            }

            return floatTransform;
        }

        public static float[,] Magnitude(Complex[][] transform)
        {
            int size = transform.GetLength(0);
            float[,] floatTransform = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    floatTransform[i, j] = transform[i][j].Modulus();
                }
            }

            return Normalise(floatTransform);
        }

        public static float[,] Phase(Complex[][] transform)
        {
            int size = transform.Length;
            float[,] floatTransform = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    floatTransform[i, j] = transform[i][j].Phase();
                }
            }

            return Normalise(floatTransform);
        }

        public static Complex[][] LowPassFilter(Complex[][] transform, int range = Colors.MIN_PIXEL_VALUE)
        {
            int size = transform.Length;
            Complex[][] resultComplex = CopyComplexArray(transform);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Math.Sqrt((i - size / 2) * (i - size / 2) + (j - size / 2) * (j - size / 2)) > range)
                    {
                        resultComplex[i][j] = new Complex(0.0f, 0.0f);
                    }
                }
            }

            return resultComplex;
        }

        public static Complex[][] HighPassFilter(Complex[][] transform, int range = Colors.MAX_PIXEL_VALUE)
        {
            int size = transform.Length;
            Complex contantComponent = new Complex(transform[size / 2][size / 2].Real, transform[size / 2][size / 2].Imaginary);
            Complex[][] resultComplex = CopyComplexArray(transform);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Math.Sqrt((i - size / 2) * (i - size / 2) + (j - size / 2) * (j - size / 2)) <= range)
                    {
                        resultComplex[i][j] = new Complex(0.0f, 0.0f);
                    }
                }
            }

            resultComplex[size / 2][size / 2] = contantComponent;

            return resultComplex;
        }

        public static Complex[][] BandPassFilter(Complex[][] transform, int minRange = Colors.MIN_PIXEL_VALUE, int maxRange = Colors.MAX_PIXEL_VALUE)
        {
            int size = transform.Length;
            Complex contantComponent = new Complex(transform[size / 2][size / 2].Real, transform[size / 2][size / 2].Imaginary);
            Complex[][] resultComplex = CopyComplexArray(transform);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double x = Math.Sqrt((i - size / 2) * (i - size / 2) + (j - size / 2) * (j - size / 2));
                    if ((x > maxRange) || (x < minRange))
                    {
                        resultComplex[i][j] = new Complex(0.0f, 0.0f);
                    }
                }
            }

            resultComplex[size / 2][size / 2] = contantComponent;

            return resultComplex;
        }

        public static Complex[][] BandCutFilter(Complex[][] transform, int minRange = Colors.MIN_PIXEL_VALUE, int maxRange = Colors.MAX_PIXEL_VALUE)
        {
            int size = transform.Length;
            Complex[][] resultComplex = CopyComplexArray(transform);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double x = Math.Sqrt((i - size / 2) * (i - size / 2) + (j - size / 2) * (j - size / 2));
                    if ((x <= maxRange) && (x >= minRange))
                    {
                        resultComplex[i][j] = new Complex(0.0f, 0.0f);
                    }
                }
            }

            return resultComplex;
        }
    }
}
