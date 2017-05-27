using CommonFramework.SqlServer.EntityFramework;

namespace CommonFramework.Test.SqlServer
{
    public class BaseDao<TEntity>:BaseDao<WebAPIDemoEntities, TEntity>,IBaseDao<TEntity>
        where TEntity:class
    {
        public BaseDao() : base(GetWebConfigConnectionString, "WebAPIDemoEntities")
        {

        }
    }
}
