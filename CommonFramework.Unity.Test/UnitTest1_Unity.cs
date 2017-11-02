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
    public class UnitTest1_Unity
    {
        [TestMethod]
        public void TestUnityRegister()
        {
            Init();
            var container = IocContainer.Instance;
            var _service = container.Resolve<ITestService>();
            var testData = _service.getList();
            var testData2 = ((ITestService)container.Resolve(typeof(TestService))).getList();
            var testData3 = ((ITestService)container.Resolve(typeof(TestService2))).getList();
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
            var resolver = IocContainer.Instance.Resolve<IBaseRepository<UserInfo>>();
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
            var resolver = IocContainer.Instance.Resolve<IUserRepository>();
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
            var container = IocContainer.Instance;
            //if (container.IsRegistered(typeof(ITestService)))
            //    return;
            CommonFrameworkBuilder.Initialize();
            CommonFrameworkBuilder.AddAssembly<IBaseDependency>(Assembly.GetExecutingAssembly());
            CommonFrameworkBuilder.AddEfService()
                .SetConnectionStringProvider(m => ConnectionStringProviderExtensions.GetAppConfigConnectionString(m), "WebAPIDemoEntities");
            CommonFrameworkBuilder.AddEmailService()
                .Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"))
                .Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());

            CommonFrameworkBuilder.AddLog4Net().Configure("Log.xml");

            //UnityBuilderExtensions.AddUnity();
            //var app = IocContainer.Instance.Resolve<IAppBuilder>();
            //app.AddAssembly<IBaseDependency>(Assembly.GetExecutingAssembly());
            //app.AddEfService()
            //    .SetConnectionStringProvider(m => ConnectionStringProviderExtensions.GetAppConfigConnectionString(m), "WebAPIDemoEntities");
            //app.AddEmailService()
            //    .Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"))
            //    .Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());
 
        }
    }
}
