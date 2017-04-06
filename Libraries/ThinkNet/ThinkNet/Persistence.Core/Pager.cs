using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Persistence.Core
{
    /// <summary>
    /// 分页参数类
    /// </summary>
    public class Pager : IPager
    {
        #region 字    段
        /// <summary>
        /// where条件
        /// </summary>
        private string where = "";
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)
        /// </summary>
        private bool orderType = false;
        /// <summary>
        /// 字段名称(逗号分隔)
        /// </summary>
        private string columns = "";
        /// <summary>
        /// 排序字段(注:可以直接加上DESC/ASC)
        /// </summary>
        private string orderField = "";
        /// <summary>
        /// 数据表名称
        /// </summary>
        private string tableName = "";
        #endregion

        #region 属    性
        /// <summary>
        /// where条件(可为空)
        /// </summary>
        public string Where 
        { 
            get
            {
                return where;
            }
        }
        /// <summary>
        /// 当前页数(-1或0均转为1)
        /// </summary>
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
        }
        /// <summary>
        /// 每页显示记录数(-1或0均转为int.MaxValue)
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
        }
        /// <summary>
        /// 排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)
        /// </summary>
        public bool OrderType
        {
            get
            {
                return orderType;
            }
        }
        /// <summary>
        /// 字段名称(逗号分隔,可为空)
        /// </summary>
        public string Columns
        {
            get
            {
                return columns;
            }
        }
        /// <summary>
        /// 排序字段(注:可以直接加上DESC/ASC,可为空)
        /// </summary>
        public string OrderField
        {
            get
            {
                return orderField;
            }
        }
        /// <summary>
        /// 数据表名称(可为空)
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        public Pager()
            : this("", 1, int.MaxValue, true, "*", "", "")
        {

        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        public Pager(string where)
            : this(where, 1, int.MaxValue, true, "*", "", "")
        {

        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        public Pager(string where, int pageIndex)
            : this(where, pageIndex, int.MaxValue, true, "*", "", "")
        {

        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        public Pager(string where, int pageIndex, int pageSize)
            : this(where, pageIndex, pageSize, true, "*", "", "")
        {

        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        public Pager(string where, int pageIndex, int pageSize, bool orderType)
            : this(where, pageIndex, pageSize, orderType, "*", "", "")
        {
            
        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔,可为空)</param>
        public Pager(string where, int pageIndex, int pageSize, bool orderType, string columns)
            : this(where, pageIndex, pageSize, orderType, columns, "", "")
        {
            
        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔,可为空)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC,可为空)</param>
        public Pager(string where, int pageIndex, int pageSize, bool orderType, string columns, string orderField)
            :this(where,pageIndex,pageSize,orderType,columns,orderField,"")
        {
            
        }
        /// <summary>
        /// 分页参数类(查询参数)
        /// </summary>
        /// <param name="where">where条件(可为空)</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔,可为空)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC,可为空)</param>
        /// <param name="tableName">数据表名称(可为空)</param>
        public Pager(string where, int pageIndex, int pageSize, bool orderType, string columns, string orderField, string tableName)
        {
            this.where = where;
            this.pageIndex = (pageIndex==-1||pageIndex==0)?1:pageIndex;
            this.pageSize = (pageSize == -1 || pageSize == 0) ? int.MaxValue : pageSize;
            this.orderType = orderType;
            this.columns = columns;
            this.orderField = orderField;
            this.tableName = tableName;
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 获取SqlServer(2005版本及以上)数据库的分页Sql语句
        /// </summary>
        /// <param name="strSql">未分页的Sql语句</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        public string GetPagerSql(string strSql, int pageIndex, int pageSize, bool orderType, string orderField)
        {
            StringBuilder strPagerSql = new StringBuilder();
            //排序字段
            string strOrder = "";
            if (string.IsNullOrEmpty(orderField))
            {
                orderField = "ID";
                if (orderType)
                {
                    strOrder = string.Format(" ORDER BY {0} DESC", orderField);//降序
                }
                else
                {
                    strOrder = string.Format(" ORDER BY {0} ASC", orderField);//升序
                }
            }
            else
            {
                //多个字段排序
                if (orderField.Contains(","))
                {
                    strOrder = string.Format(" ORDER BY {0} ", orderField);
                }
                else
                {
                    if (orderField.TrimEnd().ToUpper().EndsWith("DESC") || orderField.TrimEnd().ToUpper().EndsWith("ASC"))
                    {
                        strOrder = string.Format(" ORDER BY {0}", orderField);
                    }
                    else
                    {
                        if (orderType)
                        {
                            strOrder = string.Format(" ORDER BY {0} DESC", orderField);//降序
                        }
                        else
                        {
                            strOrder = string.Format(" ORDER BY {0} ASC", orderField);//升序
                        }
                    }//end if (orderField.ToUpper().EndsWith("DESC") || orderField.ToUpper().EndsWith("ASC"))
                }//end if (orderField.Contains(","))
            }
            strPagerSql.AppendFormat(@"SELECT * FROM (
                        SELECT *,ROW_NUMBER() OVER({0}) AS row FROM ({1}) a ) b 
                        WHERE row BETWEEN {2} AND {3}", strOrder, strSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
            return strPagerSql.ToString();
        }
        /// <summary>
        /// 获取分页Sql语句的总记录数
        /// </summary>
        /// <param name="strSql">未分页的Sql语句</param>
        /// <returns></returns>
        public string GetCountSql(string strSql)
        {
            return " SELECT COUNT(1) FROM (" + strSql + ") tb";
        }
        #endregion

        #region 其他方法

        #endregion
    }
}
