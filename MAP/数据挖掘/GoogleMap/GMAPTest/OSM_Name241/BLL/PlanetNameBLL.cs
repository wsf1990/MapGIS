using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAPTest.OSM_Name241.DAL;

namespace GMAPTest.OSM_Name241.BLL
{
    public class PlanetNameBLL
    {
        PlanetNameDAL dal = new PlanetNameDAL();

        public PlanetName GetById(int id)
        {
            return dal.GetByID(id);
        }

        public List<PlanetName> GetPageData(int pageIndex, int pageCount)
        {
            return dal.GetPageData(pageIndex, pageCount);
        }
    }
}
