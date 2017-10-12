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
    public interface ILogProvider
    {
        log4net.ILog GetLog(Type type = null);

        log4net.ILog GetLog(string name);
    }
}
