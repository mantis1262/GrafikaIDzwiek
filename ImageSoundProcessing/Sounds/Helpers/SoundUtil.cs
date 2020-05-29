using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds.Helpers
{
    public static class SoundUtil
    {
        public static int[][] ChunkIntArray(int [] array, int chunkSize)
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

        public static int MakePowerOf2(int windowWidth)
        {
            int powerOfTwo = 2;

            while (windowWidth > powerOfTwo)
            {
                powerOfTwo *= 2;
            }

            return powerOfTwo;
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
    }
}
