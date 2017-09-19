using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;

namespace CommonFramework.Core.CastleWindsor.Test
{
    public class UserDao : BaseDao<UserInfo>, IUserDao
    {
        public UserDao(IDbContextProvider contextProvider) : base(contextProvider)
        {
        }
    }
}
