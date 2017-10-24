using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using CommonFramework.Core.Dependency;
using System.Reflection;
using System.Configuration;
using CommonFramework.Core.Configure;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Unity
{
    public static class UnityBuilderExtensions
    {
        public static void AddUnity()
        {
            var container = GetConfiguredContainer();

            //FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            //FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));
             
         //   RegisterUnityTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        } 
        private static void RegisterUnityTypes(IUnityContainer container)
        {
            container.RegisterType<IDependencyProvider, DependencyProvider>();
            var _resolver = container.Resolve<IDependencyProvider>();
             
            var d = _resolver.GetInternalInterfaces();
            Register(container, d);
             
            var e = _resolver.GetInternalInterfaces(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
            Register(container, e);
        }
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterUnityTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseType"></param>
        public static void AddAssembly<BaseType>(this IAppBuilder app, Assembly assembly)
        {
            var container = GetConfiguredContainer();
            var _provider = container.Resolve<IDependencyProvider>();
            var list = _provider.GetInternalInterfaces(assembly, typeof(BaseType));
            Register(container,list);
        }

        private static void Register(IUnityContainer container,List<InternalAssemblyInfo> list) {
            list.ForEach(m =>
            {
                if (m.LifeStyle == LifeTimeOption.Singleton)
                {
                    container.RegisterType(m.InterfaceType, m.ImplementType, new ContainerControlledLifetimeManager());
                }
                else if (m.LifeStyle == LifeTimeOption.Transient)
                {
                    container.RegisterType(m.InterfaceType, m.ImplementType, new TransientLifetimeManager());
                }
                else if (m.LifeStyle == LifeTimeOption.Scoped)
                {
                    if (ConfigurationManager.AppSettings["WebProject"] != null && !Convert.ToBoolean(ConfigurationManager.AppSettings["WebProject"]))
                    {
                        container.RegisterType(m.InterfaceType, m.ImplementType, new TransientLifetimeManager());
                    }
                    else
                    {
                        container.RegisterType(m.InterfaceType, m.ImplementType, new PerRequestLifetimeManager());
                    }
                }
            });
        }
        public static void ShutDown()
        {
            var container = GetConfiguredContainer();
            container.Dispose();
        }
    }

    public class IocContainer
    {
         
        public static IUnityContainer Instance
        {
            get
            {
                return CommonFrameworkBuilder._container;
            }
        }
    }
}
