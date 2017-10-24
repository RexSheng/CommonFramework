//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using CommonFramework.Core.Dependency;
//using System.Configuration;
//using Castle.MicroKernel.Registration;
//using System.Web.Mvc;

///*
//* CopyRight ©2017 All Rights Reserved
//* 作者:Rex Sheng
//*/
//namespace CommonFramework.CastleWindsor
//{
//    public class CastleProvider
//    {
//        /// <summary>
//        /// 基础注入
//        /// </summary>
//        /// <param name="controllerAssembly">asp.net mvc controller 所在程序集</param>
//        public static void RegisterCastle(Assembly controllerAssembly=null)
//        {
//            var container = IocContainer.Instance;
//            container.Install(new ServiceInstaller());
//            if (controllerAssembly != null)
//            {
//                container.Register(Classes.FromAssembly(controllerAssembly).IncludeNonPublicTypes().BasedOn<IController>().LifestyleTransient());
//            }
//            else
//            {
//                container.Register(Classes.FromAssembly(Assembly.GetCallingAssembly()).IncludeNonPublicTypes().BasedOn<IController>().LifestyleTransient());
//            }
//            //初始化一个IOC容器
//            //完成IWindsorInstaller接口中的注册
//            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));

//        }

//        /// <summary>
//        /// 程序集注入
//        /// </summary>
//        /// <param name="assembly"></param>
//        /// <param name="baseType"></param>
//        public static void Register(Assembly assembly, Type baseType)
//        {
//            var container = IocContainer.Instance;
//            var _provider = container.Resolve<IDependencyProvider>();
//            var list = _provider.GetInternalInterfaces(assembly, baseType);
//            list.ForEach(m =>
//            {
//                if (m.LifeStyle == LifeTimeOption.Singleton)
//                {
//                    container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleSingleton().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));

//                }
//                else if (m.LifeStyle == LifeTimeOption.Transient)
//                {
//                    container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
//                }
//                else if (m.LifeStyle == LifeTimeOption.Scoped)
//                {
//                    if (ConfigurationManager.AppSettings["WebProject"] != null && !Convert.ToBoolean(ConfigurationManager.AppSettings["WebProject"]))
//                    {
//                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
//                    }
//                    else
//                    {
//                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestylePerWebRequest().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
//                    }
//                }
//            });
//        }

//        public static void Dispose()
//        {
//            var container = IocContainer.Instance;
//            container.Dispose();
//        }
//    }
//}
