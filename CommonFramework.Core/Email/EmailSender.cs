using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Email
{
    public class EmailSender : IEmailSender
    {
        private MailMessage mail;
        private readonly IEmailConfiguration _configuration;
        public EmailSender(IEmailConfiguration configuration)
        {
            _configuration = configuration;
            mail = new MailMessage();
        }
        private EmailSettings getSettings()
        {
            return _configuration.GetSettingOption();
        }
        public bool SendEmail(string To, string Name, string Subject, string Content, string AttachmentsPath = "", string MediaType = MediaTypeNames.Application.Octet)
        {
            return SendEmail(new List<string[]>() { new string[] { To, Name } }, Subject, Content, AttachmentsPath, MediaType);

        }

        public bool SendEmail(List<string[]> To, string Subject, string Content, string AttachmentsPath = "", string MediaType = MediaTypeNames.Application.Octet)
        {
            SmtpClient client = new SmtpClient();
            var setting = getSettings();
            //获取或设置用于验证发件人身份的凭据。
            if (!string.IsNullOrEmpty(setting.EmailSenderName))
            {
                client.Credentials = new System.Net.NetworkCredential(setting.EmailSenderName, setting.EmailPwd);
            }

            client.Port = setting.Port; //端口号
            //获取或设置用于 SMTP 事务的主机的名称或 IP 地址
            client.Host = setting.Host;

            if (!string.IsNullOrEmpty(AttachmentsPath))
            {
                AddAttachments(AttachmentsPath, MediaType);
            }
            client.Send(InitMailMessage(To, Subject, Content, setting));
            return true;

        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        private void AddAttachments(string attachmentsPath, string mediaType)
        {

            string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], mediaType);
                disposition = data.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(path[i]);
                disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                disposition.ReadDate = File.GetLastAccessTime(path[i]);
                mail.Attachments.Add(data);
            }

        }

        /// <summary>
        /// 初始化信件相关信息
        /// </summary>
        /// <param name="reEmailPath">发件人</param>
        /// <param name="reEmailName">发件人姓名</param>
        /// <param name="sendEmailTitle">发送标题</param>
        /// <param name="sendEmailContent">发送内容</param>
        /// <returns></returns>
        private MailMessage InitMailMessage(string To, string Name, string Subject, string sendEmailContent, EmailSettings setting)
        {

            return InitMailMessage(new List<string[]>() { new string[]{ To, Name } }, Subject, sendEmailContent, setting);

        }

        private MailMessage InitMailMessage(List<string[]> To, string Subject, string sendEmailContent, EmailSettings setting)
        {

            //发件人
            mail.From = new MailAddress(setting.SenderAddress, setting.EmailSenderName);

            //收件人
            To.ForEach(dic =>
            {
                if (dic.Length > 1 && !string.IsNullOrEmpty(dic[1]))
                {
                    MailAddress mailAdd = new MailAddress(dic[0], dic[1]);
                    mail.To.Add(mailAdd);
                }
                else
                {
                    mail.To.Add(dic[0]);
                }
            });
            //主题
            mail.Subject = Subject;

            //内容
            mail.Body = sendEmailContent;

            //邮件主题和正文的编码格式
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //邮件正文允许html编码
            mail.IsBodyHtml = true;
            //优先级
            mail.Priority = MailPriority.Normal;

            //密送——就是将信密秘抄送给收件人以外的人，所有收件人看不到密件抄送的地址
            //mail.Bcc.Add("");


            //抄送——就是将信抄送给收件人以外的人,所有的收件人可以在抄送地址处看到此信还抄送给谁
            //mail.CC.Add("");

            //mail.Attachments.Add(new Attachment("D:\\1.doc"));     //添加附件

            return mail;

        }

    }
}
