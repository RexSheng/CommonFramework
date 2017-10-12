using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using CommonFramework.Core.Dependency;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.EntityFramework.PagedList;
using CommonFramework.Core.Email;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using CommonFramework.Core.Configure;
using CommonFramework.Core.Log;

namespace CommonFramework.CastleWindsor.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CastleMultiResolve()
        {
            Init();
            var resolverTest = IocContainer.Instance.Resolve<ITestService>();
            var list = resolverTest.getList();
            var resolverTest11 = IocContainer.Instance.Resolve<ITestService>(typeof(ITestService).FullName + "_" + typeof(TestService).FullName);
            var list11 = resolverTest11.getList();
            var resolverTest22 = IocContainer.Instance.Resolve<ITestService>(typeof(ITestService).FullName + "_" + typeof(TestService2).FullName);
            var list22 = resolverTest22.getList();
            Assert.AreNotEqual(list11.Count, list22.Count);
        }
        [TestMethod]
        public void TestGetList2()
        {
            Init();
            
            var resolver = IocContainer.Instance.Resolve<IBaseRepository<WebAPIDemoEntities, UserInfo>>();
            var list1 = resolver.GetAllList("WebAPIDemoEntities");
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }

        [TestMethod]
        public void TestGetList1()
        {
            Init();
            var resolver = IocContainer.Instance.Resolve<IBaseDao<UserInfo>>();
            var list1 = resolver.GetAllList("WebAPIDemoEntities");
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }
        [TestMethod]
        public void TestGetList0()
        {
            Init();
            var resolver = IocContainer.Instance.Resolve<IUserDao>();
            var list1 = resolver.GetAllList("WebAPIDemoEntities");
            var list2 = resolver.GetAllList("WebAPIDemoEntities2");
            var list3 = resolver.GetAllList("WebAPIDemoEntities");
            Assert.AreNotEqual(list1.Count, 0);
            Assert.AreNotEqual(list1.Count, list2.Count);
            Assert.AreNotEqual(list2.Count, list3.Count);
            Assert.AreEqual(list1.Count, list3.Count);
        }

        [TestMethod]
        public void TestPagedList()
        {

            Init();
            var _user = IocContainer.Instance.Resolve<IUserDao>();
            var list = _user.ToCommonPagedList(m => m.Name, true, 2, 10);

            var query = _user.GetAll().Where(m => m.Id < 100);
            var list2 = query.ToCommonPagedList(m => m.Name, true, 2, 10);
            var list2Dto = list2.Select(m => new UserDto() { UserDtoId = m.Id, UserDtoName = m.Id + m.Name });
            var list3 = new CommonPagedList<UserDto>(list2Dto, list2.PageIndex, list2.PageSize, list2.TotalItemCount);
            Assert.AreEqual(list2.Count, list3.Count);
        }

        [TestMethod]
        public void TestAdd()
        {

            Init();
            var _user = IocContainer.Instance.Resolve<IUserDao>();
            UserInfo user = new UserInfo()
            {
                Name = DateTime.Now.ToString("HHmmssfff"),
                age = 123
            };
            UserInfo user2 = new UserInfo()
            {
                Name = DateTime.Now.ToString("HHmmssfff"),
                 age = 124
            };
            //_user.Add(user);
            //_user.Add("WebAPIDemoEntities2", user2);
            
            _user.AddByTrans(user,null, new Action<WebAPIDemoEntities, UserInfo>((context, data) =>
            {
                _user.Add(user2, "WebAPIDemoEntities2");
            }));
        }

        [TestMethod]
        public void TestEmail()
        {

            Init();
            var _email = IocContainer.Instance.Resolve<IEmailSender>();
            _email.SendEmail("991823949@qq.com", "", "这是标题", "请查看内容",senderKey:"aaa");
        }

        [TestMethod]
        public void TestLog()
        {
            Init();

            var resolver = IocContainer.Instance.Resolve<ILog>();
            resolver.Info("全部aaa");
            resolver.Info("测试aaa", name: "TestLog2");
            resolver.Debug("系统aaa", name: "SysLog2");
            resolver.Debug("用户aaa", name: "UserLog2");
        }

        #region 初始化container
        private void Init()
        {
            if (IocContainer.Instance.Kernel.HasComponent(typeof(IDependencyProvider)))
                return;
            CastleBuilderExtensions.AddCastleWindsor();
            var app = IocContainer.Instance.Resolve<IAppBuilder>();

            app.AddAssembly<IBaseDependency>(Assembly.GetExecutingAssembly());
            app.AddEfService()
                .SetConnectionStringProvider(m => ConnectionStringProviderExtensions.GetWebConfigConnectionString(m), "WebAPIDemoEntities");
            app.AddEmailService()
                .Config(cfg => cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"))
                .Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());

            app.AddLog4Net().Configure("Log.xml");
            //var _connStr = IocContainer.Instance.Resolve<IConnectionStringProvider>();
            //_connStr.SetConnectionStringProvider(m=>_connStr.GetWebConfigConnectionString(m), "WebAPIDemoEntities"); 
            //var _email = IocContainer.Instance.Resolve<IEmailConfiguration>();
            //_email.Config(cfg=>cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("999").setKey("aaa"));
            //_email.Config(cfg => cfg.setHost("smtp.exmail.qq.com").setSenderAddress("991823949@qq.com").setEmailSenderName("shengxupeng").setEmailPwd("9999").setKey("bbb").isDefault());

        }
        #endregion

    }
}
