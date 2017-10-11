using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CommonFramework.Core.EntityFramework;
using CommonFramework.Core.Email;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Configure
{
    public partial class AppBuilder : IAppBuilder
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly IEmailConfiguration _emailConfiguration;
        public AppBuilder(
            IConnectionStringProvider connectionStringProvider,
            IEmailConfiguration emailConfiguration
            )
        {
            _connectionStringProvider = connectionStringProvider;
            _emailConfiguration = emailConfiguration;
        }

        public IConnectionStringProvider AddEfService(Action<IConnectionStringProvider> action=null)
        {
            if (action != null) {
                action.Invoke(_connectionStringProvider);
            }
            return _connectionStringProvider;
        }

        public IEmailConfiguration AddEmailService(Action<IEmailConfiguration> action = null)
        {
            if (action != null)
            {
                action.Invoke(_emailConfiguration);
            }
            return _emailConfiguration;
        }
         
    }
}
