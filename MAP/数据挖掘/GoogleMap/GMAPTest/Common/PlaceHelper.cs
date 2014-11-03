using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Common
{
    /// <summary>
    /// 地名解析批量循环实现
    /// </summary>
    public class PlaceHelper
    {
        public static void GetPoint()
        {
            var list = File.ReadAllLines("D:\\simiao.txt");
            List<string> lines = new List<string>();
            var lists = GetPoints(list.ToList());
            foreach (var s in lists)
            {
                lines.Add(s.Geometry.Location.Lat + "," + s.Geometry.Location.Lng);
            }
            File.WriteAllLines("D:\\res.txt", lines);
        }
        /// <summary>
        /// 根据地址返回匹配的目标
        /// </summary>
        /// <param name="addresses"></param>
        public static List<GoogleAddress> GetPoints(List<string> addresses)
        {
            var list = new List<GoogleAddress>();
            for (int i = 0; i < addresses.Count; i++)
            {
                var ps = GoogleHelper.GetPoint(addresses[i]);
                if (ps == null || ps.Count <= 0)
                {
                    list.Add(new GoogleAddress());
                }
                else
                {
                    list.Add(ps.FirstOrDefault());//提取匹配的第一个目标
                }
            }
            return list;
        }

        /// <summary>
        /// 根据经纬度列表获取地址列表，取匹配的第一条记录
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<GoogleAddress> GetAddresses(List<PointLatLng> points)
        {
            var addresses = new List<GoogleAddress>();
            foreach (var item in points)
            {
                var add = GoogleHelper.GetAddress(item).FirstOrDefault();
                if (add == null)
                    addresses.Add(new GoogleAddress());
                else
                    addresses.Add(add);
            }
            return addresses;
        }
    }
}
