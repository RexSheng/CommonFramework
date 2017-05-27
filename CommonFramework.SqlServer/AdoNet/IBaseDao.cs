using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.SqlServer.AdoNet
{
    public interface IBaseDao<TConnectionString>
    {
        int ExecuteSql(TConnectionString conn, string sql, SqlConnection existConnection = null, SqlTransaction trans = null);
      
        object ExecuteScalar(TConnectionString conn, string sql);
      

        DataTable GetDataTable(TConnectionString conn, string sql);
      

        DataSet GetDataSet(TConnectionString conn, string sql);
       

        void BulkInsert(TConnectionString conn, DataTable dt, string destinationTable = "", int batchSize = 1000);
         

        void BulkInsert(TConnectionString conn, DataRow[] rows, string destinationTable, int batchSize = 1000);
       
         
    }
}
