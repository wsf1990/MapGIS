using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.OSM
{
    public class OSM
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public OSM GetOSMByDateRow(DataRow row)
        {
            return new OSM() {ID = Convert.ToInt32(row[0]), Name = row[1].ToString()};
        }
    }
}
