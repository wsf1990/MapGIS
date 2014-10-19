using GMap.NET;
using GMap.NET.CacheProviders;
using GMap.NET.MapProviders;
using GMap.NET.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    /// <summary>
    /// 腾讯地图基类
    /// 共四种地图类型
    /// 1、Map
    /// 2、地形
    /// 3、路网
    /// 4、影像
    /// </summary>
    public abstract class TencentMapProviderBase : GMapProvider, GeocodingProvider
    {
        public TencentMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://map.qq.com/";
            Copyright = string.Format("© {0} Tencent - GS({0})6026号  Data©  NavInfo",
                                      DateTime.Today.Year);
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
            int y = (int)pos.Y;
            y = (int)Math.Pow(2, zoom) - 1 - y;
            int num = GetServerNum(pos, 4);
            string url = string.Format(urlFormat, zoom, Math.Floor(pos.X / 16d), Math.Floor(y / 16d), pos.X, y, num);
            return url;
        }

        #region 地名解析
        public Placemark? GetPlacemark(PointLatLng location, out GeoCoderStatusCode status)
        {
            return new TencentHelper().GetAddress(location, out status);
        }

        public GeoCoderStatusCode GetPlacemarks(PointLatLng location, out List<Placemark> placemarkList)
        {
            throw new NotImplementedException();
        }

        public PointLatLng? GetPoint(Placemark placemark, out GeoCoderStatusCode status)
        {
            return GetPoint(placemark.Address, out status);
        }

        public PointLatLng? GetPoint(string keywords, out GeoCoderStatusCode status)
        {
            return new TencentHelper().GetLocation(keywords, out status);
        }

        public GeoCoderStatusCode GetPoints(Placemark placemark, out List<PointLatLng> pointList)
        {
            throw new NotImplementedException();
        }

        public GeoCoderStatusCode GetPoints(string keywords, out List<PointLatLng> pointList)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
    /// <summary>
    /// 腾讯地图瓦片
    /// </summary>
    public class TencentMapProvider : TencentMapProviderBase
    {
        public static TencentMapProvider Instance;

        static TencentMapProvider()
        {
            Instance = new TencentMapProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string urlFormat = "http://p{5}.map.gtimg.com/maptilesv3/{0}/{1}/{2}/{3}_{4}.png";
            //int y = (int)pos.Y;
            //y = (int)Math.Pow(2, zoom) - 1 - y;
            //int num = GetServerNum(pos, 4);
            //string url = string.Format(urlFormat, zoom, Math.Floor(pos.X / 16d), Math.Floor(y / 16d), pos.X, y, num);
            var url = GetUrl(urlFormat, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        private Guid id = new Guid("CEA13E28-92AE-48CB-AB8C-CFC01BD5C23B");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "TencentMap";
        public override string Name
        {
            get { return name; }
        }
    }

    /// <summary>
    /// 腾讯地形瓦片
    /// </summary>
    public class TencentTerrainProvider : TencentMapProviderBase
    {
        public static TencentTerrainProvider Instance;

        static TencentTerrainProvider()
        {
            Instance = new TencentTerrainProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string urlFormat = "http://p{5}.map.gtimg.com/demTiles/{0}/{1}/{2}/{3}_{4}.jpg";
            var url = GetUrl(urlFormat, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        private Guid id = new Guid("342FD3DF-9BE1-4156-BF9E-18808260CAC0");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "TencentTerain";
        public override string Name
        {
            get { return name; }
        }
    }

    /// <summary>
    /// 腾讯地形交通层瓦片
    /// </summary>
    public class TencentTransptationProvider : TencentMapProviderBase
    {
        public static TencentTransptationProvider Instance;

        static TencentTransptationProvider()
        {
            Instance = new TencentTransptationProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string urlFormat = "http://p{5}.map.gtimg.com/demTranTiles/{0}/{1}/{2}/{3}_{4}.png";
            var url = GetUrl(urlFormat, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        private Guid id = new Guid("3146C880-113D-4C1D-BACA-7CE4C1ADB379");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "TencentTransptation";
        public override string Name
        {
            get { return name; }
        }
    }
    /// <summary>
    /// 卫星数据
    /// </summary>
    public class TencentImageProvider : TencentMapProviderBase
    {
        public static TencentImageProvider Instance;

        static TencentImageProvider()
        {
            Instance = new TencentImageProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            //http://p3.map.gtimg.com/sateTiles/12/201/155/3231_2488.jpg
            string urlFormat = "http://p{5}.map.gtimg.com/sateTiles/{0}/{1}/{2}/{3}_{4}.jpg";
            var url = GetUrl(urlFormat, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        private Guid id = new Guid("DC1F0E26-1109-42E9-B154-EB087CF5C15B");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "TencentImage";
        public override string Name
        {
            get { return name; }
        }
    }

    /// <summary>
    /// 卫星交通层数据
    /// </summary>
    public class TencentImageTransProvider : TencentMapProviderBase
    {
        public static TencentImageTransProvider Instance;

        static TencentImageTransProvider()
        {
            Instance = new TencentImageTransProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string urlFormat = "http://p{5}.map.gtimg.com/sateTranTilesv3/{0}/{1}/{2}/{3}_{4}.png";
            var url = GetUrl(urlFormat, pos, zoom);
            //return new MsSQLPureImageCache().GetImageFromCache(0, pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        private Guid id = new Guid("86132F32-B78E-4ADD-A4A6-77D18126426C");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "TencentImageTrans";
        public override string Name
        {
            get { return name; }
        }
    }
}
