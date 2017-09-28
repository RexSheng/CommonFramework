using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Dependency;
using System.Net.Mime;

namespace CommonFramework.Core.Email
{
    public interface IEmailSender:ITransientDependency
    {
        bool SendEmail(string To, string Name, string Subject, string Content, string AttachmentsPath = "", string MediaType = MediaTypeNames.Application.Octet, string senderKey = null);

        bool SendEmail(List<string[]> To, string Subject, string Content, string AttachmentsPath = "", string MediaType = MediaTypeNames.Application.Octet, string senderKey = null);

    }
}
