using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Log
{
    public class LogConfiguration:ILogConfiguration
    {
        public void Configure(string filePathAndName=null,bool watch=true) {
            if (string.IsNullOrEmpty(filePathAndName))
            {
                XmlConfigurator.Configure();
            }
            else {
                if (watch)
                {
                    XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(filePathAndName));
                }
                else
                {
                    XmlConfigurator.Configure(new System.IO.FileInfo(filePathAndName));
                }
            }
            
        }
    }
}
