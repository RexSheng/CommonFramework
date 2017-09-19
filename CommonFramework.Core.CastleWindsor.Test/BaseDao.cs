using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;

namespace CommonFramework.Core.CastleWindsor.Test
{
    public class BaseDao<TEntity> : BaseRepository<WebAPIDemoEntities, TEntity>, IBaseDao<TEntity>
        where TEntity:class
    {
        public BaseDao(IDbContextProvider contextProvider) : base(contextProvider)
        {
        }
    }
}
