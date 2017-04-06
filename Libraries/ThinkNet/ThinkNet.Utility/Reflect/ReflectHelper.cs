using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectHelper
    {
        private static Dictionary<String, PropertyInfo[]> PropertyPool = new Dictionary<string, PropertyInfo[]>();

        /// <summary>
        /// 获取对象的属性
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(String className)
        {
            if (PropertyPool.ContainsKey(className) && PropertyPool[className] != null) return PropertyPool[className];

            var nameSpace = className.Substring(0, className.LastIndexOf("."));
            //if(nameSpace.StartsWith("ThinkNet."))
            //{
            //    nameSpace = "ThinkNet.Core";
            //}

            Assembly assembly = Assembly.Load(nameSpace);

            Object obj = assembly.CreateInstance(className);

            PropertyPool[className] = obj.GetType().GetProperties();

            return PropertyPool[className];
        }

        /// <summary>
        /// 获取属性值，如果不可读，则返回null
        /// </summary>
        /// <param name="target"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Object GetValue(object target, String key)
        {
            if (target == null || String.IsNullOrEmpty(key)) throw new Exception("参数为空!");

            var properties = GetProperties(target.GetType().FullName);

            if (properties == null) throw new Exception("properties is Null!");

            foreach (PropertyInfo p in properties)
            {
                if (p.Name.ToUpper() == key.ToUpper())
                    return GetValue(target, p);
            }

            return String.Empty;
        }
        /// <summary>
        /// 获取属性值，如果不可读，则返回null
        /// </summary>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Object GetValue(object target, PropertyInfo p)
        {
            if (target == null || p == null) throw new Exception("参数为空!");

            if (!p.CanRead) return null;//过滤掉只读属性

            return p.GetValue(target, null);
        }

        /// <summary>
        /// 设置属性值，如果该属性为只读，则跳过不赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(object target, String key, object value)
        {
            if (value == null || target == null || String.IsNullOrEmpty(key)) throw new Exception("paramater is Null!");

            var properties = GetProperties(target.GetType().FullName);

            if (properties == null) throw new Exception("properties is Null!");

            foreach (PropertyInfo p in properties)
            {
                if (p.Name.ToUpper() == key.ToUpper())
                    SetValue(target, p, value);
            }
        }
        /// <summary>
        /// 设置属性值，如果该属性为只读，则跳过不赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="p"></param>
        /// <param name="value"></param>
        public static void SetValue(object target, PropertyInfo p, object value)
        {
            if (value == null || target == null || p == null) throw new Exception("参数为空!");

            if (!p.CanWrite) return;//过滤掉只读属性

            Object t_value = GetValueByType(value, p.PropertyType);//根据类型进行转换

            try
            {
                p.SetValue(target, t_value, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 把object，string对象转换成指定类型对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Object GetValueByType(Object value, Type t)
        {
            if (value == null || t == null) throw new Exception("参数为空!");

            Object t_value = null;

            #region 根据类型进行转换
            if (t == typeof(Int16))
            {
                t_value = DataTypeConvert.ToInt16(value, 0);
            }
            else if (t == typeof(Int32))
            {
                t_value = DataTypeConvert.ToInt32(value, 0);
            }
            else if (t == typeof(Int64))
            {
                t_value = DataTypeConvert.ToInt64(value, 0);
            }
            else if (t == typeof(Boolean))
            {
                t_value = DataTypeConvert.ToBoolean(value, false);
            }
            else if (t == typeof(Double))
            {
                t_value = DataTypeConvert.ToDouble(value, 0);
            }
            else if (t == typeof(Decimal))
            {
                t_value = DataTypeConvert.ToDecimal(value, 0);
            }
            else if (t == typeof(DateTime))
            {
                t_value = DataTypeConvert.ToDateTime(value, DateTime.MinValue);
            }

            else
                t_value = value;

            #endregion

            return t_value;
        }
    }
}
