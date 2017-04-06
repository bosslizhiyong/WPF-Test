using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebServiceUrlConfig
    {
        /// <summary>
        /// 获取WebService的Url地址
        /// </summary>
        /// <param name="xmlFileName">WebService配置文件</param>
        /// <param name="webServiceUrl">WebServiceUrl名称</param>
        /// <returns></returns>
        public static string GetWebServiceUrl(string xmlFileName, string webServiceUrl)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            string xpath = @"configuration/WebServiceUrlConfig";
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode item in nodeList)
                {
                    if (item.Name == webServiceUrl)
                    {
                        return item.InnerText;
                    }
                }//end foreach (XmlNode item in nodeList)
            }//end if (nodeList != null && nodeList.Count > 0)
            return "";
        }
    }
}
