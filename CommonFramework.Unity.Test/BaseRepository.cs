using CommonFramework.Core.EntityFramework;
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
    public class BaseRepository<TEntity> : BaseRepository<WebAPIDemoEntities, TEntity>, IBaseRepository<TEntity>
        where TEntity : class
    {
        public BaseRepository(IDbContextProvider contextProvider) : base(contextProvider)
        {
        }
    }
}
