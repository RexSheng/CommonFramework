using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Redis
{
    public class BaseRedis: IBaseRedis
    {
        private readonly IRedisConnectionProvider _provider;
        public BaseRedis(IRedisConnectionProvider provider) {
            _provider = provider;
        }
        
        public ConnectionMultiplexer GetConnection()
        {
            return _provider.getConnection();
        }

        public IDatabase GetDatabase(int? dbId = null) {
            return _provider.getDatabase(dbId);
        }

        public List<T> GetAllList<T>(string key) {
            var _db = GetDatabase();
            return _db.ListRange(key).Where(m=>m.HasValue).Select(m=>JsonConvert.DeserializeObject<T>(m)).ToList();
        }
    }
}
