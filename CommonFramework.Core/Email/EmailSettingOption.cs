using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Email
{
    public class EmailSettingOption : IEmailSettingOption
    {
        private EmailSettings setting=new EmailSettings();
        public IEmailSettingOption setKey(string key)
        {
            setting.SettingKey = key;
            return this;
        }
        public IEmailSettingOption isDefault() {
            setting.IsDefault = true;
            return this;
        }
        public IEmailSettingOption setHost(string host)
        {
            setting.Host = host;
            return this;
        }

        public IEmailSettingOption setPort(int port)
        {
            setting.Port = port;
            return this;
        }

        public IEmailSettingOption setSenderAddress(string senderAddress)
        {
            setting.SenderAddress = senderAddress;
            return this;
        }
        public IEmailSettingOption setEmailSenderName(string emailSenderName)
        {
            setting.EmailSenderName = emailSenderName;
            return this;
        }
        public IEmailSettingOption setEmailPwd(string emailPwd)
        {
            setting.EmailPwd = emailPwd;
            return this;
        }
         

        public EmailSettings getSetting()
        {
            return new EmailSettings() {
                SettingKey=setting.SettingKey,
                Host=setting.Host,
                Port=setting.Port,
                EmailPwd=setting.EmailPwd,
                EmailSenderName=setting.EmailSenderName,
                IsDefault=setting.IsDefault,
                SenderAddress=setting.SenderAddress
            };
        }

        public void clearSetting()
        {
            setting = new EmailSettings();
        }
    }

   
}
