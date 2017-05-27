using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using System.Data.Entity;

namespace CommonFramework.MySql.EntityFramework
{
    public class DbContextProvider<TConnectionString, TDbContext> : Provider<TConnectionString, TDbContext>, IDisposable
        where TDbContext : DbContext, new()
    {
        private static TDbContext _dbContext;
        public DbContextProvider() { }
        public DbContextProvider(Func<TConnectionString, string> defaultConnectionProvider, TConnectionString conn)
        {
            string str = defaultConnectionProvider(conn);
            _dbContext = GetContext(str);
        }

        public TDbContext Context {
            get {
                return _dbContext;
            }
        }

        public TDbContext GetDbContext(Func<TConnectionString, string> defaultConnectionProvider, TConnectionString conn)
        {
            string str = defaultConnectionProvider(conn);
            return GetContext(str);
        }

        public TDbContext GetDbContext(TConnectionString conn)
        { 
            return GetDbContext(GetWebConfigConnectionString<TConnectionString>, conn);
        }

        public DbContextProvider(TConnectionString conn) : this(GetWebConfigConnectionString<TConnectionString>, conn)
        {

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    //public class DbContextProvider<TDbContext> : DbContextProvider<string, TDbContext>
    //    where TDbContext : DbContext, new()
    //{
    //    public DbContextProvider(string conn="Default") : base(GetWebConfigConnectionString<string>, conn)
    //    {

    //    }
    //}

    public class DbContextOptions<TDbContext>
    {
        public string DefaultConnectionString { get; set; }
        
    }

}
