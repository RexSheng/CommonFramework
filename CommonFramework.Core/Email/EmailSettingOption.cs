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
            return setting;
        }
    }

   
}
