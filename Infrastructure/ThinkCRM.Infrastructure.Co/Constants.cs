using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkCRM.Infrastructure.Co
{
    public class Constants
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public const string CONSTUSERINFO = "__UserInfo";

        /// <summary>
        /// 软件写注册表的项名称
        /// </summary>
        public const string CONSTSUBKEY = "SOFTWARE\\ThinkCRMCo\\";
        /// <summary>
        /// 软件注册键名称
        /// </summary>
        public const string CONSTREGISTERNAME = "RegisterCode";

        /// <summary>
        /// 开机启动注册表的项名称
        /// </summary>
        public const string CONSTSTARTSUBKEY = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        /// <summary>
        /// 服务器开机启动键名称
        /// </summary>
        public const string CONSTSERVERSTARTRUNNAME = "ThinkCRMCoServer";
        /// <summary>
        /// 客户端开机启动键名称
        /// </summary>
        public const string CONSTCLIENTSTARTRUNNAME = "ThinkCRMCoClient";
    }
}
