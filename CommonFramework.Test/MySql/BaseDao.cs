using CommonFramework.MySql.EntityFramework;

namespace CommonFramework.Test.MySql
{
    public class BaseDao<TEntity> : BaseDao<testdatabaseEntities, TEntity>, IBaseDao<TEntity>
        where TEntity:class
    {
        public BaseDao()
            : base(GetWebConfigConnectionString, "testdatabaseEntities")
        {

        }
    }
}
