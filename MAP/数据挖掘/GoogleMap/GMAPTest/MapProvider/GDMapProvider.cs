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
    public abstract class GDMapProviderBase : GMapProvider
    {
        public GDMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://www.amap.com/";
            Copyright = string.Format("©{0} 高德 Corporation, ©{0} NAVTEQ, ©{0} Image courtesy of NASA", DateTime.Today.Year);   
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
        
    }

    /// <summary>
    /// 高德地图
    /// </summary>
    public class GDMapProvider : GDMapProviderBase
    {
        public static readonly GDMapProvider Instance;
        static GDMapProvider()
        {
            Instance = new GDMapProvider();
        }

        private Guid id = new Guid("BAFB9B9C-EE22-4DB7-A4B0-2F2B7BE843F6");
        public override Guid Id
        {
            get { return id; }
        }

        public override string Name
        {
            get { return "GDMAP"; }
        }

        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            string url = GetUrl(pos, zoom);
            return GetTileImageUsingHttp(url);
        }

        string GetUrl(GPoint pos, int zoom)
        {
            string urlFormat = "http://webrd0{0}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=7&x={1}&y={2}&z={3}";
            int num = (int)(pos.X + pos.Y) % 4 + 1;
            return string.Format(urlFormat, num, pos.X, pos.Y, zoom);
        }
    }
}
