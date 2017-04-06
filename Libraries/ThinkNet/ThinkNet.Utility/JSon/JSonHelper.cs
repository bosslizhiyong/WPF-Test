using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class JSonHelper
    {
        /// <summary>
        /// 把Json字符串形式转换为对象T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonToType<T>(string jsonString)
        {
            if (String.IsNullOrEmpty(jsonString)) return default(T);

            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 把datatable转换成json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string TableToJson(DataTable dt)
        {
            String postData = "[";

            foreach (DataRow row in dt.Rows)
            {
                postData += "{";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string name = dt.Columns[i].ColumnName;
                    string value = row[dt.Columns[i].ColumnName].ToString();
                    postData += String.Format("\"{0}\":\"{1}\",", name, value);
                }
                postData = postData.Substring(0, postData.Length - 1);
                postData += "},";
            }

            postData = postData.Substring(0, postData.Length - 1);
            postData += "]";
            return postData;
        }

        // <summary>   
        /// 根据Json返回DateTable,JSON数据格式如:   
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}   
        /// </summary>   
        /// <param name="strJson">Json字符串</param>   
        /// <returns></returns>   
        public static DataTable JsonToTable(string strJson)
        {
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value.Replace("\"", "");
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0].Replace("\"", "");
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("|", ",").Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        /// <summary>
        /// 把对象转换为JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new DateTimeConverter(), new EnumTypeConverter());
        }
        /// <summary>
        /// 把JSON数据转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        #region easyui-datagrid json
        /// <summary>
        /// 把列表对象转换为easyui-datagrid所需要的Json数据源
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static string ToJson4EasyuiGrid(IEnumerable objects, int totalCount)
        {
            return string.Format(@"{{""total"":{0},""rows"":{1}}}", totalCount, Serialize(objects));
        }
        /// <summary>
        /// 把列表对象转换为easyui-datagrid所需要的Json数据源
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static string ToJson4EasyuiGrid(DataTable objects, int totalCount)
        {
            return string.Format(@"{{""total"":{0},""rows"":{1}}}", totalCount, TableToJson(objects));
        }
        /// <summary>
        /// 把对象转换为Json字符串形式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return Serialize(obj);
        }
        #endregion
    }
}
