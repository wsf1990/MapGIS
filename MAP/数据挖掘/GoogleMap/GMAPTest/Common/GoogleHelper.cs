using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using Newtonsoft.Json.Linq;

namespace GMAPTest.Common
{
    /// <summary>
    /// Google地名解析
    /// 解析时可以设置地区偏向Region = cn  uk等
    /// 还可以设置bounds=34.172684,-118.604794|34.236144,-118.500938
    /// 两种方式时都会优先解析该地区地名
    /// </summary>
    public class GoogleHelper
    {
        #region 1. 地址和地名解析
        /// <summary>
        /// 根据经纬度解析地址
        /// Google地名解析服务
        /// </summary>
        /// <param name="point"></param>
        /// <param name="IsAPIUseUp"></param>
        /// <returns></returns>
        public static List<GoogleAddress> GetAddress(PointLatLng point, out bool IsAPIUseUp)
        {
            IsAPIUseUp = false;
            string format = "http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&sensor=true_or_false&language=zh-CN";
            string url = string.Format(format, point.Lat, point.Lng);
            string json = CommonHelper.GetUrl("GET", url);
            //You have exceeded your daily request quota for this API.  API次数已经用完
            if (json.Contains("OVER_QUERY_LIMIT"))
            {
                IsAPIUseUp = true;
                return null;
            }
            if (json.ToUpper().Contains("\"OK\""))
            {
                json = CommonHelper.GetGoogleJson(json);
                return CommonHelper.GetObjectByJson<List<GoogleAddress>>(json);
            }
            return null;
        }
        /// <summary>
        /// 根据地址解析
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static List<GoogleAddress> GetPoint(string address, out bool IsAPIUseUp)
        {
            IsAPIUseUp = false;
            string format = "http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true_or_false&language=zh-CN";
            string url = string.Format(format, address);
            string json = CommonHelper.GetUrl("GET", url);
            if (json.Contains("OVER_QUERY_LIMIT"))
            {
                IsAPIUseUp = true;
                return null;
            }
            if (json.ToUpper().Contains("\"OK\""))
            {
                json = CommonHelper.GetGoogleJson(json);
                return CommonHelper.GetObjectByJson<List<GoogleAddress>>(json);
            }
            return null;
        } 
        #endregion

        #region 2. API用完处理
        /// <summary>
        /// API用完处理
        /// </summary>
        public static void DoAPI()
        {
            ProcessHelper.CloseP3();
            Thread.Sleep(3000);
            ProcessHelper.OpenP3(@"D:\360安全浏览器下载\psiphon3.exe");
            ProcessHelper.OpenP3(@"D:\360安全浏览器下载\psiphon3.exe");
            bool wait360 = false;
            while (!wait360)
            {
                Thread.Sleep(3000);
                wait360 = ProcessHelper.CheckAndClose360();
            }
        }
        #endregion
    }
}
