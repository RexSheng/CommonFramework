using CommonFramework.SqlServer.EntityFramework;

namespace CommonFramework.Test.SqlServer
{
    public interface IBaseDao<TEntity> : IBaseDao<WebAPIDemoEntities, TEntity>
        where TEntity : class
    {

    }
} 