using System.Collections.Generic;
using System.ServiceModel.Activation;
using WCFWeb.Co.Contract;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Utility;
using System.Data;
using System.Data.OleDb;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkCRM.Commands.Co;
using ThinkNet.Query.Core;
using System.Collections;
using System;
using System.Xml.Linq;
using System.Xml;

namespace WCFWeb.Co
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthorityService : ServiceBase, IAuthorityService
    {

        //接受Code
        public string GetCallbackCode(string code, string state)
        {
            string xmlPath = "./Configxml/ConfigTaobao.xml";
            //string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConfigTaobao.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNode xn = doc.SelectSingleNode("//code");
            string transDate = xn.InnerText;

            //修改
            xn.InnerText = code;
            doc.Save(xmlPath);
            return code;
        }


        public string GetPostTest(string code)
        {
            // http://127.0.0.1:1608/AuthorityService/client/GetPostTest
            return code;
        }
    }
}