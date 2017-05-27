using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CommonFramework.SqlServer.AdoNet
{
    public class BaseDao<TConnectionString> : Provider<TConnectionString>, IBaseDao<TConnectionString>
    {
        public BaseDao(Func<TConnectionString, string> func, TConnectionString conn)
        {
            base.DefaultConnectionString = conn;
            base.DefaultConnectionStringGetter = func;
        }

        public int ExecuteSql(TConnectionString conn, string sql, SqlConnection existConnection = null, SqlTransaction trans = null)
        {
            using (SqlConnection connection = (existConnection==null?new SqlConnection(base.GetConnectionString(conn)):existConnection))
            {
                using (SqlCommand cmd = (trans == null ? new SqlCommand(sql, connection) : new SqlCommand(sql, connection,trans)))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(TConnectionString conn, string sql) {
            using (SqlConnection connection = new SqlConnection(base.GetConnectionString(conn)))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    return cmd.ExecuteScalar();
                }
            }
        }

        public DataTable GetDataTable(TConnectionString conn, string sql) {
            using (SqlConnection connection = new SqlConnection(base.GetConnectionString(conn)))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataSet GetDataSet(TConnectionString conn, string sql)
        {
            using (SqlConnection connection = new SqlConnection(base.GetConnectionString(conn)))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }

        public void BulkInsert(TConnectionString conn, DataTable dt,string destinationTable="",int batchSize=1000)
        {
            using (SqlConnection connection = new SqlConnection(base.GetConnectionString(conn)))
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(connection))
                {
                    copy.BatchSize = batchSize;
                    copy.DestinationTableName = string.IsNullOrEmpty(destinationTable) ? dt.TableName : destinationTable;
                    copy.WriteToServer(dt);
                }
            }
        }

        public void BulkInsert(TConnectionString conn, DataRow[] rows, string destinationTable,int batchSize=1000)
        {
            using (SqlConnection connection = new SqlConnection(base.GetConnectionString(conn)))
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(connection))
                {
                    copy.BatchSize = batchSize;
                    copy.DestinationTableName = destinationTable;
                    copy.WriteToServer(rows);
                }
            }
        }
    }
}
