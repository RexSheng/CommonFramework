using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CommonFramework.Oracle.AdoNet
{
    public class Provider<TConnectionString>
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
        public static string GetAppConfigConnectionString<T>(T conn)
        {
            var result = ConfigurationManager.AppSettings[conn.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return conn.ToString();
        }

        public string GetConnectionString(TConnectionString dbIndex)
        {
            string conn = DefaultConnectionStringGetter(dbIndex);
            return conn;
        }
    }
}
