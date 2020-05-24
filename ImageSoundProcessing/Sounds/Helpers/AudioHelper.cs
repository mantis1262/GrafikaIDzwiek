using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sound.Helpers
{
    class AudioHelper
    {
        public int sampleRate = 0;

        private const int WinodwSize = 2048;

        public Tuple<double[], int, TimeSpan> openWav(string filename)
        {
            TimeSpan time = new TimeSpan();
            short[] sampleBuffer;
            using (WaveFileReader reader = new WaveFileReader(filename))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                time = reader.TotalTime;
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                sampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            double[] result = new double[WinodwSize];
            int i = 0;
            foreach (short tmp in sampleBuffer)
            {
                result[i] = sampleBuffer[i];
                i++;
                if (i == WinodwSize)
                    break;
            }

            return new Tuple<double[], int, TimeSpan>(result, sampleRate, time);
        }


        public double[] Autocorrelation(double[] data)
        {
            double[] result = new double[data.Length];

            for (int m = 1; m < result.Length; m++)
            {
                double sum = 0;
                for (int n = 0; n < result.Length - m; n++)
                {
                    sum += data[n] * data[n + m];
                }
                result[m - 1] = sum;
            }

            return result;    
        }


        public Complex[] SignalToComplex(double[] data)
        {
            Complex[] resultComplex = new Complex[data.Length];
            for (int z = 0; z < data.Length; z++)
            {
                resultComplex[z] = data[z];
            }
            return resultComplex;
        }

        public Complex[] HammingWindow(Complex[] complexData )
        {
            int N = complexData.Length;
            Complex[] complexResult = new Complex[N];
            double arg = (2 * Math.PI) / ((double)N - 1.0);

            for (int i = 0; i < N; ++i)
                complexResult[i] = complexData[i].Real * (0.54 - 0.46 * Math.Cos(arg * (double)i));
            return complexResult;
        }

        public double findLocalMax(double[] data)
        {
            double globalMax = data[0];
            double tempLocalMax = 0;
            long localMaxIndex = int.MaxValue;
            double localMin = globalMax;

            bool falling = true;
            for (int i = 1; i < data.Length; i++)
            {
                double cur = data[i];
                if (falling)
                {
                    if (cur < localMin)
                    {
                        localMin = cur;
                    }
                    else if ((globalMax - localMin) * 0.95 > globalMax - cur)
                    {
                        falling = false;
                        tempLocalMax = cur;
                    }
                }
                else
                {
                    if (cur > tempLocalMax)
                    {
                        tempLocalMax = cur;
                        localMaxIndex = i;
                    }
                    else if (1.05 * tempLocalMax > globalMax)
                    {
                        return localMaxIndex;
                    }
                }
            }

            return localMaxIndex;
            }

        public double[] TriangleWindow(double[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] * (1 - (i * 1.0) / data.Length);
            }
            return result;
        }

        public static int BitReverse(int n, int bits)
        {
            int reversedN = n;
            int count = bits - 1;

            n >>= 1;
            while (n > 0)
            {
                reversedN = (reversedN << 1) | (n & 1);
                count--;
                n >>= 1;
            }

            return ((reversedN << count) & ((1 << bits) - 1));
        }

        public Complex[] FFT(Complex[] buffer)
        {

            int bits = (int)Math.Log(buffer.Length, 2);
            for (int j = 1; j < buffer.Length; j++)
            {
                int swapPos = BitReverse(j, bits);
                if (swapPos <= j)
                {
                    continue;
                }
                var temp = buffer[j];
                buffer[j] = buffer[swapPos];
                buffer[swapPos] = temp;
            }




            for (int N = 2; N <= buffer.Length; N <<= 1)
            {
                for (int i = 0; i < buffer.Length; i += N)
                {
                    for (int k = 0; k < N / 2; k++)
                    {

                        int evenIndex = i + k;
                        int oddIndex = i + k + (N / 2);


                        Complex even = 0.0;

                        Complex odd = 0.0;

                        if (oddIndex < buffer.Length)
                        {
                            odd = buffer[oddIndex];
                        }
                        if (evenIndex < buffer.Length)
                        {
                            even = buffer[evenIndex];
                        }

                        double term = -2 * Math.PI * k / (double)N;
                        Complex exp = new Complex(Math.Cos(term), Math.Sin(term)) * odd;
                        if (evenIndex < buffer.Length)
                        {
                            buffer[evenIndex] = even + exp;
                        }
                        if (oddIndex < buffer.Length)
                        {
                            buffer[oddIndex] = even - exp;
                        }
                    }
                }
            }

                return buffer;
        }
    }
}
