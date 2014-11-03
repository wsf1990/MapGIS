using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using Newtonsoft.Json.Linq;

namespace GMAPTest.Common
{
    public class GoogleHelper
    {
        public static List<GoogleAddress> GetAddress(PointLatLng point)
        {
            string format = "http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&sensor=true_or_false&language=zh-CN";
            string url = string.Format(format, point.Lat, point.Lng);
            string json = CommonHelper.GetUrl("GET", url);
            if(json.ToUpper().Contains("\"OK\""))
            {
                json = CommonHelper.GetGoogleJson(json);
                return CommonHelper.GetObjectByJson<List<GoogleAddress>>(json);
            }
            return null;
        }
    }
}
