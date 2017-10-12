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
    public interface ILog
    {
        void Debug(object message, Exception ex = null, Type type = null, string name = null);


        void Info(object message, Exception ex = null, Type type = null, string name = null);


        void Warn(object message, Exception ex = null, Type type = null, string name = null);


        void Error(object message, Exception ex = null, Type type = null, string name = null);


        void Fatal(object message, Exception ex = null, Type type = null, string name = null);
          
         
    }
}
