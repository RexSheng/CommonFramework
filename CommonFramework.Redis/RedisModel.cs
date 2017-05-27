using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Redis
{
    public class RedisModel<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }

    public class RedisModel : RedisModel<string>
    {
    }
}
