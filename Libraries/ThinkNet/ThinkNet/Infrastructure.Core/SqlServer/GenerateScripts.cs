using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using ThinkNet.Utility;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 创建脚本工具类
    /// </summary>
    public class GenerateScripts
    {
        #region 字    段
        private Microsoft.SqlServer.Management.Common.ServerConnection _Connection = null;
        private Microsoft.SqlServer.Management.Smo.Server _Server = null;
        private Microsoft.SqlServer.Management.Smo.Scripter _Scripter = null;
        private Microsoft.SqlServer.Management.Smo.Database _Database = null;
        #endregion

        #region 属    性

        /// <summary>
        /// Connection
        /// </summary>
        public Microsoft.SqlServer.Management.Common.ServerConnection Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public Database DataBase
        {
            get { return _Database; }
            private set { _Database = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 数据库操作类
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        public GenerateScripts(string server, string userId, string password, string database)
        {
            _Connection = new Microsoft.SqlServer.Management.Common.ServerConnection(server, userId, password);
            _Server = new Microsoft.SqlServer.Management.Smo.Server(_Connection);
            _Scripter = GetScripter(_Server);
            _Database = _Server.Databases[database];
        }

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="database"></param>
        /// <param name="scriptType"></param>
        public Database SetDatabase(string database, string scriptType = "Script")
        {
            try
            {
                Database db = _Server.Databases[database];
                if (db == null)
                {
                    throw new ArgumentOutOfRangeException("不存在数据库");
                }
                _Database = db;
                return db;
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("不存在数据库" + ex.Message);
            }
            //遍历表 

        }
        #endregion

        #region 基本方法
        #region 生成数据库脚本
        /// <summary>
        /// 生成表脚本
        /// </summary>
        /// <param name="tables">数据源表</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>是否成功</returns>
        public bool GenerateTableScripts( string fileName)
        {
            try
            {
                TableCollection tables = _Database.Tables;
                DateTime start = DateTime.Now;
                using (Stream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    //声明统一资源名称集合对象 
                    UrnCollection collection = null;
                    //声明字符串集合对象：存储collection中的所有string对象（在这里其中有3个string对象）  
                    StringCollection sqls = null;
                    collection = new UrnCollection();
                    for (int i = 0; i < tables.Count; i++)
                    {
                        collection.Add(tables[i].Urn);
                    }
                    sqls = _Scripter.Script(collection);
                    //遍历字符串集合对象sqls中的string对象，选择要输出的脚本语句： 
                    if (sqls != null && sqls.Count > 0)
                    {
                        //写入文件
                        byte[] bytes = null;
                        string temp = "";
                        foreach (string s in sqls)
                        {
                            temp = s + "\r\n";
                            bytes = Encoding.Default.GetBytes(temp);
                            stream.Write(bytes, 0, bytes.Length);
                        }

                    }
                    stream.Close();
                }
                DateTime end = DateTime.Now;
                TimeSpan span = end - start;
                double seconds = span.TotalSeconds;
                Console.WriteLine("创建表所用时间" + seconds);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        /// <summary>
        /// 生成函数脚本
        /// </summary>
        /// <param name="funs">函数</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>是否成功</returns>
        public bool GertateFunctionScripts(string fileName)
        {
            try
            {
                UserDefinedFunctionCollection funs = _Database.UserDefinedFunctions;
                using (Stream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    //声明统一资源名称集合对象 
                    UrnCollection collection = null;
                    //声明字符串集合对象：存储collection中的所有string对象（在这里其中有3个string对象）  
                    StringCollection sqls = null;
                    collection = new UrnCollection();
                    for (int i = 0; i < funs.Count; i++)
                    {
                        if (funs[i].Owner == "dbo")
                        {
                            collection.Add(funs[i].Urn);
                        }
                        else
                        {
                            break;
                        }
                    }
                    sqls = _Scripter.Script(collection);
                    //遍历字符串集合对象sqls中的string对象，选择要输出的脚本语句： 
                    if (sqls != null && sqls.Count > 0)
                    {
                        //写入文件
                        byte[] bytes = null;
                        string temp = "";
                        foreach (string s in sqls)
                        {
                            temp = s + "\r\n";
                            bytes = Encoding.Default.GetBytes(temp);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //  WriteExceptionLog(ex);
            }
        }
        /// <summary>
        /// 生成视图
        /// </summary>
        /// <param name="views">视图</param>
        /// <param name="fileName">文件名路径</param>
        /// <returns>是否成功</returns>
        public bool GenerateViewScripts(string fileName)
        {
            try
            {
                ViewCollection views = _Database.Views;
                using (Stream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    //声明统一资源名称集合对象 
                    UrnCollection collection = null;
                    //声明字符串集合对象：存储collection中的所有string对象（在这里其中有3个string对象）  
                    StringCollection sqls = null;
                    collection = new UrnCollection();
                    for (int i = 0; i < views.Count; i++)
                    {
                        if (views[i].Owner == "dbo")
                        {
                            collection.Add(views[i].Urn);
                        }
                        else
                        {
                            break;
                        }
                    }
                    //collection.Add(table.Urn);
                    sqls = _Scripter.Script(collection);
                    //遍历字符串集合对象sqls中的string对象，选择要输出的脚本语句： 
                    if (sqls != null && sqls.Count > 0)
                    {
                        //写入文件
                        byte[] bytes = null;
                        string temp = "";
                        foreach (string s in sqls)
                        {
                            temp = s + "\r\n";
                            bytes = Encoding.Default.GetBytes(temp);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //  WriteExceptionLog(ex);
            }
        }

        /// <summary>
        /// 生成存储过程
        /// </summary>
        /// <param name="proc">存储过程</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>是否成功</returns>
        public bool GenerateProcScripts( string fileName)
        {
            try
            {
                StoredProcedureCollection proc = _Database.StoredProcedures;
                using (Stream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    //声明统一资源名称集合对象 
                    UrnCollection collection = null;
                    //声明字符串集合对象：存储collection中的所有string对象（在这里其中有3个string对象）  
                    StringCollection sqls = null;
                    collection = new UrnCollection();
                    for (int i = 0; i < proc.Count; i++)
                    {
                        if (proc[i].Owner == "dbo")
                        {
                            collection.Add(proc[i].Urn);
                        }
                        else
                        {
                            break;
                        }
                    }
                    sqls = _Scripter.Script(collection);
                    //遍历字符串集合对象sqls中的string对象，选择要输出的脚本语句： 
                    if (sqls != null && sqls.Count > 0)
                    {
                        byte[] bytes = null;
                        string temp = "";
                        foreach (string s in sqls)
                        {
                            temp = s + "\r\n";
                            bytes = Encoding.Default.GetBytes(temp);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // WriteExceptionLog(ex);
            }
        }
        #endregion

        #endregion

        #region 其他方法
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        private Scripter GetScripter(Microsoft.SqlServer.Management.Smo.Server server)
        {
            //初始化脚本生成器 
            Scripter scripter = new Scripter(server);
            //下面这些是可选项：作用是向脚本生成器中添加需要生成的脚本内容
            //ScripOption类的属性:http://msdn.microsoft.com/zh-cn/library/microsoft.sqlserver.management.smo.scriptingoptions.aspx
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.DriAllConstraints);
            //DriAllConstraints 获取或设置布尔属性值，指定是否所有声明的参照完整性约束包含在生成的脚本。
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.DriAllKeys);
            //DriAllKeys 获取或设置布尔属性值指定的依赖关系是否生成的脚本中定义的所有声明的参照完整性密钥。 
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.Default);
            //Default 获取或设置布尔属性值指定创建所引用对象是否包含在生成的脚本。 
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.ContinueScriptingOnError);
            //ContinueScriptingOnError 获取或设置布尔属性值指定的脚本是否继续操作时遇到错误后。 
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.ConvertUserDefinedDataTypesToBaseType);
            //ConvertUserDefinedDataTypesToBaseType 获取或设置布尔属性值指定是否将用户定义的数据类型转换成最合适的SQL Server基本数据类型生成的脚本中。 
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.IncludeIfNotExists);
            // IncludeIfNotExists 获取或设置一个布尔属性值，指定包括它在脚本之前，是否检查一个对象是否存在。 
            scripter.Options.Add(Microsoft.SqlServer.Management.Smo.ScriptOption.ExtendedProperties);
            return scripter;
        }
        #endregion

    }
}
