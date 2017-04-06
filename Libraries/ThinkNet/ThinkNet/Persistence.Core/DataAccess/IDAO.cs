using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Persistence.Core
{
    /// <summary>
    /// 数据(持久化)访问对象接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T>
        where T : DataEntityBase
    {
        #region 属性
        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        string ConnName { get; set; }
        /// <summary>
        /// 主键名称
        /// </summary>
        string PrimaryKey { get; set; }
        /// <summary>
        /// 数据表名称
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// 是否外部设置数据库
        /// </summary>
        bool IsExternalConnection { get; set; }
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        string ExternalConnectionStringsName { get; set; }
        /// <summary>
        /// 最大ID值
        /// </summary>
        int MaxID { get; }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        int Add(T dataEntity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="list">数据实体列表</param>
        /// <returns></returns>
        bool Add(List<T> list);
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        bool Update(T dataEntity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="list">数据实体列表</param>
        /// <returns></returns>
        bool Update(List<T> list);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <param name="columns">字段(逗号分隔)</param>
        /// <returns></returns>
        bool Update(T dataEntity, string columns);
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        int Delete(T dataEntity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        int Delete(int id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        int Delete(long id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        int Delete(string where);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        int Delete(string where, List<Parameter> listParameter);
        #endregion

        #region 是否存在
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        bool Exists(int id);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        bool Exists(long id);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        bool Exists(string where);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        bool Exists(string where, List<Parameter> listParameter);
        #endregion

        #region 获取记录数
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        int GetCount(string where);
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        int GetCount(string where, List<Parameter> listParameter);
        #endregion

        #region 获取数据实体
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        T GetDataEntity(int id);
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        T GetDataEntity(long id);
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        T GetDataEntity(string where);
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        T GetDataEntity(string where, List<Parameter> listParameter);
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        T GetDataEntity(DataRow row);
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="reader">数据流</param>
        /// <returns></returns>
        T GetDataEntity(IDataReader reader);
        #endregion

        #region 获取数据集合
        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        IDataReader GetReader(Pager mPager);

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <returns></returns>
        /// <param name="mPager">分页参数</param>
        DataTable GetDataTable(Pager mPager);

        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        List<T> GetList(Pager mPager);
        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <param name="reader">数据流</param>
        /// <returns></returns>
        List<T> GetList(IDataReader reader);

        /// <summary>
        /// 获取分页的数据流
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        IDataReader GetPagerDataReader(Pager mPager);
        /// <summary>
        /// 获取分页的数据集合
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        DataTable GetPagerDataTable(Pager mPager);
        #endregion

        #region 辅助方法
        /// <summary>
        /// 构建SQL语句
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        string BuildSql(Pager mPager);
        #endregion
    }
}
