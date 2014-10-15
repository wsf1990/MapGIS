using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;

namespace GMAPTest
{
    public class BaiduMapProvider : GMapProvider
    {
        
        public BaiduMapProvider()
        {
            MaxZoom = null;
            RefererUrl = "http://map.baidu.com";
            Copyright = string.Format("C{0} Baidu Corporation, C{0} NAVTEQ, C{0} Image courtesy of NASA",
                                      DateTime.Today.Year);
            this.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
        }

        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);

            return GetTileImageUsingHttp(url);
        }

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            zoom = zoom - 1;
            var offsetX = Math.Pow(2, zoom);
            var offsetY = offsetX - 1;

            var numX = pos.X - offsetX;
            var numY = -pos.Y + offsetY;

            zoom = zoom + 1;
            var num = (pos.X + pos.Y) % 8 + 1;
            var x = numX.ToString().Replace("-", "M");
            var y = numY.ToString().Replace("-", "M");

            //http://online3.map.bdimg.com/tile/?qt=tile&x=355&y=130&z=11&styles=pl&udt=20141013
            //http://q3.baidu.com/it/u=x=721;y=209;z=12;v=014;type=web&fm=44
            string url = string.Format(UrlFormat, num, x, y, zoom);
            //Console.WriteLine("url:" + url);
            return url;
        }

        static readonly string UrlFormat = "http://online{0}.map.bdimg.com/tile/?qt=tile&x={1}&y={2}&z={3}&styles=pl&udt=20141013";

        private Guid id = Guid.NewGuid();
        public override Guid Id
        {
            get
            {
                return id;
            }
        }

        public override string Name
        {
            get { return "baiduMap"; }
        }

        private GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                overlays = overlays ?? new GMapProvider[] {this};
                return overlays;
            }
        }

        public override GMap.NET.PureProjection Projection
        {
            get { return MercatorProjection.Instance; }
        }
    }
}
