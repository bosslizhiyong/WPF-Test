using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Utility;
using ThinkNet.Utility;

namespace ThinkNet.Persistence.Core
{
    public static class IDAOExtentions
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisDAO"></param>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public static int Add<T>(this IDAO<T> thisDAO,T dataEntity) where T:DataEntityBase
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append(string.Format("INSERT INTO {0}(", dataEntity.GetType().Name));

            //公共属性
            PropertyInfo[] properties = dataEntity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            bool IsAutoID = dataEntity.IsAutoID;
            string PrimaryKey = dataEntity.PrimaryKey;
            string ConnectionStringsName = dataEntity.ConnectionStringsName.ToString();
            bool IsExternalConnection = dataEntity.IsExternalConnection;
            string ExternalConnectionStringsName = dataEntity.ExternalConnectionStringsName;
            string ConnName = "";
            //外部设置数据库链接
            if (IsExternalConnection)
            {
                ConnName = string.IsNullOrEmpty(ExternalConnectionStringsName) ? ConnectionStringsName : ExternalConnectionStringsName;
            }
            else
            {
                ConnName = ConnectionStringsName;
            }

            List<Parameter> listParameter = new List<Parameter>();//值参数列表
            StringBuilder strParameter = new StringBuilder();//值参数串
            for (int i = 0; i < properties.Length; i++)
            {
                //剔除带下划线(_)的公共属性、剔除实体对象主键是自增的主键属性
                if (properties[i].Name.IndexOf('_') == 0 || (IsAutoID && PrimaryKey == properties[i].Name))
                {
                    continue;
                }
                if (properties[i].Name == "KeyID")
                {
                    continue;
                }

                strSql.Append("[" + properties[i].Name + "],");
                strParameter.Append("@" + properties[i].Name + ",");
                Parameter parameter = new Parameter();
                parameter.ParameterName = properties[i].Name;
                parameter.ParameterValue = properties[i].GetValue(dataEntity, null);
                parameter.ParameterDbType = DataTypeConvert.ToDbType(properties[i].PropertyType);
                listParameter.Add(parameter);
            }
            strSql = strSql.Replace(",", ")", strSql.Length - 1, 1);
            strSql.Append(" VALUES (");
            strParameter = strParameter.Replace(",", ")", strParameter.Length - 1, 1);
            strSql.Append(strParameter + ";");
            //如果实体对象为自增主键，返回当前主键值
            if (IsAutoID)
            {
                strSql.Append("SELECT SCOPE_IDENTITY()");
            }
            Database database = DatabaseFactory.CreateDatabase(ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                foreach (Parameter param in listParameter)
                {
                    database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                }
                object obj = database.ExecuteScalar(command);
                return DataTypeConvert.ToInt32(obj, 0);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisDAO"></param>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public static bool Update<T>(this IDAO<T> thisDAO, T dataEntity) where T : DataEntityBase
        {
            return thisDAO.Update<T>(dataEntity, null);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisDAO"></param>
        /// <param name="dataEntity">数据实体</param>
        /// <param name="columns">字段(逗号分隔)</param>
        /// <returns></returns>
        public static bool Update<T>(this IDAO<T> thisDAO, T dataEntity, string columns) where T : DataEntityBase
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append("UPDATE " + dataEntity.GetType().Name + " SET ");

            //公共属性
            PropertyInfo[] properties = dataEntity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            bool IsAutoID = dataEntity.IsAutoID;
            string PrimaryKey = dataEntity.PrimaryKey;
            string ConnectionStringsName = dataEntity.ConnectionStringsName.ToString();
            bool IsExternalConnection = dataEntity.IsExternalConnection;
            string ExternalConnectionStringsName = dataEntity.ExternalConnectionStringsName;
            string ConnName = "";
            //外部设置数据库链接
            if (IsExternalConnection)
            {
                ConnName = string.IsNullOrEmpty(ExternalConnectionStringsName) ? ConnectionStringsName : ExternalConnectionStringsName;
            }
            else
            {
                ConnName = ConnectionStringsName;
            }

            List<Parameter> listParameter = new List<Parameter>();
            //更新全部字段
            if (string.IsNullOrEmpty(columns))
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    //剔除带下划线(_)的公共属性
                    if (properties[i].Name.IndexOf('_') == 0)
                    {
                        continue;
                    }
                    if (properties[i].Name == "KeyID")
                    {
                        continue;
                    }

                    //剔除实体对象主键是自增的主键属性
                    if (properties[i].Name != PrimaryKey)
                    {
                        strSql.Append(properties[i].Name + "=@" + properties[i].Name + ",");
                    }
                    Parameter parameter = new Parameter();
                    parameter.ParameterName = properties[i].Name;
                    parameter.ParameterValue = properties[i].GetValue(dataEntity, null);
                    parameter.ParameterDbType = DataTypeConvert.ToDbType(properties[i].PropertyType);
                    listParameter.Add(parameter);
                }
            }
            else//更新具体字段
            {
                string[] arrColumns = columns.Split(new char[] { ',' });
                bool isExitsKey = false;//是否包含主键字段
                PropertyInfo ptKey = null;
                for (int j = 0; j < arrColumns.Length; j++)
                {
                    //剔除带下划线(_)的公共属性
                    if (arrColumns[j].IndexOf('_') == 0)
                    {
                        continue;
                    }
                    if (arrColumns[j].ToUpper() == PrimaryKey.ToUpper())
                    {
                        isExitsKey = true;
                    }
                    foreach (PropertyInfo pt in properties)
                    {
                        if (pt.Name == PrimaryKey)
                        {
                            ptKey = pt;
                        }
                        if (pt.Name == arrColumns[j])
                        {
                            //剔除实体对象主键是自增的主键属性
                            if (pt.Name != PrimaryKey)
                            {
                                strSql.Append(pt.Name + "=@" + pt.Name + ",");
                            }
                            Parameter parameter = new Parameter();
                            parameter.ParameterName = pt.Name;
                            parameter.ParameterValue = pt.GetValue(dataEntity, null);
                            parameter.ParameterDbType = DataTypeConvert.ToDbType(pt.PropertyType);
                            listParameter.Add(parameter);
                            break;
                        }
                    }//end foreach (PropertyInfo pt in properties)
                }//end for (int j = 0; j < arrColumns.Length; j++)
                if (!isExitsKey)
                {
                    Parameter parameter = new Parameter();
                    parameter.ParameterName = ptKey.Name;
                    parameter.ParameterValue = ptKey.GetValue(dataEntity, null);
                    parameter.ParameterDbType = DataTypeConvert.ToDbType(ptKey.PropertyType);
                    listParameter.Add(parameter);
                }
            }//end if
            strSql = strSql.Replace(",", " ", strSql.Length - 1, 1);
            strSql.Append(" WHERE " + PrimaryKey + "=@" + PrimaryKey);
            Database database = DatabaseFactory.CreateDatabase(ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                foreach (Parameter param in listParameter)
                {
                    database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                }
                int count = database.ExecuteNonQuery(command);
                return count > 0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisDAO"></param>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public static int Delete<T>(this IDAO<T> thisDAO, T dataEntity) where T : DataEntityBase
        {
            bool IsAutoID = dataEntity.IsAutoID;
            string PrimaryKey = dataEntity.PrimaryKey;
            string ConnectionStringsName = dataEntity.ConnectionStringsName.ToString();
            bool IsExternalConnection = dataEntity.IsExternalConnection;
            string ExternalConnectionStringsName = dataEntity.ExternalConnectionStringsName;
            string ConnName = "";
            //外部设置数据库链接
            if (IsExternalConnection)
            {
                ConnName = string.IsNullOrEmpty(ExternalConnectionStringsName) ? ConnectionStringsName : ExternalConnectionStringsName;
            }
            else
            {
                ConnName = ConnectionStringsName;
            }

            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append("DELETE " + dataEntity.GetType().Name);
            string where = string.Format("{0}={1}", PrimaryKey, dataEntity.KeyID);
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            Database database = DatabaseFactory.CreateDatabase(ConnName);
            int count = 0;
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                count = database.ExecuteNonQuery(command);
            }
            return count;
        }
    }
}
