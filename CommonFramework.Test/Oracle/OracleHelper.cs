using CommonFramework.Oracle.AdoNet;

namespace CommonFramework.Test.Oracle
{
    public class OracleHelper : BaseDao, IOracleHelper
    {
        public OracleHelper()
            : base(GetWebConfigConnectionString, "OracleAdo")
        {
        }
    }
}
