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
using System.Reflection;
using Top.Api.Util;
using System.Web.Script.Serialization;



namespace WCFWeb.Co
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthorityService : ServiceBase, IAuthorityService
    {

        //接受Code
        public string GetCallbackCode(string code, string state)
        {
            string xmlPath = "./xmlconfig/configtaobao.xml";
            //string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConfigTaobao.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNode xn = doc.SelectSingleNode("//code");
            string transDate = xn.InnerText;
            //修改code
            xn.InnerText = code;
            doc.Save(xmlPath);

            ////连接客户端Sockt ////通知客户端
            #region 方式1
            string appkey = "24880752";
            string secret = "86cd516f2c4c1cb0835a833d44f76a4a";
            string callbackUrl = "http://127.0.0.1:1608/AuthorityService/client/GetCallbackCode";

            WebUtils webUtils = new WebUtils();
            IDictionary<string, string> pout = new Dictionary<string, string>();
            pout.Add("grant_type", "authorization_code");
            pout.Add("client_id", appkey);
            pout.Add("client_secret", secret);
            pout.Add("code", code);
            pout.Add("redirect_uri", callbackUrl);
            string output = webUtils.DoPost("https://oauth.taobao.com/token", pout);
            Console.Write(output);
            string sessionKey = "";
            if (!string.IsNullOrEmpty(output.ToString()))
            {
                JavaScriptSerializer Serializers = new JavaScriptSerializer();
                Root objroot = Serializers.Deserialize<Root>(output.ToString());

                xmlPath = "./xmlconfig/configtaobao.xml";
                doc = new XmlDocument();
                doc.Load(xmlPath);
                xn = doc.SelectSingleNode("//session");
                xn.InnerText = objroot.refresh_token;
                 sessionKey = objroot.refresh_token;
                doc.Save(xmlPath);
            }

            APISeedSessionKey(sessionKey);
            #endregion


            return "验证成功请打开客户端";
        }


        public string GetPostTest(string code)
        {
            // http://127.0.0.1:1608/AuthorityService/client/GetPostTest
            return code;
        }

        public bool APISeedSessionKey(string sessionKey)
        {
            try
            {
                Type type = Assembly.Load("TaobaoTest").GetType("WCFWeb.Co.ApiHost.ApiHost");
                MethodInfo method = type.GetMethod("SeedSessionKey");
                object obj = Activator.CreateInstance(type);
                object[] parameters = new object[] { sessionKey };
                method.Invoke(obj, parameters);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return true;
        }
    }
}