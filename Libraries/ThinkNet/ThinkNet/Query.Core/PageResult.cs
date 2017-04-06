using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 查询结果
    /// </summary>
    public class PageResult<T>
    {
        #region 字    段

        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int totalRecord = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int totalPage = 0;
        /// <summary>
        /// 当前记录集合
        /// </summary>
        private IEnumerable<T> collections = null;

        #endregion

        #region 属    性
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
        }
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord
        {
            get { return totalRecord; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return totalPage; }
        }
        /// <summary>
        /// 当前记录集合
        /// </summary>
        public IEnumerable<T> Collections
        {
            get { return collections; }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResult()
            :this(1,int.MaxValue,0,null)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="collections">当前记录集合</param>
        public PageResult(int pageIndex, int pageSize, int totalRecord, IEnumerable<T> collections)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.totalRecord = totalRecord;
            this.collections = collections;
            if(pageSize==0)
            {
                this.totalPage = 0;
            }
            else
            {
                this.totalPage = totalRecord % PageSize == 0 ? (totalRecord / pageSize) : (totalRecord / pageSize) + 1;
            }
        }

        #endregion

        #region 基本方法

        /// <summary>
        /// Json字符串
        /// </summary>
        /// <param name="elseArgs">其它需要一同生成Json的键值对</param>
        /// <returns></returns>
        public string ToString(Dictionary<string, object> elseArgs)
        {
            Dictionary<string, object> args = elseArgs != null ? new Dictionary<string, object>(elseArgs) : new Dictionary<string, object>();
            args["PageIndex"] = PageIndex;
            args["PageSize"] = PageSize;
            args["TotalRecord"] = TotalRecord;
            args["TotalPage"] = TotalPage;
            args["Collections"] = Collections;
            //return JSonHelper.Serialize(args);
            return ObjectContainer.Resolve<IJsonSerializer>().Serialize(args);
        }
        /// <summary>
        /// Json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null);
        }

        #endregion

        #region 其他方法

        /// <summary>
        /// Json字符串(面向EasyUI的Grid)
        /// </summary>
        /// <returns></returns>
        public string ToEasyuiString()
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["total"] = TotalRecord;
            args["rows"] = Collections;
            //return JSonHelper.Serialize(args);
            return ObjectContainer.Resolve<IJsonSerializer>().Serialize(args);
        }

        #endregion
    }
}
