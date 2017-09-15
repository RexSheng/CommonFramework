using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CommonFramework.Redis
{
    public class BaseRedis<T>
        where T : new()
    {
        public static string _host, _pwd, _clientName;
        public static int _port, _dbId;

        public BaseRedis(string host, int port = 6379, string pwd = "", string clientName = "", int dbId = 0)
        {
            _host = host;
            _port = port;
            _pwd = pwd;
            _clientName = clientName;
            _dbId = dbId;
        }
        public static ConnectionMultiplexer _redis;

        protected static object _locker = new object();

        public static ConnectionMultiplexer manager
        {
            get
            {
                if (_redis == null)
                {
                    lock (_locker)
                    {
                        if (_redis != null) return _redis;
                        T t = new T();

                        _redis = GetManager();
                        return _redis;
                    }
                }
                return _redis;
            }
        }

        private static ConnectionMultiplexer GetManager()
        {
            ConfigurationOptions options = new ConfigurationOptions();
            options.EndPoints.Add(_host, _port);
            if (!string.IsNullOrEmpty(_pwd))
                options.Password = _pwd;
            options.ResolveDns = true;
            if (!string.IsNullOrEmpty(_clientName))
                options.ClientName = _clientName;
            options.SyncTimeout = 60000;//操作超时时间为1分钟
            options.ConnectTimeout = 10000;
            return ConnectionMultiplexer.Connect(options);
        }

        private static IDatabase GetClient()
        {
            IDatabase db = manager.GetDatabase(_dbId);
            return db;
        }

        #region hash：key-value操作

        /// <summary>
        /// 新增key-value
        /// </summary>
        /// <typeparam name="T">泛型类继承RedisModel</typeparam>
        /// <param name="List">数据</param>
        /// <param name="Key">key</param>
        public static void SetHashValue<T>(List<T> List, string Key) where T : RedisModel
        {
            var _db = GetClient();
            HashEntry[] entry = List.Select(m => new HashEntry(m.Id, JsonConvert.SerializeObject(m))).ToArray();
            _db.HashSet(Key, entry);
        }

        /// <summary>
        /// 新增key-value
        /// </summary>
        /// <typeparam name="T">泛型类继承RedisModel</typeparam>
        /// <param name="List">数据</param>
        /// <param name="Key">key</param>
        public static void SetHashValue<T, TKey>(List<T> List, string Key) where T : RedisModel<TKey>
        {
            var _db = GetClient();
            HashEntry[] entry = List.Select(m => new HashEntry(m.Id.ToString(), JsonConvert.SerializeObject(m))).ToArray();
            _db.HashSet(Key, entry);
        }

        /// <summary>
        /// 获取hash列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public static List<T> GetHashValue<T>(List<int> Ids, string Key) where T : RedisModel
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)m).ToArray();
            var list = _db.HashGet(Key, Keys);
            var result = list.Where(m => m.HasValue).Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;

        }

        /// <summary>
        /// 获取hash列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public static List<T> GetHashValue<T, TKey>(List<TKey> Ids, string Key) where T : RedisModel<TKey>
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)(m.ToString())).ToArray();
            var list = _db.HashGet(Key, Keys);
            var result = list.Where(m => m.HasValue).Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }


        /// <summary>
        /// 获取hash全部列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public static List<T> GetHashValueAll<T, TKey>(string Key) where T : RedisModel<TKey>
        {
            try
            {
                var _db = GetClient();
                var list = _db.HashGetAll(Key).Select(x => x.Value).ToArray();
                var result = list.Select(m => JsonConvert.DeserializeObject<T>(m.ToString())).ToList();
                return result;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 删除hash
        /// </summary>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        public static void DelHashValue(List<int> Ids, string Key)
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)m).ToArray();
            _db.HashDelete(Key, Keys);
        }

        /// <summary>
        /// 删除hash
        /// </summary>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        public static void DelHashValue(List<string> Ids, string Key)
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)m).ToArray();
            _db.HashDelete(Key, Keys);
        }

        #endregion

        #region list链表操作
        /// <summary>
        /// 新增列表
        /// </summary>
        /// <param name="List">列表数据</param>
        /// <param name="Key">key</param>
        public static void SetList(List<RedisModel> List, string Key)
        {
            var _db = GetClient();
            var result = List.Select(m => (RedisValue)JsonConvert.SerializeObject(m)).ToArray();
            _db.ListRightPush(Key, result);
        }

        /// <summary>
        /// 新增列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="List">列表数据</param>
        /// <param name="Key">key</param>
        public static void SetList<T>(List<T> List, string Key)
        {
            var _db = GetClient();
            var result = List.Select(m => (RedisValue)JsonConvert.SerializeObject(m)).ToArray();
            _db.ListRightPush(Key, result);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="start">开始序号</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<RedisModel> GetList(string Key, int start = 0, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, start, start + length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="start">开始序号</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<T> GetList<T>(string Key, int start = 0, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, start, start + length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 返回获取的列表数据，并从redis中删除（从0开始）
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<RedisModel> GetListThenDel(string Key, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, 0, length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            if (result.Count > 0)
            {
                _db.ListTrim(Key, length, -1);
            }
            return result;
        }

        /// <summary>
        /// 返回获取的列表数据，并从redis中删除（从0开始）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<T> GetListThenDel<T>(string Key, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, 0, length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            if (result.Count > 0)
            {
                _db.ListTrim(Key, length, -1);
            }
            return result;
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        public static void DelList(string Key, int length = 0)
        {
            var _db = GetClient();
            _db.ListTrim(Key, length, -1);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<RedisModel> GetPagedList(string Key, int PageIndex = 1, int PageSize = 10)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, (PageIndex - 1) * PageSize, PageIndex * PageSize - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<T> GetPagedList<T>(string Key, int PageIndex = 1, int PageSize = 10)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, (PageIndex - 1) * PageSize, PageIndex * PageSize - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }

        #endregion

        #region string操作
        /// <summary>
        /// 将数据保存至Redis
        /// </summary>
        /// <param name="key">要保存的位置</param>
        /// <param name="vals">保存的值</param>
        public static void Save(string key, params string[] vals)
        {
            var client = GetClient();
            var values = vals.Select(s => (RedisValue)s).ToArray();
            var result = client.ListRightPush(key, values);
        }

        /// <summary>
        /// 读取数据所使用的Key
        /// </summary>
        /// <param name="key">读取数据所使用的Key</param>
        /// <returns>从Redis中读取到的数据</returns>
        public static string[] Load(string key)
        {
            var client = GetClient();
            var vals = client.ListRange(key, 0);
            if (vals.Length > 0)
                client.ListTrim(key, vals.Length, -1);
            var result = new List<string>(vals.Length);
            foreach (var v in vals)
            {
                if (!v.IsNullOrEmpty)
                    result.Add(v);
            }
            return result.ToArray();
        }

        public static void SetString(string key, object value, int expireMinutes = 0)
        {
            var client = GetClient();
            if (expireMinutes > 0)
            {
                client.StringSet(key, (RedisValue)JsonConvert.SerializeObject(value), TimeSpan.FromMinutes(expireMinutes));
            }
            else
            {
                client.StringSet(key, (RedisValue)JsonConvert.SerializeObject(value));
            }
        }
        public static ST GetString<ST>(string key)
        {
            var client = GetClient();
            var data = client.StringGet(key);
            if (data.IsNull)
                return default(ST);
            return JsonConvert.DeserializeObject<ST>(data);
        }

        public static string GetString(string key)
        {
            var client = GetClient();
            return client.StringGet(key);
        }

        public static void RemoveString(string key)
        {
            var client = GetClient();
            client.KeyDelete(key);
        }

        public static bool HasKey(string key)
        {
            var client = GetClient();
            return client.KeyExists(key);
        }

        #endregion
    }
}
