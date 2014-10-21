using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// shx文件内容
    /// </summary>
    public class SHXFileContent
    {
        /// <summary>
        /// 头部与SHP相同
        /// </summary>
        public ShpHead Head { get; set; }
        /// <summary>
        /// 正文内容
        /// </summary>
        public List<SHXContent> Contents { get; set; }

        public SHXFileContent()
        {

        }

        public SHXFileContent(string fileName)
        {
            var con = SHXHelper.ImportShxFormFile(fileName);
            this.Head = con.Head;
            this.Contents = con.Contents;
        }

        public SHXFileContent(ShpHead head, List<SHXContent> con)
        {
            this.Head = head;
            this.Contents = con;
        }
    }
    /// <summary>
    /// SHX文件记录正文内容
    /// </summary>
    public class SHXContent
    {
        /// <summary>
        /// 偏移
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int ContentLength { get; set; }
    }
}
