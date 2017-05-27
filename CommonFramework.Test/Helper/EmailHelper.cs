using CommonFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Test.Helper
{
    public class EmailHelper:EmailSender<EmailHelper>
    {
        public EmailHelper()
            : base("sxp@126.com", "sxp", "sxp", "smtp.126.com", 25)
        { 
        
        }
    }
}
