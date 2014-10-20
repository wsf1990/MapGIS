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
        public List<ShpPolyLine> PolyLines { get; set; }

        /// <summary>
        /// 点状内容
        /// </summary>
        public List<ShpPoint> Points { get; set; }

        /// <summary>
        /// 面状内容
        /// </summary>
        public List<ShpPolygon> Polygons { get; set; }

        #endregion

        #region 2. Construct
        public ShpFileContent()
        {

        }
        public ShpFileContent(string fileName)
        {
            var con = SHPHelper.ImportShapeFileData(fileName);
            this.Head = con.Head;
            this.PolyLines = con.PolyLines;
            this.Polygons = con.Polygons;
            this.Points = con.Points;
        } 
        #endregion

        #region 3. Method
        
        #endregion

    }
}
