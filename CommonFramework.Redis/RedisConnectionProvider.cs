using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Redis
{
    public class RedisConnectionProvider : IRedisConnectionProvider
    {
        private readonly IRedisConfiguration _configuration;

        public RedisConnectionProvider(IRedisConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConnectionMultiplexer getConnection()
        {
            var option = _configuration.getConfigurationOption();
            return ConnectionMultiplexer.Connect(option);
        }

        public IDatabase getDatabase(int? dbId = null)
        {
            if (dbId.HasValue)
            {
                return getConnection().GetDatabase(dbId.Value);
            }
            else
            {
                return getConnection().GetDatabase();
            }
        }
    }
}
