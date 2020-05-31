﻿using NAudio.Wave;
using Sounds.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds.Helpers
{
    public static class SoundUtil
    {
        public static int[] ExtractFirstChunk(int[] array, int chunkSize)
        {
            return array.Take(chunkSize).ToArray();
        }
        public static int[][] ChunkArray(int[] array, int chunkSize)
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

        public static void SaveSound(string fileName, int totalFrames, int sampleRate, int chunkSize, List<int> frequencies)
        {
            string resultFileName = "result_" + fileName;
            WaveFormat waveFormat = new WaveFormat(sampleRate: sampleRate, channels: 1);
            using (WaveFileWriter writer = new WaveFileWriter(resultFileName, waveFormat))
            {
                int prevFreq = 0;
                int curSoundLength = 1;
                for (int i = 0; i < frequencies.Count; i++)
                {
                    int frequency = frequencies[i];
                    if (prevFreq == 0)
                    {
                        prevFreq = frequency;
                    }
                    else
                    {
                        if (frequency == 0 && i != frequencies.Count - 1)
                        {
                            curSoundLength++;
                        }
                        else
                        {
                            int bufSize = chunkSize * curSoundLength;
                            float[] buffer = new float[bufSize];
                            float L1 = (float)sampleRate / prevFreq;
                            for (int j = 0; j < bufSize; j++)
                            {
                                buffer[j] = (float)Math.Sin(2 * Math.PI * j / L1);
                            }
                            writer.WriteSamples(buffer, 0, bufSize);

                            curSoundLength = 1;
                            prevFreq = frequency;
                        }
                    }
                }
            }
        }
    }
}
