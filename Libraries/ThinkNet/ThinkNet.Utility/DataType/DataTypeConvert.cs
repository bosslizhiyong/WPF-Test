using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 日期显示格式
    /// </summary>
    public enum DTFormate
    {
        /// <summary>
        /// 格式：2012-01-01
        /// </summary>
        SHORT_EN_US,
        /// <summary>
        /// //格式：2012年01月01日
        /// </summary>
        SHORT_ZH_CN,
        /// <summary>
        /// //格式：2012-01-01 12：34：23
        /// </summary>
        LONG_EN_US,
        /// <summary>
        /// //格式：2012年01月01日12时34分23秒
        /// </summary>
        LONG_ZH_CN
    }

    public class DataTypeConvert
    {
        #region 将object值转换为整型值
        /// <summary>
        /// 将object值转换为16位的整型值，失败则返回0
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static short ToInt16(object value)
        {
            return ToInt16(value, 0);
        }
        /// <summary>
        /// 将object值转换为16位的整型值，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static short ToInt16(object value, short defaultValue)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToInt16(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将object值转换为32位的整型值，失败则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(object value)
        {
            return ToInt32(value, 0);
        }
        /// <summary>
        /// 将object值转换为32位的整型值，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(object value, int defaultValue)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将object值转换为64位的整型值，失败则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToInt64(object value)
        {
            return ToInt64(value, 0);
        }
        /// <summary>
        /// 将object值转换为64位的整型值，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long ToInt64(object value, long defaultValue)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToInt64(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region 将object值转换为浮点值
        /// <summary>
        /// 将object值转换成double类型，失败则返回0
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }
        /// <summary>
        /// 将object值转换成double类型，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defautValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(object value, Double defautValue)
        {
            Double r = 0;

            if (value == null) return r;

            if (Double.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }

        /// <summary>
        /// 将object值转换成decimal类型，失败则返回0
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }
        /// <summary>
        /// 将object值转换成decimal类型，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defautValue">默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(object value, Decimal defautValue)
        {
            decimal r = 0;

            if (value == null) return r;

            if (decimal.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        #endregion

        #region 将object值转换为布尔值
        /// <summary>
        /// 将object值转换成Boolean类型，失败则返回false
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static bool ToBoolean(object value)
        {
            return ToBoolean(value, false);
        }
        /// <summary>
        /// 将object值转换成Boolean类型，失败则返回false
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defautValue">默认值</param>
        /// <returns></returns>
        public static bool ToBoolean(object value, Boolean defautValue)
        {
            Boolean r = false;

            if (value == null) return r;

            if (Boolean.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        #endregion

        #region 将object值转换为时间值
        /// <summary>
        /// 将object值转换成DateTime类型，失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value)
        {
            DateTime r = DateTime.MinValue;

            if (value == null) return r;

            if (DateTime.TryParse(value.ToString(), out r)) return r;

            return r;
        }
        /// <summary>
        /// 将object值转换成DateTime类型，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defautValue">默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value, DateTime defautValue)
        {
            DateTime r = defautValue;

            if (value == null) return r;

            if (DateTime.TryParse(value.ToString(), out r)) return r;

            return r;
        }
        /// <summary>
        /// 将object值转换成DateTime类型，失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <param name="dtFormate">日期显示格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value, DTFormate dtFormate)
        {
            DateTime dm = ToDateTime(value);
            dm = ToDateTime(DateTimeToString(dm, dtFormate));
            return dm;
        }
        /// <summary>
        /// 将object值转换成DateTime类型，失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <param name="defautValue">默认值</param>
        /// <param name="dtFormate">日期显示格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value, DateTime defautValue, DTFormate dtFormate)
        {
            DateTime dm = ToDateTime(value, defautValue);
            dm = ToDateTime(DateTimeToString(dm, dtFormate));
            return dm;
        }
        /// <summary>
        /// 将object值转换成DateTime?类型，失败则返回null
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(object value)
        {
            DateTime r = DateTime.MinValue;

            if (value == null) return null;

            if (DateTime.TryParse(value.ToString(), out r))
                return r;
            else
                return null;
        }
        /// <summary>
        /// 将object值转换成DateTime类型，失败则返回默认值
        /// </summary>
        /// <param name="value">object类型的值</param>
        /// <param name="defautValue">默认值</param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(object value, DateTime? defautValue)
        {
            DateTime r = DateTime.MinValue;

            if (value == null) return defautValue;

            if (DateTime.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        #endregion

        #region 将时间值转换为字符串值
        /// <summary>
        /// 将DateTime转换成字符串，失败则返回空
        /// </summary>
        /// <param name="value">时间值</param>
        /// <returns></returns>
        public static String DateTimeToString(DateTime value)
        {
            return DateTimeToString(value, DTFormate.SHORT_EN_US);
        }
        /// <summary>
        /// 将DateTime转换成字符串，失败则返回空
        /// </summary>
        /// <param name="value">时间值</param>
        /// <returns></returns>
        public static String DateTimeToString(object value)
        {
            DateTime dm = ToDateTime(value);
            return DateTimeToString(dm, DTFormate.SHORT_EN_US);
        }
        /// <summary>
        /// 将DateTime转换成字符串，失败则返回空
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <param name="dtFormate">日期显示格式</param>
        /// <returns></returns>
        public static String DateTimeToString(object value, DTFormate dtFormate)
        {
            DateTime dm = ToDateTime(value);
            return DateTimeToString(dm, dtFormate);
        }
        /// <summary>
        /// 将DateTime转换成字符串，失败则返回空
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <param name="dtFormate">日期显示格式</param>
        /// <returns></returns>
        public static String DateTimeToString(DateTime value, DTFormate dtFormate)
        {
            if (value == null) return String.Empty;

            DateTime m_dt = (DateTime)value;

            switch (dtFormate)
            {
                case DTFormate.SHORT_EN_US: return m_dt.ToString("yyyy-MM-dd");
                case DTFormate.SHORT_ZH_CN: return m_dt.ToString("yyyy年MM月dd日");
                case DTFormate.LONG_EN_US: return m_dt.ToString("yyyy-MM-dd HH:mm:ss");
                case DTFormate.LONG_ZH_CN: return m_dt.ToString("yyyy年MM月dd日HH时mm分ss秒");
                default: return m_dt.ToString("yyyy-MM-dd");
            }
        }
        #endregion

        #region Type类型转换
        /// <summary>
        /// 将Type类型转换为DbType类型
        /// </summary>
        /// <param name="type">Type类型</param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            DbType dbType = DbType.String;
            switch (type.Name)
            {
                case "String":
                    dbType = DbType.String;
                    break;
                case "Int32":
                    dbType = DbType.Int32;
                    break;
                case "DateTime":
                    dbType = DbType.DateTime;
                    break;
                case "Double":
                    dbType = DbType.Double;
                    break;
                case "Boolean":
                    dbType = DbType.Boolean;
                    break;
                case "Guid":
                    dbType = DbType.Guid;
                    break;
                default:
                    break;
            }
            return dbType;
        }
        /// <summary>
        /// 将字符串Type类型转换为Type类型
        /// </summary>
        /// <param name="strType">Type的字符串类型</param>
        /// <returns></returns>
        public static Type ToType(string strType)
        {
            Type typ = typeof(string);
            switch (strType)
            {
                case "System.String":
                    typ = typeof(string);
                    break;
                case "System.Int32":
                    typ = typeof(int);
                    break;
                case "System.Int16":
                    typ = typeof(Int16);
                    break;
                case "System.Int64":
                    typ = typeof(Int64);
                    break;
                case "System.Decimal":
                    typ = typeof(decimal);
                    break;
                case "System.DateTime":
                    typ = typeof(DateTime);
                    break;
                case "System.Double":
                    typ = typeof(double);
                    break;
                case "System.Single":
                    typ = typeof(float);
                    break;
                case "System.Boolean":
                    typ = typeof(bool);
                    break;
                default:
                    break;
            }
            return typ;
        }
        #endregion

        #region 格式化sql语句
        /// <summary>
        /// 格式化字符串为sql语句字符串,防止使用保留字，给所有字段加上[]
        /// </summary>
        /// <param name="strSql">sql字符串</param>
        /// <returns></returns>
        public static string FormatToSqlString(string strSql)
        {
            if (strSql == null)
            {
                return "*";
            }
            if (strSql.Trim() == "")
            {
                return "*";
            }
            if (strSql.Trim() == "*")
            {
                return "*";
            }
            //先去掉空格，[]符号
            string strTemp = strSql;
            strTemp = strTemp.Trim();
            strTemp = strTemp.Replace("[", "").Replace("]", "");
            //为防止使用保留字，给所有字段加上[]
            strTemp = "[" + strTemp + "]";
            strTemp = strTemp.Replace('，', ',');
            strTemp = strTemp.Replace(",", "],[");
            return strTemp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="escape"></param>
        /// <param name="transferred"></param>
        /// <returns></returns>
        public static string EscapeToSqlString(string strSql, string escape, string transferred)
        {
            return strSql.Replace(escape, transferred);
        }
        /// <summary>
        /// 格式化字符串为sql语句的条件
        /// </summary>
        /// <param name="field">字段项</param>
        /// <param name="value">字段值(如:eq_10)</param>
        /// <returns></returns>
        public static string FormatToSqlCondiction(string field, string value)
        {
            var array =ToArray(value,'_');
            //转为sql语句的条件
            if (array != null && array.Length >= 2)
            {
                switch (array[0].ToLower())
                {
                    case "eq": return String.Format("{0}='{1}'", field, array[1]);
                    case "like": return String.Format("{0} like '{1}%'", field, array[1]);
                    case "likeAll": return String.Format("{0} like '%{1}%'", field, array[1]);
                    case "le": return String.Format("{0}<={1}", field, array[1]);
                    case "Lt": return String.Format("{0}<{1}", field, array[1]);
                    case "ge": return String.Format("{0}>={1}", field, array[1]);
                    case "gt": return String.Format("{0}>{1}", field, array[1]);
                    case "between":
                        return String.Format("{0} between '{1}' and '{2}'", field, array[1], array[2]);
                    case "isnull": return String.Format("{0} is null ", field);
                    case "isnotnull": return String.Format("{0} is not null ", field);
                    default: break;
                }
            }
            return "";
        }
        #endregion

        #region 将字符串转为数组
        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static String[] ToArray(String value)
        {
            return ToArray(value, ',');
        }
        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="split">间隔符</param>
        /// <returns></returns>
        public static String[] ToArray(String value, char split)
        {
            if (String.IsNullOrEmpty(value)) return new string[0];

            return value.Split(split);
        }

        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static int[] ToIntArray(String value)
        {
            return ToIntArray(value, ',');
        }
        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="split">间隔符</param>
        /// <returns></returns>
        public static int[] ToIntArray(String value, char split)
        {
            string[] arrString = ToArray(value, split);
            return Array.ConvertAll(arrString, new Converter<string, int>(ToInt32));    
        }
        #endregion

        #region 将字符串转为字典
        /// <summary>
        /// 将字符串类型转换成字典类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<String, String> ToDictionary(String value)
        {
            return ToDictionary(value, '&', '=');
        }

        /// <summary>
        /// 将字符串类型转换成字典类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <param name="split1"></param>
        /// <returns></returns>
        public static Dictionary<String, String> ToDictionary(String value, char split, char split1)
        {
            Dictionary<String, String> collection = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(value))
            {
                var tmpCollection = ToArray(value, split);

                foreach (var t in tmpCollection)
                {
                    var tmp = ToArray(t, split1);

                    if (tmp != null && tmp.Length >= 2)
                        collection[tmp[0]] = tmp[1];
                }
            }
            return collection;
        }
        #endregion

        #region 将表单数据或HTTP请求中的数据解释为对象
        /// <summary>
        /// 将HttpRequest请求中的数据解释为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="httpRequest">请求数据</param>
        /// <returns></returns>
        public static T ParseHttpRequestToObject<T>(HttpRequestBase httpRequest) where T : new()
        {
            return ParseHttpRequestToObject<T>(httpRequest, new T());
        }

        /// <summary>
        /// 从页面提交的数据中解释对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="httpRequest">请求数据</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ParseHttpRequestToObject<T>(HttpRequestBase httpRequest, T obj) where T : new()
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            if (properties != null && properties.Length > 0)
            {
                foreach (PropertyInfo pt in properties)
                {
                    if (pt.CanWrite)
                    {
                        if (httpRequest[pt.Name] != null)
                        {
                            #region 根据类型转换页面提交的值
                            string value = httpRequest[pt.Name];//页面提交的值
                            if (pt.PropertyType == typeof(string))
                            {
                                pt.SetValue(obj, value, null);
                            }
                            else if (pt.PropertyType == typeof(bool))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value) : false, null);
                            }
                            else if (pt.PropertyType == typeof(short))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToInt16(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(int))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToInt32(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(long))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToInt64(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(DateTime))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToDateTime(value) : Convert.ToDateTime("1900-01-01"), null);
                            }
                            else if (pt.PropertyType.BaseType == typeof(Enum))
                            {
                                pt.SetValue(obj, Enum.Parse(pt.PropertyType, value), null);
                            }
                            else if (pt.PropertyType == typeof(decimal))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToDecimal(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(double))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToDouble(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(float))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToSingle(value) : 0, null);
                            }
                            else if (pt.PropertyType == typeof(Guid))
                            {
                                pt.SetValue(obj, Guid.Parse(value), null);
                            }
                            else if (pt.PropertyType == typeof(byte))
                            {
                                pt.SetValue(obj, Convert.ToByte(value), null);
                            }
                            else if (pt.PropertyType == typeof(int?))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? new Nullable<int>(Convert.ToInt32(value)) : null, null);
                            }
                            else if (pt.PropertyType == typeof(DateTime?))
                            {
                                pt.SetValue(obj, !string.IsNullOrEmpty(value) ? Convert.ToDateTime(value) : Convert.ToDateTime("1900-01-01"), null);
                            }
                            #endregion
                        }//end if (httpRequest[pt.Name] != null)
                    }//end if (pt.CanWrite)
                }//end foreach (System.Reflection.PropertyInfo pt in properties)
            }//end if (properties != null && properties.Length > 0)
            return obj;
        }

        /// <summary>
        /// 从表单提交的数据中解释对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fromData">表单数据</param>
        /// <returns></returns>
        public static T ParseFormToObject<T>(NameValueCollection formData, T obj) where T : new()
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            if (properties != null && properties.Length > 0)
            {
                foreach (PropertyInfo curProperty in properties)
                {
                    if (curProperty.CanWrite)
                    {
                        if (formData[curProperty.Name] != null)
                        {
                            #region 根据常用类型进行转换
                            string valueText = formData[curProperty.Name];
                            if (curProperty.PropertyType == typeof(string))
                            {
                                curProperty.SetValue(obj, valueText, null);
                            }
                            else if (curProperty.PropertyType == typeof(bool))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToBoolean(valueText) : false, null);
                            }
                            else if (curProperty.PropertyType == typeof(short))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToInt16(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(int))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToInt32(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(long))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToInt64(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(DateTime))
                            {
                                //curProperty.SetValue(obj, Convert.ToDateTime(valueText), null);
                                //curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDateTime(valueText) : DateTime.Now, null);
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDateTime(valueText) : Convert.ToDateTime("1900-01-01"), null);
                            }
                            else if (curProperty.PropertyType.BaseType == typeof(Enum))
                            {
                                curProperty.SetValue(obj, Enum.Parse(curProperty.PropertyType, valueText), null);
                            }
                            else if (curProperty.PropertyType == typeof(decimal))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDecimal(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(double))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDouble(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(float))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToSingle(valueText) : 0, null);
                            }
                            else if (curProperty.PropertyType == typeof(Guid))
                            {
                                curProperty.SetValue(obj, Guid.Parse(valueText), null);
                            }
                            else if (curProperty.PropertyType == typeof(byte))
                            {
                                curProperty.SetValue(obj, Convert.ToByte(valueText), null);
                            }
                            else if (curProperty.PropertyType == typeof(int?))
                            {
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? new Nullable<int>(Convert.ToInt32(valueText)) : null, null);
                            }
                            else if (curProperty.PropertyType == typeof(DateTime?))
                            {
                                //curProperty.SetValue(obj, Convert.ToDateTime(valueText), null);
                                //curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDateTime(valueText) : DateTime.Now, null);
                                curProperty.SetValue(obj, !string.IsNullOrEmpty(valueText) ? Convert.ToDateTime(valueText) : Convert.ToDateTime("1900-01-01"), null);
                            }
                            #endregion
                        }
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 从表单提交的数据中解释对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        public static T ParseFormToObject<T>(NameValueCollection formData) where T : new()
        {
            return ParseFormToObject<T>(formData, new T());
        }
        #endregion

        #region 获取随机数
        /// <summary>
        /// 根据GUID，得到不重复的随机数
        /// </summary>
        /// <param name="number">随机数位数</param>
        /// <returns>随机数</returns>
        public static string GetRandomNumberByGuid(int number)
        {
            Guid randSeedGuid = Guid.NewGuid();
            Random rand = new Random(BitConverter.ToInt32(randSeedGuid.ToByteArray(), 0));
            string sBit = "0".ToString().PadRight((number - 1), '0');
            int minValue = Convert.ToInt32("1" + sBit);
            int maxValue = Convert.ToInt32("9" + sBit.Replace("0", "9"));
            return rand.Next(minValue, maxValue).ToString();
        }
        #endregion

        #region 获取日期在一年中的周数
        public static int GetWeekOfYearByDateTime(DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("zh-CN");
            int weekOfYear = myCI.Calendar.GetWeekOfYear(dateTime,
                 System.Globalization.CalendarWeekRule.FirstDay,
                 System.DayOfWeek.Sunday);
            //string weekName = dateTime.ToString("dddd", myCI);
            return weekOfYear;
        }
        public static int GetWeekOfYearByDateTime(DateTime dateTime)
        {
            return GetWeekOfYearByDateTime(dateTime, System.DayOfWeek.Sunday);
        }

        public static int GetQuarterOfYearByMonth(int month)
        {
            int quarter = 0;
            if (month >= 1 && month <= 3)
            {
                quarter = 1;
            }
            if (month >= 4 && month <= 6)
            {
                quarter = 2;
            }
            if (month >= 7 && month <= 9)
            {
                quarter = 3;
            }
            if (month >= 10 && month <= 12)
            {
                quarter = 4;
            }
            return quarter;
        }
        ///// <summary>
        ///// 1 根据年获取开始时间
        ///// </summary>
        ///// <param name="year"></param>
        ///// <returns></returns>
        //public static string GetStartDateOfYear(int year)
        //{
        //    DateTime startYear = new DateTime(DataTypeConvert.ToInt32(year), 1, 1);
        //    return startYear + "";
        //}
        ///// <summary>
        ///// 1.1 根据年获取结束时间
        ///// </summary>
        ///// <param name="year"></param>
        ///// <returns></returns>
        //public static string GetEndDateOfYear(int year)
        //{
        //    DateTime endYear = new DateTime(DataTypeConvert.ToInt32(year), 12, 31);
        //    return endYear + "";
        //}
        /// <summary>
        /// 2 根据年月获取开始时间
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetStarDateOfYearAndMonth(int year, int month)
        {
            int Days = DateTime.DaysInMonth(DataTypeConvert.ToInt32(year), DataTypeConvert.ToInt32(month));
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + Days.ToString());//本月最后一天
            string startDate = dt.AddDays(1 - dt.Day) + ""; //本月月初
            return startDate;
        }
        /// <summary>
        /// 2.1 根据年月获取结束时间
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetEndDateOfYearAndMonth(int year, int month)
        {
            int Days = DateTime.DaysInMonth(DataTypeConvert.ToInt32(year), DataTypeConvert.ToInt32(month));
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + Days.ToString());//本月最后一天
            return dt+"";
        }
        /// <summary>
        /// 3 根据年,季度得到开始时间
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public static string GetStartDateOfQuarter(int year,int quarter)
        {
            int month = 0;
            switch (quarter)
            {
                case 1:
                    month = 1;
                    break;
                case 2:
                    month = 4;
                    break;
                case 3:
                    month = 7;
                    break;
                case 4:
                    month = 10;
                    break;
            }
            int Days = DateTime.DaysInMonth(DataTypeConvert.ToInt32(year), month);
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + Days.ToString());//本月最后一天
            string startDate = dt.AddDays(1 - dt.Day) + ""; //本月月初
            return startDate;
        }
        /// <summary>
        /// 3.1 根据年,季度得到最后一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public static string GetEndDateOfQuarter(int year, int quarter)
        {
            int month = 0;
            switch (quarter)
            {
                case 1:
                    month = 3;
                    break;
                case 2:
                    month = 6;
                    break;
                case 3:
                    month = 9;
                    break;
                case 4:
                    month = 12;
                    break;
            }
            int Days = DateTime.DaysInMonth(DataTypeConvert.ToInt32(year), month);
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + Days.ToString());//本月最后一天
            return dt+"";
        }

        /// <summary>
        /// 获取指定周数的开始日期和结束日期，开始日期为周日
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周数</param>
        /// <param name="firstDate">当此方法返回时，则包含参数 year 和 index 指定的周的开始日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。</param>
        /// <param name="lastDate">当此方法返回时，则包含参数 year 和 index 指定的周的结束日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。</param>
        /// <returns></returns>
        public static void GetDateOfWeek(int year, int week, out DateTime firstDate, out DateTime lastDate)
        {
            firstDate = DateTime.MinValue;
            lastDate = DateTime.MinValue;
            if (year < 1700 || year > 9999)
            {
                //"年份超限"
                return;
            }
            if (week < 1 || week > 53)
            {
                //"周数错误"
                return;
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = 0;
            if (Convert.ToInt32(startDay.DayOfWeek.ToString("d")) > 0)
                dayOfWeek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几
            if (dayOfWeek == 7) { dayOfWeek = 0; }
            if (week == 1)
            {
                firstDate = startDay;
                if (dayOfWeek == 6)
                {
                    lastDate = firstDate;
                }
                else
                {
                    lastDate = startDay.AddDays((6 - dayOfWeek));
                }
            }
            else
            {
                firstDate = startDay.AddDays((7 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
                lastDate = firstDate.AddDays(6);
                if (lastDate > endDay)
                {
                    lastDate = endDay;
                }
            }
            if (firstDate > endDay)  //startDayOfWeeks不在该年范围内
            {
                //"输入周数大于本年最大周数";
                return;
            }
            return;
        }
        #endregion

        #region 获取星期几
        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <returns></returns>
        public static string GetWeek()
        {
            return GetWeek(DateTime.Now);
        }
        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetWeek(DateTime date)
        {
            //string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            //string week = weekdays[Convert.ToInt32(date.DayOfWeek)];
            return GetWeek(date.DayOfWeek.ToString());
        }
        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="weekName">DayOfWeek的字符串形式,如：DateTime.Now.DayOfWeek.ToString()</param>
        /// <returns></returns>
        public static string GetWeek(string weekName)
        {
            string week="";
            switch (weekName)
            {
                case "Sunday":
                    week = "星期日";
                    break;
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期五";
                    break;
            }
            return week;
        }
        #endregion

        #region 创建DataTable
        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="strColumns">逗号分隔的列名</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(string strColumns)
        {
            DataTable dt = new DataTable();
            string[] arrColumns = ToArray(strColumns);
            DataColumn dc = null;
            foreach(string col in arrColumns)
            {
                dc = new DataColumn(col, typeof(string));
                dt.Columns.Add(dc);
            }
            return dt;
        }
        #endregion

        #region 获取ArrayList行对象的值
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">ArrayList行对象</param>
        /// <param name="fieldName">键名称</param>
        /// <returns>返回值</returns>
        public static object GetArrayRowFieldValue(object row,string fieldName)
        {
            if (row == null) return null;

            //{ ID = 6, FunctionCode = 006, FunctionName = 查看, FunctionButton = btnView, Status = 正常, Sequence = 6, FunctionDesc =  }
            string strObject = row.ToString();
            strObject = strObject.Replace("{", "").Replace("}", "");
            var tmpCollection = ToArray(strObject, ',');
            foreach (var t in tmpCollection)
            {
                var tmp = ToArray(t, '=');
                if (tmp != null && tmp.Length >= 2 && tmp[0].Trim() == fieldName)
                {
                    return tmp[1].Trim();
                }
            }
            return null;
        }
        #endregion
    }
}
