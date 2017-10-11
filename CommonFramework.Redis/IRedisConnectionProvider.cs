using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Dependency;
using StackExchange.Redis;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Redis
{
    public interface IRedisConnectionProvider:ITransientDependency
    {
        ConnectionMultiplexer getConnection();


        IDatabase getDatabase(int? dbId = null);
        
    }
}
