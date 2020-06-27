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
        public Complex[] fftDataForSpectrumChart;

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
            // dzielimy dane na kawa³ki wg chunkSize
            int[][] parts = SoundUtil.ChunkArray(data, chunkSize);
            autoCorrelations = new List<long[]>();
            List<int> frequencies = new List<int>();

            // analizujemy chunki
            foreach (int[] part in parts)
            {
                // bufor na wyliczenia wartoœci autokorelacji
                long[] autocorrelation = new long[part.Length];

                // liczymy wartoœci autokorelacji
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

                // czêstotliwoœæ podstawow¹ wyliczamy jako iloraz czêstotliwoœci próbkowania
                // i indeksu lokalnego maksimum
                int frequency = (int)(sampleRate / localMaxIndex);
                autoCorrelations.Add(autocorrelation);
                frequencies.Add(frequency);
            }

            return frequencies;   
        }

        private long FindLocalMax(long[] autocorrelation)
        {
            // jako pocz¹tkowe globalne maximum ustawiamy pierwszy element wyników autokorelacji
            double globalMax = autocorrelation[0];
            double tempLocalMax = 0;
            int localMaxIndex = int.MaxValue;
            // na pocz¹tku lokalne minimum jest równe globalnemu maksimum
            double localMin = globalMax;

            // flaga okreœlaj¹ca czy zbocze jest opadaj¹ce
            bool falling = true;
            for (int i = 1; i < autocorrelation.Length; i++)
            {
                // pobieramy bie¿ac¹ wartosæ autokorelacji
                double current = autocorrelation[i];

                // weryfikujemy czy zbocze opada
                if (falling)
                {
                    // sprawdzamy czy bie¿¹ca wartoœæ autokorelacji jest mniejsza od lokalnego minimum
                    if (current < localMin)
                    {
                        // lokalnym minimum staje siê bie¿¹ca wartoœci¹ autokorelacji
                        localMin = current;
                    }
                    // je¿eli ró¿nica miêdzy globalnym maximum a bie¿¹c¹ wartoœci¹ autokorelacji
                    // jest mniejsza od przyjêtej tolerancji ró¿nicy miêdzy globalnym maksimum a lokalnym minimum
                    // wtedy wykrywamy zbocze rosn¹ce a za tymczasowym lokalnym maksimum staje siê bie¿¹ca wartosæ autokorelacji
                    else if (globalMax - current < (globalMax - localMin) * 0.95)
                    {
                        falling = false;
                        tempLocalMax = current;
                    }
                }
                else
                {
                    // sprawdzamy czy bie¿¹ca wartoœæ autokorelacji jest wiêksza od tymczasowego lokalnego maksimum
                    if (current > tempLocalMax)
                    {
                        // tymczasowym lokalnym maksimum staje siê bie¹¿a wartoœæ autkorelacji
                        tempLocalMax = current;
                        localMaxIndex = i;
                    }
                    // je¿eli tymczasowe maksimum lokalne (z tolerancj¹) jest wiêksze od globalnego maksimum
                    // znaleziono indeks lokalnego maksimum
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
            List<int> frequencies = new List<int>();
            float[][] parts;

            // ustawiamy chunkSize jako potêge 2 dla póŸniejszego FFT
            chunkSize = SoundUtil.MakePowerOf2(chunkSize);
            // dzielimy pobrane próbki dŸwiêku na fragmenty
            parts = SoundUtil.ChunkArrayPowerOf2(dataNormalized, chunkSize);

            foreach (float[] buffer in parts)
            {
                // konwertujemy dane wejœciowe spróbkowanego sygna³u na postaæ zespolon¹
                Complex[] complexSound = SoundUtil.SignalToComplex(buffer);

                // przepuszczamy dane przez okno Hamminga
                Complex[] complexWindows = SoundUtil.HammingWindow(complexSound);

                // pierwsze FFT
                Complex[] fftComplex = SoundUtil.FFT(complexWindows);

                // zapamiêtujemy wynik pierwszego FFT w celu wyœwietlenia wykresu
                fftDataForSpectrumChart = new Complex[fftComplex.Length];
                fftComplex.CopyTo(fftDataForSpectrumChart, 0);

                // przepuszczamy modu³ widma przez logarytm
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    fftComplex[i] = new Complex(10.0f * (float)Math.Log10(fftComplex[i].Modulus() + 1), 0);
                }

                // drugie FFT, w celu uzyskania cepstrum
                fftComplex = SoundUtil.FFT(fftComplex);

                // zapamiêtujemy wynik cepstrum w celu wyœwietlenia wykresu
                cepstrum = new Complex[fftComplex.Length];
                fftComplex.CopyTo(cepstrum, 0);

                // bierzemy 1/4 wyników bo po ka¿dym FFT po³owa wyników jest taka sama a FFT wykonujemy 2 razy
                fftComplex = fftComplex.Take(fftComplex.Length / 4).ToArray();

                // modu³y wartoœci z FFT
                double[] dataArray = new double[chunkSize];

                // zapisanie modu³ów wartoœci z FFT do pierwszego wymiaru bufora
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    dataArray[i] = fftComplex[i].Modulus();
                }

                List<int> localMaxIndexes = new List<int>();
                //promieñ, zakres w którym badamy czy wartoœæ jest maksimum lokalnym
                int range = 10;
                          
                for (int i = range; i < dataArray.Length - range; i++)
                {
                    int biggerValue = 0;
                    // badamy otoczenie punktu
                    for (int j = i - range; j < i + range; ++j)
                    { 
                        // je¿eli bie¿¹ca wartoœæ z otoczenia jest mniejsza od punktu,
                        // który sprawdzamy wtedy zliczamy je
                        if (dataArray[j] <= dataArray[i] && i != j)
                            biggerValue++;
                    }

                    // jeœli w otoczeniu nie ma mniejszych wartoœci to 
                    // dodajemy numer wartoœci do tablicy lokalnych maksimów
                    if (biggerValue == (range * 2) - 1)
                    {
                        localMaxIndexes.Add(i);
                    }
                }

                // odrzucanie wysokich wartoœci ale nie stromych
                // musza opadaæ w obu kierunkach
                for (int index = 0; index < localMaxIndexes.Count;)
                {
                    int i = localMaxIndexes[index], leftIndexOffset = 0, rightIndexOffset = 0;

                    // badamy lewe zbocze w poszukiwaniu wartoœci najni¿szej wartoœci,
                    // w której zmienia siê omnotonicznoœæ
                    while ((i - leftIndexOffset - 1) >= 0)
                    {
                        if ((dataArray[i - leftIndexOffset - 1] <= dataArray[i - leftIndexOffset]))
                            ++leftIndexOffset;
                        else break;
                    }

                    // badamy prawe zbocze w poszukiwaniu najwy¿szej wartoœci
                    // w której zmienia siê omnotonicznoœæ
                    while ((i + rightIndexOffset + 1) < dataArray.Length)
                    {
                        if ((dataArray[i + rightIndexOffset + 1] <= dataArray[i + rightIndexOffset]))
                            ++rightIndexOffset;
                        else
                            break;
                    }

                    // progowanie co do najwiêkszego peaku
                    // wybieramy punktu o wiêkszej wartoœci spoœród znalezionych po lewej i prawej stronoe
                    double maxmin = Math.Max(dataArray[i - leftIndexOffset], dataArray[i + rightIndexOffset]);

                    // porównujemy wartoœæ maksymaln¹ minimów z wczeœniej znalezionych punktów i porówujemy je z lokalnym maksem
                    // je¿eli najwiêksza wartoœæ minimalna jest wiêksza od bie¿¹co rozpatrywanego poziomu maksimum
                    if (maxmin > dataArray[i] * 0.2)
                    {
                        // wtedy usuwamy to maksimum
                        localMaxIndexes.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }

                // szukanie pozycji maksymalnej wartoœci z listy lokalnych maksimów
                int max_ind = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dataArray);

                // przeszukanie listy maksimów w celu usuniêcia tych znajduj¹cych siê ponad progiem
                for (int index = 0; index < localMaxIndexes.Count;)
                {
                    // je¿eli bie¿¹ca wartoœæ lokalnego maksimum z listy jest ponad progiem znalezionej wartoœci maksymalnej na liœcie
                    if (dataArray[localMaxIndexes[index]] > dataArray[max_ind] * 0.4)
                    {
                        index++;
                    }
                    else
                    {
                        // w przeciwnym wypadku usuwamy maksimum z listy
                        localMaxIndexes.RemoveAt(index);
                    }
                }

                // zmienne maj¹ce zapamiêtaæ dwa indeksy z listy maksimów
                int max_b, max_a;

                // szukanie pozycji maksymalnej wartoœci z listy lokalnych maksimów
                max_b = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dataArray);

                int a = 0, b = 0;
                while (localMaxIndexes.Count > 1)
                {
                    // usuwamy wczeœniej zapamiêtane maksimum z listy
                    localMaxIndexes.Remove(max_b);

                    // szukanie pozycji maksymalnej wartoœci z listy lokalnych maksimów
                    max_a = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dataArray);

                    // zapamiêtanie wczeœniej pozycji wczeœniej usuniêtego maksimum
                    // i tego znaleziono po nim
                    a = max_a; b = max_b;

                    // je¿eli indeksy maksimów nie s¹ w kolejnoœci rosn¹æ zamieniamy je
                    if (a > b)
                    {
                        int tmp = a;
                        a = b;
                        b = tmp;
                    }

                    for (int i = 0; i < localMaxIndexes.Count;)
                    {
                        int num = localMaxIndexes[i];

                        // je¿eli indeks bie¿¹cego maksimum z listy jest poza przedzia³em
                        // wyznaczonym przez poprzednie zapisane maksima
                        if (num < a || num > b)
                            // usuwamy indeks maksimum z listy
                            localMaxIndexes.RemoveAt(i);
                        else
                            i++;
                    }

                    // najstarszey zapamiêtany indeks maksimum zastêpujemy kolejnym maksimmum po nim
                    max_b = max_a;
                }

                // wyliczamy ró¿nicê miêdzy znalezionymi indeksami maksimów
                max_ind = Math.Abs(b - a);

                // je¿eli lokalnym maksimum z listy jest pierwszy indeks i jedyny indeks na liœcie
                if (max_ind == 0 && localMaxIndexes.Count == 1)
                {
                    max_ind = localMaxIndexes[0];
                }

                // czêstotliwoœæ podstawowa wyliczana jest jako iloraz czêstotliwoœci próbkowania
                // i pozycji maksimum (która tak naprawde wyznacza okres)
                int frequency = (int)(sampleRate / (double)max_ind);
                frequencies.Add(frequency);
            }

            return frequencies;
        }

        public double[] TimeFiltration(int filterLength = 1025, double cutFreq = 550, int windowType = 2)
        {
            // d³ugoœæ sygna³u wynikowego bêdzie wynosi³a K(iloœæ próbek) + L(d³ugoœæ odpowiedzi impulsowej) - 1
            double[] result = new double[dataNormalized.Length + filterLength - 1];

            // obliczenie wspó³czynników filtra
            double[] filterFactors = SoundUtil.LowPassFilterFactors(cutFreq, sampleRate, filterLength);
            
            // wymno¿enie wspó³czynników przez funkcjê okna
            double[] filtered = SoundUtil.Windowing(filterFactors, windowType);

            List<float> data = dataNormalized.ToList();
            float[] zeros = new float[filterLength - 1];

            // uzupe³nienie okna zerami na pocz¹tku i koñcu
            data.InsertRange(0, zeros);
            data.AddRange(zeros);

            // wykonanie operacji splotu 
            for (int i = filterLength - 1; i < data.Count; i++)
            {
                for (int j = 0; j < filtered.Length; j++)
                {
                    result[i - filterLength + 1] += data[i - j] * filtered[j];
                }
            }

            return result;
        }

        public float[] FrequencyFiltration(int windowLength, int filterLength, double cutFreq, int windowHopSize, int windowType, string filterType)
        {
            // rozmiar transformacji DFT
            int n = SoundUtil.GetExpandedPow2(windowLength + filterLength - 1);
            
            // d³ugoœæ sygna³u wynikowego
            int size = dataNormalized.Length + n - windowLength;
            float[] result = new float[size];

            // okna
            double[][] windows = new double[size / windowHopSize][];
            Complex[][] windowsComplex = new Complex[size / windowHopSize][];

            for (int i = 0; i < windows.Length; i++)
            {
                windows[i] = new double[n];
                windowsComplex[i] = new Complex[n];
            }

            // wyliczenie wspó³czynników okna
            double[] windowFactors = SoundUtil.Factors(windowType, windowLength);

            for (int i = 0; i < windows.Length; i++)
            {
                // wymno¿enie wspó³czynników okna przez wartoœci sygna³u 
                for (int j = 0; j < windowLength; j++)
                {
                    if (i * windowHopSize + j < dataNormalized.Length)
                    {
                        windows[i][j] = windowFactors[j] * dataNormalized[i * windowHopSize + j];
                    }
                    else
                    {
                        windows[i][j] = 0;
                    }
                }

                // uzupe³nienie pozosta³ych miejsc zerami
                for (int j = windowLength; j < n; j++)
                {
                    windows[i][j] = 0;
                }
            }

            // wyliczenie wspó³czynników okna
            double[] windowFilterFactors = SoundUtil.Factors(windowType, filterLength);

            // wyliczenie wspó³czynników filtru
            double[] filterFactors = SoundUtil.LowPassFilterFactors(cutFreq, sampleRate, filterLength);
            double[] filtered = new double[n];

            // wymno¿enie wspó³czynników okna i filtru
            for (int i = 0; i < filterLength; i++)
            {
                filtered[i] = windowFilterFactors[i] * filterFactors[i];
            }

            // uzupe³nienie pozosta³ych miejsc zerami
            for (int i = filterLength; i < n; i++)
            {
                filtered[i] = 0;
            }

            if (filterType == "notCasual")
            {
                // dla filtra nieprzyczynowego przesuwamy po³owê wartoœci filtra na jego koniec
                int shiftNumberFilter = (filterLength - 1) / 2;
                IEnumerable<double> shiftedFilter = filtered.Take(shiftNumberFilter);
                List<double> filteredTemp = filtered.Skip(shiftNumberFilter).ToList();
                filteredTemp.AddRange(shiftedFilter);
                filtered = filteredTemp.ToArray();
            }

            // zmiana przefiltrowanych wspó³czynników na liczby zespolone i wykonanie FFT
            Complex[] complexSound = SoundUtil.SignalToComplex(filtered);
            Complex[] filteredComplex = SoundUtil.FFT(complexSound);

            for (int i = 0; i < windows.Length; i++)
            {
                // zmiana wartoœci sygna³u okna na liczby zespolone i wykonanie FFT
                windowsComplex[i] = SoundUtil.FFT(SoundUtil.SignalToComplex(windows[i]));

                // wymno¿enie wartoœci sygna³u okna ze wspó³czynnikami
                for (int j = 0; j < windowsComplex[i].Length; j++)
                {
                    windowsComplex[i][j] *= filteredComplex[j];
                }

                // wykonanie IFFT na oknie i zmiana na sygna³ wyjœciowy
                windows[i] = SoundUtil.SignalFromComplex(SoundUtil.IFFT(windowsComplex[i]));
            }

            // dodanie wyniku do sygna³u wyjœciowego
            for (int i = 0; i < windows.Length; i++)
            {
                for (int j = 0; j < windows[i].Length; j++)
                {
                    if (i * windowHopSize + j < dataNormalized.Length)
                    {
                        result[i * windowHopSize + j] += (float)windows[i][j];
                    }
                }
            }

            return result;
        }
    }
}
