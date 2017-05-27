using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace CommonFramework.Oracle.EntityFramework
{
    public class Provider<TConnectionString, TDbContext>: CommonInterface
        where TDbContext : DbContext, new()
    {
        
        public TConnectionString DefaultConnectionString { get; set; }

        public Func<TConnectionString, string> DefaultConnectionStringGetter { get; set; }

        /// <summary>
        /// 从web.config的connectionStrings节点获取连接字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static string GetWebConfigConnectionString<T>(T conn)
        {
            var result = ConfigurationManager.ConnectionStrings[conn.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return conn.ToString();
        }

        /// <summary>
        /// 从web.config的appSettings节点获取连接字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static string GetAppConfigConnectionString<T>(T conn) {
            var result = ConfigurationManager.AppSettings[conn.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return conn.ToString();
        }

        public TDbContext GetContext(TConnectionString dbIndex)
        {
            string conn = DefaultConnectionStringGetter(dbIndex);
            return GetContext(conn);
        }

        public TDbContext GetContext(string conn)
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

        //protected TDbContext GetContext(TConnectionString connStr)
        //{
        //    string conn = GetConnectionString(connStr);
        //    string code = conn.GetHashCode().ToString();
        //    //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）
        //    //传递DbContext进去获取实例的信息，在这里进行强制转换。
        //    TDbContext dbContext = CallContext.GetData("DbContext" + code) as TDbContext;
        //    if (dbContext == null) //线程在数据槽里面没有此上下文
        //    {
        //        dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), conn); //如果不存在上下文的话，创建一个EF上下文
        //                                                                                    //我们在创建一个，放到数据槽中去
        //        CallContext.SetData("DbContext" + code, dbContext);
        //    }
        //    return dbContext;
        //}
    }
}
