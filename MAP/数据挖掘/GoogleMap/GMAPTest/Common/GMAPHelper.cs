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
using GMAPTest.Marker.ToolTip;

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
        GMapControl Control, Eagel;

        public GMapHelper(GMapControl con, GMapControl eagle)
        {
            this.Control = con;
            this.Eagel = eagle;

            
        }
        #region 1. 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="control"></param>
        /// <param name="provider"></param>
        public void InitMapBox(GMapProvider provider)
        {
            Control.BackColor = Color.CadetBlue;
            Control.Position = new PointLatLng(39.9, 116.4);
            //Control
            Singleton<GMaps>.Instance.ShuffleTilesOnLoad = true;
            //gMapControl1.MapProvider.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.BoundsOfMap = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            //control.MapProvider = GMapProviders.GoogleChinaMap;
            Control.MapProvider = provider;
            Eagel.MapProvider = provider;
            Eagel.DragButton = MouseButtons.Left;
            Control.DragButton = MouseButtons.Left;

            Control.MinZoom = 4;//百度最低level为4
            Eagel.MinZoom = 4;
            Eagel.MaxZoom = 14;
            Control.MaxZoom = 19;
            Control.Zoom = 4;
            Eagel.Zoom = 4;
            //添加第二图层
            //GMapOverlay overlay = new GMapOverlay("second");
            //overlay.
            //添加路径层
            Route = new GMapOverlay("routes");
            Control.Overlays.Add(Route);

            //添加标记层
            Top_Marker = new GMapOverlay("top");
            Control.Overlays.Add(Top_Marker);

            //注册Marker事件
            Control.OnMarkerClick += (s1, s2) =>
                {
                    //s1.ToolTipText = "lm";
                    RemoveMarker(s1);
                };
            Control.OnPolygonClick += (s1, s2) =>
                {
                    s1.Fill = new SolidBrush(Color.Red);
                };
        } 
        #endregion

        #region 添加标记、线、矩形等
        /// <summary>
        /// 添加标记
        /// </summary>
        /// <returns></returns>
        public GMapMarker AddMarker(PointLatLng markerPosition)
        {
            Top_Marker.Markers.Clear();
            //var currentMarker = new GMapMarkerGoogleRed(markerPosition);//Google红点

            var currentMarker = new MyHomeMarker(markerPosition);
            currentMarker.ToolTip = new MyToolTip(currentMarker);
            Top_Marker.Markers.Add(currentMarker);
            var center = new GMarkerCross(markerPosition);
            //var center = new GMapMarker(markerPosition);//十字叉丝
            Top_Marker.Markers.Add(center);
            return currentMarker;
        }
        /// <summary>
        /// 画出所有点
        /// </summary>
        /// <param name="list"></param>
        public void AddMarker(List<PointLatLng> list)
        {
            //Top_Marker.Markers.Clear();
            foreach (var pointLatLng in list)
            {
                var currentMarker = new GMarkerGoogle(pointLatLng, GMarkerGoogleType.red);//Google红点
                Top_Marker.Markers.Add(currentMarker);
            }
        }
        /// <summary>
        /// 移除Marker
        /// </summary>
        /// <param name="marker"></param>
        public void RemoveMarker(GMapMarker marker)
        {
            Top_Marker.Markers.Remove(marker);
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
        /// <summary>
        /// 画多边形
        /// </summary>
        /// <param name="points"></param>
        public void DrawPolygn(params PointLatLng[] points)
        {
            var list = points.ToList();
            var mapPolygn = new GMapPolygon(list, "polygn");
            Route.Polygons.Add(mapPolygn);
        }
        /// <summary>
        /// 画四边形
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawBox(PointLatLng start, PointLatLng end)
        {
            var p2 = new PointLatLng() {Lat = start.Lat, Lng = end.Lng};
            var p4 = new PointLatLng() {Lat = end.Lat, Lng = start.Lng};
            DrawPolygn(start, p2, end, p4);
        }
        /// <summary>
        /// 绘制圆，未实现
        /// </summary>
        /// <param name="point"></param>
        public void DrawCircle(PointLatLng point, params string[] addressName)
        {
            //var circle = new GMarkerCross(point);
            var circle = new CircleMarker(point);
            //var circle = new GMarkerGoogle(point, GMarkerGoogleType.pink_pushpin);
            //var circle = new GMarkerGoogle(point, new Bitmap("Snow.png"));//使用自定义图标完成Mark
            //var circle = new MyHomeMarker(point);
            circle.ToolTipText = addressName[0] ?? "wsf";
            circle.ToolTip = new MyToolTip(circle);
            circle.ToolTipPosition.Offset(new Point(10, 20));
            circle.ToolTipMode = MarkerTooltipMode.Always;
            Top_Marker.Markers.Add(circle);
        }
        #endregion

        #region 地名解析
        public PointLatLng? GetAddressPoint(string address)
        {
            GeoCoderStatusCode code = GeoCoderStatusCode.Unknow;
            var provider = Control.MapProvider as GeocodingProvider;
            provider = provider ?? GMapProviders.OpenStreetMap as GeocodingProvider;//如果为空就使用OSM
            var point = provider.GetPoint(address, out code);
            return point;
        }
        /// <summary>
        /// 根据地址查询位置
        /// </summary>
        /// <param name="city"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<PointLatLng> SearchAddress(string address)
        {
            //string search = string.Format("{0},{1}", city, address);
            GeoCoderStatusCode code = Control.SetPositionByKeywords(address);
            List<PointLatLng> list = null;
            if (code == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                var provider = Control.MapProvider as GeocodingProvider;
                provider = provider ?? GMapProviders.OpenStreetMap as GeocodingProvider;//如果为空就使用OSM
                code = provider.GetPoints(address, out list);
            }
            return list;
        }
        /// <summary>
        /// 绘制地址
        /// </summary>
        /// <param name="address"></param>
        public void DrawAddress(string address)
        {
            var list = SearchAddress(address);
            DrawAddress(list);
        }
        /// <summary>
        /// 绘制地址
        /// </summary>
        /// <param name="pos"></param>
        public void DrawAddress(PointLatLng pos)
        {
            Control.Zoom = 15;
            AddMarker(pos);
        }
        /// <summary>
        /// 绘制地址
        /// </summary>
        /// <param name="list"></param>
        public void DrawAddress(List<PointLatLng> list)
        {
            foreach (var pos in list)
            {
                DrawAddress(pos);
            }
        }

        /// <summary>
        /// 根据坐标查询地址
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public string GetPlaceName(PointLatLng place)
        {
            //GeocodingProvider provider = GoogleChinaMapProvider.Instance;
            var provider = Control.MapProvider as GeocodingProvider;
            provider = provider ?? GMapProviders.OpenStreetMap as GeocodingProvider;//如果为空就使用OSM
            GeoCoderStatusCode code = GeoCoderStatusCode.Unknow;
            var mark = provider.GetPlacemark(place, out code);
            if (mark.HasValue)
                return mark.Value.HouseNo;
            return "";
        } 
        #endregion

        #region 路线规划
        /// <summary>
        /// 查找路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public MapRoute FindRoute(PointLatLng start, PointLatLng end)
        {
            RoutingProvider rp = Control.MapProvider as RoutingProvider;
            rp = rp ?? OpenStreetMapProvider.Instance as RoutingProvider;
            MapRoute route = rp.GetRoute(start, end, false, true, (int)Control.Zoom);
            if (route.Distance < 10)//小于10km放到18级继续算，其他可以以此类推
            {
                Control.Zoom = 18;
                route = rp.GetRoute(start, end, false, true, (int)Control.Zoom);
            }
            return route;
        }
        /// <summary>
        /// 根据地址查找路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public MapRoute FindRoute(string start, string end)
        {
            //此法有Bug  ,采用求出坐标的方式代替之
            /*RoutingProvider rp = Control.MapProvider as RoutingProvider;
            rp = rp ?? OpenStreetMapProvider.Instance as RoutingProvider;
            MapRoute route = rp.GetRoute(start, end, false, true, (int)Control.Zoom);
            GMapRoute mapRoute = new GMapRoute(route.Points, route.Name);
            Route.Routes.Add(mapRoute);*/

            return FindRoute(SearchAddress(start).FirstOrDefault(), SearchAddress(end).FirstOrDefault());
        }
        /// <summary>
        /// 绘制路径
        /// </summary>
        /// <param name="route"></param>
        public void DrawRoute(MapRoute route)
        {
            GMapRoute mapRoute = new GMapRoute(route.Points, route.Name);
            Route.Routes.Add(mapRoute);
        }
        #endregion
    }
}
