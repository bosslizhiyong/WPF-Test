#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          Class1.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-05-14 15:51:43
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
    public class Top_auth_token_create_response
    {
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string token_result { get; set; }
        /// <summary>
        /// 
        /// </summary>
      [DataMember]
        public string request_id { get; set; }
    }
     [DataContract]
    public class TaobaoRoot
    {
        /// <summary>
        /// 
        /// </summary>
         [DataMember]
        public Top_auth_token_create_response top_auth_token_create_response { get; set; }
    }
}
