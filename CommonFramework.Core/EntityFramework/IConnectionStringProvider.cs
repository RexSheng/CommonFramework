using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.EntityFramework
{
    /// <summary>
    /// 连接字符串提供接口
    /// </summary>
    public interface IConnectionStringProvider : IInternalDependency
    {
        /// <summary>
        /// 从web.config的connectionStrings节点获取连接字符串
        /// </summary>
        /// <param name="connKey">链接key</param>
        /// <returns></returns>
        string GetWebConfigConnectionString(object connKey);

        /// <summary>
        /// 从web.config的appSettings节点获取连接字符串
        /// </summary>
        /// <param name="connKey">链接key</param>
        /// <returns></returns>
        string GetAppConfigConnectionString(object connKey);

        /// <summary>
        /// 设置默认字符串获取方式
        /// </summary>
        /// <param name="customerConnectionGetter">字符串获取方法</param>
        /// <param name="defaultConnKey"></param>
        void SetConnectionStringProvider(Expression<Func<object, string>> customerConnectionGetter, object defaultConnKey);

        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <param name="connKey">链接key</param>
        /// <returns></returns>
        string GetConnectionString(object connKey);
    }

}
