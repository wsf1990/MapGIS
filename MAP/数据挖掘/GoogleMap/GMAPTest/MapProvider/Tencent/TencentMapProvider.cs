using GMap.NET;
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

    public class TencentMapProvider: TencentMapProviderBase
    {
        public static TencentMapProvider Instance;

        static TencentMapProvider()
        {
            Instance = new TencentMapProvider();
        }
        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string urlFormat = "http://p{5}.map.gtimg.com/maptilesv3/{0}/{1}/{2}/{3}_{4}.png";
            int y = (int)pos.Y;
            y = (int)Math.Pow(2, zoom) - 1 - y;
            int num = GetServerNum(pos, 4);
            string url = string.Format(urlFormat, zoom, Math.Floor(pos.X / 16d), Math.Floor(y / 16d), pos.X, y, num);
            return GetTileImageUsingHttp(url);
        }
        private Guid id = new Guid("CEA13E28-92AE-48CB-AB8C-CFC01BD5C23B");
        public override Guid Id
        {
            get { return id; }
        }

        private string name = "Tencent";
        public override string Name
        {
            get { return name; }
        }
    }
}
