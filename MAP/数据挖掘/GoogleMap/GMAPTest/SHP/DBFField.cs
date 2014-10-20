using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// DBF字段信息
    /// </summary>
    public class DBFField
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string FieldType  { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public byte FieldLength { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public byte FieldPricision { get; set; }
        /// <summary>
        /// 工作区ID
        /// </summary>
        public byte FieldID { get; set; }
        /// <summary>
        /// MDX
        /// </summary>
        public byte FieldMdx { get; set; }
    }
}
