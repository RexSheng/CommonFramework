using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace CommonFramework.MySql.AdoNet
{
    public class BaseDao<TConnectionString> : Provider<TConnectionString>,IBaseDao<TConnectionString>
    {
        public BaseDao(Func<TConnectionString, string> func, TConnectionString conn)
        {
            base.DefaultConnectionString = conn;
            base.DefaultConnectionStringGetter = func;
        }

        public int ExecuteSql(TConnectionString conn, string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(base.GetConnectionString(conn)))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(TConnectionString conn, string sql) {
            using (MySqlConnection connection = new MySqlConnection(base.GetConnectionString(conn)))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataSet GetDataSet(TConnectionString conn, string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(base.GetConnectionString(conn)))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }
    }

    public class BaseDao : BaseDao<string>, IBaseDao
    {
        public BaseDao(Func<string, string> func, string conn):base(func,conn)
        {
        }

        public int ExecuteSql(string sql)
        {
            return base.ExecuteSql(DefaultConnectionString, sql);
        }

        public DataTable GetDataTable(string sql)
        {
            return base.GetDataTable(DefaultConnectionString, sql);
        }

        public DataSet GetDataSet(string sql)
        {
            return base.GetDataSet(DefaultConnectionString, sql);
        }
    }
}
