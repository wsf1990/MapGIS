using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GMAPTest.PgSQL
{
    public class PGHelper : DBHelper, IDBHelper
    {
        #region 0. 连接字符串等
        public override string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["241"].ConnectionString;
            }
        }

        private Hashtable parmCache;

        public override System.Collections.Hashtable ParmCache
        {
            get
            {
                parmCache = parmCache ?? Hashtable.Synchronized(new Hashtable());
                return parmCache;
            }
        }  
        #endregion

        #region 1. ExecteNonQuery方法

        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                   params IDbDataParameter[] commandParameters)
        {
            using (var cmd = new NpgsqlCommand())
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    //清空SqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }

        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecuteNonQueryProcedure(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// Sql语句专用
        /// </summary>
        /// <param name="cmdText">T_Sql语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecuteNonQueryText(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region 2. GetTable方法

        /// <summary>
        /// 执行一条返回结果集的SqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connecttionString">一个现有的数据库连接</param>
        /// <param name="cmdTye">SqlCommand命令类型</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public DataTableCollection GetTable(string connecttionString, CommandType cmdTye, string cmdText,
                                            IDbDataParameter[] commandParameters)
        {
            using (var cmd = new NpgsqlCommand())
            {
                using (var ds = new DataSet())
                {
                    using (var conn = new NpgsqlConnection(connecttionString))
                    {
                        PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
                        var adapter = new NpgsqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                    }
                    var table = ds.Tables;
                    return table;
                }
            }
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public DataTableCollection GetTable(CommandType cmdType, string cmdText, IDbDataParameter[] commandParameters)
        {
            return GetTable(ConnectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public DataTableCollection GetTableProcedure(string cmdText, IDbDataParameter[] commandParameters)
        {
            return GetTable(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// Sql语句专用
        /// </summary>
        /// <param name="cmdText"> T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public DataTableCollection GetTableText(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return GetTable(CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region 3. ExecuteReader
        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public IDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            using (var cmd = new NpgsqlCommand())
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    var rdr = cmd.ExecuteReader();
                    return rdr;
                }
            }
        }

        public IDataReader ExecuteReader(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public IDataReader ExecuteReaderText(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.Text, cmdText, commandParameters);
        }

        public IDataReader ExecuteReaderProcedure(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.StoredProcedure, cmdText, commandParameters);
        }
        #endregion

        #region 4. ExecuteScalar方法
        /// <summary>
        /// 返回第一行的第一列
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public object ExecuteScalarProcedure(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列Sql语句专用
        /// </summary>
        /// <param name="cmdText">者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public object ExecuteScalarText(string cmdText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new IDbDataParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            using (var cmd = new NpgsqlCommand())
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public bool Exists(string strSql, params IDbDataParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(ConnectionString, CommandType.Text, strSql, cmdParms));
            return cmdresult > 0;
        }
        #endregion

        #region 5. Parameters
        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public void CacheParameters(string cacheKey, params IDbDataParameter[] commandParameters)
        {
            ParmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public IDbDataParameter[] GetCachedParameters(string cacheKey)
        {
            var cachedParms = (IDbDataParameter[])ParmCache[cacheKey];
            if (cachedParms == null)
                return null;
            var clonedParms = new IDbDataParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (IDbDataParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        } 
        #endregion

        #region 6. 数据库和表操作
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool CreateTable(string schemaName, string tableName)
        {

            return true;
        }
        /// <summary>
        /// 获取所有表的列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableList()
        {
            string sql = "select tablename from pg_tables where schemaname='Test'";
            var dt = GetTableText(sql)[0];
            var tablenames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tablenames.Add(dt.Rows[i][0].ToString());
            }
            return tablenames;
        }

        #endregion
    }
}
