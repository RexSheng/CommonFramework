using CommonFramework.MySql.EntityFramework;

namespace CommonFramework.Test.MySql
{
    public interface IBaseDao<TEntity> : IBaseDao<testdatabaseEntities, TEntity>
        where TEntity : class
    {

    }
} 