using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域服务接口
    /// </summary>
    public interface IDomainService
    {
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        string ExternalConnectionStringsName { set; }
        /// <summary>
        /// 当前服务所使用的上下文实例
        /// </summary>
        IDomainServiceContext Context { get; }

        /// <summary>
        /// 简单结果描述
        /// </summary>
        SimpleResult SimpleResult { get; set; }

        /// <summary>
        /// 将指定的数据实体添加到服务中
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        void Add<T>(T dataEntity) where T : DataEntityBase;
        /// <summary>
        /// 更新服务中指定的数据实体
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        void Update<T>(T dataEntity) where T : DataEntityBase;
        /// <summary>
        /// 将指定的数据实体从服务中删除
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        void Delete<T>(T dataEntity) where T : DataEntityBase;

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        void ExecuteNonQuery(string strSql);

        /// <summary>
        /// 执行插入语句
        /// </summary>
        /// <param name="insertSql"></param>
        void ExecuteInsertSql(string insertSql);
        /// <summary>
        /// 执行更新语句
        /// </summary>
        /// <param name="updateSql"></param>
        void ExecuteUpdateSql(string updateSql);
        /// <summary>
        /// 执行删除语句
        /// </summary>
        /// <param name="deleteSql"></param>
        void ExecuteDeleteSql(string deleteSql);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <param name="direction">输入或输出(ParameterDirection.Input/ParameterDirection.Output)</param>
        /// <returns></returns>
        SqlParameter AddParameter(string parameterName, SqlDbType dbType, int size, object value, ParameterDirection direction);
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        SqlParameter AddInParameter(string parameterName, SqlDbType dbType, int size, object value);
        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        SqlParameter AddOutParameter(string parameterName, SqlDbType dbType, int size);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        void ExecuteProcedure(string procName, List<DbParameter> parameters);
    }
}
