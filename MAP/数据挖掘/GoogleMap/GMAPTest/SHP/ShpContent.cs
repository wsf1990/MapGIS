using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// 一个记录项
    /// </summary>
    public class ShpContent
    {
        public int ShapeType { get; set; }

        public double Lng { get; set; }

        public double Lat { get; set; }
    }
}
