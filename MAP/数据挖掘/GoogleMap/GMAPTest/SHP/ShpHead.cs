using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// shp头部
    /// </summary>
    public class ShpHead
    {
        public int FileCode { get { return 9994; } }
        public int FileLength { get; set; }
        public int Version { get { return 1000; } }
        public int ShpType { get; set; }
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public double Xmax { get; set; }
        public double Ymax { get; set; }
        public double Zmin { get; set; }
        public double Zmax { get; set; }
        public double Mmin { get; set; }
        public double Mmax { get; set; }
    }   
}          
