using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Configure;
using CommonFramework.Redis;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Configure
{
    public static class RedisBuilderExtensions
    {
        public static IRedisConfiguration  AddRedisService(this IAppBuilder app,IRedisConfiguration configuration) {

            return configuration;
        }
    }
}
