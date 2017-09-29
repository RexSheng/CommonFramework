using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;

namespace CommonFramework.CastleWindsor
{
    //public class Bootstrapper
    //{
        //private static IWindsorContainer container;
        //public static void Register(Action<IWindsorContainer> action)
        //{
        //    container = IocContainer.Instance;
        //    container.Install(new ServiceInstaller(), new ControllerInstaller());
        //    if (action != null) {
        //        action.Invoke(container);
        //    }
        //    //初始化一个IOC容器
        //    //完成IWindsorInstaller接口中的注册
        //    ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        //}

        //public static void Dispose()
        //{
        //    container.Dispose();
        //}
    //}

   

                
}
