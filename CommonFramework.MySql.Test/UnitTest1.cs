using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonFramework.CastleWindsor;
using System.Reflection;
using CommonFramework.Core.Dependency;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;

namespace CommonFramework.MySql.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMySqlMethod1()
        {
            Init();
            var _user = IocContainer.Instance.Resolve<IBaseRepository<testuser>>();
            var list1 = _user.GetAllList();
            var _userRepo2 = IocContainer.Instance.Resolve<IUserRepository>();
            var list2 = _userRepo2.GetAllList();
            var list3 = _userRepo2.GetAllList("testdatabaseEntities2");
        }

        private void Init()
        {
            if (IocContainer.Instance.Kernel.HasComponent(typeof(IDependencyProvider)))
                return;
            CastleProvider.RegisterCastle();
            CastleProvider.Register(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
             

            var _connStr = IocContainer.Instance.Resolve<IConnectionStringProvider>();
            _connStr.SetConnectionStringProvider(m => _connStr.GetWebConfigConnectionString(m), "testdatabaseEntities");
            var _email = IocContainer.Instance.Resolve<IEmailConfiguration>();
            _email.Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"));
            _email.Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());

        }
    }
}
