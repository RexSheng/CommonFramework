using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using System.Reflection;
using CommonFramework.Core.Dependency;
using System.Configuration;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;
using CommonFramework.Core.Log;
using CommonFramework.DTO;
using AutoMapper;
using CommonFramework.Redis;
using CommonFramework.Core.Localization;

using System.Linq.Expressions;

namespace CommonFramework.CastleWindsor
{
    public static class CommonFrameworkBuilder
    {
        #region 系统配置
        public static void Initialize(Assembly controllerAssembly = null)
        {
            var container = IocContainer.Instance;
            container.Install(new ServiceInstaller());
            if (controllerAssembly != null)
            {
                container.Register(Classes.FromAssembly(controllerAssembly).IncludeNonPublicTypes().BasedOn<IController>().LifestyleTransient());
            }
            else
            {
                container.Register(Classes.FromAssembly(Assembly.GetCallingAssembly()).IncludeNonPublicTypes().BasedOn<IController>().LifestyleTransient());
            }
            //初始化一个IOC容器
            //完成IWindsorInstaller接口中的注册
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }

        public static void AddAssembly<BaseType>(Assembly assembly)
        {
            var container = IocContainer.Instance;
            var _provider = container.Resolve<IDependencyProvider>();
            var list = _provider.GetInternalInterfaces(assembly, typeof(BaseType));
            container.Register(list);
        }

        public static void Dispose()
        {
            var container = IocContainer.Instance;
            container.Dispose();
        }

        internal static void Register(this IWindsorContainer container, List<InternalAssemblyInfo> list)
        {
            list.ForEach(m =>
            {
                if (m.LifeStyle == LifeTimeOption.Singleton)
                {
                    container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleSingleton().Named(m.InterfaceType.FullName + "_" + m.ImplementType.FullName));

                }
                else if (m.LifeStyle == LifeTimeOption.Transient)
                {
                    container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.FullName + "_" + m.ImplementType.FullName));
                }
                else if (m.LifeStyle == LifeTimeOption.Scoped)
                {
                    if (ConfigurationManager.AppSettings["WebProject"] != null && !Convert.ToBoolean(ConfigurationManager.AppSettings["WebProject"]))
                    {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.FullName + "_" + m.ImplementType.FullName));
                    }
                    else
                    {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestylePerWebRequest().Named(m.InterfaceType.FullName + "_" + m.ImplementType.FullName));
                    }
                }
            });
        }

        #endregion

        #region 服务配置

        public static IConnectionStringProvider AddEfService(Action<IConnectionStringProvider> action = null)
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();
            var assembly=Assembly.GetExecutingAssembly();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="additionalAction">用户自定义的配置</param>
        /// <param name="assemblyToScan">系统默认初始化带有DtoMapAttribute的程序集</param>
        public static void AddDtoService(Action<IMapperConfigurationExpression> additionalAction = null, params string[] assemblyToScan)
        {
            DtoBuilderExtensions.Config(additionalAction,assemblyToScan);
            IocContainer.Instance.Register(
                Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
            );
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
