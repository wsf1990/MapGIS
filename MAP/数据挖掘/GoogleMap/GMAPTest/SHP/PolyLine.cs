using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    public class PolyLine
    {
        /// <summary>
        /// 坐标范围
        /// </summary>
        public double[] Box{get;set;}

        /// <summary>
        /// 子线段个数
        /// </summary>
        public int NumParts { get; set; }
        /// <summary>
        /// 坐标点数
        /// </summary>
        public int NumPoints { get;set; }
        /// <summary>
        /// 记录了每个子线段的坐标在Points数组中的起始位置  NumParts
        /// </summary>
        public int[] Parts { get; set; }
        /// <summary>
        /// 坐标点 NumPoints
        /// </summary>
       public PointLatLng[] Points { get; set; }
    }
}
