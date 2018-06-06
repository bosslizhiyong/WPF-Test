#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          oauth_helper.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-06-06 10:03:00
 *
 * 公  司（Copyright）：          www.Leapline.com.cn
 * ----------------------------------------------------------------
 * 描  述（Description）：
 * 
 * ----------------------------------------------------------------
 * 修改记录（Revision History）
 *      R1 -
 *         修改人（Editor）：
 *         修改日期（Date）：
 *         修改原因（Reason）：
 *----------------------------------------------------------------*/
#endregion Copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Taobao.Common;

namespace TaobaoTest
{
    public class oauth_helper
    {
        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        /// <param name="oauth_name"></param>
        public static oauth_config get_config(string oauth_name)
        {
            ////读取接口配置信息
            //Model.user_oauth_app model = new BLL.user_oauth_app().GetModel(oauth_name);
            //if (model != null)
            //{
            //    //读取站点配置信息
            //    Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            //    //赋值
            //    oauth_config config = new oauth_config();
            //    config.oauth_name = model.api_path.Trim();
            //    config.oauth_app_id = model.app_id.Trim();
            //    config.oauth_app_key = model.app_key.Trim();
            //    config.return_uri = HttpContext.Current.Request.Url.Authority.ToLower() + siteConfig.webpath + "api/oauth/" + model.api_path + "/return_url.aspx";
            //    return config;
            //}
            //return null;

            //配置文件
            if (oauth_name == "taobao")
            {
                string fullPath = Utils.GetMapPath("./xmlconfig/configtaobao.config");

         //   file:///E:/WFC-Test/WPF-Test/WPF-Test/Shell/TaobaoTest/bin/Debug/xmlconfig/configtaobao.xml
                XmlDocument doc = new XmlDocument();
                doc.Load(fullPath);
                XmlNode _appUrl = doc.SelectSingleNode(@"Root/appUrl");
                XmlNode _appKey = doc.SelectSingleNode(@"Root/appKey");
                XmlNode _appSecret = doc.SelectSingleNode(@"Root/appSecret");
                XmlNode _return_url = doc.SelectSingleNode(@"Root/return_url");
                
                XmlNode _appcode = doc.SelectSingleNode(@"Root/code");
                XmlNode _appsession = doc.SelectSingleNode(@"Root/session");
                    

                //赋值
                oauth_config config = new oauth_config();
                config.oauth_name = oauth_name;
                config.oauth_app_id = _appKey.InnerText;
                config.oauth_app_key = _appSecret.InnerText;
                config.oauth_app_url = _appUrl.InnerText;
                config.return_uri = _return_url.InnerText;
                return config;
            }
            return null;
        }
    }
}
