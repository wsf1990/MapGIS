using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Common
{
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
    public class GoogleAddress
    {
        public List<Address_Component> Address_components { get; set; }

        public string Formatted_address { get; set; }

        public Geometry Geometry { get; set; }

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
        public Bounds Bounds { get; set; }

        public PointLatLng Location { get; set; }

        public string Location_type { get; set; }

        public Bounds Viewport { get; set; }
    }
    /// <summary>
    /// 范围
    /// </summary>
    public class Bounds
    {
        public PointLatLng Northeast { get; set; }
        public PointLatLng Southwest { get; set; }
    }
    /// <summary>
    /// 地址类型
    /// </summary>
    public enum AddressType
    {
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
        /// 县级
        /// </summary>
        administrative_area_level_3
    }
}
