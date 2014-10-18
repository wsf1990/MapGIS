using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
   public class Utility
    {
       /// <summary>
       /// 获取两点之间距离，单位为米
       /// </summary>
       /// <param name="a"></param>
       /// <param name="b"></param>
       /// <returns></returns>
       public static double GetDistance(PointLatLng a, PointLatLng b)
       {
           return GMapProviders.EmptyProvider.Projection.GetDistance(a, b);
           //double lat = Rad(Math.Abs(a.Lat - b.Lat));
           //double lng = Rad(Math.Abs(a.Lng - b.Lng));
           //double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(lat / 2), 2) +
           //  Math.Cos(Rad(a.Lat)) * Math.Cos(Rad(b.Lat)) * Math.Pow(Math.Sin(lng / 2), 2)));
           //var gs = GaussSphere.Xian80;
           //s = s * (gs == GaussSphere.WGS84 ? 6378137.0 : (gs == GaussSphere.Xian80 ? 6378140.0 : 6378245.0));
           //s = Math.Round(s * 10000) / 10000;
           //return s;
       }
       /// <summary>
       /// 弧度
       /// </summary>
       /// <param name="d"></param>
       /// <returns></returns>
       static double Rad(double d)
       {
           return d * Math.PI / 180;
       }
    }

   /// <summary>
   /// 高斯投影中所选用的参考椭球
   /// </summary>
   public enum GaussSphere
   {
       Beijing54,
       Xian80,
       WGS84,
   }
}
