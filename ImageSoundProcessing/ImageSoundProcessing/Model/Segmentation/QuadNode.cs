using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Model.Segmentation
{
    public class QuadNode
    {
        private PixelPoint[][] _values;
        private QuadNode NW = null;
        private QuadNode NE = null;
        private QuadNode SE = null;
        private QuadNode SW = null;

        public PixelPoint[][] Values { get => _values; set => _values = value; }

        public QuadNode(PixelPoint[][] values)
        {
            _values = values;
        }

        public void ExecuteSplit(int threshold, List<PixelPoint[][]> leavesValues)
        {
            if (_values.Length == 1)
            {
                leavesValues.Add(_values);
                return;
            }

            int curMin = _values[0][0].Value;
            int curMax = _values[0][0].Value;

            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0; j < _values[0].Length; j++)
                {
                    int curVal = _values[i][j].Value;
                    if (curMin > curVal)
                    {
                        curMin = curVal;
                    }
                    if (curMax < curVal)
                    {
                        curMax = curVal;
                    }
                }
            }

            if (Math.Abs(curMax - curMin) > threshold)
            {
                Split(threshold, leavesValues);
            }
            else
            {
                leavesValues.Add(_values);
            }
        }

        private void Split(int threshold, List<PixelPoint[][]> leavesValues)
        {
            int quadSize = _values.Length / 2;

            PixelPoint[][] valuesNW = new PixelPoint[quadSize][];
            PixelPoint[][] valuesNE = new PixelPoint[quadSize][];
            PixelPoint[][] valuesSE = new PixelPoint[quadSize][];
            PixelPoint[][] valuesSW = new PixelPoint[quadSize][];

            for (int i = 0; i < quadSize; i++)
            {
                valuesNW[i] = new PixelPoint[quadSize];
                valuesNE[i] = new PixelPoint[quadSize];
                valuesSE[i] = new PixelPoint[quadSize];
                valuesSW[i] = new PixelPoint[quadSize];
            }

            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0; j < _values[0].Length; j++)
                {
                    if (i < quadSize && j < quadSize)
                    {
                        valuesNW[i][j] = new PixelPoint(_values[i][j].Value, _values[i][j].X, _values[i][j].Y);
                    }
                    else if (i >= quadSize && j < quadSize)
                    {
                        valuesNE[i - quadSize][j] = new PixelPoint(_values[i][j].Value, _values[i][j].X, _values[i][j].Y);
                    }
                    else if (i < quadSize && j >= quadSize)
                    {
                        valuesSE[i][j - quadSize] = new PixelPoint(_values[i][j].Value, _values[i][j].X, _values[i][j].Y);
                    }
                    else
                    {
                        valuesSW[i - quadSize][j - quadSize] = new PixelPoint(_values[i][j].Value, _values[i][j].X, _values[i][j].Y);
                    }
                }
            }

            NW = new QuadNode(valuesNW);
            NW.ExecuteSplit(threshold, leavesValues);
            NE = new QuadNode(valuesNE);
            NE.ExecuteSplit(threshold, leavesValues);
            SE = new QuadNode(valuesSE);
            SE.ExecuteSplit(threshold, leavesValues);
            SW = new QuadNode(valuesSW);
            SW.ExecuteSplit(threshold, leavesValues);
        }
    }
}
