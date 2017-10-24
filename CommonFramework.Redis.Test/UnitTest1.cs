using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonFramework.CastleWindsor;
using System.Reflection;
using CommonFramework.Core.Dependency;
using CommonFramework.Core.Configure;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;

namespace CommonFramework.Redis.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RedisTest1()
        {
            Init();
        }

        private void Init()
        {
            //if (IocContainer.Instance.Kernel.HasComponent(typeof(IDependencyProvider)))
            //    return; 
            //CastleBuilderExtensions.AddCastleWindsor();
            //var app = IocContainer.Instance.Resolve<IAppBuilder>();
            
            //app.AddAssembly<IBaseDependency>(Assembly.GetExecutingAssembly());
            //app.AddAssembly<IBaseDependency>(Assembly.GetAssembly(typeof(IRedisConfiguration)));
            //app.AddEfService()
            //    .SetConnectionStringProvider(m => ConnectionStringProviderExtensions.GetWebConfigConnectionString(m), "testdatabaseEntities");
            //app.AddEmailService()
            //    .Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"))
            //    .Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());
            //app.AddRedisService(IocContainer.Instance.Resolve<IRedisConfiguration>())
            //    .AddEndPoints("127.0.0.1")
            //    .Password("123456");
        }
    }
}
