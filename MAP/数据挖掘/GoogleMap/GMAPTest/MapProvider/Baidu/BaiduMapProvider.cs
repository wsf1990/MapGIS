using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;
using GMAPTest.MapProvider.Baidu;

namespace GMAPTest
{
    /// <summary>
    /// 百度瓦片基类
    /// </summary>
    public abstract class BaiduMapProviderBase : GMapProvider, GeocodingProvider
    {
        public BaiduMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://map.baidu.com";
            Copyright = string.Format("C{0} Baidu Corporation, C{0} NAVTEQ, C{0} Image courtesy of NASA",
                                      DateTime.Today.Year);
            //this.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
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
        /// 获取瓦片xyz
        /// 此处要先看源码分析其GPoint pos实如何算出的，在此基础上继续进行xyz的计算。
        /// </summary>
        /// <param name="zoom"></param>
        /// <param name="pos"></param>
        /// <param name="num"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void GetTilePos(int zoom, GPoint pos, out int num, out string x, out string y)
        {
            //北纬39°26'至41°03'，东经115°25'
            //398 146
            var level = zoom - 1;
            var offset = (int)Math.Pow(2, level);
            //var offsetY = offsetX / 2 - 1;

            var numX = pos.X - offset - (int)Math.Pow(2, level - 2);
            var numY = -pos.Y + offset - (int)(7 * Math.Pow(2, level - 6));
            //if (zoom <= 6)
            //    numY -= 2;
            num = GetServerNum(pos, 5);// (int)(pos.X + pos.Y) % 4 + 1;
            x = numX.ToString().Replace("-", "M");
            y = numY.ToString().Replace("-", "M");
        }
        #region 百度地名解析

        public Placemark? GetPlacemark(PointLatLng location, out GeoCoderStatusCode status)
        {
            var address = BaiduHelper.GetAddress(location, out status);
            return address;
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
            return BaiduHelper.GetLocation(keywords, out status);
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
    /// 百度  地图  瓦片
    /// </summary>
    public class BaiduMapProvider : BaiduMapProviderBase
    {
        public static readonly BaiduMapProvider Instance;
        
        static BaiduMapProvider()
        {
            Instance = new BaiduMapProvider();
        }

        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);
            //url = "http://online3.map.bdimg.com/tile/?qt=tile&x=1425&y=521&z=13&styles=pl&udt=20141013";
            return GetTileImageUsingHttp(url);
        }
        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            int num;
            string x, y;
            GetTilePos(zoom, pos, out num, out x, out y);
            //http://online3.map.bdimg.com/tile/?qt=tile&x=355&y=130&z=11&styles=pl&udt=20141013
            //http://q3.baidu.com/it/u=x=721;y=209;z=12;v=014;type=web&fm=44
            string url = string.Format(UrlFormat, num, x, y, zoom);
            //Console.WriteLine("url:" + url);
            return url;
        }

        static readonly string UrlFormat = "http://online{0}.map.bdimg.com/tile/?qt=tile&x={1}&y={2}&z={3}&styles=pl&udt=20141013";

        private Guid id = new Guid("F624DC13-E0BB-47B3-BCD8-335302368011");
        public override Guid Id
        {
            get
            {
                return id;
            }
        }

        public override string Name
        {
            get { return "BaiDuMap"; }
        }

        
    }
}
