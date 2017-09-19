using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CommonFramework.Core.Dependency;

namespace CommonFramework.Core.EntityFramework
{
    /// <summary>
    /// dbcontext提供接口(sqlserver)
    /// </summary>
    public interface IDbContextProvider : IInternalDependency
    {
        TDbContext GetContext<TDbContext, TConnectionString>(TConnectionString dbIndex) where TDbContext : DbContext;

        TDbContext GetContext<TDbContext>(string conn) where TDbContext : DbContext;
    }
}
