using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using CommonFramework.Core.Dependency;

namespace CommonFramework.Core.Email
{
    public interface IEmailConfiguration:ITransientDependency
    {
        /// <summary>
        /// 配置邮件
        /// </summary>
        /// <param name="option"></param>
        void Config(Expression<Action<IEmailSettingOption>> option);

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        EmailSettings GetSettingOption();
    }


}
