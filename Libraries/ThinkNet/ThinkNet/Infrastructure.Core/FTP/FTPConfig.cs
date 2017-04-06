using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ThinkNet.Utility;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 获取FTP配置文件的内容
    /// </summary>
    public class FTPConfig
    {
        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        public string RemoteHost { get; private set; }
        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public string RemotePort { get; private set; }
        /// <summary>
        /// 登录用户账号
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 获取FTP配置文件的内容
        /// </summary>
        /// <param name="xmlFileName"></param>
        public FTPConfig(string xmlFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            string xpath = @"configuration/FtpConfig";
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode item in nodeList)
                {
                    switch(item.Name)
                    {
                        case "RemoteHost":
                            this.RemoteHost = item.InnerText;
                            break;
                        case "RemotePort":
                            this.RemotePort = item.InnerText;
                            break;
                        case "UserName":
                            this.UserName = item.InnerText;
                            break;
                        case "Password":
                            string pwd = CryptoFactory.Create(CrytoType.TripleDES).Decrypt(item.InnerText);//三重加密算法--解密
                            this.Password = pwd;
                            break;
                    }
                }//end foreach (XmlNode item in nodeList)
            }//end if (nodeList != null && nodeList.Count > 0)
        }
    }
}
