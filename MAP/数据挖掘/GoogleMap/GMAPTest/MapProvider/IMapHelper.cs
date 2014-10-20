using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public interface IMapHelper
    {
        /// <summary>
        /// 获取地址的坐标
        /// </summary>
        /// <param name="address"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        PointLatLng GetLocation(string address, out GeoCoderStatusCode code);

        /// <summary>
        /// 获取坐标地址名称
        /// </summary>
        /// <param name="point"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Placemark GetAddress(PointLatLng point, out GeoCoderStatusCode code);
    }
}
