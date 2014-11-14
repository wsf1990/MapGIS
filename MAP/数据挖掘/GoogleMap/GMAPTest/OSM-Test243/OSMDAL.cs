using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GMAPTest.PgSQL;
using Npgsql;

namespace GMAPTest.OSM
{
    public class OSMDAL
    {
        private string tableName = "\"Test\".\"Table1\"";

        public bool Save(OSM osm)
        {
            var maxid = GetMaxID();
            var parameter1 = new NpgsqlParameter("@ID", maxid + 1);
            var parameter2 = new NpgsqlParameter("@Name", osm.Name);
            return PgSQLHelper.ExecuteNonQuery("insert into " + tableName + "values(@ID, @Name)", parameter1, parameter2) >= 1;
        }

        public int GetMaxID()
        {
            var max =PgSQLHelper.ExecuteScalar("select max(\"ID\") from " + tableName);
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
            string sql = "select * from " + tableName + "where \"ID\" > @ID";
            var parameter1 = new NpgsqlParameter("@ID", id);
            var reader = PgSQLHelper.ExecuteScalar(sql, parameter1);
            //{
            //    var osm = new OSM();
            //    while (reader.Read())
            //    {
            //        osm.ID = Convert.ToInt32(reader.GetValue(0));
            //        osm.Name = reader.GetValue(1).ToString();
            //        return osm;
            //    }
            //    return null;
            //}
            return null;
        }

        public bool Update(OSM osm)
        {
            string sql = "update " + tableName + " set \"Name\" = @name where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", osm.ID);
            var parameter2 = new NpgsqlParameter("Name", osm.Name);
            return PgSQLHelper.ExecuteNonQuery(sql, parameter1, parameter2) >= 1;
        }

        public bool Delete(int id)
        {
            string sql = "delete from " + tableName + " where \"ID\" = @ID";
            var par = new NpgsqlParameter("@ID", id);
            return PgSQLHelper.ExecuteNonQuery(sql, par) >= 1;
        }

        //public List<string> Query()
        //{
        //    string sql = "select \"Name\" from " + tableName + " where \"ID\" = @ID";
        //    var parameter1 = new NpgsqlParameter("@ID", 10);
        //    List<string> list = new List<string>();
        //    using (var reader = ExecuteReader(sql, parameter1)) //此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(reader[0].ToString());
        //        }
        //    }
        //    return list;
        //}
    }
}
