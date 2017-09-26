using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Dependency
{
    /// <summary>
    /// 依赖注入提供接口
    /// </summary>
    public interface IDependencyProvider
    {
        /// <summary>
        /// 获取CommonFramework.Core程序集中所有的内部接口及其实现
        /// </summary>
        /// <returns></returns>
        List<InternalAssemblyInfo> GetInternalInterfaces();

        /// <summary>
        /// 获取程序集中实现某个类型的所有接口及其实现类
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="interfaceType">被继承的类型</param>
        /// <returns></returns>
        List<InternalAssemblyInfo> GetInternalInterfaces(Assembly assembly, Type interfaceType);

    

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <typeparam name="TContainer">容器</typeparam>
        /// <param name="types"></param>
        //void Register<TContainer>(Expression<Action<TContainer,IDependencyProvider>> types) where TContainer:IDisposable;
    }
}
