using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ThinkNet.Command.Core;
using ThinkNet.Query.Core;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DefaultExportTypes
    {
        /// <summary>
        /// 数据集列表
        /// </summary>
        ListDataTable=1,
        /// <summary>
        /// 导出行列表
        /// </summary>
        ListExportRow = 2,
        /// <summary>
        /// 数据集DataSet
        /// </summary>
        DataSet = 3,
        /// <summary>
        /// 数据集DataTable
        /// </summary>
        DataTable = 4
    }
    /// <summary>
    /// 
    /// </summary>
    public class DefaultExportHandler : NPOIExportHandler
    {
        #region 字    段
        
        #endregion

        #region 属    性
        /// <summary>
        /// 命令总线
        /// </summary>
        private ICommandBus CommandBus
        {
            get;
            set;
        }
        /// <summary>
        /// 查询服务
        /// </summary>
        private IDynamicQuery QueryService
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public DefaultExportHandler()
            : base(null)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBus"></param>
        /// <param name="queryService"></param>
        public DefaultExportHandler(ICommandBus commandBus, IDynamicQuery queryService)
            : base(null)
        {
            this.CommandBus = commandBus;
            this.QueryService = queryService;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryCondictions"></param>
        /// <returns></returns>
        public override object GetExportData(object queryCondictions)
        {
            Dictionary<string, object> dicCondictions = queryCondictions as Dictionary<string, object>;
            if (dicCondictions == null || dicCondictions.Count <= 0) return null;
            string defaultType = dicCondictions["DefaultExportType"]+"";//默认导出类型(DefaultExportTypes枚举的字符串)
            if(defaultType==DefaultExportTypes.DataSet.ToString())
            {
                return dicCondictions["Data"] as DataSet;
            }
            else if (defaultType == DefaultExportTypes.DataTable.ToString())
            {
                return dicCondictions["Data"] as DataTable;
            }
            else if (defaultType == DefaultExportTypes.ListDataTable.ToString())
            {
                return dicCondictions["Data"] as List<DataTable>;
            }
            else if (defaultType == DefaultExportTypes.ListExportRow.ToString())
            {
                return dicCondictions["Data"] as List<ExportRow>;
            }
            else
            {
                return null;
            }
        }
    }
}
