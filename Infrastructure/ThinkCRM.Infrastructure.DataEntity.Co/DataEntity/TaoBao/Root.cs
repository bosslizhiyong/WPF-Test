#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          Class1.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-05-14 15:50:35
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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    [DataContract]
    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string w1_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string refresh_token_valid_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string taobao_user_nick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string re_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string expire_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string open_uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string token_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string taobao_open_uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string w1_valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string refresh_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string w2_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string w2_valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string r1_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string r2_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string r2_valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string r1_valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string expires_in { get; set; }
    }
}
