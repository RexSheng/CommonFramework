using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Dependency;
/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.MySql.Test
{
    public interface IBaseRepository<TEntity>:IBaseRepository<testdatabaseEntities,TEntity>
        where TEntity:class
    {
    }
}
