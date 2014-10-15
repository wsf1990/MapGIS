using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;

namespace GoogleMap
{
    /// <summary>
    /// 通用地图图层管理以及地图控件Utility
    /// </summary>
    public class MapHelper
    {
        /// <summary>
        /// 添加Shp图层
        /// </summary>
        /// <param name="shpFile"></param>
        /// <returns></returns>
        public static SharpMap.Layers.VectorLayer AddShpLayer(string shpFile)
        {
            SharpMap.Layers.VectorLayer layer = new VectorLayer("shp");
            layer.DataSource = new ShapeFile(shpFile, true);

            //创建大陆的样式 
            VectorStyle landStyle = new VectorStyle(); 
            landStyle.Fill = new SolidBrush(Color.FromArgb(2, 2, 2)); 
            
            //创建水域的样式
            VectorStyle waterStyle = new VectorStyle(); 
            waterStyle.Fill = new SolidBrush(Color.FromArgb(191, 98, 255)); 
            
            //创建样式组 
            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>(); 
            styles.Add("land", landStyle); 
            styles.Add("water", waterStyle); 
            
            //添加样式至层 
            //layer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("class", styles, landStyle); 
            return layer;
        }

        /// <summary>
        /// 根据世界坐标（控件坐标）得到经纬度坐标
        /// </summary>
        /// <param name="map"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static GeoAPI.Geometries.Coordinate GetCoordinate(SharpMap.Map map, PointF point)
        {
            return map.ImageToWorld(point);
        }

        /// <summary>
        /// 根据经纬度坐标得到世界坐标（控件坐标）
        /// </summary>
        /// <param name="map"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static PointF GetPoint(SharpMap.Map map, Coordinate coordinate)
        {
            return map.WorldToImage(coordinate);
        }
    }
}
