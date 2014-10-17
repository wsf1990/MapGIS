using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace GMAPTest.MapProvider.Baidu
{
    /// <summary>
    /// 百度返回的位置信息
    /// </summary>
    public class BaiduPosition
    {
        public PointLatLng location { get; set; }
        /// <summary>
        /// 位置的附加信息，是否精确查找。1为精确查找，0为不精确。
        /// </summary>
        public int precise { get; set; }
        /// <summary>
        /// 可信度
        /// </summary>
        public int confidence { get; set; }
        /// <summary>
        /// 地址类型
        /// </summary>
        public string level { get; set; }
    }

    public class BaiduAddress
    {
        public PointLatLng location { get; set; }
		public string formatted_address { get; set; }
		public string business { get; set; }
		public AddressComponent addressComponent { get; set; }
        public int cityCode { get; set; }
    }

    public class AddressComponent
    {
        public string city { get; set; }
		public string district { get; set; }
		public string province { get; set; }
		public string street { get; set; }
        public string street_number { get; set; }
    }

    public class Location
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }
}