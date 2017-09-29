using CommonFramework.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.CastleWindsor
{
    public interface ICastleProvider:IDependencyProvider,ITransientDependency
    {
        void Register(Assembly assembly,Type baseType);
    }
}
