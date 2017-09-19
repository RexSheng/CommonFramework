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

namespace CommonFramework.Core.CastleWindsor.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetList2()
        {
            Init();
            var resolverTest = IocContainer.Instance.Resolve<ITestService>();
            var list = resolverTest.getList();
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
            
            _user.AddByTrans(user, new Action<WebAPIDemoEntities, UserInfo>((context, data) =>
            {
                _user.Add("WebAPIDemoEntities2", user2);
            }),false);
        }

        [TestMethod]
        public void TestEmail()
        {

            Init();
            var _email = IocContainer.Instance.Resolve<IEmailSender>();
            _email.SendEmail("991823949@qq.com", "", "这是标题", "请查看内容");
        }


        #region 初始化container
        private void Init()
        {
            if (IocContainer.Instance.Kernel.HasComponent(typeof(IDependencyProvider)))
                return;

            Bootstrapper.Register(new Action<IWindsorContainer>((container) =>
            {
                var _resolver = container.Resolve<IDependencyProvider>();
                var e = _resolver.GetInternalInterfaces(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
                e.ForEach(m =>
                {
                    if (m.LifeStyle == LifeTimeOption.Singleton)
                    {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleSingleton().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));

                    }
                    else if (m.LifeStyle == LifeTimeOption.Transient)
                    {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
                    }
                    else if (m.LifeStyle == LifeTimeOption.Scoped)
                    {
                        if (ConfigurationManager.AppSettings["WebProject"] != null && !Convert.ToBoolean(ConfigurationManager.AppSettings["WebProject"]))
                        {
                            container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
                        }
                        else
                        {
                            container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestylePerWebRequest().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
                        }

                    }
                });
            }));
            var _connStr = IocContainer.Instance.Resolve<IConnectionStringProvider>();
            _connStr.SetConnectionStringProvider(_connStr.GetWebConfigConnectionString, "WebAPIDemoEntities"); 
            var _email = IocContainer.Instance.Resolve<IEmailConfiguration>();
            _email.Config(cfg=>cfg.setHost("smtp.126.com").setSenderAddress("shengxupeng@126.com").setEmailSenderName("shengxupeng").setEmailPwd("xxxx"));

        }
        #endregion

    }
}
