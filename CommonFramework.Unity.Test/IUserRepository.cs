using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Unity.Test
{
    public interface IUserRepository:IBaseRepository<UserInfo>,ITransientDependency
    {
    }
}
