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



     

        public static double[] lowPassFilter(int cutFrequency, double samplingFrequency, double[] windowData)
        {

            double[] result = new double[windowData.Length];
            windowData.CopyTo(result, 0);
            int resultLenght = result.Length;
            double arg = (resultLenght - 1) / 2;
            double[] lowPassFilterArr = new double[resultLenght];

            for (int k = 0; k < result.Length; ++k)
            {
                if (k == arg)
                {
                    lowPassFilterArr[k] = 2 * cutFrequency / samplingFrequency;
                }
                else
                {
                    lowPassFilterArr[k] = Math.Sin( 2 * Math.PI * cutFrequency / samplingFrequency * (k - arg)) / (Math.PI * (k - arg));
                }
                result[k] *= lowPassFilterArr[k];
            }
            return result;
        }

        public static double[] LowPassFilterFactors(double cutFreq, double sampleFreq, int filterLength)
        {
            double[] result = new double[filterLength];
            var half = (filterLength - 1) / 2.0;
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

        public static double[] Splot(double[] signalData, double[] impulseData)
        {
            double[] result = new double[signalData.Length];
            for (int n = 0; n < signalData.Length; n++)
            {
                result[n] = 0;
                for (int k = 0; k < impulseData.Length; k++)
                {
                    if (n - k < 0)
                        continue;
                    if (n - k > signalData.Length)
                        continue;
                    result[n] += signalData[n - k] * impulseData[k]; 
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

        public static double[] Factors(string type, int windowsLength)
        {
            switch (type)
            {
                case "rect":
                    {
                        return RectangularFactors(windowsLength);
                    }
                case "hamm":
                    {
                        return HammingFactors(windowsLength);
                    }
                case "hann":
                    {
                        return HannFactors(windowsLength);
                    }
                default: return new double[] { };
            }
        }

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

        public static double[] Windowing(double[] data, string type)
        {
            switch(type)
            {
                case "rect":
                    {
                        return RectangularWindowing(data);
                    }
                case "hamm":
                    {
                        return HammingWindowing(data);
                    }
                case "hann":
                    {
                        return HanningWindowing(data);
                    }
                default: return new double[] { };
            }
        }

        public static double[] AddZerosCasual(int howMany, double[] data)
        {
            
            double[] result = new double[MakePowerOf2(data.Length + howMany)];
            data.CopyTo(result, 0);
            return result;
        }
        public static double[] AddZerosNotCasual(int howMany, double[] data)
        {
            double[] result = new double[MakePowerOf2(data.Length + howMany)];
            for (int i = 0; i<data.Length; i++)
            {
                if (i < data.Length / 2)
                    result[result.Length - data.Length / 2 + i] = data[i];
                else
                    result[i - data.Length / 2] = data[i];
            }
            return result;
        }

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

        public static Complex[] FFT2(Complex[] input, int R)
        {
            int N = input.Length;
            float omega = (float)(-2.0 * Math.PI / N);
            Complex[] result = new Complex[N];

            if (N == 1)
            {
                return new Complex[] { input[0] };
            }

            if (N % 2 != 0)
            {
                throw new ArgumentException("N has to be the power of 2.");
            }

            Complex[] evenInput = new Complex[N / 2];
            Complex[] oddInput = new Complex[N / 2];

            for (int i = 0; i < N / 2; i++)
            {
                if (2 * i + R < 0 || 2 * i + R > N)
                {
                    evenInput[i] = new Complex(0, 0);
                    oddInput[i] = new Complex(0, 0);
                }
                else
                {

                    evenInput[i] = input[2 * i + R];
                    oddInput[i] = input[2 * i + 1 + R];
                }
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

        public static double[] STFT(int R, double[] wnd, double[] data)
        {
            int N = data.Length;
            Complex [] input = SoundUtil.SignalToComplex(data);
            Complex [] output = new Complex[N];
            double [] amplitude = new double[N];
            double [] frequencies = new double[N / 2];

            for (int k = 0; k < N; ++k)
            {
                double sumReal = 0.0D;
                double sumImag = 0.0D;
                for (int n = 0; n < N; ++n)
                {
                    for (int m = 0; m < wnd.Length; ++m)
                    {
                        if (n - m + R < 0 || n - m + R >= N)
                        {
                            sumReal += 0.0D;
                            sumImag += 0.0D;
                        }
                        else
                        {
                            double angle = -2 * Math.PI * (double)n * (double)k / (double)N;
                            sumReal += input[n - m + R].Real * wnd[m] * Math.Cos(angle);
                            sumImag += input[n - m + R].Real * wnd[m] * Math.Sin(angle);
                        }
                    }
                }
                output[k] = new Complex((float)(sumReal * 2.0f / N), (float)(sumImag * 2.0f / N));

                if (k < N / 2)
                {
                    frequencies[k] = (double)(N * k);
                }
            }
            for (int i = 0; i < output.Length; ++i)
            {
                amplitude[i] = output[i].Modulus();
            }

            return frequencies;
            
        }


        public static Complex[] ISTFT(double[] windowData, double[] stft)
        {
            int N = stft.Length;
            Complex[] input = new Complex[N];
            Complex[] output = new Complex[N];
            for(int i = 0; i< N; i++)
            {
                input[i] = new Complex(0, 1);
            }
            int samplesFrequency = N;
            for (int n = 0; n < N; ++n)
            {
                double sumReal = 0.0D;
                foreach (double v in windowData)
                {
                    for (int k = 0; k < samplesFrequency; ++k)
                    {
                        double angle = 2 * Math.PI * k * n / N;
                        sumReal += stft[k] * Math.Sin(angle) + input[k].Imaginary * Math.Cos(angle);
                    }
                    output[n] =new Complex((float)(100.0f * sumReal / (N * v)),0);
                }
            }
            return output;
        }

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

        public static void SaveSound(string fileName, int sampleRate, List<float> frequencies)
        {
            string resultFileName = "result_" + fileName;
            WaveFormat waveFormat = new WaveFormat(sampleRate: sampleRate, channels: 1);
            using (WaveFileWriter writer = new WaveFileWriter(resultFileName, waveFormat))
            {
                writer.WriteSamples(frequencies.ToArray(), 0, frequencies.Count);
            }
        }
    }
}
