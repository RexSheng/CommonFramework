using System.Data.Entity;
using CommonFramework.SqlServer.EntityFramework;

namespace CommonFramework.Test.SqlServer
{
    public class DbContextProvider<TDbContext> : DbContextProvider<string, TDbContext>
        where TDbContext : DbContext, new()
    {
        public DbContextProvider(string conn = "Default") : base(GetWebConfigConnectionString<string>, conn)
        {

        }
    }
}
