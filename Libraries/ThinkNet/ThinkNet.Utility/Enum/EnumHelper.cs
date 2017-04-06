using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转DataTable
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static DataTable EnumToDataTable(Type enumType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Value"));//值
                dt.Columns.Add(new DataColumn("Text"));//中文名称

                List<string> result = new List<string>();
                FieldInfo[] fieldinfo = enumType.GetFields();
                DataRow dr = null;
                foreach (FieldInfo item in fieldinfo)
                {
                    if (item.Name != "value__")
                    {
                        dr = dt.NewRow();
                        dr["Value"] = Enum.Format(enumType, Enum.Parse(enumType, item.Name), "d");
                        dr["Text"] = GetEnumItemDesc((Enum)Enum.Parse(enumType, item.Name));
                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 枚举转DataTable
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="ListExist">已存在的项</param>
        /// <returns></returns>
        public static DataTable EnumToDataTable(Type enumType, List<string> ListExist)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Value"));//值
                dt.Columns.Add(new DataColumn("Text"));//中文名称

                List<string> result = new List<string>();
                FieldInfo[] fieldinfo = enumType.GetFields();
                DataRow dr = null;
                int i = 0;
                string value = "";
                foreach (FieldInfo item in fieldinfo)
                {
                    if (i != 0)
                    {
                        value = Enum.Format(enumType, Enum.Parse(enumType, item.Name), "d");
                        if (ListExist != null && ListExist.Count > 0)
                        {
                            if (!ListExist.Contains(value))
                            {
                                dr = dt.NewRow();
                                dr["Value"] = value;
                                dr["Text"] = GetEnumItemDesc((Enum)Enum.Parse(enumType, item.Name));
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            dr = dt.NewRow();
                            dr["Value"] = value;
                            dr["Text"] = GetEnumItemDesc((Enum)Enum.Parse(enumType, item.Name));
                            dt.Rows.Add(dr);
                        }
                    }
                    i++;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType)
        {
            return EnumToList(enumType, false);
        }

        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType,bool isNullSelect)
        {
            return EnumToList(enumType, isNullSelect, "-1");
        }
        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType, bool isNullSelect, string nullValue)
        {
            return EnumToList(enumType, isNullSelect, nullValue, "请选择...");
        }
        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <param name="nullValue"></param>
        /// <param name="nullText"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType, bool isNullSelect, string nullValue, string nullText)
        {
            List<EnumItem> list = new List<EnumItem>();
            Array arrValues = Enum.GetValues(enumType);
            foreach (Enum value in arrValues)
            {
                int val = Convert.ToInt32(value);
                string text = GetEnumItemDesc(enumType, val);
                if (!string.IsNullOrEmpty(text))
                {
                    list.Add(new EnumItem { Value = val.ToString(), Text = text });
                }
            }
            if (isNullSelect)
            {
                if (string.IsNullOrEmpty(nullValue))
                {
                    nullValue = "-1";
                }
                list.Insert(0, new EnumItem(nullValue, nullText));
            }
            return list;
        }

        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <param name="arrExcludeValues"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType, bool isNullSelect, int[] arrExcludeValues)
        {
            return EnumToList(enumType, isNullSelect, arrExcludeValues, "-1", "请选择...", true);
        }
        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <param name="arrExcludeValues"></param>
        /// <param name="nullValue"></param>
        /// <param name="nullText"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType, bool isNullSelect, int[] arrExcludeValues, string nullValue, string nullText)
        {
            return EnumToList(enumType,isNullSelect,arrExcludeValues,nullValue,nullText,true);
        }
        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNullSelect"></param>
        /// <param name="arrExcludeValues"></param>
        /// <param name="nullValue"></param>
        /// <param name="nullText"></param>
        /// <param name="isExclude"></param>
        /// <returns></returns>
        public static List<EnumItem> EnumToList(Type enumType, bool isNullSelect, int[] arrEnumValues, string nullValue, string nullText, bool isExclude)
        {
            List<EnumItem> list = new List<EnumItem>();
            Array arrValues = Enum.GetValues(enumType);
            foreach (Enum value in arrValues)
            {
                int val = Convert.ToInt32(value);
                string text = GetEnumItemDesc(enumType, val);
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }
                if (arrEnumValues == null || arrEnumValues.Length <= 0)
                {
                    list.Add(new EnumItem { Value = val.ToString(), Text = text });
                }
                else
                {
                    bool isExits = IsExitsValue(val, arrEnumValues);
                    if(isExclude)
                    {
                        if(!isExits)
                        {
                            list.Add(new EnumItem { Value = val.ToString(), Text = text });
                        }
                    }
                    else
                    {
                        if (isExits)
                        {
                            list.Add(new EnumItem { Value = val.ToString(), Text = text });
                        }
                    }
                }
            }
            if (isNullSelect)
            {
                list.Insert(0, new EnumItem(nullValue, nullText));
            }
            return list;
        }
        /// <summary>
        /// 是否存在值项
        /// </summary>
        /// <param name="value"></param>
        /// <param name="arrEnumValues"></param>
        /// <returns></returns>
        private static bool IsExitsValue(int value, int[] arrEnumValues)
        {
            if (arrEnumValues == null || arrEnumValues.Length <= 0) return false;
            foreach (int v in arrEnumValues)
            {
                if(value==v)
                {
                    return true;
                }
            }
            return false;
        }
        

        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <param name="enmItem">要返回描述的枚举项</param>
        /// <returns></returns>
        public static string GetEnumItemDesc(Type enumType, int enumValue)
        {
            try
            {
                string result = "";
                FieldInfo[] fieldinfo = enumType.GetFields();
                int i = 0;
                foreach (FieldInfo item in fieldinfo)
                {
                    if (item.Name != "value__")
                    {
                        if (DataTypeConvert.ToInt32(Enum.Format(enumType, Enum.Parse(enumType, item.Name), "d"), -1) == enumValue)
                        {
                            result = GetEnumItemDesc((Enum)Enum.Parse(enumType, item.Name));
                            break;
                        }
                    }
                    i++;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据描述获取枚举项的值
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static int GetEnumValueByDescription(Type enumType, string description)
        {
            FieldInfo[] fieldinfo = enumType.GetFields();
            foreach (FieldInfo item in fieldinfo)
            {
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (obj != null && obj.Length != 0)
                {
                    DescriptionAttribute des = (DescriptionAttribute)obj[0];
                    if (Convert.ToString(des.Description) != description + "")
                        continue;
                    string[] names = Enum.GetNames(enumType);
                    Array values = Enum.GetValues(enumType);
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (names[i] == item.Name)
                            return (int)values.GetValue(i);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <param name="enmItem">要返回描述的枚举项</param>
        /// <returns></returns>
        public static string GetEnumItemDesc(Enum enmItem)
        {
            try
            {
                Type temType = enmItem.GetType();
                MemberInfo[] memberInfos = temType.GetMember(enmItem.ToString());
                if (memberInfos != null && memberInfos.Length > 0)
                {
                    object[] objs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (objs != null && objs.Length > 0)
                    {
                        return ((DescriptionAttribute)objs[0]).Description;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有枚举项的描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<string> GetAllEnumItemDesc(Type enumType)
        {
            try
            {
                List<string> result = new List<string>();
                FieldInfo[] fieldinfo = enumType.GetFields();
                foreach (FieldInfo item in fieldinfo)
                {
                    Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (obj != null && obj.Length != 0)
                    {
                        DescriptionAttribute des = (DescriptionAttribute)obj[0];
                        result.Add(des.Description);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        //public static string GetEnumItemString(Type enumType,string desc)
        //{
        //    try
        //    {
        //        FieldInfo[] fieldinfo = enumType.GetFields();
        //        foreach (FieldInfo item in fieldinfo)
        //        {
        //            Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //            if (obj != null && obj.Length != 0)
        //            {
        //                DescriptionAttribute des = (DescriptionAttribute)obj[0];
        //                if (des.Description==desc)
        //                {
        //                    return item.Name;
        //                }                      
        //            }
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static Dictionary<string, string> GetEnumItemString(Type enumType, Dictionary<string, string> dicDescription)
        {
            try
            {
                FieldInfo[] fieldinfo = enumType.GetFields();
                foreach (FieldInfo item in fieldinfo)
                {
                    Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (obj != null && obj.Length != 0)
                    {
                        DescriptionAttribute des = (DescriptionAttribute)obj[0];
                        if (dicDescription.ContainsKey(des.Description))
                        {
                            dicDescription[des.Description] = item.Name + "";
                        }
                    }
                }
                return dicDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
