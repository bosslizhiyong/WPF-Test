using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 邮件处理类
    /// </summary>
    public class EmailHandler : IMessageHandler
    {
        #region 字    段
        private string _smtpServer = "";//发件服务器
        private string _sendEmailAddress = "";//发件邮箱地址
        private string _sendUserName = "";//发件用户名称
        private string _sendUserPassword = "";//发件用户密码
        private MailMessage _mMailMessage=null;
        private SmtpClient _mSmtpClient=null;
        #endregion

        #region 属    性
        
        #endregion

        #region 构造函数
        public EmailHandler()
        {
            _mMailMessage = new MailMessage();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smtpServer">发件服务器</param>
        /// <param name="sendEmailAddress">发件邮箱地址</param>
        /// <param name="sendUserName">发件用户名称</param>
        /// <param name="sendUserPassword">发件用户密码</param>
        /// <param name="emailTo">收件人邮箱地址</param>
        /// <param name="title">主题</param>
        /// <param name="content">内容</param>
        public EmailHandler(string smtpServer, string sendEmailAddress, string sendUserName, string sendUserPassword, string emailTo, string title, string content)
        {
            _mMailMessage = new MailMessage();

            this._smtpServer = smtpServer;
            this._sendEmailAddress = sendEmailAddress;
            this._sendUserName = sendUserName;
            this._sendUserPassword = sendUserPassword;

            SetEmailFrom(_sendEmailAddress);
            AddEmailTo(emailTo);
            SetSubject(title);
            SetBody(content.Replace("\r\n", "<br/>").Replace("\r", "<br/>"));
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public SimpleResult Send()
        {
            try
            {
                Connect();
                _mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                _mSmtpClient.Credentials = new System.Net.NetworkCredential(_sendUserName, _sendUserPassword);
                _mSmtpClient.Send(_mMailMessage);
                _mMailMessage.Dispose();
                return new SimpleResult(true, "发送成功!");
            }
            catch (Exception ex)
            {
                return new SimpleResult(false, ex.Message);
            }
        }
        #endregion

        #region 其他方法

        #region 连接发件服务器
        /// <summary>
        /// 连接发件服务器
        /// </summary>
        public void Connect()
        {
            _mSmtpClient = new SmtpClient(_smtpServer);
        }
        /// <summary>
        /// 连接发件服务器
        /// </summary>
        /// <param name="smtpServer">发件服务器</param>
        public void Connect(string smtpServer)
        {
            if (smtpServer.Trim() == "")
            { 
                smtpServer = _smtpServer; 
            }
            _mSmtpClient = new SmtpClient(smtpServer);
        }
        #endregion

        #region 设置发件人
        /// <summary>
        /// 设置发件人邮箱地址
        /// </summary>
        /// <param name="emailFrom">发件人邮箱地址</param>
        public void SetEmailFrom(string emailFrom)
        {
            _mMailMessage.From = new MailAddress(emailFrom);
        }
        #endregion

        #region 添加收件人
        /// <summary>
        /// 添加收件人邮箱地址
        /// </summary>
        /// <param name="emailTo">收件人邮箱地址</param>
        public void AddEmailTo(string emailTo)
        {
            if (!string.IsNullOrEmpty(emailTo))
            {
                _mMailMessage.To.Add(emailTo);
            }
        }
        /// <summary>
        /// 添加收件人邮箱地址
        /// </summary>
        /// <param name="arrEmailTo">收件人邮箱地址集合</param>
        public void AddEmailTo(string[] arrEmailTo)
        {
            for (int i = 0; i < arrEmailTo.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrEmailTo[i]))
                {
                    _mMailMessage.To.Add(arrEmailTo[i]);
                }
            }
        }
        #endregion

        #region 添加抄送人
        /// <summary>
        /// 添加抄送邮箱地址
        /// </summary>
        /// <param name="emailCc">抄送邮箱地址</param>
        public void AddEmailCC(string emailCc)
        {
            if (!string.IsNullOrEmpty(emailCc))
            {
                _mMailMessage.CC.Add(emailCc);
            }
        }
        /// <summary>
        /// 添加抄送邮箱地址
        /// </summary>
        /// <param name="arrEmailCc">抄送邮箱地址集合</param>
        public void AddEmailCC(string[] arrEmailCc)
        {
            for (int i = 0; i < arrEmailCc.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrEmailCc[i]))
                {
                    _mMailMessage.CC.Add(arrEmailCc[i]);
                }
            }
        }
        #endregion

        #region 添加密件抄送人
        /// <summary>
        /// 添加密件抄送邮箱地址
        /// </summary>
        /// <param name="emailBcc">密件抄送邮箱地址</param>
        public void AddEmailBcc(string emailBcc)
        {
            if (!string.IsNullOrEmpty(emailBcc))
            {
                _mMailMessage.Bcc.Add(emailBcc);
            }
        }
        /// <summary>
        /// 添加密件抄送邮箱地址
        /// </summary>
        /// <param name="arrEmailBcc">密件抄送邮箱地址集合</param>
        public void AddEmailBcc(string[] arrEmailBcc)
        {
            for (int i = 0; i < arrEmailBcc.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrEmailBcc[i]))
                {
                    _mMailMessage.Bcc.Add(arrEmailBcc[i]);
                }
            }
        }
        #endregion

        #region 添加附件
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="filePath">附件路径</param>
        public void AddAttachment(string filePath)
        {
            Attachment attachment = new Attachment(filePath);
            attachment.Name = Path.GetFileName(filePath);
            attachment.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
            attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
            _mMailMessage.Attachments.Add(attachment);
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="arrFilePath">附件路径集合</param>
        public void AddAttachment(string[] arrFilePath)
        {
            for (int i = 0; i < arrFilePath.Length; i++)
            {
                Attachment attachment = new Attachment(arrFilePath[i]);
                attachment.Name = Path.GetFileName(arrFilePath[i]);
                attachment.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                attachment.ContentDisposition.Inline = true;
                attachment.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                _mMailMessage.Attachments.Add(attachment);
            }
        }
        #endregion

        #region 设置邮件主题
        /// <summary>
        /// 设置邮件主题
        /// </summary>
        /// <param name="subject">邮件主题</param>
        public void SetSubject(string subject)
        {
            _mMailMessage.Subject = subject;
        }
        #endregion

        #region 设置邮件内容
        /// <summary>
        /// 设置邮件内容
        /// </summary>
        /// <param name="body">邮件内容</param>
        public void SetBody(string body)
        {
            _mMailMessage.IsBodyHtml = true;
            _mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mMailMessage.Body = body;
        }
        #endregion

        #endregion
    }
}
