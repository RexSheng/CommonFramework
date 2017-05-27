using CommonFramework.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Test.Redis
{
    public class RedisHelper : BaseRedis<RedisHelper>
    {
        public RedisHelper()
            : base("127.0.0.1")
        {
        
        }
    }
}
