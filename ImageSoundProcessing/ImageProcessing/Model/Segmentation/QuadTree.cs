using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Model.Segmentation
{
    public class QuadTree
    {
        private QuadNode _root = null;
        private List<PixelPoint[][]> _regions = new List<PixelPoint[][]>();

        public QuadTree(PixelPoint[][] values)
        {
            _root = new QuadNode(values);
        }

        public List<PixelPoint[][]> Regions { get => _regions; set => _regions = value; }

        public void Process(int threshold)
        {
            _root.ExecuteSplit(threshold, _regions);

            //int sum = 0;

            //foreach (PixelPoint[][] arr in _regions)
            //{
            //    sum += arr.Length * arr.Length;
            //}
        }
    }
}
