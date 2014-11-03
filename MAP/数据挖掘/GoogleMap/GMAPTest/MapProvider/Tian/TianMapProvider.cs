using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.MapProvider.Tian
{
    /// <summary>
    /// 天地图抽象基类
    /// 地图
    /// 影像
    /// 地形
    /// 三维
    /// </summary>
    public abstract class TianMapProviderBase : GMapProvider
    {
        public TianMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://map.qq.com/";
            Copyright = string.Format("GS({0})6032号", DateTime.Today.Year);
        }

        private GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                overlays = overlays ?? new GMapProvider[] { this };
                return overlays;
            }
        }
        public override GMap.NET.PureProjection Projection
        {
            get { return MercatorProjection.Instance; }
        }
        /// <summary>
        /// 获取瓦片url地址
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <param name="pos"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public string GetUrl(string urlFormat, GPoint pos, int zoom)
        {
            PointLatLng empty = PointLatLng.Empty;
            GSize tileMatrixSizePixel = new MercatorProjection().GetTileMatrixSizePixel(zoom);
            double width = tileMatrixSizePixel.Width;
            double height = tileMatrixSizePixel.Height;
            double num3 = (double)pos.X / width - 0.5;
            double num4 = 0.5 - (double)pos.Y / height;
            empty.Lat = 90.0 - ((360.0 * Math.Atan(Math.Exp((-num4 * 2.0) * 3.1415926535897931))) / 3.1415926535897931);
            empty.Lng = 360.0 * num3;


            var latlng = new MercatorProjection().FromPixelToLatLng(pos.X, pos.Y, zoom);
            int y = (int)pos.Y;
            //y = (int)Math.Pow(2, zoom) - 1 - y;
            var ll = new PointLatLng();
            
            //y = y - (int)Math.Pow(2, zoom - 2) + (int)Math.Pow(2, zoom - 7) + (int)Math.Pow(2, zoom - 9) + (int)Math.Pow(2, zoom - 12) + (int)Math.Pow(2, zoom - 13);
            //y *= 2;
            int num = GetServerNum(pos, 8);
            string url = string.Format(urlFormat, num, pos.X, y, zoom);
            return url;
        }
    }
    /// <summary>
    /// 天地图地图
    /// </summary>
    public class TianMapProvider : TianMapProviderBase
    {
        public static TianMapProvider Instance;
        static TianMapProvider()
        {
            Instance = new TianMapProvider();
        }
        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string format = "http://t{0}.tianditu.com/DataServer?T=vec_c&x={1}&y={2}&l={3}";
            string url = GetUrl(format, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        public override Guid Id
        {
            get { return new Guid("920CBFE9-4631-4671-82DC-91134E1F2FC9"); }
        }

        public override string Name
        {
            get { return "TianMap"; }
        }
    }
}
