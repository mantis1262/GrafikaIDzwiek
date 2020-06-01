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

                // pierwszy wymiar bedzie zawieraæ modu³y wartoœci z FFT
                // drugi wymiar bêdzie zawieraæ ...
                double[][] dataArray = new double[2][];
                dataArray[0] = new double[chunkSize];
                dataArray[1] = new double[chunkSize];

                // zapisanie modu³ów wartoœci z FFT do pierwszego wymiaru bufora
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    dataArray[0][i] = fftComplex[i].Modulus();
                }

                double[] dat = dataArray[0];
                List<int> localMaxIndexes = new List<int>();
                //promieñ, zakres w którym badamy czy wartoœæ jest maksimum lokalnym
                int range = 1;
                          
                for (int i = range; i < dat.Length - range; i++)
                {
                    int biggerValue = 0;
                    // badamy otoczenie punktu
                    // sprawdz czy jest to ,,dolina o zboczu wysokim na ,,range''
                    // sprawdzamy wysokoœæ, ale nie stromoœæ zbocza - peaki s¹ ostre
                    for (int j = i - range; j < i + range; ++j)
                    { 
                        // je¿eli bie¿¹ca wartoœæ z otoczenia jest mniejsza od punktu,
                        // który sprawdzamy wtedy zliczamy je
                        if (dat[j] <= dat[i] && i != j)
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
                        if ((dat[i - leftIndexOffset - 1] <= dat[i - leftIndexOffset]))
                            ++leftIndexOffset;
                        else break;
                    }

                    // badamy prawe zbocze w poszukiwaniu najwy¿szej wartoœci
                    // w której zmienia siê omnotonicznoœæ
                    while ((i + rightIndexOffset + 1) < dat.Length)
                    {
                        if ((dat[i + rightIndexOffset + 1] <= dat[i + rightIndexOffset]))
                            ++rightIndexOffset;
                        else
                            break;
                    }

                    // progowanie co do najwiêkszego peaku
                    // wybieramy punktu o wiêkszej wartoœci spoœród znalezionych po lewej i prawej stronoe
                    double maxmin = Math.Max(dat[i - leftIndexOffset], dat[i + rightIndexOffset]);

                    // porównujemy wartoœæ maksymaln¹ minimów z wczeœniej znalezionych punktów i porówujemy je z lokalnym maksem
                    // je¿eli najwiêksza wartoœæ minimalna jest wiêksza od bie¿¹co rozpatrywanego poziomu maksimum
                    if (maxmin > dat[i] * 0.2)
                    {
                        // wtedy usuwamy to maksimum
                        localMaxIndexes.RemoveAt(index);
                    }
                    else
                    {
                        // w przeciwnym zapamiêtujemy wartoœæ w drugim wymiarze bufora
                        dataArray[1][i] = dat[i];
                        index++;
                    }
                }

                // szukanie pozycji maksymalnej wartoœci z listy lokalnych maksimów
                int max_ind = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

                // przeszukanie listy maksimów w celu usuniêcia tych znajduj¹cych siê ponad progiem
                for (int index = 0; index < localMaxIndexes.Count;)
                {
                    // je¿eli bie¿¹ca wartoœæ lokalnego maksimum z listy jest ponad progiem znalezionej wartoœci maksymalnej na liœcie
                    if (dat[localMaxIndexes[index]] > dat[max_ind] * 0.4)
                    {
                        // wtedy zapamiêtujemy wartoœæ w drugim wymiarze bufora
                        dataArray[1][localMaxIndexes[index]] = dat[localMaxIndexes[index]];
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
                max_b = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

                int a = 0, b = 0;
                while (localMaxIndexes.Count > 1)
                {
                    // usuwamy wczeœniej zapamiêtane maksimum z listy
                    localMaxIndexes.Remove(max_b);

                    // szukanie pozycji maksymalnej wartoœci z listy lokalnych maksimów
                    max_a = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

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
    }
}
