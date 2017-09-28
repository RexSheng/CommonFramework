using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Dependency;

namespace CommonFramework.Core.Email
{
    public interface IEmailSettingOption:ITransientDependency
    {
        /// <summary>
        /// 设置key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEmailSettingOption setKey(string key);

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        IEmailSettingOption isDefault();
        /// <summary>
        /// 邮件服务器地址，例如smtp.exmail.qq.com，smtp.126.com
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        IEmailSettingOption setHost(string host);

        /// <summary>
        /// 端口号，默认25
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        IEmailSettingOption setPort(int port);

        /// <summary>
        /// 设置发送人的邮箱地址
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        IEmailSettingOption setSenderAddress(string senderAddress);

        /// <summary>
        /// 设置发送人的邮件名称
        /// </summary>
        /// <param name="emailSenderName"></param>
        /// <returns></returns>
        IEmailSettingOption setEmailSenderName(string emailSenderName);

        /// <summary>
        /// 设置邮箱密码
        /// </summary>
        /// <param name="emailPwd"></param>
        /// <returns></returns>
        IEmailSettingOption setEmailPwd(string emailPwd);

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        EmailSettings getSetting();

        void clearSetting();
    }
}
