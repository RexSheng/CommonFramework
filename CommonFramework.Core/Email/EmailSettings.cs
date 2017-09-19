using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Email
{
    public class EmailSettings
    {
        private string _host;
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        private int? _port;
        public int Port
        {
            get
            {
                return _port.HasValue ? _port.Value : 25;
            }
            set
            {
                _port = value;
            }
        }

        private string senderAddress;
        public string SenderAddress
        {
            get
            {
                return senderAddress;
            }
            set
            {
                senderAddress = value;
            }
        }

        private string emailSenderName;
        public string EmailSenderName
        {
            get
            {
                return emailSenderName;
            }
            set
            {
                emailSenderName = value;
            }
        }

        private string emailPwd;
        public string EmailPwd
        {
            get
            {
                return emailPwd;
            }
            set
            {
                emailPwd = value;
            }
        }
    }
}
