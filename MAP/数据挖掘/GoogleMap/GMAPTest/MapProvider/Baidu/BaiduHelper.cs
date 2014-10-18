using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using Newtonsoft.Json.Linq;

namespace GMAPTest.MapProvider.Baidu
{
    public class BaiduHelper : IMapHelper
    {
        /// <summary>
        /// 地名解析为经纬度
        /// </summary>
        /// <param name="address"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public PointLatLng GetLocation(string address, out GeoCoderStatusCode status)
        {
            //Dictionary<string, string> dict = new Dictionary<string,string>();
            //dict.Add("address", address);
            //dict.Add("output","json");
            //dict.Add("ak", AKSNCaculater.ak);
            //var sn = AKSNCaculater.CaculateAKSN("/geocoder/v2/", dict);
            string url = "http://api.map.baidu.com/geocoder/v2/?address=" + address + "&output=json&ak=" +
                         AKSNCaculater.ak;
            string json = CommonHelper.GetUrl("GET", url);
            if (json.Contains("\"status\":0"))
            {
                status = GeoCoderStatusCode.G_GEO_SUCCESS;
                var str = CommonHelper.GetResultJsonBaidu(json);
                var pos = CommonHelper.GetObjectByJson<BaiduPosition>(str);
                return pos.location;
            }
            status = GeoCoderStatusCode.G_GEO_UNKNOWN_ADDRESS;
            return new PointLatLng(0, 0);

        }

        public Placemark GetAddress(PointLatLng point, out GeoCoderStatusCode status)
        {
            //坐标的类型，目前支持的坐标类型包括：bd09ll（百度经纬度坐标）、gcj02ll（国测局经纬度坐标）、wgs84ll（ GPS经纬度）
            string coordtype = "bd09ll";
            string url = "http://api.map.baidu.com/geocoder/v2/?ak=" + AKSNCaculater.ak + "&callback=renderReverse&location=" + point.Lat + "," + point.Lng + "&output=json&pois=0&coordtype=" + coordtype;
            string json = CommonHelper.GetUrl("GET", url);
            status = GeoCoderStatusCode.G_GEO_SUCCESS;
            var str = CommonHelper.GetResultJsonBaidu(json);
            var address = CommonHelper.GetObjectByJson<BaiduAddress>(str);
            Placemark placemark = new Placemark();
            placemark.LocalityName = address.formatted_address;
            placemark.DistrictName = address.addressComponent.district;//区
            placemark.StreetNumber = address.addressComponent.street;
            placemark.HouseNo = address.addressComponent.street_number;
            return placemark;
        }
    }
}
