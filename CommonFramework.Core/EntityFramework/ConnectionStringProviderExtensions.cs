using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.EntityFramework
{
    public static class ConnectionStringProviderExtensions
    {
        public static string GetWebConfigConnectionString(object connKey)
        {
            var result = ConfigurationManager.ConnectionStrings[connKey.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return connKey.ToString();
        }

        public static string GetAppConfigConnectionString(object connKey)
        {
            var result = ConfigurationManager.AppSettings[connKey.ToString()];
            if (result != null)
            {
                return result.ToString();
            }
            return connKey.ToString();
        }
    }
}
