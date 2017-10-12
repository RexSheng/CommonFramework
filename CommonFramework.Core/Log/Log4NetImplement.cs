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
    public class Log4NetImplement: ILog
    {
        private readonly ILogProvider _logProvider;
        public Log4NetImplement(ILogProvider logProvider) {
            _logProvider = logProvider;
        }


        private log4net.ILog getLogger(Type type=null, string name=null) {
            if (!string.IsNullOrEmpty(name)) {
                return _logProvider.GetLog(name);
            }
            else
            {
                return _logProvider.GetLog(type);
            }
            
        }
          
        public void Debug(object message, Exception ex = null,Type type=null,string name=null)
        {
            var _logger = getLogger(type, name);
            if (_logger.IsDebugEnabled)
            {
                if (ex == null)
                {
                    _logger.Debug(message);
                }
                else
                {
                    _logger.Debug(message, ex);
                }
            }
        }

        public void Info(object message, Exception ex = null, Type type = null, string name = null)
        {
            var _logger = getLogger(type, name);
            if (_logger.IsInfoEnabled)
            {
                if (ex == null)
                {
                    _logger.Info(message);
                }
                else
                {
                    _logger.Info(message, ex);
                }
            }
        }

        public void Warn(object message, Exception ex = null, Type type = null, string name = null)
        {
            var _logger = getLogger(type, name);
            if (_logger.IsWarnEnabled)
            {
                if (ex == null)
                {
                    _logger.Warn(message);
                }
                else
                {
                    _logger.Warn(message, ex);
                }
            }
        }

        public void Error(object message, Exception ex = null, Type type = null, string name = null)
        {
            var _logger = getLogger(type, name);
            if (_logger.IsErrorEnabled)
            {
                if (ex == null)
                {
                    _logger.Error(message);
                }
                else
                {
                    _logger.Error(message, ex);
                }
            }
        }

        public void Fatal(object message, Exception ex = null, Type type = null, string name = null)
        {
            var _logger = getLogger(type, name);
            if (_logger.IsFatalEnabled)
            {
                if (ex == null)
                {
                    _logger.Fatal(message);
                }
                else
                {
                    _logger.Fatal(message, ex);
                }
            }
        }
         
    }
}
