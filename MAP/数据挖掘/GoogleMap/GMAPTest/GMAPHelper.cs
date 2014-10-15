using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GMAPTest
{
    /// <summary>
    /// GMap帮助类
    /// </summary>
    public class GMapHelper
    {
        /// <summary>
        /// 第一层用于显示标记
        /// </summary>
        GMapOverlay Top_Marker;

        /// <summary>
        /// 用于画线的层
        /// </summary>
        GMapOverlay Route;

        /// <summary>
        /// 显示控件
        /// </summary>
        GMapControl Control;

        public GMapHelper(GMapControl con)
        {
            this.Control = con;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="control"></param>
        /// <param name="provider"></param>
        public void InitMapBox(GMapProvider provider)
        {
            Control.BackColor = Color.CadetBlue;
            Control.Position = new PointLatLng(30.981178, 105.351914);

            //gMapControl1.MapProvider.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.BoundsOfMap = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            //control.MapProvider = GMapProviders.GoogleChinaMap;
            Control.MapProvider = provider;
            Control.DragButton = MouseButtons.Left;

            Control.MinZoom = 1;
            Control.MaxZoom = 24;
            Control.Zoom = 5;

            Route = new GMapOverlay(Control, "routes");
            Control.Overlays.Add(Route);

            //添加标记
            Top_Marker = new GMapOverlay(Control, "top");
            Control.Overlays.Add(Top_Marker);
           

        }
        /// <summary>
        /// 添加标记
        /// </summary>
        /// <returns></returns>
        public GMapMarker AddMarker(PointLatLng markerPosition)
        {
            Top_Marker.Markers.Clear();
            //var currentMarker = new GMapMarkerGoogleRed(markerPosition);//Google红点

            var currentMarker = new MyHomeMarker(markerPosition);

            Top_Marker.Markers.Add(currentMarker);
            var center = new GMapMarkerCross(markerPosition);//十字叉丝
            Top_Marker.Markers.Add(center);
            return currentMarker;
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawLine(PointLatLng start, PointLatLng end)
        {
            Route.Routes.Clear();
            List<PointLatLng> list = new List<PointLatLng>() { start, end };
            GMapRoute route = new GMapRoute(list, "road");
            Pen stroke = new Pen(Color.Red, 5);
            route.Stroke = stroke;
            Route.Routes.Add(route);
        }
        /// <summary>
        /// 返回a和b之间的角度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int GetAngle(PointLatLng a, PointLatLng b)
        {
            int ang = (int)(Math.Atan((b.Lng - a.Lng) / (b.Lat - a.Lat)) * 180 / Math.PI);
            if (ang > 360)
                ang -= 360;
            if (ang < 0)
                ang += 360;
            return ang;
        }

        public List<PointLatLng> SearchAddress(string city, string address)
        {
            //string search = string.Format("{0},{1}", city, address);
            GeoCoderStatusCode code = Control.SetCurrentPositionByKeywords(address);
            List<PointLatLng> list = null;

            if (code == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                var provider = Control.MapProvider as GeocodingProvider;
                provider = provider ?? GMapProviders.OpenStreetMap as GeocodingProvider;//如果为空就使用OSM
                provider.GetPoints(address, out list);
            }

            return list;
        }
    }
}
