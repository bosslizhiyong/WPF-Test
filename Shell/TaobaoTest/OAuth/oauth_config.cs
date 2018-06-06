#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          oauth_config.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-06-06 10:01:45
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

namespace TaobaoTest
{
    /// <summary>
    /// OAuth基本信息
    /// </summary>
    [Serializable]
    public class oauth_config
    {
        public oauth_config() { }

        private string _oauth_name = string.Empty;
        private string _oauth_app_id = string.Empty;
        private string _oauth_app_key = string.Empty;
        private string _oauth_app_url = string.Empty;
        private string _return_uri = string.Empty;

        private string _code = string.Empty;
        private string _session = string.Empty;

        /// <summary>
        /// OAuth名称
        /// </summary>
        public string oauth_name
        {
            set { _oauth_name = value; }
            get { return _oauth_name; }
        }

        /// <summary>
        /// APP ID
        /// </summary>
        public string oauth_app_id
        {
            set { _oauth_app_id = value; }
            get { return _oauth_app_id; }
        }

        /// <summary>
        /// APP KEY
        /// </summary>
        public string oauth_app_key
        {
            set { _oauth_app_key = value; }
            get { return _oauth_app_key; }
        }

           /// <summary>
        /// APP KEY
        /// </summary>
        public string oauth_app_url
        {
            set { _oauth_app_url = value; }
            get { return _oauth_app_url; }
        }
        /// <summary>
        /// 回传的URL
        /// </summary>
        public string return_uri
        {
            set { _return_uri = value; }
            get { return _return_uri; }
        }

        /// <summary>
        /// 验证code
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }


        /// <summary>
        /// 令牌
        /// </summary>
        public string session
        {
            set { _session = value; }
            get { return _session; }
        }
    }
}
