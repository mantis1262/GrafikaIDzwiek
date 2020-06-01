using NAudio.Wave;
using Sounds.Helpers;
using Sounds.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sound.Model
{
    public class AudioContainer
    {
        public string fileName;
        public int chunkSize = 44100;
        public int sampleRate;
        public int framesNumber;
        public int[] data;
        public float[] dataNormalized;
        public TimeSpan totalTime;
        public List<long[]> autoCorrelations;
        public Complex[] cepstrum;
        public Complex[] spectrum;

        public void OpenWav(string filename)
        {
            short[] sampleBuffer;
            using (WaveFileReader reader = new WaveFileReader(filename))
            {
                fileName = Path.GetFileName(filename);
                sampleRate = reader.WaveFormat.SampleRate;
                framesNumber = (int)reader.SampleCount * reader.WaveFormat.Channels;
                totalTime = reader.TotalTime;
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                sampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            int[] result = new int[sampleBuffer.Length];
            float[] resultNormalized = new float[sampleBuffer.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = sampleBuffer[i];
                resultNormalized[i] = sampleBuffer[i] / (float)(short.MaxValue + 1);
            }

            data = result;
            dataNormalized = resultNormalized;
        }

        public List<int> Autocorrelation()
        {
            // dzielimy dane na kawa�ki wg chunkSize
            int[][] parts = SoundUtil.ChunkArray(data, chunkSize);
            autoCorrelations = new List<long[]>();
            List<int> frequencies = new List<int>();

            // analizujemy chunki
            foreach (int[] part in parts)
            {
                // bufor na wyliczenia warto�ci autokorelacji
                long[] autocorrelation = new long[part.Length];

                // liczymy warto�ci autokorelacji
                for (int m = 1; m < autocorrelation.Length; m++)
                {
                    long sum = 0;
                    for (int n = 0; n < autocorrelation.Length - m; n++)
                    {
                        sum += part[n] * part[n + m];
                    }
                    autocorrelation[m - 1] = sum;
                }

                // szukamy lokalnego maksimum
                long localMaxIndex = FindLocalMax(autocorrelation);

                // cz�stotliwo�� podstawow� wyliczamy jako iloraz cz�stotliwo�ci pr�bkowania i czasu lokalnego maximum
                int frequency = (int)(sampleRate / localMaxIndex);
                autoCorrelations.Add(autocorrelation);
                frequencies.Add(frequency);
            }

            return frequencies;   
        }

        private long FindLocalMax(long[] autocorrelation)
        {
            // jako pocz�tkowe globalne maximum ustawiamy pierwszy element wynik�w autokorelacji
            double globalMax = autocorrelation[0];
            double tempLocalMax = 0;
            int localMaxIndex = int.MaxValue;
            // na pocz�tku lokalne minimum jest r�wne globalnemu maksimum
            double localMin = globalMax;

            // flaga okre�laj�ca czy zbocze jest opadaj�ce
            bool falling = true;
            for (int i = 1; i < autocorrelation.Length; i++)
            {
                // pobieramy bie�ac� wartos� autokorelacji
                double current = autocorrelation[i];

                // weryfikujemy czy zbocze opada
                if (falling)
                {
                    // sprawdzamy czy bie��ca warto�� autokorelacji jest mniejsza od lokalnego minimum
                    if (current < localMin)
                    {
                        // lokalnym minimum staje si� bie��ca warto�ci� autokorelacji
                        localMin = current;
                    }
                    // je�eli r�nica mi�dzy globalnym maximum a bie��c� warto�ci� autokorelacji
                    // jest mniejsza od przyj�tej tolerancji r�nicy mi�dzy globalnym maksimum a lokalnym minimum
                    // wtedy wykrywamy zbocze rosn�ce a za tymczasowym lokalnym maksimum staje si� bie��ca wartos� autokorelacji
                    else if (globalMax - current < (globalMax - localMin) * 0.95)
                    {
                        falling = false;
                        tempLocalMax = current;
                    }
                }
                else
                {
                    // sprawdzamy czy bie��ca warto�� autokorelacji jest wi�ksza od tymczasowego lokalnego maksimum
                    if (current > tempLocalMax)
                    {
                        // tymczasowym lokalnym maksimum staje si� bie��a warto�� autkorelacji
                        tempLocalMax = current;
                        localMaxIndex = i;
                    }
                    // je�eli tymczasowe maksimum lokalne (z tolerancj�) jest wi�ksze od globalnego maksimum
                    // znaleziono lokalne maksimum
                    else if (1.05 * tempLocalMax > globalMax)
                    {
                        return localMaxIndex;
                    }
                }
            }

            return localMaxIndex;
        }

        public List<int> Cepstrum()
        {
            int chunkSize = this.chunkSize;
            int windowWidth = framesNumber;
            int N = SoundUtil.MakePowerOf2(windowWidth);
            List<int> frequencies = new List<int>();
            float[][] parts = new float[1][];

            chunkSize = SoundUtil.MakePowerOf2(chunkSize);
            N = chunkSize;
            parts = SoundUtil.ChunkArrayPowerOf2(dataNormalized, chunkSize);

            foreach (float[] buffer in parts)
            {
                Complex[] complexSound = SoundUtil.SignalToComplex(buffer);

                //hammming window
                Complex[] complexWindows = SoundUtil.HammingWindow(complexSound);
                Complex[] fftComplex = SoundUtil.FftDit1d(complexWindows);
                fftComplex = fftComplex.Take(fftComplex.Length / 2).ToArray();


                spectrum = new Complex[fftComplex.Length];
                fftComplex.CopyTo(spectrum, 0);

                //cepstrum rzeczywiste i zespolone
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    fftComplex[i] = new Complex(10.0f * (float)Math.Log10(fftComplex[i].Modulus() + 1), 0);
                }

                fftComplex = SoundUtil.FftDit1d(fftComplex);
                fftComplex = fftComplex.Take(fftComplex.Length / 2).ToArray();
                cepstrum = fftComplex;
                double[][] d = new double[2][];
                d[0] = new double[N];
                d[1] = new double[N];

                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    //power cepstrum
                    d[0][i] = fftComplex[i].Modulus();
                }

                double[] dd = d[0];
                List<int> pperiod = new List<int>();

                //RANGE
                int range = 10;

                for (int i = range; i < dd.Length - range; i++)
                {
                    int bigger = 0;

                    //sprawdz czy jest to ,,dolina o zboczu wysokim na ,,range''
                    //sprawdzamy wysoko��, ale nie stromo�� zbocza - peaki s� ostre
                    for (int j = i - range; j < i + range; ++j)
                    {
                        if (dd[j] <= dd[i] && i != j)
                            bigger++;
                    }

                    //sprawdz czy zbocza sa tak wysokie jak to zalozylismy
                    if (bigger == (range * 2) - 1)
                    {
                        pperiod.Add(i);
                    }
                }

                //odrzucanie wysokich ale peakow ale nie stromych
                //musza opadac w obu kierunkach - nisko
                for (int index = 0; index < pperiod.Count;)
                {
                    int i = pperiod[index], j = 0, k = 0;

                    //szukamy najni�szego wartosci na zboczu lewym
                    while (i - j - 1 >= 0)
                    {
                        if ((dd[i - j - 1] <= dd[i - j]))
                            ++j;
                        else
                            break;
                    }

                    //szukamy najnizszej wartosci na zboczu prawym
                    while (((i + k + 1) < dd.Length))
                    {
                        if ((dd[i + k + 1] <= dd[i + k]))
                            ++k;
                        else
                            break;
                    }

                    double maxmin = Math.Max(dd[i - j], dd[i + k]);
                    if (maxmin > dd[i] * 0.2)
                    {
                        pperiod.RemoveAt(index);
                    }
                    else
                    {
                        d[1][i] = dd[i];
                        index++;
                    }
                }

                //progowanie co do najwi�kszego peaku
                int max_ind = SoundUtil.MaxFromPeriods(pperiod, dd);

                for (int index = 0; index < pperiod.Count;)
                {
                    int num = pperiod[index];
                    if (dd[num] > dd[max_ind] * 0.4)
                    {
                        d[1][num] = dd[num];
                        index++;
                    }
                    else
                    {
                        pperiod.RemoveAt(index);
                    }
                }

                int max_b, max_a;
                max_b = SoundUtil.MaxFromPeriods(pperiod, dd);

                int a = 0, b = 0;
                while (pperiod.Count > 1)
                {
                    for (int i = 0; i < pperiod.Count;)
                    {
                        if (pperiod[i] == max_b)
                        {
                            pperiod.RemoveAt(i);
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    max_a = SoundUtil.MaxFromPeriods(pperiod, dd);
                    a = max_a; b = max_b;

                    if (a > b)
                    {
                        int tmp = a;
                        a = b;
                        b = tmp;
                    }

                    for (int i = 0; i < pperiod.Count;)
                    {
                        int num = pperiod[i];
                        if (num < a || num > b)
                            pperiod.RemoveAt(i);
                        else
                            i++;
                    }

                    max_b = max_a;
                }

                max_ind = Math.Abs(b - a);
                if (max_ind == 0 && pperiod.Count == 1)
                {
                    max_ind = pperiod[0];
                }

                int frequency = (int)(sampleRate / (double)max_ind);
                frequencies.Add(frequency);
            }

            return frequencies;
        }
    }
}
