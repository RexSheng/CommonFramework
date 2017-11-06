using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using CommonFramework.Core.Dependency;
using System.Reflection;
using System.Configuration;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Unity
{
    public class IocContainer
    {
         
        public static IUnityContainer Instance
        {
            get
            {
                return CommonFrameworkBuilder._container;
            }
        }
    }
}
