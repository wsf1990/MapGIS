using GMap.NET;
using GMap.NET.MapProviders;
using GMAPTest.ImageCache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    /// <summary>
    /// 将截图表瓦片进行读取处理
    /// 如果采用此种方式，则需要将整体截图表先生成图片，然后进行不同层级切瓦片。
    /// </summary>
    public class MyImageCache : PureImageCache
    {

        public int DeleteOlderThan(DateTime date, int? type)
        {
            throw new NotImplementedException();
        }

        public PureImage GetImageFromCache(int type, GPoint pos, int zoom)
        {
            PureImage ret = null;
            byte[] tile = (byte[])GetBytes(pos, zoom);
            if (tile != null && tile.Length > 0)
            {
                ret = new MyTileImageProxy().FromArray(tile);
            }
            tile = null;
            return ret;
        }

        /// <summary>
        /// 图像转换为字节数组
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public byte[] GetBytes(GPoint pos, int zoom)
        {
            string fileName = CommonHelper.GetImgFileName(pos, zoom);
            return CommonHelper.GetBytesFormBM(fileName);
        }

        public bool PutImageToCache(byte[] tile, int type, GPoint pos, int zoom)
        {
            throw new NotImplementedException();
        }
    }
}
