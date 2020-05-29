using NAudio.Wave;
using Sounds.Helpers;
using System;
using System.Collections;
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
    public class AudioContainer
    {
        public int chunkSize = 4096;
        public int sampleRate;
        public int framesNumber;
        public int[] data;
        public TimeSpan totalTime;

        public void OpenWav(string filename)
        {
            short[] sampleBuffer;
            using (WaveFileReader reader = new WaveFileReader(filename))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                framesNumber = (int)reader.SampleCount * reader.WaveFormat.Channels;
                totalTime = reader.TotalTime;
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                sampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            int[] result = new int[sampleBuffer.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = sampleBuffer[i];
            }

            data = result;
        }

        public Tuple<List<long[]>, List<int>> Autocorrelation()
        {
            int[][] parts = SoundUtil.ChunkIntArray(data, chunkSize);
            List<long[]> autoCorrelations = new List<long[]>();
            List<int> frequencies = new List<int>();

            foreach (int[] buffer in parts)
            {
                long[] autocorrelation = new long[buffer.Length];
                for (int m = 1; m < autocorrelation.Length; m++)
                {
                    long sum = 0;
                    for (int n = 0; n < autocorrelation.Length - m; n++)
                    {
                        sum += buffer[n] * buffer[n + m];
                    }
                    autocorrelation[m - 1] = sum;
                }

                long localMaxIndex = FindLocalMax(autocorrelation);
                int frequency = (int)(sampleRate / localMaxIndex);
                autoCorrelations.Add(autocorrelation);
                frequencies.Add(frequency);
            }

            return new Tuple<List<long[]>, List<int>>(autoCorrelations, frequencies);   
        }

        private long FindLocalMax(long[] autocorrelation)
        {
            double globalMax = autocorrelation[0];
            double tempLocalMax = 0;
            int localMaxIndex = int.MaxValue;
            double localMin = globalMax;

            bool falling = true;
            for (int i = 1; i < autocorrelation.Length; i++)
            {
                double cur = autocorrelation[i];
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

        public Complex[] SignalToComplex(int[] data)
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
  

        public double[] TriangleWindow(double[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] * (1 - (i * 1.0) / data.Length);
            }
            return result;
        }


        public Complex[] FFT(Complex[] buffer)
        {
            int bits = (int)Math.Log(buffer.Length, 2);
            for (int j = 1; j < buffer.Length; j++)
            {
                int swapPos = SoundUtil.BitReverse(j, bits);
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

        //public double[] Cepstrum(double[] data)
        //{

        //}
    }
}
