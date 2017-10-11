using Castle.MicroKernel.Registration;
using CommonFramework.Core.Configure;
using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Windsor;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.CastleWindsor
{
    public static class CastleBuilderExtensions
    {
        public static void AddCastleWindsor(Assembly controllerAssembly = null)
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
        public static void AddAssembly<BaseType>(this IAppBuilder app, Assembly assembly)
        {
            var container = IocContainer.Instance;
            var _provider = container.Resolve<IDependencyProvider>();
            var list = _provider.GetInternalInterfaces(assembly, typeof(BaseType));
            container.Register(list);
        }

        public static void Dispose(this IAppBuilder app)
        {
            var container = IocContainer.Instance;
            container.Dispose();
        }

        public static void Register(this IWindsorContainer container,List<InternalAssemblyInfo> list) {
            list.ForEach(m =>
            {
                if (m.LifeStyle == LifeTimeOption.Singleton)
                {
                    container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleSingleton().Named(m.InterfaceType.FullName+"_"+m.ImplementType.FullName));

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
    }
}
