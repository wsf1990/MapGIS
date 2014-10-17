using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public class CommonHelper
    {
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取URL地址网页数据
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string GetUrl(string method, string url, string postData = "")
        {
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = method.ToUpper();
                request.ContentType = "application/x-www-form-urlencoded";
                if (method.ToUpper() == "POST")
                {
                    if (!string.IsNullOrWhiteSpace(postData))
                    {
                        byte[] data = encoding.GetBytes(postData);
                        request.ContentLength = data.Length;
                        using (Stream outstream = request.GetRequestStream())
                        {
                            outstream.Write(data, 0, data.Length);
                        }
                    }
                }
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                using (Stream instream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(instream, encoding))
                    {
                        //返回结果网页（html）代码
                        string content = sr.ReadToEnd();
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        #region JSON处理
        /// <summary>
        /// 根据Dictionary获取JSON数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string GetJsonByDict(Dictionary<string, string> dict)
        {
            string str = "{";
            foreach (var item in dict.Keys)
            {
                str += "\"" + item + "\":\"" + dict[item] + "\",";
            }
            str = str.TrimEnd(',');
            str += "}";
            return str;
        }
        /// <summary>
        /// 根据列表得到JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetJsonByList<T>(List<T> list)
        {
            JsonSerializer js = new JsonSerializer();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter jw = new JsonTextWriter(sw);
            js.Serialize(jw, list);
            return sb.ToString();
        }
        /// <summary>
        /// 根据json得到对象，可以是单一也可以是列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T GetObjectByJson<T>(string json)
        {
            StringReader sr = new StringReader(json);
            //使用JsonSerializer解析数组
            JsonSerializer js = new JsonSerializer();
            return js.Deserialize<T>(new JsonTextReader(sr));
        }
        /// <summary>
        /// 获取对象Json对象,此处也可使用匿名类的方式生成json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetJson<T>(T t)
        {
            JsonSerializer js = new JsonSerializer();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter jw = new JsonTextWriter(sw);
            js.Serialize(jw, t);
            return sb.ToString();
        }
        #endregion
    }
}
