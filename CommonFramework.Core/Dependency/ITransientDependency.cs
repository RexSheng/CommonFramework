using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Dependency
{
    /// <summary>
    /// 每次调用创建实例
    /// </summary>
    public interface ITransientDependency : IBaseDependency
    {
    }
}
