using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Dependency;

namespace CommonFramework.CastleWindsor.Test
{
    public interface IBaseDao<TEntity>:IBaseRepository<WebAPIDemoEntities,TEntity>,ITransientDependency
        where TEntity : class
    {

    }
}
