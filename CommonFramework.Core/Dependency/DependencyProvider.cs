﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;
using System.Linq.Expressions;
using CommonFramework.Core.Log;
using CommonFramework.Core.Localization;

namespace CommonFramework.Core.Dependency
{
    public class DependencyProvider : IDependencyProvider, ITransientDependency
    {
        public List<InternalAssemblyInfo> GetInternalInterfaces(Assembly assembly, Type interfaceType)
        {
            List<InternalAssemblyInfo> list = new List<InternalAssemblyInfo>();

            var allTypes = assembly.GetTypes();
            //foreach (var t in allTypes.Where(m => m.IsClass && m.GetInterfaces().Contains(interfaceType)))
            //{
            //    foreach (var cla in t.GetInterfaces()) {

            //            list.Add(new InternalAssemblyInfo()
            //            {
            //                Assembly = assembly,
            //                InterfaceType = cla,
            //                ImplementType = t,
            //                LifeStyle = getLifeTime(t.GetInterfaces())
            //            });

            //    }

            //}
            foreach (var t in allTypes.Where(m => m.IsInterface && m.GetInterfaces().Contains(interfaceType)))
            {
                //var aaaa = allTypes.Where(m => m.IsClass).Select(m => m.GetInterfaces()).ToArray();
                var temps = allTypes.Where(m => m.IsClass && m.GetInterfaces().Any(n => n.Name == t.Name && n.Namespace == t.Namespace)).ToArray();
                foreach (var cla in temps)
                {
                    list.Add(new InternalAssemblyInfo()
                    {
                        Assembly = assembly,
                        InterfaceType = t,
                        ImplementType = cla,
                        LifeStyle = getLifeTime(temps)
                    });
                }
            }
            return list;

        }
       
        private LifeTimeOption getLifeTime(Type[] types)
        {
            if (types.Contains(typeof(ITransientDependency)))
            {
                return LifeTimeOption.Transient;
            }
            else if (types.Contains(typeof(ISingletonDependency)))
            {
                return LifeTimeOption.Singleton;
            }
            else
            {
                return LifeTimeOption.Scoped;
            }
        }
    }

    public class InternalAssemblyInfo
    {
        public Assembly Assembly { get; set; }

        public Type InterfaceType
        {
            get; set;
        }

        public Type ImplementType
        {
            get; set;
        }

        public LifeTimeOption LifeStyle
        {
            get; set;
        }
    }

    public enum LifeTimeOption
    {
        Transient = 0,
        Singleton = 1,
        Scoped = 2
    }
}
