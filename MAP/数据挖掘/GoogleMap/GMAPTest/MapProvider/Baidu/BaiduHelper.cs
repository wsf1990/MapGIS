using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.MapProvider.Baidu
{
    public class BaiduHelper
    {
        public static string GetLocation(string address)
        {
            //Dictionary<string, string> dict = new Dictionary<string,string>();
            //dict.Add("address", address);
            //dict.Add("output","json");
            //dict.Add("ak", AKSNCaculater.ak);
            //var sn = AKSNCaculater.CaculateAKSN("/geocoder/v2/", dict);
            string url = "http://api.map.baidu.com/geocoder/v2/?address=" + address + "&output=json&ak=" + AKSNCaculater.ak + "&callback=showLocation";
            string json = CommonHelper.GetUrl("GET", url);

        }


    }
}
