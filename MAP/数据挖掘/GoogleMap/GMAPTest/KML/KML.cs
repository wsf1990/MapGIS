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
        public PlaceMark Marker { get; set; }
    }

    public class PlaceMark
    {
        public string Description{get;set;}
        public string Name{get;set;}
            //<LookAt>
            //    <longitude>-122.0839</longitude>
            //    <latitude>37.4219</latitude>
            //    <range>540.68</range>
            //    <tilt>0</tilt>
            //    <heading>3</heading>
            //</LookAt>
		public KMLPoint Point{get;set;}
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
