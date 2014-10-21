using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// 索引文件帮助类 --主要包含坐标文件.shp的索引信息 
    /// 文件中每个记录包含对应的坐标文件记录距离坐标文件的文件头的偏移量。
    /// 通过索引文件可以很方便地在坐标文件中定位到指定目标的坐标信息。
    /// </summary>
    public class SHXHelper
    {
        /// <summary>
        /// 从shx文件中读取内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static SHXFileContent ImportShxFormFile(string fileName)
        {
            using(Stream stream = File.OpenRead(fileName))
            {
                using(BinaryReader br = new BinaryReader(stream))
                {
                    SHXFileContent Content = new SHXFileContent();
                    Content.Head = SHPHelper.GetHead(br);
                    Content.Contents = GetContent(br);
                    return Content;
                }
            }
        }
        /// <summary>
        /// 读取正文
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        static List<SHXContent> GetContent(BinaryReader br)
        {
            var contents = new List<SHXContent>();
            while(br.BaseStream.Position != br.BaseStream.Length)
            {
                var con = new SHXContent();
                con.Offset = CommonHelper.ChangeOrder(br.ReadInt32());
                con.ContentLength = CommonHelper.ChangeOrder(br.ReadInt32());
                contents.Add(con);
            }
            return contents;
        }
    }
}
