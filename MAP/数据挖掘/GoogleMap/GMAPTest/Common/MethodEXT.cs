using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Common
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class MethodEXT
    {
        /// <summary>
        /// 地址类型到字符串
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static string GetString(this List<AddressType> types)
        {
            return string.Join(",", types);
        }
    }
}
