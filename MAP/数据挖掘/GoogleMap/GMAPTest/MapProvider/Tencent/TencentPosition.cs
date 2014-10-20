using GMap.NET;
using GMAPTest.MapProvider.Baidu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public class TencentPosition
    {
        public PointLatLng Location { get; set; }

        public AddressComponent Address_components { get; set; }
        /// <summary>
        /// 查询字符串与查询结果的文本相似度
        /// </summary>
        public int Similarity { get; set; }
        /// <summary>
        /// 误差距离
        /// </summary>
        public float Deviation { get; set; }
        /// <summary>
        /// 可信度参考：值范围 1 <低可信> - 10 <高可信>
        /// </summary>
        public int Reliability { get; set; }
    }

    public class TencentAddress
    {
        public string Address { get; set; }

        public AddressComponent Address_component { get; set; }
    }
}
