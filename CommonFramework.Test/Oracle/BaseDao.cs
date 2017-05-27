using CommonFramework.Oracle.EntityFramework;

namespace CommonFramework.Test.Oracle
{
    public class BaseDao<TEntity> : BaseDao<OracleEntities, TEntity>, IBaseDao<TEntity>
        where TEntity:class
    {
        public BaseDao()
            : base(GetWebConfigConnectionString, "OracleEntities")
        {

        }
    }
}
