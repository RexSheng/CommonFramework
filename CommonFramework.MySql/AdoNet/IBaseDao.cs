using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CommonFramework.MySql.AdoNet
{
    public interface IBaseDao<TConnectionString>
    {
        DataTable GetDataTable(TConnectionString conn, string sql);
    }

    public interface IBaseDao: IBaseDao<string>
    {
        DataTable GetDataTable(string sql);
    }
}
