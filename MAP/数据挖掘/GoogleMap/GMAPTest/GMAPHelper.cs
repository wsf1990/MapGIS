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
    public class GMAPHelper
    {
        public static void InitMapBox(GMapControl control, GMapProvider provider)
        {
            control.BackColor = Color.CadetBlue;
            control.Position = new PointLatLng(30.981178, 105.351914);

            //gMapControl1.MapProvider.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.BoundsOfMap = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            //control.MapProvider = GMapProviders.GoogleChinaMap;
            control.MapProvider = provider;
            control.DragButton = MouseButtons.Left;

            control.MinZoom = 1;
            control.MaxZoom = 24;
            control.Zoom = 13;

            var routes = new GMapOverlay(control, "routes");
            control.Overlays.Add(routes);

            //添加标记
            var top = new GMapOverlay(control, "top");
            control.Overlays.Add(top);
            var currentMarker = new GMapMarkerGoogleRed(control.Position);
            top.Markers.Add(currentMarker);
            var center = new GMapMarkerCross(control.Position);
            top.Markers.Add(center);

        }
    }
}
