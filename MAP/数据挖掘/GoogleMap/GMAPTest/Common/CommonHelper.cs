using System.Globalization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace GMAPTest
{
    public class CommonHelper
    {
        #region 1. MD5
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
        #endregion

        #region 2. URL读取内容
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
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
                //request.Host = "ogc.tianditu.com";
                //request.Connection = "Keep-Alive";
                //request.ProtocolVersion = HttpVersion.Version10;
                //request.Referer = "http://www.tianditu.com/guide/index.html";
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
        #endregion

        #region 3. JSON处理
        /// <summary>
        /// 获取返回JSON中的result
        /// </summary>
        /// <returns></returns>
        public static string GetResultJsonBaidu(string json)
        {
            return GetResultJson(json, 1);
        }

        public static string GetResultJsonTencent(string json)
        {
            return GetResultJson(json, 2);
        }

        public static string GetResultJson(string json, int index)
        {
            json = json.Substring(json.IndexOf("{")).Trim(')');
            return JObject.Parse(json).Properties().Select(s => s.Value.ToString()).ToArray()[index];
        }
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

        #region 4. Unicode处理
        /// <summary>
        /// 获取Unicode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUnicode(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2)
                r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetStr(string code)
        {
            //\u9910
            MatchCollection mc = Regex.Matches(code, @"\\u([\w]{2}([\w]{2}))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var bytes = new byte[2];
            var sb = new StringBuilder();
            foreach (Match match in mc)
            {
                bytes[0] = (byte)int.Parse(match.Groups[2].Value, NumberStyles.HexNumber);
                bytes[1] = (byte)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
                sb.Append(Encoding.Unicode.GetString(bytes));
            }
            return sb.ToString();
        }
        #endregion

        #region 5. 图像处理
        /// <summary>
        /// 图像转换为字节数组
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static byte[] GetBytesFormBM(string fileName)
        {
            using (Bitmap bm = new Bitmap(fileName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }
        /// <summary>
        /// 得到瓦片存放路径
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static string GetImgFileName(GMap.NET.GPoint pos, int zoom)
        {
            return Path.Combine("root", zoom.ToString(), pos.X.ToString(), pos.Y + ".png");
        }
        #endregion

        #region 6. 字节转换
        /// <summary>
        /// 位序转换
        /// </summary>
        /// <param name="lbt"></param>
        /// <returns></returns>
        public static int ChangeOrder(byte[] lbt)
        {
            //int a = 9994;
            //byte[] lbt = BitConverter.GetBytes(a);  //将int转变为byte
            byte[] bbt = new byte[4];             //用于存放big byte，维数为4
            bbt[0] = lbt[3];                       //0
            bbt[1] = lbt[2];                       //0
            bbt[2] = lbt[1];                       //39
            bbt[3] = lbt[0];                       //10
            int a = BitConverter.ToInt32(bbt, 0);
            return a;
        }
        /// <summary>
        /// 位序转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ChangeOrder(int b)
        {
            return ChangeOrder(BitConverter.GetBytes(b));
        }
        #endregion
    }
}
