using ImageSoundProcessing.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Model.Segmentation
{
    public class RegionSplittingAndMerging
    {
        private int threshold;
        private int minimumPixelsForRegion;

        public int Threshold { get => threshold; set => threshold = value; }
        public int MinimumPixelsForRegion { get => minimumPixelsForRegion; set => minimumPixelsForRegion = value; }

        public RegionSplittingAndMerging()
        {
        }

        public RegionSplittingAndMerging(int threshold, int minimumPixelsForRegion)
        {
            this.threshold = threshold;
            this.minimumPixelsForRegion = minimumPixelsForRegion;
        }

        public Bitmap Process(Bitmap originalBitmap)
        {
            int size = originalBitmap.Width;
            Bitmap resultBitmap = new Bitmap(originalBitmap);
            int[,] imageWithRegionNumbers = new int[size, size];
            LockBitmap originalBitmapLock = new LockBitmap(originalBitmap);
            LockBitmap resultBitmapLock = new LockBitmap(resultBitmap);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            resultBitmapLock.LockBits(ImageLockMode.WriteOnly);

            resultBitmapLock.Pixels = (byte[])originalBitmapLock.Pixels.Clone();

            for (int n = 0; n < imageWithRegionNumbers.GetLength(0); n++)
                for (int n2 = 0; n2 < imageWithRegionNumbers.GetLength(1); n2++)
                {
                    imageWithRegionNumbers[n, n2] = -10;
                }

            PixelPoint[][] pixelPoints = new PixelPoint[size][];

            for (int x = 0; x < size; x++)
            {
                pixelPoints[x] = new PixelPoint[size];
                for (int y = 0; y < size; y++)
                {
                    pixelPoints[x][y] = new PixelPoint(originalBitmapLock.GetPixel(x, y).R, x, y);
                }
            }

            QuadTree tree = new QuadTree(pixelPoints);
            tree.Process(threshold);

            List<PixelPoint[][]> regions = tree.Regions;
            SortedDictionary<int, PixelPoint[][]> regionsMap = new SortedDictionary<int, PixelPoint[][]>();
            for (int i = 0; i < regions.Count; i++)
            {
                PixelPoint[][] region = regions[i];
                for (int x = 0; x < region.Length; x++)
                {
                    for (int y = 0; y < region[0].Length; y++)
                    {
                        imageWithRegionNumbers[region[x][y].X, region[x][y].Y] = i;
                    }
                }
                regionsMap.Add(i, region);
            }

            int[,] regionsMask = new int[size, size];

            for(int n = 0; n < regionsMask.GetLength(0); n++)
                for(int n2 = 0; n2 < regionsMask.GetLength(1); n2++)
                {
                    regionsMask[n, n2] = -10;
                }

            int foundRegions = 0;
            
            while (regionsMap.Count != 0)
            {
                int curKey = regionsMap.First().Key;

                PixelPoint[][] cur = regionsMap[curKey];
                Stack<int> regionNumbers = new Stack<int>();
                int[,] finalMask = new int[size, size];

                for (int n = 0; n < finalMask.GetLength(0); n++)
                    for (int n2 = 0; n2 < finalMask.GetLength(1); n2++)
                    {
                        finalMask[n, n2] = -10;
                    }

                regionNumbers.Push(curKey);
                int curMin = GetMin(cur);
                int curMax = GetMax(cur);

                while (cur != null)
                {
                    for (int x = 0; x < cur.Length; x++)
                    {
                        for (int y = 0; y < cur.Length; y++)
                        {
                            regionsMask[cur[x][y].X, cur[x][y].Y] = cur[x][y].Value;
                            finalMask[cur[x][y].X, cur[x][y].Y] = Colors.MIN_PIXEL_VALUE;
                        }
                    }

                    HashSet<int> adjacentRegions = new HashSet<int>();

                    for (int x = 1; x < regionsMask.GetLength(0) - 1; x++)
                    {
                        for (int y = 1; y < regionsMask.GetLength(1) - 1; y++)
                        {
                            if (regionsMask[x, y] == -10)
                            {
                                if (regionsMask[x + 1, y] != -10 ||
                                    regionsMask[x - 1, y] != -10 ||
                                    regionsMask[x, y + 1] != -10 ||
                                    regionsMask[x, y - 1] != -10)
                                {
                                    int regionNumber = imageWithRegionNumbers[x, y];
                                    adjacentRegions.Add(regionNumber);
                                }
                            }
                        }
                    }

                    foreach (int key in adjacentRegions)
                    {
                        PixelPoint[][] region;
                        regionsMap.TryGetValue(key, out region);
                        if (region != null)
                        {
                            int regionMin = GetMin(region);
                            int regionMax = GetMax(region);

                            int newMin = (regionMin < curMin) ? regionMin : curMin;
                            int newMax = (regionMax > curMax) ? regionMax : curMax;

                            if (Math.Abs(newMax - newMin) <= threshold)
                            {
                                for (int x = 0; x < region.Length; x++)
                                {
                                    for (int y = 0; y < region.Length; y++)
                                    {
                                        regionsMask[region[x][y].X, region[x][y].Y] = region[x][y].Value;
                                        finalMask[region[x][y].X, region[x][y].Y] = Colors.MIN_PIXEL_VALUE;
                                    }
                                }
                                if (!regionNumbers.Contains(key))
                                {
                                    regionNumbers.Push(key);
                                }
                            }
                        }
                    }

                    regionsMap.Remove(curKey);
                    if (regionNumbers.Count != 0)
                    {
                        curKey = regionNumbers.Pop();
                        regionsMap.TryGetValue(curKey, out cur);
                    }
                    else
                    {
                        cur = null;
                    }
                }

                int regionElements = 0;
                for (int i = 0; i < finalMask.GetLength(0); i++)
                {
                    for (int j = 0; j < finalMask.GetLength(0); j++)
                    {
                        if (finalMask[i, j] >= 0)
                        {
                            regionElements++;
                        }
                    }
                }

                if (regionElements >= minimumPixelsForRegion)
                {
                    for (int i = 0; i < finalMask.GetLength(0); i++)
                    {
                        for (int j = 0; j < finalMask.GetLength(1); j++)
                        {
                            if (finalMask[i, j] >= 0)
                            {
                                resultBitmapLock.SetPixel(i, j, Color.FromArgb( 
                                    Colors.MIN_PIXEL_VALUE, 
                                    Math.Abs(255 - foundRegions * 15) > 256 ? 255 : Math.Abs(255 - foundRegions * 15),
                                    Math.Abs(255 - foundRegions * 15) > 256 ? 255 : Math.Abs(255 - foundRegions * 15)));
                            }
                        }
                    }

                    foundRegions++;
                }
                else
                {
                    for (int i = 0; i < finalMask.GetLength(0); i++)
                    {
                        for (int j = 0; j < finalMask.GetLength(1); j++)
                        {
                            if (finalMask[i, j] >= 0)
                            {
                                regionsMask[i, j] = -10;
                            }
                        }
                    }
                }
            }

            originalBitmapLock.UnlockBits();
            resultBitmapLock.UnlockBits();
            return resultBitmap;
        }

        private int GetMin(PixelPoint[][] pixelPoints)
        {
            int min = pixelPoints[0][0].Value;
            for (int i = 0; i < pixelPoints.Length; i++)
            {
                for (int j = 0; j < pixelPoints[0].Length; j++)
                {
                    int curVal = pixelPoints[i][j].Value;
                    if (min > curVal)
                    {
                        min = curVal;
                    }
                }
            }
            return min;
        }

        private int GetMax(PixelPoint[][] pixelPoints)
        {
            int max = pixelPoints[0][0].Value;
            for (int i = 0; i < pixelPoints.Length; i++)
            {
                for (int j = 0; j < pixelPoints[0].Length; j++)
                {
                    int curVal = pixelPoints[i][j].Value;
                    if (max < curVal)
                    {
                        max = curVal;
                    }
                }
            }
            return max;
        }
    }
}
