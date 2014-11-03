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
            foreach (var s in list)
            {
                var ps = GoogleHelper.GetPoint(s);
                if (ps == null || ps.Count <= 0)
                {
                    lines.Add("无");
                }
                else
                {
                    lines.Add(ps.FirstOrDefault().Geometry.Location.Lat + "," + ps.FirstOrDefault().Geometry.Location.Lng);
                }
            }
            File.WriteAllLines("D:\\res.txt", lines);
        }
    }
}
