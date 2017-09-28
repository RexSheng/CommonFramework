using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStringProvider : IConnectionStringProvider, ISingletonDependency
    {
        /// <summary>
        /// 自定义获取方法
        /// </summary>
        private Expression<Func<object, string>> _customerConnectionGetter;
        /// <summary>
        /// 默认连接key
        /// </summary>
        private object _defaultConnKey;
        /// <summary>
        /// 从web.config的connectionStrings节点获取连接字符串
        /// </summary>
        /// <param name="connKey"></param>
        /// <returns></returns>
        public string GetWebConfigConnectionString(object connKey)
        {
            var result = ConfigurationManager.ConnectionStrings[connKey.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return connKey.ToString();
        }

        /// <summary>
        /// 从web.config的appSettings节点获取连接字符串
        /// </summary>
        /// <param name="connKey"></param>
        /// <returns></returns>
        public string GetAppConfigConnectionString(object connKey)
        {
            var result = ConfigurationManager.AppSettings[connKey.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return connKey.ToString();
        }

        public void SetConnectionStringProvider(Expression<Func<object, string>> customerConnectionGetter, object defaultConnKey)
        {
            _customerConnectionGetter = customerConnectionGetter;
            _defaultConnKey = defaultConnKey;
        }

        public string GetConnectionString(object connKey)
        {
            string conn = (connKey==null) ? (_defaultConnKey==null ? "Default" : _defaultConnKey.ToString()) : connKey.ToString();
            if (_customerConnectionGetter == null)
            {
                return GetWebConfigConnectionString(conn);
            }
            else
            {
                return _customerConnectionGetter.Compile().Invoke(conn);
            }
        }
    }
}
