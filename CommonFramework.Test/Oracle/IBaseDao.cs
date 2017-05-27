using CommonFramework.Oracle.EntityFramework;

namespace CommonFramework.Test.Oracle
{
    public interface IBaseDao<TEntity> : IBaseDao<OracleEntities, TEntity>
        where TEntity : class
    {

    }
} 