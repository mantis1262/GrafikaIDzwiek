using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sounds.Helpers
{
    public static class SoundUtil
    {
        public static int[][] ChunkArray(int [] array, int chunkSize)
        {
            int arrayLength = array.Length;
            int numOfChunks = arrayLength / chunkSize;
            int remains = arrayLength - numOfChunks * chunkSize;
            if (remains > 0)
            {
                numOfChunks++;
            }
            int[][] result = new int[numOfChunks][];
            for (int i = 0; i < numOfChunks; i++)
            {
                result[i] = array.Skip(i * chunkSize).Take(chunkSize).ToArray();
            }
            return result;
        }

        public static int[][] ChunkArrayPowerOf2(int[] array, int chunkSize)
        {
            int arrayLength = array.Length;
            int numOfChunks = arrayLength / chunkSize;
            int[][] result = new int[numOfChunks][];
            for (int i = 0; i < numOfChunks; i++)
            {
                result[i] = array.Skip(i * chunkSize).Take(chunkSize).ToArray();
            }
            return result;
        }

        public static float[][] ChunkArrayPowerOf2(float[] array, int chunkSize)
        {
            int arrayLength = array.Length;
            int numOfChunks = arrayLength / chunkSize + 1;
            float[][] result = new float[numOfChunks][];
            int arrayIndex = 0;
            for (int i = 0; i < numOfChunks; i++)
            {
                result[i] = new float[chunkSize];
                for (int j = 0; j < chunkSize; j++)
                {
                    if (arrayIndex >= arrayLength)
                        break;
                    result[i][j] = array[arrayIndex];
                    arrayIndex++;
                }
            }
            return result;
        }

        public static int MakePowerOf2(int windowWidth)
        {
            int powerOfTwo = 2;

            while (windowWidth > powerOfTwo)
            {
                powerOfTwo *= 2;
            }

            return powerOfTwo;
        }

        public static int MaxFromPeriods(IList<int> pperiod, double[] dd)
        {
            int maxPeriodIndex = pperiod[0];
            double maxValue = dd[maxPeriodIndex];

            for (int i = 1; i < pperiod.Count; i++)
            {
                int period = pperiod[i];
                if (dd[period] > maxValue)
                {
                    maxValue = dd[period];
                    maxPeriodIndex = period;
                }
            }

            return maxPeriodIndex;
        }

        //public static int BitReverse(int n, int bits)
        //{
        //    int reversedN = n;
        //    int count = bits - 1;

        //    n >>= 1;
        //    while (n > 0)
        //    {
        //        reversedN = (reversedN << 1) | (n & 1);
        //        count--;
        //        n >>= 1;
        //    }

        //    return ((reversedN << count) & ((1 << bits) - 1));
        //}

        //public static Complex[] FFT(Complex[] buffer)
        //{
        //    int bits = (int)Math.Log(buffer.Length, 2);
        //    for (int j = 1; j < buffer.Length; j++)
        //    {
        //        int swapPos = BitReverse(j, bits);
        //        if (swapPos <= j)
        //        {
        //            continue;
        //        }
        //        var temp = buffer[j];
        //        buffer[j] = buffer[swapPos];
        //        buffer[swapPos] = temp;
        //    }

        //    for (int N = 2; N <= buffer.Length; N <<= 1)
        //    {
        //        for (int i = 0; i < buffer.Length; i += N)
        //        {
        //            for (int k = 0; k < N / 2; k++)
        //            {

        //                int evenIndex = i + k;
        //                int oddIndex = i + k + (N / 2);


        //                Complex even = new Complex(0, 0);

        //                Complex odd = new Complex(0, 0);

        //                if (oddIndex < buffer.Length)
        //                {
        //                    odd = buffer[oddIndex];
        //                }
        //                if (evenIndex < buffer.Length)
        //                {
        //                    even = buffer[evenIndex];
        //                }

        //                double term = -2 * Math.PI * k / (double)N;
        //                Complex exp = new Complex((float)Math.Cos(term), (float)Math.Sin(term)) * odd;
        //                if (evenIndex < buffer.Length)
        //                {
        //                    buffer[evenIndex] = even + exp;
        //                }
        //                if (oddIndex < buffer.Length)
        //                {
        //                    buffer[oddIndex] = even - exp;
        //                }
        //            }
        //        }
        //    }

        //    return buffer;
        //}

        public static Complex[] SignalToComplex(int[] data)
        {
            Complex[] resultComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                resultComplex[i] = new Complex(data[i], 0.0f);
            }
            return resultComplex;
        }

        public static Complex[] SignalToComplex(float[] data)
        {
            Complex[] resultComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                resultComplex[i] = new Complex(data[i], 0.0f);
            }
            return resultComplex;
        }

        public static Complex[] HammingWindow(Complex[] complexData)
        {
            int N = complexData.Length;
            Complex[] complexResult = new Complex[N];
            double arg = (2 * Math.PI) / (N - 1.0);

            for (int i = 0; i < N; ++i)
                complexResult[i] = new Complex(complexData[i].Real * (0.54f - 0.46f * (float)Math.Cos(arg * i)), 0.0f);
            return complexResult;
        }

        public static Complex[] FftDit1d(Complex[] input)
        {
            int N = input.Length;
            float omega = (float)(-2.0 * Math.PI / N);
            Complex[] result = new Complex[N];

            // base case
            if (N == 1)
            {
                return new Complex[] { input[0] };
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
    }
}
