using GMAPTest.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GMAPTest.KML
{
    /// <summary>
    /// KML、KMZ操作类
    /// </summary>
    public class KMLHelper
    {
        #region 1. KML操作
        /// <summary>
        /// 从KML文件中读取KML实体类
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static KML ReadKMLFile(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            var rootElement = doc.DocumentElement;
            var kml = new KML();
            var list = new List<PlaceMark>();

            var places = rootElement.GetElementsByTagName("Placemark");
            foreach (XmlNode place in places)
            {
                var marker = GetMarker(place);
                list.Add(marker);
            }
            //var placemark = rootElement.FirstChild.Name.ToLower() == "document" ? rootElement["Document"]["Placemark"] : rootElement["Placemark"];
            kml.Marker = list;
            return kml;
        }
        /// <summary>
        /// 写KML
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="kml"></param>
        public static void WriteKML(string fileName, KML kml)
        {
            var doc = new XmlDocument();
            var declare = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(declare);

            var root = doc.CreateElement("kml", @"http://earth.google.com/kml/2.0\");
            doc.AppendChild(root);

            foreach (var marker in kml.Marker)
            {
                var placeMarker = CreateMarker(marker, doc);
                root.AppendChild(placeMarker);
            }
            doc.Save(fileName);
        }
        #endregion

        #region 2. 读KML辅助方法
        /// <summary>
        /// 从XmlNode中读取PlaceMark
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        static PlaceMark GetMarker(XmlNode place)
        {
            var marker = new PlaceMark();
            marker.Name = place["name"] != null ? place["name"].InnerText : "";
            marker.LookAt = GetLookAt(place);
            marker.Description = place["description"] != null ? place["description"].InnerText : "";
            marker.Name = place["name"] != null ? place["name"].InnerText : "";
            marker.Point = GetPoint(place);
            return marker;
        }
        /// <summary>
        /// 从XmlNode中读取LookAt
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        static LookAt GetLookAt(XmlNode place)
        {
            var lookat = place["LookAt"];
            if (lookat == null)
                return null;
            var look = new LookAt();
            look.Longitude = Convert.ToDouble(lookat["longitude"].InnerText);

            look.Latitude = Convert.ToDouble(lookat["latitude"].InnerText);

            look.Altitude = Convert.ToDouble(lookat["altitude"].InnerText);

            look.Range = Convert.ToDouble(lookat["range"].InnerText);

            look.Tilt = Convert.ToDouble(lookat["tilt"].InnerText);

            look.Heading = Convert.ToDouble(lookat["heading"].InnerText);
            return look;
        }
        /// <summary>
        /// 从XmlNode中读取KmlPoint
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        static KMLPoint GetPoint(XmlNode place)
        {
            KMLPoint point = new KMLPoint();
            var p = place["Point"];
            if (p == null || p["coordinates"] == null)
                return null;
            string[] temp = p["coordinates"].InnerText.Split(',');
            if (temp.Length < 3)
                return null;
            point.Point = new GMap.NET.PointLatLng(Convert.ToDouble(temp[1]), Convert.ToDouble(temp[0]));
            point.Height = (float)Convert.ToDouble(temp[2]);
            return point;
        } 
        #endregion

        #region 3. 写KML辅助方法
        /// <summary>
        /// 创建一个Placemark节点
        /// </summary>
        /// <param name="marker"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        static XmlNode CreateMarker(PlaceMark marker, XmlDocument doc)
        {
            var placeMarker = doc.CreateElement("Placemark");

            if (!string.IsNullOrWhiteSpace(marker.Description))
            {
                var des = doc.CreateElement("description");
                des.InnerText = marker.Description;
                placeMarker.AppendChild(des);
            }

            if (!string.IsNullOrWhiteSpace(marker.Name))
            {
                var name = doc.CreateElement("name");
                name.InnerText = marker.Name;
                placeMarker.AppendChild(name);
            }

            if (marker.LookAt != null)
            {
                var lookat = CreateLookAt(marker.LookAt, doc);
                placeMarker.AppendChild(lookat);
            }
            if (!string.IsNullOrWhiteSpace(marker.StyleUrl))
            {
                var styleUrl = doc.CreateElement("styleUrl");
                styleUrl.InnerText = marker.StyleUrl;
                placeMarker.AppendChild(styleUrl);
            }
            if (marker.Point != null)
            {
                var point = CreateKmlPoint(marker.Point, doc);
                placeMarker.AppendChild(point);
            }
            return placeMarker;
        }
        /// <summary>
        /// 创建一个lookat节点
        /// </summary>
        /// <param name="look"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        static XmlNode CreateLookAt(LookAt look, XmlDocument doc)
        {
            var lookat = doc.CreateElement("LookAt");

            var longitude = doc.CreateElement("longitude");
            longitude.InnerText = look.Longitude.ToString();
            lookat.AppendChild(longitude);

            var latitude = doc.CreateElement("latitude");
            latitude.InnerText = look.Latitude.ToString();
            lookat.AppendChild(latitude);

            var altitude = doc.CreateElement("altitude");
            altitude.InnerText = look.Altitude.ToString();
            lookat.AppendChild(altitude);

            var range = doc.CreateElement("range");
            range.InnerText = look.Range.ToString();
            lookat.AppendChild(range);

            var tilt = doc.CreateElement("tilt");
            tilt.InnerText = look.Tilt.ToString();
            lookat.AppendChild(tilt);

            var heading = doc.CreateElement("heading");
            heading.InnerText = look.Heading.ToString();
            lookat.AppendChild(heading);
            return lookat;
        } 
        /// <summary>
        /// 创建一个KmlPoint节点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        static XmlNode CreateKmlPoint(KMLPoint point, XmlDocument doc)
        {
            var p = doc.CreateElement("Point");
            var coordinates = doc.CreateElement("coordinates");
            coordinates.InnerText = point.ToString();
            p.AppendChild(coordinates);
            return p;
        }
        #endregion

        #region 4. KMZ操作
        /// <summary>
        /// 读取KMZ文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static KML ReadKMZ(string fileName)
        {
            ZipHelper.UnZip(fileName);
            string kmlPath = Path.Combine(Path.GetDirectoryName(fileName), "doc.kml");
            var kml = ReadKMLFile(kmlPath);
            File.Delete(kmlPath);
            return kml;
        } 
        /// <summary>
        /// 写KMZ文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="kml"></param>
        public static void WriteKMZ(string fileName, KML kml)
        {
            string temp = Path.GetDirectoryName(fileName);
            string name = Path.GetFileName(fileName);
            string kmlPath = Path.Combine(temp, "doc.kml");
            WriteKML(kmlPath, kml);
            ZipHelper.Zip(kmlPath, Path.Combine(temp, name));
            File.Delete(kmlPath);
        }
        #endregion
    }
}
