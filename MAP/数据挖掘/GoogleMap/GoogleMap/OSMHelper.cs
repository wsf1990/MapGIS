using BruTile;
using BruTile.Web;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap
{
    /// <summary>
    /// 加载OSM瓦片数据
    /// </summary>
    public class OSMHelper
    {
        /// <summary>
        /// 创建瓦片框架
        /// </summary>
        /// <returns></returns>
        public static ITileSchema CreateTileSchema()
        {
            //定义不同层的分辨率
            var resolutions = new[] { 
                156543.033900000, 78271.516950000, 39135.758475000, 19567.879237500, 9783.939618750, 
                4891.969809375, 2445.984904688, 1222.992452344, 611.496226172, 305.748113086, 
                152.874056543, 76.437028271, 38.218514136, 19.109257068, 9.554628534, 4.777314267,
                2.388657133, 1.194328567, 0.597164283};

            var schema = new TileSchema();//瓦片框架
            schema.Name = "OpenStreetMap";
            for (int i = 0; i < resolutions.Length; i++)
            {
                var res = resolutions[i];
                schema.Resolutions.Add(new Resolution() { Id = i.ToString(), UnitsPerPixel = res });
            }
            schema.OriginX = -20037508.342789;//起算点
            schema.OriginY = 20037508.342789;
            schema.Axis = AxisDirection.InvertedY;
            //范围
            schema.Extent = new Extent(-20037508.342789, -20037508.342789, 20037508.342789, 20037508.342789);
            schema.Height = 256;//瓦片大小
            schema.Width = 256;
            schema.Format = "png";
            schema.Srs = "EPSG:900913";//瓦片类型  经纬度
            return schema;
        }

        public static TileLayer AddOSMLayer()
        {
            BruTile.Web.OsmRequest request = new OsmRequest();
            WebTileProvider provider = new WebTileProvider(request);
            //var tiles = provider.GetTile(new TileInfo() { Extent = new Extent(100, 30, 102, 31), Index = new TileIndex(1, 2, 5) });
            
            ITileSource tileSource = new TileSource(provider, CreateTileSchema());
            SharpMap.Layers.TileLayer layer = new SharpMap.Layers.TileLayer(tileSource, "OSM");
            return layer;
        }
    }
}
