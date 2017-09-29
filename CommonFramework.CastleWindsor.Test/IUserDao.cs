using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Dependency;

namespace CommonFramework.CastleWindsor.Test
{
    public interface IUserDao:IBaseDao<UserInfo>,ITransientDependency
    {
    }
}
