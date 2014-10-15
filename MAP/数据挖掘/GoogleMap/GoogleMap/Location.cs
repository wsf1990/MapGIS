using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap
{
    /// <summary>
    /// 瓦片使用的位置信息
    /// </summary>
    public class Location
    {
        /// <summary>
        /// x
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// z
        /// </summary>
        public int Z { get; set; }
    }
    /// <summary>
    /// 普通经纬度信息加级别
    /// </summary>
    public class Location2
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Lon { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Lat { get; set; }
    }

    public static class LocationHelper
    {
        /// <summary>
        /// 瓦片位置信息
        /// </summary>
        public static Location TileLocation(this Location2 loc, int level)
        {
            return new Location(){ X = getXFromLongitude(level, loc.Lon), Y = getYFromLatitude(level, loc.Lat), Z = level };
        }
        /// <summary>
        /// 获取X
        /// </summary>
        /// <returns></returns>
        private static int getXFromLongitude(int level, double lon)
        {
            return (int)(((lon + 180) / 360.0) * Math.Pow(2, level));
        }
        /// <summary>
        /// 获取Y
        /// </summary>
        /// <returns></returns>
        private static int getYFromLatitude(int level, double lat)
        {
            if (lat == 90)
            {
                return 0;
            }
            double sinLat = Math.Sin(Math.PI * lat / 180);
            double y = 0.5 - Math.Log((1 + sinLat) / (1 - sinLat)) / (4 * Math.PI);
            return (int)(y * Math.Pow(2, level));
        }
    }
}
