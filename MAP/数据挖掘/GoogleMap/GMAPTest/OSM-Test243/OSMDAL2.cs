using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAPTest.PgSQL;
using Npgsql;

namespace GMAPTest.OSM
{
    public class OSMDAL2
    {
        private string tableName = "\"Test\".\"Table1\"";
        PGHelper helper = new PGHelper();

        public bool Save(OSM osm)
        {
            var maxid = GetMaxID();
            var parameter1 = new NpgsqlParameter("@ID", maxid + 1);
            var parameter2 = new NpgsqlParameter("@Name", osm.Name);
            return helper.ExecuteNonQueryText("insert into " + tableName + "values(@ID, @Name)", parameter1, parameter2) >= 1;
        }

        public int GetMaxID()
        {
            var max = helper.ExecuteScalarText("select max(\"ID\") from " + tableName);
            return Convert.ToInt32(max);
        }

        public List<OSM> Query()
        {
            //var body = whereLamda.Body.ToString();
            //var typte = body.NodeType;
            return new List<OSM>();
        }

        public OSM GetByID(int id)
        {
            string sql = "select * from " + tableName + "where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", id);

            var dts = helper.GetTableText(sql, parameter1);
            var dt = dts[0];
            OSM osm = new OSM();
            if (dt.Rows.Count >= 1)
            {
                osm = osm.GetOSMByDataRow(dt.Rows[0]);
            }
            return osm;
        }

        public bool Update(OSM osm)
        {
            string sql = "update " + tableName + " set \"Name\" = @name where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", osm.ID);
            var parameter2 = new NpgsqlParameter("Name", osm.Name);
            return helper.ExecuteNonQueryText(sql, parameter1, parameter2) >= 1;
        }

        public bool Delete(int id)
        {
            string sql = "delete from " + tableName + " where \"ID\" = @ID";
            var par = new NpgsqlParameter("@ID", id);
            return helper.ExecuteNonQueryText(sql, par) >= 1;
        }

    }
}
