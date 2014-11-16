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
    /// KML操作类
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
            var marker = new PlaceMark();

            var placemark = rootElement.FirstChild.Name.ToLower() == "document" ? rootElement["Document"]["Placemark"] : rootElement["Placemark"];
            marker.Description = placemark["description"].InnerText;
            marker.Name = placemark["name"].InnerText;
            marker.Point = new KMLPoint(placemark);
            kml.Marker = marker;
            return kml;
        }
        /// <summary>
        /// 写KML
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="kml"></param>
        public static void WriteKML(string fileName, KML kml)
        {
            string kmlFormat = "<kml xmlns=\"http://earth.google.com/kml/2.0\">" +
		                       "<Placemark><description><![CDATA[{0}]]></description><name>{1}</name><Point><coordinates>{2}</coordinates></Point></Placemark></kml>";
            string xml = string.Format(kmlFormat, kml.Marker.Description, kml.Marker.Name, kml.Marker.Point.ToString());
            File.WriteAllText(fileName, xml);
        }
    }
}
