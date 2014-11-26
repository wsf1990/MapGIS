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
        /// <summary>
        /// 从KML文件中读取KML实体类
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static KML ReadKMLFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement rootElement = doc.DocumentElement;
            KML kml = new KML();
            var list = new List<PlaceMark>();

            var places = rootElement.GetElementsByTagName("Placemark");
            foreach (XmlNode place in places)
            {
                var marker = new PlaceMark();
                marker.Name = place["name"].InnerText;
                marker.LookAt = GetLookAt(place);
                marker.Description = place["description"].InnerText;
                marker.Name = place["name"].InnerText;
                marker.Point = new KMLPoint(place);
                list.Add(marker);
            }
            //var placemark = rootElement.FirstChild.Name.ToLower() == "document" ? rootElement["Document"]["Placemark"] : rootElement["Placemark"];
            kml.Marker = list;
            return kml;
        }

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
                var placeMarker = doc.CreateElement("Placemark");

                var name = doc.CreateElement("name");
                name.InnerText = marker.Name;
                placeMarker.AppendChild(name);

                if (marker.LookAt != null)
                {
                    var lookat = doc.CreateElement("LookAt");

                    var longitude = doc.CreateElement("longitude");
                    longitude.InnerText = marker.LookAt.Longitude.ToString();
                    lookat.AppendChild(longitude);

                    var latitude = doc.CreateElement("latitude");
                    latitude.InnerText = marker.LookAt.Latitude.ToString();
                    lookat.AppendChild(latitude);

                    var altitude = doc.CreateElement("altitude");
                    altitude.InnerText = marker.LookAt.Altitude.ToString();
                    lookat.AppendChild(altitude);

                    var range = doc.CreateElement("range");
                    range.InnerText = marker.LookAt.Range.ToString();
                    lookat.AppendChild(range);

                    var tilt = doc.CreateElement("tilt");
                    tilt.InnerText = marker.LookAt.Tilt.ToString();
                    lookat.AppendChild(tilt);

                    var heading = doc.CreateElement("heading");
                    heading.InnerText = marker.LookAt.Heading.ToString();
                    lookat.AppendChild(heading);

                    placeMarker.AppendChild(lookat);
                }

                var styleUrl = doc.CreateElement("styleUrl");
                styleUrl.InnerText = marker.StyleUrl;
                placeMarker.AppendChild(styleUrl);

                var point = doc.CreateElement("Point");
                var coordinates = doc.CreateElement("coordinates");
                coordinates.InnerText = marker.Point.ToString();
                point.AppendChild(coordinates);

                placeMarker.AppendChild(point);

                root.AppendChild(placeMarker);

            }
            doc.Save(fileName);
        }

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
    }
}
