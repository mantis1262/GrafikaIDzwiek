using NAudio.Wave;
using Sounds.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        
        public static double[][] ChunkArrayWithHop(float[] array, int chunkSize, int hopSize)
        {
            List<double[]> result = new List<double[]>();
            int listIndex = 0;
            int listInsideIndex = 0;
            int steps = -1;
            for (int i = 0; i < array.Length; i++)
            {
                result.Add(new double[chunkSize]);
                result[listIndex][listInsideIndex] = array[i];    
                if(listInsideIndex >= chunkSize - 1)
                {
                    listIndex = 0;
                    steps += hopSize;
                    i = steps;
                }
                else
                {
                    listInsideIndex++;
                }

            }
            return result.ToArray();
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

        public static int MakePowerOf2(int value)
        {
            int powerOfTwo = 2;

            while (value > powerOfTwo)
            {
                powerOfTwo *= 2;
            }

            return powerOfTwo;
        }

        public static int GetExpandedPow2(int length)
        {
            return (int)Math.Pow(2, (int)Math.Log(length, 2) + 1);
        }

        public static int MaxFromLocalMaxList(IList<int> localMaxList, double[] data)
        {
            int localMaxIndex = 0;

            if (localMaxList.Count > 0)
            {
                localMaxIndex = localMaxList[0];
                double maxValue = data[localMaxIndex];

                for (int i = 1; i < localMaxList.Count; i++)
                {
                    int index = localMaxList[i];
                    if (data[index] > maxValue)
                    {
                        maxValue = data[index];
                        localMaxIndex = index;
                    }
                }
            }

            return localMaxIndex;
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

        public static Complex[] SignalToComplex(double[] data)
        {
            Complex[] resultComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                resultComplex[i] = new Complex((float)data[i], 0.0f);
            }
            return resultComplex;
        }

        public static double[] SignalFromComplex(Complex[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i].Real;
            }
            return result;
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

        public static double[] LowPassFilterFactors(double cutFreq, double sampleFreq, int filterLength)
        {
            double[] result = new double[filterLength];
            double half = (filterLength - 1) / 2.0;
            for (int i = 0; i < filterLength; i++)
            {
                if (i == half)
                {
                    result[i] = 2 * cutFreq / sampleFreq;
                }
                else
                {
                    result[i] = Math.Sin(2 * Math.PI * cutFreq / sampleFreq * (i - half)) / (Math.PI * (i - half));
                }
            }

            return result;
        }


        public static float[] ConvertDoubleArrayToFloatArray(double[] array)
        {
            float[] floatArray = new float[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                floatArray[i] = (float)array[i];
            }
            return floatArray;
        }

        #region WindowFactors
        public static double[] RectangularFactors(int windowSize)
        {
            double[] windowData = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                windowData[i] = i < windowSize ? 1 : 0;
            }
            return windowData;
        }

        public static double[] HammingFactors(int windowSize)
        {
            double[] windowData = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                windowData[i] = 0.53836 - 0.46164 * Math.Cos(2 * Math.PI * i / (windowSize - 1));
            }
            return windowData;
        }

        public static double[] HannFactors(int windowSize)
        {
            double[] windowData = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                windowData[i] = 0.5 * (1 - Math.Cos(2 * Math.PI * i / (windowSize - 1)));
            }
            return windowData;
        }

        public static double[] Factors(int type, int windowsLength)
        {
            switch (type)
            {
                case 0:
                    {
                        return RectangularFactors(windowsLength);
                    }
                case 2:
                    {
                        return HammingFactors(windowsLength);
                    }
                case 1:
                    {
                        return HannFactors(windowsLength);
                    }
                default: return new double[] { };
            }
        }

        #endregion
        #region WindowSignal
        public static double[] RectangularWindowing(double[] data)
        {
            int n = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i < n ? data[i] : 0;
            }

            return data;
        }

        public static double[] HammingWindowing(double[] data)
        {
            int n = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] *= 0.53836 - 0.46164 * Math.Cos(2 * Math.PI * i / (n - 1));
            }

            return data;
        }

        public static double[] HanningWindowing(double[] data)
        {
            int n = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] *= 0.5 * (1 - Math.Cos(2 * Math.PI * i / (n - 1)));
            }

            return data;
        }

        public static double[] Windowing(double[] data, int type)
        {
            switch(type)
            {
                case 0:
                    {
                        return RectangularWindowing(data);
                    }
                case 2:
                    {
                        return HammingWindowing(data);
                    }
                case 1:
                    {
                        return HanningWindowing(data);
                    }
                default: return new double[] { };
            }
        }

        #endregion
        #region FFTRegion
        public static Complex[] FFT(Complex[] input)
        {
            int N = input.Length;
            float omega = (float)(-2.0 * Math.PI / N);
            Complex[] result = new Complex[N];

            if (N == 1)
            {
                return new Complex[] { input[0] };
            }


            Complex[] evenInput = new Complex[N / 2];
            Complex[] oddInput = new Complex[N / 2];

            for (int i = 0; i < N / 2; i++)
            {
                evenInput[i] = input[2 * i];
                oddInput[i] = input[2 * i + 1];
            }

            Complex[] even = FFT(evenInput);
            Complex[] odd = FFT(oddInput);

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

        public static Complex[] IFFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] result = new Complex[N];

            // take conjugate
            for (int i = 0; i < N; i++)
            {
                result[i] = input[i].Conjugate();
            }

            // compute forward FFT
            result = FFT(result);

            // take conjugate again
            for (int i = 0; i < N; i++)
            {
                result[i] = result[i].Conjugate();
            }

            return result;
        }

        #endregion

        public static void SaveSound(string fileName, int sampleRate, int chunkSize, List<int> frequencies)
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

        public static void SaveSound(string fileName, List<float> frequencies)
        {
            string resultFileName = "result_" + fileName;
            WaveFormat waveFormat = new WaveFormat();
            using (WaveFileWriter writer = new WaveFileWriter(resultFileName, waveFormat))

            {
                foreach(float sample in frequencies)
                  writer.WriteSample(sample);
            }
        }
    }
}
