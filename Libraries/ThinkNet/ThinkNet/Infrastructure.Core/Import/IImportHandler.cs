using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    public interface IImportHandler
    {
        /// <summary>
        /// 唯一Key值
        /// </summary>
        String UnquieKey { get; set; }

        /// <summary>
        /// 临时数据
        /// </summary>
        List<RowInfo> DataCollection { get; }

        /// <summary>
        /// 是否所有数据都是通过校验
        /// </summary>
        bool IsAllValid { get; }
        /// <summary>
        /// 针对快速导入
        /// </summary>
        /// <param name="columns">逗号分隔的列(可为空)</param>
        /// <returns></returns>
        bool IsExistError(string columns);
        /// <summary>
        /// 针对快速导入
        /// </summary>
        /// <param name="columns">逗号分隔的列(可为空)</param>
        /// <returns></returns>
        bool IsColumnValid(string columns);

        /// <summary>
        /// 读取Excel，然后写入临时表、启动数据验证、显示数据验证结果
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns></returns>
        List<RowInfo> SourceToTempTable(String filePath);
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <param name="target"></param>
        Boolean ValidData(ColumnInfo target);

        /// <summary>
        /// 导入临时表
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        Boolean ImportToTempTable(List<RowInfo> target);

        /// <summary>
        /// 修改临时表中数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        ColumnInfo ModifyDataInTempTable(int rowIndex, String columnName, Object dataValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="dataValue"></param>
        /// <param name="otherColumnName"></param>
        /// <param name="otherDataValue"></param>
        /// <returns></returns>
        ColumnInfo ModifyDataInTempTableOther(int rowIndex, String columnName, Object dataValue, String otherColumnName, Object otherDataValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="oldDataValue"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        Boolean ModifyDataByOriginalValue(String columnName,Object oldDataValue, Object dataValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="originalValue"></param>
        /// <param name="dataValue"></param>
        /// <param name="otherColumnName"></param>
        /// <param name="otherOriginalValue"></param>
        /// <returns></returns>
        Boolean ModifyDataByOriginalValueOther(String columnName, Object oldDataValue, Object dataValue, Object oldOtherDataValue, String otherColumnName, Object otherDataValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="originalValue"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        Boolean ModifyDataValueByOriginalValue(String columnName, Object originalValue, Object dataValue); 

        /// <summary>
        /// 删除临时表中的行
        /// </summary>
        /// <returns></returns>
        Boolean DeleteRowInTempTable(int rowIndex);

        /// <summary>
        /// 导入到实际表中
        /// </summary>
        /// <returns></returns>
        Boolean ImportToTargetTable(int operaterId, string operaterName);

        /// <summary>
        /// 清空已经处理的行
        /// </summary>
        /// <returns></returns>
        string ClearHandleRows();

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="unquieKey"></param>
        /// <returns></returns>
        bool RemoveTempTableByUnquieKey(string unquieKey);
    }
}
