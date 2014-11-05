using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Common
{
    #region 地址意义
    /// <summary>
    /// 谷歌查询返回地址类
    /// street_address，用于表示一个精确的街道地址。
    /// route，用于表示一条已命名的路线（例如“US 101”）。
    /// intersection，用于表示一个大型十字路口（通常由两条主道交叉形成）。
    /// political，用于表示一个政治实体（通常为一个代表行政管理区的多边形）。
    /// country，用于表示政治实体，且通常列在地理编码器所返回结果的最前面。
    /// administrative_area_level_1，用于表示仅次于国家/地区级别的行政实体。在美国，这类行政实体是指州。并非所有国家/地区都有该行政级别。
    /// administrative_area_level_2，用于表示国家/地区级别下的二级行政实体。在美国，这类行政实体是指县。并非所有国家/地区都有该行政级别。
    /// administrative_area_level_3，用于表示国家/地区级别下的三级行政实体。此类型表示较小的行政单位。并非所有国家/地区都有该行政级别。
    /// colloquial_area，用于表示实体的通用别名。
    /// locality，用于表示合并的市镇级别政治实体。
    /// sublocality，用于表示仅次于地区级别的行政实体
    /// neighborhood，用于表示已命名的邻近地区
    /// premise，用于表示已命名的位置（通常为具有常用名称的建筑物或建筑群）
    /// subpremise，用于表示仅次于已命名位置级别的实体（通常为使用常用名称的建筑群中的某座建筑物）
    /// postal_code，用于表示邮政编码，以确定相应国家/地区内的邮寄地址。
    /// natural_feature，用于表示著名的自然景观。
    /// airport，用于表示机场。
    /// park，用于表示已命名的公园。
    /// point_of_interest，用于表示已命名的兴趣点。通常，这些“POI”是一些不易归入其他类别的比较有名的当地实体，如“帝国大厦”或“自由女神像”。
    /// 除此之外，地址组成部分还可以使用以下类型：
    /// post_box，用于表示特定邮筒。
    /// street_number，用于表示准确的街道编号。
    /// floor，用于表示建筑物地址的楼层号。
    /// room，用于表示建筑物地址的房间编号。
    /// </summary> 
    #endregion
    public class GoogleAddress
    {
        /// <summary>
        /// 地址组成
        /// </summary>
        public List<Address_Component> Address_components { get; set; }
        /// <summary>
        /// 格式化地址输出（通信地址）
        /// </summary>
        public string Formatted_address { get; set; }
        /// <summary>
        /// 地理位置
        /// </summary>
        public Geometry Geometry { get; set; }

        /// <summary>
        /// 表示地址解析器未传回原始请求的完全匹配项，但与请求地址的一部分匹配
        /// </summary>
        public bool Partial_match { get; set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public List<AddressType> Types { get; set; }
    }

    /// <summary>
    /// 地址信息
    /// </summary>
    public class Address_Component
    {
        public string Long_name{get;set;}
        public string short_name{get;set;}
        public List<AddressType> Types { get; set; }
    }

    /// <summary>
    /// 地理信息
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// （可选择传回）存储可完全包含传回结果的边框。请注意，这些边界可能与建议的可视区域不相符。
        /// （例如，旧金山包含费拉隆岛。该岛实际上是旧金山市的一部分，但不应该在可视区域内传回。）
        /// </summary>
        public Bounds Bounds { get; set; }
        /// <summary>
        /// 返回的坐标信息
        /// </summary>
        public PointLatLng Location { get; set; }
        /// <summary>
        /// 返回的坐标经度以及类型
        /// </summary>
        public LocationType Location_type { get; set; }
        /// <summary>
        /// viewport 包含用于显示传回结果的建议可视区域，
        /// 并被指定为两个纬度/经度值，分别定义可视区域边框的 southwest 和 northeast 角。
        /// 通常，该可视区域用于在将结果显示给用户时作为结果的框架
        /// </summary>
        public Bounds Viewport { get; set; }
    }

    /// <summary>
    /// 范围
    /// </summary>
    public class Bounds
    {
        public PointLatLng Northeast { get; set; }
        public PointLatLng Southwest { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}|{2},{3}", Southwest.Lat, Southwest.Lng, Northeast.Lat, Northeast.Lng);
        }
    }

    /// <summary>
    /// location的精确程度或者说是类型
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// "ROOFTOP" 表示传回的结果是一个精确的地址解析值，我们可获得精确到街道地址的位置信息。
        /// </summary>
        ROOFTOP,
        /// <summary>
        /// "RANGE_INTERPOLATED" 表示返回的结果是一个近似值（通常表示某条道路上的地址），该地址处于两个精确点（如十字路口）之间。当无法对街道地址进行精确的地址解析时，通常会返回近似结果。
        /// </summary>
        RANGE_INTERPOLATED,
        /// <summary>
        /// "GEOMETRIC_CENTER" 表示返回的结果是折线（如街道）或多边形（区域）等内容的几何中心。
        /// </summary>
        GEOMETRIC_CENTER,
        /// <summary>
        /// "APPROXIMATE" 表示返回的结果是一个近似值。
        /// </summary>
        APPROXIMATE
    }

    /// <summary>
    /// 地址类型
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// 
        /// </summary>
        street_address,
        /// <summary>
        /// POI
        /// </summary>
        point_of_interest,
        /// <summary>
        /// 大型十字路口
        /// </summary>
        intersection,
        /// <summary>
        /// 用于表示实体的通用别名
        /// </summary>
        colloquial_area,
        /// <summary>
        /// 用于表示合并的市镇级别政治实体
        /// </summary>
        locality,
        /// <summary>
        /// 自然景观
        /// </summary>
        natural_feature,
        /// <summary>
        /// 机构?
        /// </summary>
        establishment,
        /// <summary>
        /// 公交车站
        /// </summary>
        bus_station,
        /// <summary>
        /// 中转站
        /// </summary>
        transit_station,
        /// <summary>
        /// 邮编
        /// </summary>
        postal_code,
        /// <summary>
        /// 政治实体
        /// </summary>
        political,
        /// <summary>
        /// 国家
        /// </summary>
        country,
        /// <summary>
        /// 省州级
        /// </summary>
        administrative_area_level_1,
        /// <summary>
        /// 市级
        /// </summary>
        administrative_area_level_2,
        /// <summary>
        /// 
        /// </summary>
        administrative_area_level_3,
        /// <summary>
        /// 仅次于地区级别的行政实体
        /// </summary>
        sublocality,
        /// <summary>
        /// 区、县
        /// </summary>
        sublocality_level_1,
        /// <summary>
        /// 乡、镇
        /// </summary>
        sublocality_level_2,
        /// <summary>
        /// 村
        /// </summary>
        sublocality_level_3,
        /// <summary>
        /// 用于表示已命名的邻近地区
        /// </summary>
        neighborhood,
        /// <summary>
        /// 用于表示已命名的位置（通常为具有常用名称的建筑物或建筑群）
        /// </summary>
        premise,
        /// <summary>
        /// 用于表示仅次于已命名位置级别的实体（通常为使用常用名称的建筑群中的某座建筑物）
        /// </summary>
        subpremise,
        /// <summary>
        /// 用于表示机场
        /// </summary>
        airport,
        /// <summary>
        /// 用于表示已命名的公园
        /// </summary>
        park,
        /// <summary>
        /// 用于表示特定邮筒
        /// </summary>
        post_box,
        /// <summary>
        /// 准确的街道编号
        /// </summary>
        street_number,
        /// <summary>
        /// 用于表示建筑物地址的楼层号
        /// </summary>
        floor,
        /// <summary>
        /// 用于表示建筑物地址的房间编号
        /// </summary>
        room,
        /// <summary>
        /// 
        /// </summary>
        place_of_worship,
        /// <summary>
        /// 路线
        /// </summary>
        route,
        /// <summary>
        /// 地铁站
        /// </summary>
        subway_station,
        /// <summary>
        /// 火车站
        /// </summary>
        train_station
    }
}
