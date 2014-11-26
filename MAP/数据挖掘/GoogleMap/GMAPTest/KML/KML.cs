using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GMAPTest.KML
{
    /// <summary>
    /// KML实体类
    /// </summary>
    public class KML
    {
        public List<PlaceMark> Marker { get; set; }
    }

    public class PlaceMark
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Visibility { get; set; }
        public LookAt LookAt { get; set; }
        public KMLPoint Point { get; set; }
        public string StyleUrl { get; set; }
    }

    public class LookAt
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public double Range { get; set; }
        public double Tilt { get; set; }
        public double Heading { get; set; }

    }

    /// <summary>
    /// KML中的Point类
    /// </summary>
    public class KMLPoint
    {
        public PointLatLng Point { get; set; }
        public float Height { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Point.Lng, Point.Lat, Height);
        }

        public KMLPoint(XmlNode placemark)
        {
            string[] temp = placemark["Point"]["coordinates"].InnerText.Split(',');
            Point = new GMap.NET.PointLatLng(Convert.ToDouble(temp[1]), Convert.ToDouble(temp[0]));
            Height = (float)Convert.ToDouble(temp[2]);
        }
    }

}
