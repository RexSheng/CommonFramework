using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Extension
{
    public class EmailSender<T> where T:new()
    {
        private static string _emailHost, _emailSenderAddress, _emailSenderName, _emailPwd;
        private static int _emailPort;
        private static MailMessage mail;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailHost"></param>
        /// <param name="emailUser">发件人邮箱地址</param>
        /// <param name="emailName">发件人名称</param>
        /// <param name="emailPwd">客户端授权码</param>
        /// <param name="emailPort"></param>
        public EmailSender(string emailSenderAddress, string emailSenderName, string emailPwd, string emailHost = "smtp.exmail.qq.com", int emailPort = 25)
        {
            mail = new MailMessage();

            _emailHost = emailHost;
            _emailSenderAddress = emailSenderAddress;
            _emailSenderName = emailSenderName;
            _emailPwd = emailPwd;
            _emailPort = emailPort;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public static bool SendEmail(string To, string Name, string Subject, string Content,string AttachmentsPath = "", string MediaType = MediaTypeNames.Application.Octet)
        {
            T t = new T();
            SmtpClient client = new SmtpClient();

            //获取或设置用于验证发件人身份的凭据。
            client.Credentials = new System.Net.NetworkCredential(_emailSenderName, _emailPwd);
            client.Port = _emailPort; //端口号
            //获取或设置用于 SMTP 事务的主机的名称或 IP 地址
            client.Host = _emailHost;
            try
            {
                if (!string.IsNullOrEmpty(AttachmentsPath))
                {
                    AddAttachments(AttachmentsPath, MediaType);
                }
                client.Send(InitMailMessage(To, Name, Subject, Content));
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
            }
        }
        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public static void AddAttachments(string attachmentsPath,string mediaType)
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
        private static MailMessage InitMailMessage(string To, string name, string Subject, string sendEmailContent)
        {
            
            //发件人
            mail.From = new MailAddress(_emailSenderAddress, _emailSenderName);

            //收件人
            if (name != "")
            {
                MailAddress mailAdd = new MailAddress(To, name);
                mail.To.Add(mailAdd);
            }
            else
            {
                mail.To.Add(To);
            }
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
