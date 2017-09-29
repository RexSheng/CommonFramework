using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.MySql.Test
{
    public class BaseRepository<TEntity> : BaseRepository<testdatabaseEntities, TEntity>,IBaseRepository<TEntity>
        where TEntity : class
    {
        public BaseRepository(IDbContextProvider contextProvider) : base(contextProvider)
        {
        }
    }
}
