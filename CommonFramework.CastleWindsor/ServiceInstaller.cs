using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CommonFramework.CastleWindsor
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            container.Register(Component.For(typeof(IDependencyProvider)).ImplementedBy(typeof(DependencyProvider)));
            var _resolver = container.Resolve<IDependencyProvider>();
            var d = _resolver.GetInternalInterfaces();
            d.ForEach(m =>
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
                    if (ConfigurationManager.AppSettings["WebProject"] != null && !Convert.ToBoolean(ConfigurationManager.AppSettings["WebProject"])) {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestyleTransient().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
                    }
                    else
                    {
                        container.Register(Component.For(m.InterfaceType).ImplementedBy(m.ImplementType).LifestylePerWebRequest().Named(m.InterfaceType.ToString() + "_" + m.ImplementType.ToString()));
                    }
                }
            });
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
        }
    }
}
