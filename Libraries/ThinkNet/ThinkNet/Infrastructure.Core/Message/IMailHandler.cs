using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core.Message
{
    public  interface IMailHandler
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        SimpleResult SendMail(string MailTo, string Subject, string Body);
        /// <summary>
        /// 发送邮件（发件人为邮件管理员）
        /// </summary>
        /// <param name="MailTo">收件人信箱（ea@sohu.com,ea@163.com,,,可以逗号隔开发送给多个信箱）</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">内容</param>
        /// <param name="strErr">错误信息</param>
        /// <returns>发送是否成功</returns>
        SimpleResult SendEmailAdmin(string MailTo, string Subject, string Body);
         /// <summary>
        /// 发送邮件（发件人自定义）
        /// </summary>
        /// <param name="MailFrom">发件人信箱</param>
        /// <param name="MailTo">收件人信箱（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">内容</param>
        /// <param name="strErr">错误信息</param>
        /// <returns>发送是否成功</returns>
         SimpleResult SendEmailCustom(string MailFrom, string MailTo, string Subject, string Body);
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
         SimpleResult SendEmailCopyTo(string MailFrom, string MailTo, string CCTo, string BCCTo, string Subject, string Body);
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
         SimpleResult SendEmailCopyToAndDarkTo(string MailFrom, ArrayList MailTo, ArrayList CCTo, ArrayList BCCTo, string Subject, string Body);
        /// <summary>
        /// 发送邮件，可以抄送、暗送给其他人
        /// </summary>
        /// <param name="MailTo"></param>
        /// <param name="CCTo">抄送给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="BCCTo">暗抄给谁（ea@21cn.com,ea@sohu.com,,,）</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
         SimpleResult SendEmailCopyToAndDarkTo(ArrayList MailTo, ArrayList CCTo, ArrayList BCCTo, string Subject, string Body);
        /// <summary>
        /// 发送邮件，可以抄送、暗送给其他人
        /// </summary>
        /// <param name="MailTo"></param>
        /// <param name="SendType">1.普通2.抄送3.密送</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
         SimpleResult SendEmailCopyToAndDarkTo(string MailTo, int SendType, string Subject, string Body);
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
         SimpleResult SendEmailAttachment(string MailTo, string CCTo, string BCCTo, string Subject, string Body, ArrayList AttachUrls);
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
         SimpleResult SendEmailAttachment(string MailFrom, string MailTo, string CCTo, string BCCTo, string Subject, string Body, ArrayList AttachUrls);
    }
}
