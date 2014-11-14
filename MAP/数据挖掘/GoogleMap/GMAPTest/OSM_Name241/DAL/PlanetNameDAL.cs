using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAPTest.PgSQL;
using Npgsql;

namespace GMAPTest.OSM_Name241.DAL
{
    public class PlanetNameDAL
    {
        private string tableName = "planet_name";
        PGHelper helper = new PGHelper();

        public bool Save(PlanetName planetName)
        {
            var maxid = GetMaxID();
            var parameter1 = new NpgsqlParameter("@ID", maxid + 1);
            var parameter2 = new NpgsqlParameter("@Name", planetName.Name);
            return helper.ExecuteNonQueryText("insert into " + tableName + "values(@ID, @Name)", parameter1, parameter2) >= 1;
        }

        public int GetMaxID()
        {
            var max = helper.ExecuteScalarText("select max(id) from " + tableName);
            return Convert.ToInt32(max);
        }

        public List<PlanetName> Query()
        {
            //var body = whereLamda.Body.ToString();
            //var typte = body.NodeType;
            return new List<PlanetName>();
        }

        public PlanetName GetByID(int id)
        {
            string sql = "select * from " + tableName + " where id = @ID";
            var parameter1 = new NpgsqlParameter("@ID", id);

            var dts = helper.GetTableText(sql, parameter1);
            var dt = dts[0];
            PlanetName planetName = new PlanetName();
            if (dt.Rows.Count >= 1)
            {
                planetName = GetByDataRow(dt.Rows[0]);
            }
            return planetName;
        }

        PlanetName GetByDataRow(System.Data.DataRow dataRow)
        {
            if(dataRow == null)
                return null;
            PlanetName pn = new PlanetName();
            pn.ID = Convert.ToInt32(dataRow["id"]);
            pn.Latitude = Convert.ToDouble(dataRow["Latitude"]);
            pn.Longitude = Convert.ToDouble(dataRow["Longitude"]);
            if(!string.IsNullOrWhiteSpace(dataRow["ModifyDate"].ToString()))
                pn.ModifyDate =  DateTime.Parse(dataRow["ModifyDate"].ToString());
            pn.Name = dataRow["Name"].ToString();
            pn.NameCH = dataRow["name_ch"].ToString();
            pn.NameEN = dataRow["name_en"].ToString();
            pn.NameID = Convert.ToInt32(dataRow["NameID"]);
            pn.NamePY = dataRow["name_py"].ToString();
            pn.NameRoman = dataRow["name_roman"].ToString();
            pn.OSMID = Convert.ToInt64(dataRow["osm_id"]);
            if (!string.IsNullOrWhiteSpace(dataRow["RegionFontCode"].ToString()))
                pn.RegionFontCode = Convert.ToInt32(dataRow["RegionFontCode"]);
            pn.Source = Convert.ToInt32(dataRow["source"]);
            //pn.TheGeom = DbGeometry.FromBinary(Encoding.BigEndianUnicode.GetBytes(dataRow["the_geom"].ToString()));
            return pn;
        }

        List<PlanetName> GetListByTable(DataTable dt)
        {
            var list = new List<PlanetName>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(GetByDataRow(dt.Rows[i]));
            }
            return list;
        }

        public bool Update(PlanetName planetName)
        {
            string sql = "update " + tableName + " set \"Name\" = @name where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", planetName.ID);
            var parameter2 = new NpgsqlParameter("Name", planetName.Name);
            return helper.ExecuteNonQueryText(sql, parameter1, parameter2) >= 1;
        }

        public bool Delete(int id)
        {
            string sql = "delete from " + tableName + " where \"ID\" = @ID";
            var par = new NpgsqlParameter("@ID", id);
            return helper.ExecuteNonQueryText(sql, par) >= 1;
        }

        public List<PlanetName> GetPageData(int pageIndex, int pageCount)
        {
            string sql = "select * from " + tableName + " where(id not in (select id from "+ tableName + " limit @start)) limit @count";
            var par1 = new NpgsqlParameter("@start", (pageIndex - 1) * pageCount);
            var par2 = new NpgsqlParameter("@count", pageCount);
            var dts = helper.GetTableText(sql, par1, par2);
            return GetListByTable(dts[0]);
        }
    }
}
