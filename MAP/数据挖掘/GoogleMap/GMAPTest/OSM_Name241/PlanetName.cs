using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.OSM_Name241
{
    public class PlanetName
    {
        public int ID { get; set; }

        public Int64 OSMID { get; set; }

        public string Name { get; set; }

        public string NameCH { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int NameID { get; set; }

        public int Source { get; set; }

        public string ClassCodeGb { get; set; }

        public string ClassCodeJb { get; set; }

        public int RegionFontCode { get; set; }

        public string CC1 { get; set; }

        public string ADM1 { get; set; }

        public string NameRoman { get; set; }

        public string NameEN { get; set; }

        public string DisplayMark { get; set; }

        public DateTime ModifyDate { get; set; }

        public DbGeometry TheGeom { get; set; }

        public string NamePY { get; set; }

        
    }
}
