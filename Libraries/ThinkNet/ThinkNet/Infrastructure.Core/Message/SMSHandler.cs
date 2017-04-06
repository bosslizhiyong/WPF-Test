using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 短信处理类
    /// </summary>
    public class SMSHandler : IMessageHandler
    {
        #region 字    段
        private string _smsApiServer = "";//发信服务器(url接口)
        private string _sendSmsAccount = "";//发信账号名称
        private string _sendSmsPassword = "";//发信账号密码

        private string _mobile = "";
        private string _content = "";
        #endregion

        #region 属    性
        
        #endregion

        #region 构造函数
        public SMSHandler()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsApiServer">发信服务器(url接口)</param>
        /// <param name="sendSmsAccount">发信账号名称</param>
        /// <param name="sendSmsPassword">发信账号密码</param>
        /// <param name="mobileTo">收信人手机号码</param>
        /// <param name="content">内容</param>
        public SMSHandler(string smsApiServer, string sendSmsAccount, string sendSmsPassword, string mobileTo, string content)
        {
            this._smsApiServer = smsApiServer;
            this._sendSmsAccount = sendSmsAccount;
            this._sendSmsPassword = sendSmsPassword;

            _mobile = mobileTo;
            _content = content;
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
                //WebClient client = new WebClient();
                //string apiUrl = _smsApiServer + "?uid=" + _sendSmsAccount + "&pwd=" + _sendSmsPassword + "&mobile=" + _mobile + "&msg=" + System.Web.HttpUtility.UrlEncode(_content, System.Text.Encoding.GetEncoding("GB2312")) + "&dtime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //Stream data = client.OpenRead(apiUrl);
                //StreamReader reader = new StreamReader(data, Encoding.Default);
                //string result=reader.ReadToEnd();
                //return new SimpleResult(true, result);
                return new SimpleResult(true, "");
            }
            catch (Exception ex)
            {
                return new SimpleResult(false, ex.Message);
            }
        }
        #endregion

        #region 其他方法

        #endregion
    }
}
