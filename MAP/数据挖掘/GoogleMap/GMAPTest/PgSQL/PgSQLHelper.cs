using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace GMAPTest.PgSQL
{
    public class PgSQLHelper
    {
        private static string ConnStr = "Server=192.168.1.124;Port=5432;UserId=postgres;Password=sjzxadmin;Database=openexchange;";

        private static NpgsqlConnection Conn;

        public static void Init()
        {
            try
            {
                Conn = new NpgsqlConnection(ConnStr);
                Conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Test");;
            }
            
        }

        public static void Insert()
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (NpgsqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "insert into \"Test\".\"Table1\" values @val";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@val", 10);
                    cmd.Parameters.Add(par);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update()
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (NpgsqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "update \"Test\".\"Table1\" set ID = 12 where ID = @ID";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@ID", 10);
                    cmd.Parameters.Add(par);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<string> GetTableList()
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (NpgsqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "select tablename from pg_tables where schemaname='Test'";
                    var tablenames = new List<string>();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tablenames.Add(reader[0].ToString());
                    }
                    return tablenames;
                }
            }
        }
    }
}
