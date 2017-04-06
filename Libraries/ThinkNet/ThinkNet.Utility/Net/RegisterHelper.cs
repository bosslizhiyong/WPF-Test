using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 注册表基项静态域
    /// </summary>
    public enum RegDomain
    {
        /// <summary>
        /// 对应于HKEY_CLASSES_ROOT主键
        /// </summary>
        HKEY_CLASSES_ROOT = 0,
        /// <summary>
        /// 对应于HKEY_CURRENT_USER主键
        /// </summary>
        HKEY_CURRENT_USER = 1,
        /// <summary>
        /// 对应于HKEY_LOCAL_MACHINE主键
        /// </summary>
        HKEY_LOCAL_MACHINE = 2,
        /// <summary>
        /// 对应于HKEY_USERS主键
        /// </summary>
        HKEY_USERS = 3,
        /// <summary>
        /// 对应于HKEY_CURRENT_CONFIG主键
        /// </summary>
        HKEY_CURRENT_CONFIG = 4
    }
    /// <summary>
    /// 指定在注册表中存储值时所用的数据类型，或标识注册表中某个值的数据类型
    /// </summary>
    public enum RegValueKind
    {
        /// <summary>
        /// 指示一个不受支持的注册表数据类型。例如，不支持 Microsoft Win32 API 注册表数据类型 REG_RESOURCE_LIST。使用此值指定
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 指定一个以 Null 结尾的字符串。此值与 Win32 API 注册表数据类型 REG_SZ 等效。
        /// </summary>
        String = 1,
        /// <summary>
        /// 指定一个以 NULL 结尾的字符串，该字符串中包含对环境变量（如 %PATH%，当值被检索时，就会展开）的未展开的引用。
        /// 此值与 Win32 API注册表数据类型 REG_EXPAND_SZ 等效。
        /// </summary>
        ExpandString = 2,
        /// <summary>
        /// 指定任意格式的二进制数据。此值与 Win32 API 注册表数据类型 REG_BINARY 等效。
        /// </summary>
        Binary = 3,
        /// <summary>
        /// 指定一个 32 位二进制数。此值与 Win32 API 注册表数据类型 REG_DWORD 等效。
        /// </summary>
        DWord = 4,
        /// <summary>
        /// 指定一个以 NULL 结尾的字符串数组，以两个空字符结束。此值与 Win32 API 注册表数据类型 REG_MULTI_SZ 等效。
        /// </summary>
        MultiString = 5,
        /// <summary>
        /// 指定一个 64 位二进制数。此值与 Win32 API 注册表数据类型 REG_QWORD 等效。
        /// </summary>
        QWord = 6,
    }

    /// <summary>
    /// 注册表操作类
    /// 主要包括以下操作：
    /// 1.创建注册表项
    /// 2.读取注册表项
    /// 3.判断注册表项是否存在
    /// 4.删除注册表项
    /// 5.创建注册表键值
    /// 6.读取注册表键值
    /// 7.判断注册表键值是否存在
    /// 8.删除注册表键值
    /// </summary>
    public static class RegisterHelper
    {
        #region 基本方法
        #region 创建注册表项
        /// <summary>
        /// 创建注册表项
        /// 例子：如regDomain是HKEY_LOCAL_MACHINE，subkey是SOFTWARE\\higame\\，则将创建HKEY_LOCAL_MACHINE\\SOFTWARE\\higame\\注册表项
        /// </summary>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        public static void CreateSubKey(string subKey, RegDomain regDomain)
        {
            //判断注册表项名称是否为空，如果为空，返回false
            if (subKey == string.Empty || subKey == null)
            {
                return;
            }

            //创建基于注册表基项的节点
            RegistryKey key = GetRegDomain(regDomain);

            //要创建的注册表项的节点
            RegistryKey sKey;
            if (!IsSubKeyExist(subKey, regDomain))
            {
                sKey = key.CreateSubKey(subKey);
            }
            //sKey.Close();
            //关闭对注册表项的更改
            key.Close();
        }
        #endregion

        #region 判断注册表项是否存在
        /// <summary>
        /// 判断注册表项是否存在
        /// 例子：如regDomain是HKEY_CLASSES_ROOT，subkey是SOFTWARE\\higame\\，则将判断HKEY_CLASSES_ROOT\\SOFTWARE\\higame\\注册表项是否存在
        /// </summary>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>返回注册表项是否存在，存在返回true，否则返回false</returns>
        public static bool IsSubKeyExist(string subKey, RegDomain regDomain)
        {
            //判断注册表项名称是否为空，如果为空，返回false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            //检索注册表子项
            //如果sKey为null,说明没有该注册表项不存在，否则存在
            RegistryKey sKey = OpenSubKey(subKey, regDomain);
            if (sKey == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 删除注册表项
        /// <summary>
        /// 删除注册表项
        /// </summary>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>如果删除成功，则返回true，否则为false</returns>
        public static bool DeleteSubKey(string subKey, RegDomain regDomain)
        {
            //返回删除是否成功
            bool result = false;

            //判断注册表项名称是否为空，如果为空，返回false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            //创建基于注册表基项的节点
            RegistryKey key = GetRegDomain(regDomain);

            if (IsSubKeyExist(subKey, regDomain))
            {
                try
                {
                    ///删除注册表项
                    key.DeleteSubKey(subKey);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            //关闭对注册表项的更改
            key.Close();
            return result;
        }
        #endregion

        #region 判断键值是否存在
        /// <summary>
        /// 判断键值是否存在
        /// </summary>
        /// <param name="name">键值名称</param>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>返回键值是否存在，存在返回true，否则返回false</returns>
        public static bool IsRegeditKeyExist(string name, string subKey, RegDomain regDomain)
        {
            //返回结果
            bool result = false;

            //判断是否设置键值属性
            if (name == string.Empty || name == null)
            {
                return false;
            }

            //判断注册表项是否存在
            if (IsSubKeyExist(subKey,regDomain))
            {
                //打开注册表项
                RegistryKey key = OpenSubKey(subKey, regDomain);
                //键值集合
                string[] regeditKeyNames;
                //获取键值集合
                regeditKeyNames = key.GetValueNames();
                //遍历键值集合，如果存在键值，则退出遍历
                foreach (string regeditKey in regeditKeyNames)
                {
                    if (string.Compare(regeditKey, name, true) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                //关闭对注册表项的更改
                key.Close();
            }
            return result;
        }
        #endregion

        #region 设置键值内容
        /// <summary>
        /// 设置指定的键值内容，指定内容数据类型
        /// 存在改键值则修改键值内容，不存在键值则先创建键值，再设置键值内容
        /// </summary>
        /// <param name="name">键值名称</param>
        /// <param name="content">键值内容</param>
        /// <param name="regValueKind"></param>
        /// <param name="subKey"></param>
        /// <param name="regDomain"></param>
        /// <returns>键值内容设置成功，则返回true，否则返回false</returns>
        public static bool WriteRegeditKey(string name, object content, RegValueKind regValueKind, string subKey, RegDomain regDomain)
        {
            //返回结果
            bool result = false;

            //判断键值是否存在
            if (name == string.Empty || name == null)
            {
                return false;
            }

            //判断注册表项是否存在，如果不存在，则直接创建
            if (!IsSubKeyExist(subKey,regDomain))
            {
                CreateSubKey(subKey, regDomain);
            }

            //以可写方式打开注册表项
            RegistryKey key = OpenSubKey(subKey,regDomain,true);

            //如果注册表项打开失败，则返回false
            if (key == null)
            {
                return false;
            }

            try
            {
                key.SetValue(name, content, GetRegValueKind(regValueKind));
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                //关闭对注册表项的更改
                key.Close();
            }
            return result;
        }
        #endregion

        #region 读取键值内容
        /// <summary>
        /// 读取键值内容
        /// </summary>
        /// <param name="name">键值名称</param>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>返回键值内容</returns>
        public static object ReadRegeditKey(string name, string subKey, RegDomain regDomain)
        {
            //键值内容结果
            object obj = null;

            //判断是否设置键值属性
            if (name == string.Empty || name == null)
            {
                return null;
            }

            //判断键值是否存在
            if (IsRegeditKeyExist(name,subKey,regDomain))
            {
                ///打开注册表项
                RegistryKey key = OpenSubKey(subKey, regDomain);
                if (key != null)
                {
                    obj = key.GetValue(name);
                }
                //关闭对注册表项的更改
                key.Close();
            }
            return obj;
        }
        #endregion

        #region 删除键值
        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="name">键值名称</param>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>如果删除成功，返回true，否则返回false</returns>
        public static bool DeleteRegeditKey(string name, string subKey, RegDomain regDomain)
        {
            //删除结果
            bool result = false;

            //判断键值名称和注册表项名称是否为空，如果为空，则返回false
            if (name == string.Empty || name == null || subKey == string.Empty || subKey == null)
            {
                return false;
            }

            //判断键值是否存在
            if (IsRegeditKeyExist(name,subKey,regDomain))
            {
                //以可写方式打开注册表项
                RegistryKey key = OpenSubKey(subKey, regDomain, true);
                if (key != null)
                {
                    try
                    {
                        ///删除键值
                        key.DeleteValue(name);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        //关闭对注册表项的更改
                        key.Close();
                    }
                }
            }

            return result;
        }
        #endregion
        #endregion

        #region 受保护方法
        /// <summary>
        /// 获取注册表基项域对应顶级节点
        /// 例子：如regDomain是ClassesRoot，则返回Registry.ClassesRoot
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>注册表基项域对应顶级节点</returns>
        public static RegistryKey GetRegDomain(RegDomain regDomain)
        {
            //创建基于注册表基项的节点
            RegistryKey key;

            #region 判断注册表基项域
            switch (regDomain)
            {
                case RegDomain.HKEY_CLASSES_ROOT:
                    key = Registry.ClassesRoot; break;
                case RegDomain.HKEY_CURRENT_USER:
                    key = Registry.CurrentUser; break;
                case RegDomain.HKEY_LOCAL_MACHINE:
                    key = Registry.LocalMachine; break;
                case RegDomain.HKEY_USERS:
                    key = Registry.Users; break;
                case RegDomain.HKEY_CURRENT_CONFIG:
                    key = Registry.CurrentConfig; break;
                default:
                    key = Registry.LocalMachine; break;
            }
            #endregion

            return key;
        }

        /// <summary>
        /// 获取在注册表中对应的值数据类型
        /// 例子：如regValueKind是DWord，则返回RegistryValueKind.DWord
        /// </summary>
        /// <param name="regValueKind">注册表数据类型</param>
        /// <returns>注册表中对应的数据类型</returns>
        public static RegistryValueKind GetRegValueKind(RegValueKind regValueKind)
        {
            RegistryValueKind regValueK;

            #region 判断注册表数据类型
            switch (regValueKind)
            {
                case RegValueKind.Unknown:
                    regValueK = RegistryValueKind.Unknown; break;
                case RegValueKind.String:
                    regValueK = RegistryValueKind.String; break;
                case RegValueKind.ExpandString:
                    regValueK = RegistryValueKind.ExpandString; break;
                case RegValueKind.Binary:
                    regValueK = RegistryValueKind.Binary; break;
                case RegValueKind.DWord:
                    regValueK = RegistryValueKind.DWord; break;
                case RegValueKind.MultiString:
                    regValueK = RegistryValueKind.MultiString; break;
                case RegValueKind.QWord:
                    regValueK = RegistryValueKind.QWord; break;
                default:
                    regValueK = RegistryValueKind.String; break;
            }
            #endregion
            return regValueK;
        }

        #region 打开注册表项
        /// <summary>
        /// 打开注册表项节点，以只读方式检索子项
        /// 虚方法，子类可进行重写
        /// </summary>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>如果SubKey为空、null或者SubKey指示注册表项不存在，则返回null，否则返回注册表节点</returns>
        public static RegistryKey OpenSubKey(string subKey, RegDomain regDomain)
        {
            //判断注册表项名称是否为空
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            //创建基于注册表基项的节点
            RegistryKey key = GetRegDomain(regDomain);

            //要打开的注册表项的节点
            RegistryKey sKey = null;
            //打开注册表项
            sKey = key.OpenSubKey(subKey);
            //关闭对注册表项的更改
            key.Close();
            //返回注册表节点
            return sKey;
        }

        /// <summary>
        /// 打开注册表项节点
        /// 虚方法，子类可进行重写
        /// </summary>
        /// <param name="subKey">注册表项名称</param>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="writable">如果需要项的写访问权限，则设置为 true</param>
        /// <returns>如果SubKey为空、null或者SubKey指示注册表项不存在，则返回null，否则返回注册表节点</returns>
        public static RegistryKey OpenSubKey(string subKey, RegDomain regDomain, bool writable)
        {
            //判断注册表项名称是否为空
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            //创建基于注册表基项的节点
            RegistryKey key = GetRegDomain(regDomain);

            //要打开的注册表项的节点
            RegistryKey sKey = null;
            //打开注册表项
            sKey = key.OpenSubKey(subKey, writable);
            //关闭对注册表项的更改
            key.Close();
            //返回注册表节点
            return sKey;
        }
        #endregion
        #endregion
    }
}