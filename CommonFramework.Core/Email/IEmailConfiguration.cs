using System;
using System.Linq.Expressions;
using CommonFramework.Core.Dependency;

namespace CommonFramework.Core.Email
{
    /// <summary>
    /// 邮件配置接口
    /// </summary>
    public interface IEmailConfiguration:ITransientDependency
    {
        /// <summary>
        /// 配置邮件
        /// </summary>
        /// <param name="option"></param>
        IEmailConfiguration Config(Expression<Action<IEmailSettingOption>> option);

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        EmailSettings GetSettingOption(string key=null);
    }


}
