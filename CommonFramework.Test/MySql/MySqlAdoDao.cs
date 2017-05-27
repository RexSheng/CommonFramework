using CommonFramework.MySql.AdoNet;

namespace CommonFramework.Test.MySql
{
    public class MySqlAdoDao:BaseDao, IMySqlAdoDao
    {
        public MySqlAdoDao() : base(GetWebConfigConnectionString, "MySqlAdo")
        {
        }
    }
}
