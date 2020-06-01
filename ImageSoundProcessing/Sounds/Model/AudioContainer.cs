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

                // cz�stotliwo�� podstawow� wyliczamy jako iloraz cz�stotliwo�ci pr�bkowania
                // i indeksu lokalnego maksimum
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

            // ustawiamy chunkSize jako pot�ge 2 dla p�niejszego FFT
            chunkSize = SoundUtil.MakePowerOf2(chunkSize);
            // dzielimy pobrane pr�bki d�wi�ku na fragmenty
            parts = SoundUtil.ChunkArrayPowerOf2(dataNormalized, chunkSize);

            foreach (float[] buffer in parts)
            {
                // konwertujemy dane wej�ciowe spr�bkowanego sygna�u na posta� zespolon�
                Complex[] complexSound = SoundUtil.SignalToComplex(buffer);

                // przepuszczamy dane przez okno Hamminga
                Complex[] complexWindows = SoundUtil.HammingWindow(complexSound);

                // pierwsze FFT
                Complex[] fftComplex = SoundUtil.FFT(complexWindows);

                // zapami�tujemy wynik pierwszego FFT w celu wy�wietlenia wykresu
                fftDataForSpectrumChart = new Complex[fftComplex.Length];
                fftComplex.CopyTo(fftDataForSpectrumChart, 0);

                // przepuszczamy modu� widma przez logarytm
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    fftComplex[i] = new Complex(10.0f * (float)Math.Log10(fftComplex[i].Modulus() + 1), 0);
                }

                // drugie FFT, w celu uzyskania cepstrum
                fftComplex = SoundUtil.FFT(fftComplex);

                // zapami�tujemy wynik cepstrum w celu wy�wietlenia wykresu
                cepstrum = new Complex[fftComplex.Length];
                fftComplex.CopyTo(cepstrum, 0);

                // bierzemy 1/4 wynik�w bo po ka�dym FFT po�owa wynik�w jest taka sama a FFT wykonujemy 2 razy
                fftComplex = fftComplex.Take(fftComplex.Length / 4).ToArray();

                // pierwszy wymiar bedzie zawiera� modu�y warto�ci z FFT
                // drugi wymiar b�dzie zawiera� ...
                double[][] dataArray = new double[2][];
                dataArray[0] = new double[chunkSize];
                dataArray[1] = new double[chunkSize];

                // zapisanie modu��w warto�ci z FFT do pierwszego wymiaru bufora
                for (int i = 0; i < fftComplex.Length; ++i)
                {
                    dataArray[0][i] = fftComplex[i].Modulus();
                }

                double[] dat = dataArray[0];
                List<int> localMaxIndexes = new List<int>();
                //promie�, zakres w kt�rym badamy czy warto�� jest maksimum lokalnym
                int range = 1;
                          
                for (int i = range; i < dat.Length - range; i++)
                {
                    int biggerValue = 0;
                    // badamy otoczenie punktu
                    // sprawdz czy jest to ,,dolina o zboczu wysokim na ,,range''
                    // sprawdzamy wysoko��, ale nie stromo�� zbocza - peaki s� ostre
                    for (int j = i - range; j < i + range; ++j)
                    { 
                        // je�eli bie��ca warto�� z otoczenia jest mniejsza od punktu,
                        // kt�ry sprawdzamy wtedy zliczamy je
                        if (dat[j] <= dat[i] && i != j)
                            biggerValue++;
                    }

                    // je�li w otoczeniu nie ma mniejszych warto�ci to 
                    // dodajemy numer warto�ci do tablicy lokalnych maksim�w
                    if (biggerValue == (range * 2) - 1)
                    {
                        localMaxIndexes.Add(i);
                    }
                }

                // odrzucanie wysokich warto�ci ale nie stromych
                // musza opada� w obu kierunkach
                for (int index = 0; index < localMaxIndexes.Count;)
                {
                    int i = localMaxIndexes[index], leftIndexOffset = 0, rightIndexOffset = 0;

                    // badamy lewe zbocze w poszukiwaniu warto�ci najni�szej warto�ci,
                    // w kt�rej zmienia si� omnotoniczno��
                    while ((i - leftIndexOffset - 1) >= 0)
                    {
                        if ((dat[i - leftIndexOffset - 1] <= dat[i - leftIndexOffset]))
                            ++leftIndexOffset;
                        else break;
                    }

                    // badamy prawe zbocze w poszukiwaniu najwy�szej warto�ci
                    // w kt�rej zmienia si� omnotoniczno��
                    while ((i + rightIndexOffset + 1) < dat.Length)
                    {
                        if ((dat[i + rightIndexOffset + 1] <= dat[i + rightIndexOffset]))
                            ++rightIndexOffset;
                        else
                            break;
                    }

                    // progowanie co do najwi�kszego peaku
                    // wybieramy punktu o wi�kszej warto�ci spo�r�d znalezionych po lewej i prawej stronoe
                    double maxmin = Math.Max(dat[i - leftIndexOffset], dat[i + rightIndexOffset]);

                    // por�wnujemy warto�� maksymaln� minim�w z wcze�niej znalezionych punkt�w i por�wujemy je z lokalnym maksem
                    // je�eli najwi�ksza warto�� minimalna jest wi�ksza od bie��co rozpatrywanego poziomu maksimum
                    if (maxmin > dat[i] * 0.2)
                    {
                        // wtedy usuwamy to maksimum
                        localMaxIndexes.RemoveAt(index);
                    }
                    else
                    {
                        // w przeciwnym zapami�tujemy warto�� w drugim wymiarze bufora
                        dataArray[1][i] = dat[i];
                        index++;
                    }
                }

                // szukanie pozycji maksymalnej warto�ci z listy lokalnych maksim�w
                int max_ind = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

                // przeszukanie listy maksim�w w celu usuni�cia tych znajduj�cych si� ponad progiem
                for (int index = 0; index < localMaxIndexes.Count;)
                {
                    // je�eli bie��ca warto�� lokalnego maksimum z listy jest ponad progiem znalezionej warto�ci maksymalnej na li�cie
                    if (dat[localMaxIndexes[index]] > dat[max_ind] * 0.4)
                    {
                        // wtedy zapami�tujemy warto�� w drugim wymiarze bufora
                        dataArray[1][localMaxIndexes[index]] = dat[localMaxIndexes[index]];
                        index++;
                    }
                    else
                    {
                        // w przeciwnym wypadku usuwamy maksimum z listy
                        localMaxIndexes.RemoveAt(index);
                    }
                }

                // zmienne maj�ce zapami�ta� dwa indeksy z listy maksim�w
                int max_b, max_a;

                // szukanie pozycji maksymalnej warto�ci z listy lokalnych maksim�w
                max_b = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

                int a = 0, b = 0;
                while (localMaxIndexes.Count > 1)
                {
                    // usuwamy wcze�niej zapami�tane maksimum z listy
                    localMaxIndexes.Remove(max_b);

                    // szukanie pozycji maksymalnej warto�ci z listy lokalnych maksim�w
                    max_a = SoundUtil.MaxFromLocalMaxList(localMaxIndexes, dat);

                    // zapami�tanie wcze�niej pozycji wcze�niej usuni�tego maksimum
                    // i tego znaleziono po nim
                    a = max_a; b = max_b;

                    // je�eli indeksy maksim�w nie s� w kolejno�ci rosn�� zamieniamy je
                    if (a > b)
                    {
                        int tmp = a;
                        a = b;
                        b = tmp;
                    }

                    for (int i = 0; i < localMaxIndexes.Count;)
                    {
                        int num = localMaxIndexes[i];

                        // je�eli indeks bie��cego maksimum z listy jest poza przedzia�em
                        // wyznaczonym przez poprzednie zapisane maksima
                        if (num < a || num > b)
                            // usuwamy indeks maksimum z listy
                            localMaxIndexes.RemoveAt(i);
                        else
                            i++;
                    }

                    // najstarszey zapami�tany indeks maksimum zast�pujemy kolejnym maksimmum po nim
                    max_b = max_a;
                }

                // wyliczamy r�nic� mi�dzy znalezionymi indeksami maksim�w
                max_ind = Math.Abs(b - a);

                // je�eli lokalnym maksimum z listy jest pierwszy indeks i jedyny indeks na li�cie
                if (max_ind == 0 && localMaxIndexes.Count == 1)
                {
                    max_ind = localMaxIndexes[0];
                }

                // cz�stotliwo�� podstawowa wyliczana jest jako iloraz cz�stotliwo�ci pr�bkowania
                // i pozycji maksimum (kt�ra tak naprawde wyznacza okres)
                int frequency = (int)(sampleRate / (double)max_ind);
                frequencies.Add(frequency);
            }

            return frequencies;
        }
    }
}
