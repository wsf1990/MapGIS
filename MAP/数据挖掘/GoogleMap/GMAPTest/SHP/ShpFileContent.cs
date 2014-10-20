using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// Shp文件内容
    /// </summary>
    public class ShpFileContent
    {
        #region 1. Fields
        /// <summary>
        /// 头部
        /// </summary>
        public ShpHead Head { get; set; }
        /// <summary>
        /// 直线内容
        /// </summary>
        public List<PolyLine> PolyLines { get; set; } 
        #endregion

        #region 2. Construct
        public ShpFileContent()
        {

        }
        public ShpFileContent(string fileName)
        {

        } 
        #endregion

        #region 3. Method
        
        #endregion

    }
}
