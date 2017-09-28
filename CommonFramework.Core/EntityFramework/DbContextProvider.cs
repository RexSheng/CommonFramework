using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using CommonFramework.Core.Dependency;

namespace CommonFramework.Core.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContextProvider:IDbContextProvider,IScopedDependency
    {
        private IConnectionStringProvider _connectionStringProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionProvider"></param>
        public DbContextProvider(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        public TDbContext GetContext<TDbContext>(object dbIndex) where TDbContext : DbContext
        {
            string conn = _connectionStringProvider.GetConnectionString(dbIndex);
            return GetContext<TDbContext>(conn);
        }

        public TDbContext GetContext<TDbContext>(string conn) where TDbContext : DbContext
        {
            string code = conn.ToString();
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）
            //传递DbContext进去获取实例的信息，在这里进行强制转换。
            TDbContext dbContext = CallContext.GetData("DbContext" + code) as TDbContext;
            if (dbContext == null) //线程在数据槽里面没有此上下文
            {
                dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), conn); //如果不存在上下文的话，创建一个EF上下文
                                                                                            //我们在创建一个，放到数据槽中去
                CallContext.SetData("DbContext" + code, dbContext);
            }
            return dbContext;
        }
    }
}
