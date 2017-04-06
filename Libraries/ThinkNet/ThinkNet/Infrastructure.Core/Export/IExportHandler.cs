using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 导出接口
    /// </summary>
    public interface IExportHandler
    {
        /// <summary>
        /// 获取要导出列的列表
        /// </summary>
        /// <returns></returns>
        List<ExportColumn> GetExportColumnList();
        /// <summary>
        /// 获取要导出列的列表
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<ExportColumn>> GetExportColumnDictionary();
        /// <summary>
        /// 获取要导出列的列表
        /// </summary>
        /// <param name="category">类别</param>
        /// <returns></returns>
        List<ExportColumn> GetExportColumnListByCategory(string category);

        /// <summary>
        /// 将数据导出到Excel
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns></returns>
        bool ExportDataToExcel(List<ExportColumn> columns, string sheetName, string fileName, object queryCondictions);
        /// <summary>
        /// 根据模板将数据导出到Excel
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns></returns>
        bool ExportDataToExcelByTemplate(string templatePath, string sheetName, string fileName, object queryCondictions);

        /// <summary>
        /// 将DataTable的数据导出到Excel
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dtData">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportDataTableToExcel(List<ExportColumn> columns, DataTable dtData, string sheetName,string fileName);
        /// <summary>
        /// 将ListDataTable的数据导出到Excel(导出单个个工作簿)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dataTables">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportListDataTableToExcel(Dictionary<string, List<ExportColumn>> dicColumns, List<DataTable> dataTables, string sheetName, string fileName);
        /// <summary>
        /// 将DataSet的数据导出到Excel(导出多个工作簿)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dsData">要导出的数据集</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportDataSetToExcel(List<ExportColumn> columns, DataSet dsData, string fileName);
        /// <summary>
        /// 将ListRow的数据导出到Excel(在客户端获取数据)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="rows">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportListRowToExcel(List<ExportColumn> columns, List<ExportRow> rows, string sheetName, string fileName);
        /// <summary>
        /// 根据模板将数据导出到Excel
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="dicColumns">要导出的列集合(如果为空默认导出所有已配置的列,字典键：类别)</param>
        /// <param name="dicData">要导出的数据集(字典键值：类别)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportDataToExcelByTemplate(string templatePath, Dictionary<string, List<ExportColumn>> dicColumns, Dictionary<string, DataTable> dicData, string sheetName, string fileName);
        /// <summary>
        /// 根据模板将数据导出到Excel(多个工作簿)
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="dicColumns">要导出的列集合(如果为空默认导出所有已配置的列,字典键：类别)</param>
        /// <param name="dicDicData">要导出的数据集(字典键值：类别)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        bool ExportDataToExcelByTemplate(string templatePath, Dictionary<string, List<ExportColumn>> dicColumns, Dictionary<string, Dictionary<string, DataTable>> dicDicData, string sheetName, string fileName);

        /// <summary>
        /// 获取要导出数据
        /// </summary>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns>返回DataTable集合或返回ExportRow列表</returns>
        object GetExportData(object queryCondictions);
    }
}
