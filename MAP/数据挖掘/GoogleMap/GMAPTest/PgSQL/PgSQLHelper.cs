using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static string ConnStr = ConfigurationManager.ConnectionStrings["pgsqlConn"].ConnectionString;

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
                MessageBox.Show("Test");
            }
        }

        public static void GetConn()
        {
            try
            {
                Conn = new NpgsqlConnection(ConnStr);
                Conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        /// <summary>
        /// 返回一个结果的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (var cmd = new NpgsqlCommand(sql, Conn)) //此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (var cmd = new NpgsqlCommand(sql, Conn)) //此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 返回查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static NpgsqlDataReader ExecuteReader(string sql, params NpgsqlParameter[] parameters)
        {
            using (Conn = new NpgsqlConnection(ConnStr))
            {
                Conn.Open();
                using (var cmd = new NpgsqlCommand(sql, Conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteReader();
                }
            }
        }

        private static string tableName = "\"Test\".\"Table1\"";
        public static void Insert()
        {
            var maxID = ExecuteScalar("select max(\"ID\") from " + tableName);

            var sql = "insert into " + tableName + " values (@ID,@Name)";
            var parameter1 = new NpgsqlParameter("@ID", Convert.ToInt32(maxID) + 1);
            var parameter2 = new NpgsqlParameter("Name", "npgsqltest");
            ExecuteNonQuery(sql, parameter1, parameter2);
        }

        public static void Update()
        {
            string sql = "update " + tableName + " set \"Name\" = @name where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", 11);
            var parameter2 = new NpgsqlParameter("Name", "update");
            ExecuteNonQuery(sql, parameter1, parameter2);
        }

        public static void Delete()
        {
            string sql = "delete from " + tableName + " where \"ID\" = @ID";
            var par = new NpgsqlParameter("@ID", 12);
            ExecuteNonQuery(sql, par);
        }

        public static List<string> Query()
        {
            string sql = "select \"Name\" from " + tableName + " where \"ID\" = @ID";
            var parameter1 = new NpgsqlParameter("@ID", 10);
            List<string> list = new List<string>();
            using (var reader = ExecuteReader(sql, parameter1)) //此处注意PostgreSQL中模式的使用  \"Test\".\"Table1\"
            {
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
            }
            return list;
        }

        public static List<string> GetTableList()
        {
            string sql = "select tablename from pg_tables where schemaname='Test'";
            using (var reader = ExecuteReader(sql))
            {
                var tablenames = new List<string>();
                while (reader.Read())
                {
                    tablenames.Add(reader[0].ToString());
                }
                return tablenames;
            }
        }
    }
}
