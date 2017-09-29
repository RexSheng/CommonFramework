using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using CommonFramework.Core.Dependency;
using CommonFramework.Unity;
using System.Reflection;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;

namespace CommonFramework.Unity.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUnityRegister()
        {
            Init();
            var container = UnityProvider.GetConfiguredContainer();
            var _service = container.Resolve<ITestService>();
            var testData = _service.getList();
            var resolver = container.Resolve<IBaseRepository<WebAPIDemoEntities, UserInfo>>();
            var list1 = resolver.GetAllList("WebAPIDemoEntities");
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }
        [TestMethod]
        public void TestUnityGetList1()
        {
            Init();
            var resolver = UnityProvider.GetConfiguredContainer().Resolve<IBaseRepository<UserInfo>>();
            var list1 = resolver.GetAllList();
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }
        [TestMethod]
        public void TestUnityGetList0()
        {
            Init();
            var resolver = UnityProvider.GetConfiguredContainer().Resolve<IUserRepository>();
            var list1 = resolver.GetAllList("WebAPIDemoEntities");
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }
        private void Init()
        {
            var container = UnityProvider.GetConfiguredContainer();
            if (container.IsRegistered(typeof(ITestService)))
                return;
            UnityProvider.StartUp();
            UnityProvider.Register(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
            var _connStr = container.Resolve<IConnectionStringProvider>();
            _connStr.SetConnectionStringProvider(m=>_connStr.GetAppConfigConnectionString(m), "WebAPIDemoEntities");
            var _email = container.Resolve<IEmailConfiguration>();
            _email.Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("asdfasf"));
        }
    }
}
