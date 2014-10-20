using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace GMAPTest.SHP
{
    public class ShpPolygon
    {
        /// <summary>
        /// 文件中的记录号
        /// </summary>
        public int RecordNumber { get; set; }

        /// <summary>
        /// 坐标范围
        /// </summary>
        public double[] Box { get; set; }

        /// <summary>
        /// 表示构成当前面状目标的子环的个数
        /// </summary>
        public int NumParts { get; set; }
        /// <summary>
        /// 坐标点数
        /// </summary>
        public int NumPoints { get; set; }
        /// <summary>
        /// 记录了每个子环的坐标在Points数组中的起始位置  NumParts
        /// </summary>
        public int[] Parts { get; set; }
        /// <summary>
        /// 坐标点 NumPoints
        /// </summary>
        public PointLatLng[] Points { get; set; }
    }
}
