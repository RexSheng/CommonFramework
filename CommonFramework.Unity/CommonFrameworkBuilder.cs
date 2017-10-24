using AutoMapper;
using CommonFramework.Core.Dependency;
using CommonFramework.Core.Email;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Localization;
using CommonFramework.Core.Log;
using CommonFramework.DTO;
using CommonFramework.Redis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Unity
{
    public static class CommonFrameworkBuilder
    {
        #region 基础

        internal static IUnityContainer _container { get; private set; }

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        internal static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion 
        #region 系统配置
        public static void Initialize(Assembly controllerAssembly = null)
        {
            var container = GetConfiguredContainer();

            //FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            //FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            //   RegisterUnityTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            _container = container;
        }

        public static void AddAssembly<BaseType>(Assembly assembly)
        {
            var container = IocContainer.Instance;
            var _provider = container.Resolve<IDependencyProvider>();
            var list = _provider.GetInternalInterfaces(assembly, typeof(BaseType));
            Register(container, list);
        }

        public static void Dispose()
        {
            var container = IocContainer.Instance;
            container.Dispose();
        }
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterUnityTypes(container);
            return container;
        });
        private static void RegisterUnityTypes(IUnityContainer container)
        {
            container.RegisterType<IDependencyProvider, DependencyProvider>();
            var _resolver = container.Resolve<IDependencyProvider>();

            var d = _resolver.GetInternalInterfaces();
            Register(container, d);

            var e = _resolver.GetInternalInterfaces(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
            Register(container, e);
        }
        private static void Register(IUnityContainer container, List<InternalAssemblyInfo> list)
        {
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
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        #endregion

        #region 服务配置

        public static IConnectionStringProvider AddEfService(Action<IConnectionStringProvider> action = null)
        {
            var _connectionStringProvider = IocContainer.Instance.Resolve<IConnectionStringProvider>();
            if (action != null)
            {
                action.Invoke(_connectionStringProvider);
            }
            return _connectionStringProvider;
        }

        public static IEmailConfiguration AddEmailService(Action<IEmailConfiguration> action = null)
        {
            var _emailConfiguration = IocContainer.Instance.Resolve<IEmailConfiguration>();
            if (action != null)
            {
                action.Invoke(_emailConfiguration);
            }
            return _emailConfiguration;
        }

        public static ILogConfiguration AddLog4Net()
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = Assembly.GetExecutingAssembly(),
                InterfaceType = typeof(ILog),
                ImplementType = typeof(Log4NetImplement),
                LifeStyle = LifeTimeOption.Singleton
            });
            var container = IocContainer.Instance;
            Register(container,list);
            return container.Resolve<ILogConfiguration>();
        }

        public static void AddDtoService(Action<IMapperConfigurationExpression> additionalAction = null, params string[] assemblyToScan)
        {
            DtoBuilderExtensions.Config(additionalAction,assemblyToScan);
            IocContainer.Instance.RegisterInstance<IMapper>(Mapper.Instance, new ContainerControlledLifetimeManager());
        }

        public static IRedisConfiguration AddRedisService()
        {
            var container = IocContainer.Instance;
            return container.Resolve<IRedisConfiguration>();
        }

        public static ILanguageProvider AddLocalization()
        {
            var container = IocContainer.Instance;
            LocalizationBuilderExtensions.Enabled = true;
            return container.Resolve<ILanguageProvider>();
        }

        #endregion
    }
}
