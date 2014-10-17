using GMAPTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 百度SN计算方法
/// </summary>
public class AKSNCaculater
{
    public static string ak = "Dg13FsaBgcF67w40utVswRRV";
    private static string sk = "pLV6EneRMRFPDmAG26FoyfaS4wjWBbE9";
    private static string UrlEncode(string str)
    {
        str = System.Web.HttpUtility.UrlEncode(str);
        byte[] buf = Encoding.ASCII.GetBytes(str);//等同于Encoding.ASCII.GetBytes(str)
        for (int i = 0; i < buf.Length; i++)
            if (buf[i] == '%')
            {
                if (buf[i + 1] >= 'a') buf[i + 1] -= 32;
                if (buf[i + 2] >= 'a') buf[i + 2] -= 32;
                i += 2;
            }
        return Encoding.ASCII.GetString(buf);//同上，等同于Encoding.ASCII.GetString(buf)
    }

    private static string HttpBuildQuery(IDictionary<string, string> querystring_arrays)
    {

        StringBuilder sb = new StringBuilder();
        foreach (var item in querystring_arrays)
        {
            sb.Append(UrlEncode(item.Key));
            sb.Append("=");
            sb.Append(UrlEncode(item.Value));
            sb.Append("&");
        }
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    /// <summary>
    /// 计算SN
    /// </summary>
    /// <param name="url"></param>
    /// <param name="querystring_arrays"></param>
    /// <returns></returns>
    public static string CaculateAKSN(string url, IDictionary<string, string> querystring_arrays)
    {
        var queryString = HttpBuildQuery(querystring_arrays);

        var str = UrlEncode(url + "?" + queryString + sk);

        return CommonHelper.MD5(str);
    }
}
