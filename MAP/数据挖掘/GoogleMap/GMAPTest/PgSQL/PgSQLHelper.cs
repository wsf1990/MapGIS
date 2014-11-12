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
    /// <summary>
    /// PGSQL帮助类
    /// 注意字段均需要加双引号
    /// 注意大小写
    /// </summary>
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
                    cmd.CommandText = "insert into \"Test\".\"Table1\" values (@ID,@Name)";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@id", 10);
                    cmd.Parameters.Add(par);
                    cmd.Parameters.Add(new NpgsqlParameter("@name", "wsf"));
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
                    cmd.CommandText = "update \"Test\".\"Table1\" set \"Name\" = @name where \"ID\" = @ID";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@ID", 10);
                    cmd.Parameters.Add(par);
                    cmd.Parameters.Add(new NpgsqlParameter("@name", "wwwww"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete()
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (NpgsqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "delete from \"Test\".\"Table1\" where \"ID\" = @ID";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@ID", 12);
                    cmd.Parameters.Add(par);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<string> Query()
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (NpgsqlCommand cmd = Conn.CreateCommand())
                {
                    var list = new List<string>();
                    cmd.CommandText = "select \"Name\" from \"Test\".\"Table1\" where \"ID\" = @ID";//此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                    var par = new NpgsqlParameter("@ID", 10);
                    cmd.Parameters.Add(par);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader[0].ToString());
                        }
                        return list;
                    }
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
                    using (var reader = cmd.ExecuteReader())
                    {
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
}
