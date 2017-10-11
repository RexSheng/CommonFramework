using CommonFramework.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Configure
{
    public static class AppServiceBuilderExtensions
    {
        public static IAppBuilder AddEfService(this IAppBuilder app, Action<IConnectionStringProvider> action = null) {
            return app;
        }

         
    }
}
