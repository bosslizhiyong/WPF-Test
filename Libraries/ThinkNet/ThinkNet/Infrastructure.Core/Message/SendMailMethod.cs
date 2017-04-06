using SendMail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ThinkNet.Infrastructure.Core.Message
{
    public class SendMailMethod : IMailHandler
    {
        #region 字段
        private MailMessage mail = new MailMessage();      //邮件对象
        private string MailFrom = "master@eascu.com";  //发件人erich.zhou@eascs.com
        private string MailServer = ConfigurationManager.AppSettings["MailServer"];//邮件服务器smtp.eternalasia.com
        private string DefaultAccount = "master";      //邮件管理员帐号erich.zhou
        private string DefaultPassword = "Abc123";         //邮件管理员密码A1B2c3d4 
        #endregion

        #region 属性
        public string PMailServer
        {
            get { return this.MailServer; }
            set { this.MailServer = value; }
        }
        public string PMailFrom
        {
            get { return this.MailFrom; }
            set { this.MailFrom = value; }
        }
        public string PDefaultAccount
        {
            get { return this.DefaultAccount; }
            set { this.DefaultAccount = value; }
        }
        public string PDefaultPassword
        {
            get { return this.DefaultPassword; }
            set { this.DefaultPassword = value; }
        } 
        #endregion

        #region 基本方法
        /// <summary>
        /// 发送邮件（发件人为邮件管理员）
        /// </summary>
        /// <param name="MailTo">收件人信箱（ea@sohu.com,ea@163.com,,,可以逗号隔开发送给多个信箱）</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">内容</param>
        /// <param name="strErr">错误信息</param>
        /// <returns>发送是否成功</returns>
        public SimpleResult SendEmailAdmin(string MailTo, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            mail.Subject = Subject;
            mail.Body = Body;

            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;
            mail.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送邮件（发件人自定义）
        /// </summary>
        /// <param name="MailFrom">发件人信箱</param>
        /// <param name="MailTo">收件人信箱（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">内容</param>
        /// <param name="strErr">错误信息</param>
        /// <returns>发送是否成功</returns>
        public SimpleResult SendEmailCustom(string MailFrom, string MailTo, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            mail.Subject = Subject;
            mail.Body = Body;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送邮件，可以抄送给其他人
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailTo"></param>
        /// <param name="CCTo">抄送给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="BCCTo">暗抄给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
        public SimpleResult SendEmailCopyTo(string MailFrom, string MailTo, string CCTo, string BCCTo, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            if (CCTo != string.Empty)
                mail.CC.Add(new MailAddress(CCTo));
            if (BCCTo != string.Empty)
                mail.Bcc.Add(new MailAddress(BCCTo));
            mail.Subject = Subject;
            mail.Body = Body;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送邮件，可以抄送、暗送给其他人
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailTo"></param>
        /// <param name="CCTo">抄送给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="BCCTo">暗抄给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
        public SimpleResult SendEmailCopyToAndDarkTo(string MailFrom, ArrayList MailTo, ArrayList CCTo, ArrayList BCCTo, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            if (MailTo.Count != 0)
            {
                for (int i = 0; i < MailTo.Count; i++)
                {
                    mail.To.Add(new MailAddress(MailTo[i].ToString()));
                }
            }
            if (CCTo.Count != 0)
            {
                for (int i = 0; i < CCTo.Count; i++)
                {
                    mail.CC.Add(new MailAddress(CCTo[i].ToString()));
                }
            }
            if (BCCTo.Count != 0)
            {
                for (int i = 0; i < BCCTo.Count; i++)
                {
                    mail.Bcc.Add(new MailAddress(BCCTo[i].ToString()));
                }
            }
            mail.Subject = Subject;
            mail.Body = Body;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送邮件，可以抄送、暗送给其他人
        /// </summary>
        /// <param name="MailTo"></param>
        /// <param name="CCTo">抄送给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="BCCTo">暗抄给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
        public SimpleResult SendEmailCopyToAndDarkTo(ArrayList MailTo, ArrayList CCTo, ArrayList BCCTo, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            if (MailTo.Count != 0)
            {
                for (int i = 0; i < MailTo.Count; i++)
                {
                    mail.To.Add(new MailAddress(MailTo[i].ToString()));
                }
            }
            if (CCTo.Count != 0)
            {
                for (int i = 0; i < CCTo.Count; i++)
                {
                    mail.CC.Add(new MailAddress(CCTo[i].ToString()));
                }
            }
            if (BCCTo.Count != 0)
            {
                for (int i = 0; i < BCCTo.Count; i++)
                {
                    mail.Bcc.Add(new MailAddress(BCCTo[i].ToString()));
                }
            }
            mail.Subject = Subject;
            mail.Body = Body;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送邮件，可以抄送、暗送给其他人
        /// </summary>
        /// <param name="MailTo"></param>
        /// <param name="SendType">1.普通2.抄送3.密送</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
        public SimpleResult SendEmailCopyToAndDarkTo(string MailTo, int SendType, string Subject, string Body)
        {
            mail.From = new MailAddress(MailFrom);
            if (SendType == 1)
            {
                mail.To.Add(new MailAddress(MailTo));
            }
            else if (SendType == 2)
            {
                mail.CC.Add(new MailAddress(MailTo));
            }
            else if (SendType == 3)
            {
                mail.Bcc.Add(new MailAddress(MailTo));
            }
            mail.Subject = Subject;
            mail.Body = Body;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailTo"></param>
        /// <param name="CCTo"></param>
        /// <param name="BCCTo"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="AttachUrls">附件路径列表</param>
        /// <returns></returns>
        public SimpleResult SendEmailAttachment(string MailTo, string CCTo, string BCCTo, string Subject, string Body, ArrayList AttachUrls)
        {
            mail.From = new MailAddress(MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            if (CCTo != string.Empty)
                mail.CC.Add(new MailAddress(CCTo));
            if (BCCTo != string.Empty)
                mail.Bcc.Add(new MailAddress(BCCTo));
            mail.Subject = Subject;
            mail.Body = Body;

            for (int i = 0; i < AttachUrls.Count; i++)
            {
                mail.Attachments.Add(new Attachment(AttachUrls[i].ToString()));
            }

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }
        }
        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailTo"></param>
        /// <param name="CCTo"></param>
        /// <param name="BCCTo"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="AttachUrls">附件路径列表</param>
        /// <returns></returns>
        public SimpleResult SendEmailAttachment(string MailFrom, string MailTo, string CCTo, string BCCTo, string Subject, string Body, ArrayList AttachUrls)
        {
            mail.From = new MailAddress(MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            if (CCTo != string.Empty)
                mail.CC.Add(new MailAddress(CCTo));
            if (BCCTo != string.Empty)
                mail.Bcc.Add(new MailAddress(BCCTo));
            mail.Subject = Subject;
            mail.Body = Body;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.Default;
            mail.SubjectEncoding = Encoding.Default;

            for (int i = 0; i < AttachUrls.Count; i++)
            {
                mail.Attachments.Add(new Attachment(AttachUrls[i].ToString()));
            }

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Credentials = new System.Net.NetworkCredential(DefaultAccount, DefaultPassword);
            try
            {
                smtp.Send(mail);
                return new SimpleResult(true, "发送成功");
            }
            catch
            {
                return new SimpleResult(false, "发送失败!");
            }

        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="MailTo">收件人信箱</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">内容</param>
        /// <returns></returns>
        public SimpleResult SendMail(string MailTo, string Subject, string Body)
        {
            MailSender mailsend = new MailSender();
            try
            {
                mailsend.Server = System.Configuration.ConfigurationManager.AppSettings["MailServer"];
                mailsend.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MailPort"]);
                mailsend.UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                mailsend.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                mailsend.From = System.Configuration.ConfigurationManager.AppSettings["From"];
                mailsend.FromName = System.Configuration.ConfigurationManager.AppSettings["FromName"];
                mailsend.Priority = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Priority"]);

                mailsend.To = MailTo;
                mailsend.Subject = Subject;
                mailsend.Body = Body;
                mailsend.Send();
                return new SimpleResult(true, "发送成功");
            }
            catch (Exception ex)
            {
                return new SimpleResult(false, ex.Message);
            }
        } 
        #endregion
    }
}
