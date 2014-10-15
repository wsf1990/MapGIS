using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap
{
    public enum MapTypeEnum
    {
        /// <summary>
        /// 地图：http://mt2.google.cn/vt/lyrs=m@177000000&hl=zh-CN&gl=cn&src=app&。。。
        /// </summary>
        Map = 0,
        /// <summary>
        /// 影像底图：http://mt3.google.cn/vt/lyrs=s@158&hl=zh-CN&gl=cn&src=app&。。。
        /// </summary>
        Image = 1,
        /// <summary>
        /// 影像的叠加层：http://mt3.google.cn/vt/imgtp=png32&lyrs=h@177000000&hl=zh-CN&gl=cn&src=app&。。。
        /// </summary>
        ImageOverlay = 2
    }
}
