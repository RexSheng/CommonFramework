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
            container.Register(d);
             
            var e = _resolver.GetInternalInterfaces(Assembly.GetExecutingAssembly(), typeof(IBaseDependency));
            container.Register(e);
        }
    }
}
