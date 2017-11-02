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
            var _container = IocContainer.Instance;
            var _provider = _container.Resolve<IDependencyProvider>();
            var list = _provider.GetInternalInterfaces(assembly, typeof(BaseType));
            _container.Register(list);
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

            //var d = _resolver.GetInternalInterfaces();
            //Register(container, d);

            //var e = _resolver.GetInternalInterfaces(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
            //Register(container, e);
        }
        private static void Register(this IUnityContainer container, List<InternalAssemblyInfo> list)
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
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly = Assembly.GetExecutingAssembly();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IBaseRepository<,>),
                ImplementType = typeof(BaseRepository<,>),
                LifeStyle = LifeTimeOption.Transient
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IConnectionStringProvider),
                ImplementType = typeof(ConnectionStringProvider),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IDbContextProvider),
                ImplementType = typeof(DbContextProvider),
                LifeStyle = LifeTimeOption.Scoped
            });
            var container = IocContainer.Instance;
            container.Register(list);

            var _connectionStringProvider = container.Resolve<IConnectionStringProvider>();
            if (action != null)
            {
                action.Invoke(_connectionStringProvider);
            }
            return _connectionStringProvider;
        }

        public static IEmailConfiguration AddEmailService(Action<IEmailConfiguration> action = null)
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly = Assembly.GetExecutingAssembly();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IEmailSettingOption),
                ImplementType = typeof(EmailSettingOption),
                LifeStyle = LifeTimeOption.Transient
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IEmailConfiguration),
                ImplementType = typeof(EmailConfiguration),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IEmailSender),
                ImplementType = typeof(EmailSender),
                LifeStyle = LifeTimeOption.Transient
            });
            var container = IocContainer.Instance;
            container.Register(list);
            var _emailConfiguration = container.Resolve<IEmailConfiguration>();
            if (action != null)
            {
                action.Invoke(_emailConfiguration);
            }
            return _emailConfiguration;
        }

        public static ILogConfiguration AddLog4Net()
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly = Assembly.GetExecutingAssembly();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(ILogConfiguration),
                ImplementType = typeof(LogConfiguration),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(ILogProvider),
                ImplementType = typeof(LogProvider),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(ILog),
                ImplementType = typeof(Log4NetImplement),
                LifeStyle = LifeTimeOption.Singleton
            });
            var container = IocContainer.Instance;
            container.Register(list);
            return container.Resolve<ILogConfiguration>();
        }

        public static void AddDtoService(Action<IMapperConfigurationExpression> additionalAction = null, params string[] assemblyToScan)
        {
            DtoBuilderExtensions.Config(additionalAction,assemblyToScan);
            IocContainer.Instance.RegisterInstance<IMapper>(Mapper.Instance, new ContainerControlledLifetimeManager());
        }

        public static IRedisConfiguration AddRedisService()
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly = Assembly.GetExecutingAssembly();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IRedisConfiguration),
                ImplementType = typeof(RedisConfiguration),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IRedisConnectionProvider),
                ImplementType = typeof(RedisConnectionProvider),
                LifeStyle = LifeTimeOption.Transient
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(IBaseRedis),
                ImplementType = typeof(BaseRedis),
                LifeStyle = LifeTimeOption.Transient
            });
            var container = IocContainer.Instance;
            container.Register(list);

            return container.Resolve<IRedisConfiguration>();
        }

        public static ILanguageProvider AddLocalization()
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly = Assembly.GetExecutingAssembly();
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(ILocalizationDictionary),
                ImplementType = typeof(LocalizationDictionary),
                LifeStyle = LifeTimeOption.Singleton
            });
            list.Add(new InternalAssemblyInfo()
            {
                Assembly = assembly,
                InterfaceType = typeof(ILanguageProvider),
                ImplementType = typeof(LanguageProvider),
                LifeStyle = LifeTimeOption.Singleton
            });
            var container = IocContainer.Instance;
            container.Register(list);

            return container.Resolve<ILanguageProvider>();
        }

        #endregion
    }
}
