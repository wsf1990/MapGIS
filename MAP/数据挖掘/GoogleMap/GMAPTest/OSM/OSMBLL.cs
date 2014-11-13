using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.OSM
{
    public class OSMBLL
    {
        private OSMDAL2 dal = new OSMDAL2();

        public bool Save(OSM osm)
        {
            return dal.Save(osm);
        }

        public bool Update(OSM osm)
        {
            return dal.Update(osm);
        }

        public List<OSM> Query()
        {
            return dal.Query();
        }

        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public OSM GetByID(int id)
        {
            return dal.GetByID(id);
        }
    }
}
