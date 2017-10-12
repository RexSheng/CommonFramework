using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Log
{
    public class LogProvider : ILogProvider
    {
        public log4net.ILog GetLog(Type type = null)
        {
            if (type != null)
            {
                return log4net.LogManager.GetLogger(type);
            }
            else
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        public log4net.ILog GetLog(string name)
        {
            return log4net.LogManager.GetLogger(name);
        }
    }
}
