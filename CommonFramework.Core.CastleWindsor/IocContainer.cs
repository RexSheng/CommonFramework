using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.CastleWindsor
{
    public class IocContainer
    {
        private static readonly object syncRoot = new object();
        private IocContainer() { }
        private static IWindsorContainer _Instance;

        public static IWindsorContainer Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (_Instance == null)
                    {
                        _Instance = new WindsorContainer();
                    }
                    return _Instance;
                }
            }
        }
    }

}
