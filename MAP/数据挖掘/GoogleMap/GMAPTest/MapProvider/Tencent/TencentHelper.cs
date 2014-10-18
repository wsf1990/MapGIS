using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public class TencentHelper : IMapHelper
    {
        string key = "E42BZ-EVHAG-SDIQV-IROHG-25X5V-WXBFZ";
        /// <summary>
        /// 解析地名
        /// </summary>
        /// <param name="address"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public GMap.NET.PointLatLng GetLocation(string address, out GMap.NET.GeoCoderStatusCode code)
        {
            var add = address.Split(',');
            string street = add[0] ?? "北京";
            string region = "";
            if (add.Length >= 2)
                region = add[1];
            string url = "http://apis.map.qq.com/ws/geocoder/v1?address=" + street + "&region=" + region + "&key=" + key;
            string json = CommonHelper.GetUrl("GET", url);
            if (json.Contains("query ok"))
            {
                json = CommonHelper.GetResultJsonTencent(json);
                code = GMap.NET.GeoCoderStatusCode.G_GEO_SUCCESS;
                var pos = CommonHelper.GetObjectByJson<TencentPosition>(json);
                return pos.Location;
            }
            code = GMap.NET.GeoCoderStatusCode.G_GEO_UNKNOWN_ADDRESS;
            return new GMap.NET.PointLatLng(0, 0);
        }
        /// <summary>
        /// 获取地址名称
        /// </summary>
        /// <param name="point"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public GMap.NET.Placemark GetAddress(GMap.NET.PointLatLng point, out GMap.NET.GeoCoderStatusCode code)
        {
            //coord_type	-	 输入的locations的坐标类型
            //可选值为[1,6]之间的整数，每个数字代表的类型说明：
            //1 GPS坐标
            //2 sogou经纬度
            //3 baidu经纬度
            //4 mapbar经纬度
            //5 [默认]腾讯、google、高德坐标
            //6 sogou墨卡托
            int coord_typte = 3;
            string urlFormat = "http://apis.map.qq.com/ws/geocoder/v1?location={0},{1}&coord_typte={2}&key={3}";
            string url = string.Format(urlFormat, point.Lat, point.Lng, coord_typte, key);
            string json = CommonHelper.GetUrl("GET", url);
            if (json.Contains("query ok"))
            {
                json = CommonHelper.GetResultJsonTencent(json);
                code = GMap.NET.GeoCoderStatusCode.G_GEO_SUCCESS;
                var pos = CommonHelper.GetObjectByJson<TencentAddress>(json);
                return new GMap.NET.Placemark() { CountryName = pos.Address_component.nation, StreetNumber = pos.Address_component.street, HouseNo = pos.Address_component.street_number, DistrictName = pos.Address_component.district };
            }
            code = GMap.NET.GeoCoderStatusCode.G_GEO_UNKNOWN_ADDRESS;
            return new GMap.NET.Placemark();
        }
    }
}
