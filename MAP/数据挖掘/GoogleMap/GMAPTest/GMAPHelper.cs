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
            Control.Zoom = 13;

            //var routes = new GMapOverlay(control, "routes");
            //control.Overlays.Add(routes);

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
            var currentMarker = new GMapMarkerGoogleRed(markerPosition);//Google红点
            Top_Marker.Markers.Add(currentMarker);
            var center = new GMapMarkerCross(markerPosition);//十字叉丝
            Top_Marker.Markers.Add(center);
            return currentMarker;
        }
    }
}
