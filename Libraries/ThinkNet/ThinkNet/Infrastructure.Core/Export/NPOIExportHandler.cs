using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ThinkNet.Infrastructure.Core
{
    public abstract class NPOIExportHandler : IExportHandler
    {
        #region 属    性

        /// <summary>
        /// xml配置的文档对象
        /// </summary>
        public XmlDocument DataTableSchema { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaPath">xml配置路径</param>
        public NPOIExportHandler(string schemaPath)
        {
            //加载xml配置的文档对象
            DataTableSchema=LoadXmlDocument(schemaPath);
        }
        #endregion

        #region 基本方法
        #region 获取xml配置定义的结构
        /// <summary>
        /// 获取xml配置定义的结构
        /// </summary>
        /// <param name="schemaPath">xml配置路径</param>
        /// <returns></returns>
        public static List<ExportColumn> GetTableSchemaToExportColumn(string schemaPath)
        {
            XmlDocument schemaDoc = new XmlDocument();
            schemaDoc.Load(schemaPath);
            XmlNode tableNode = schemaDoc.SelectSingleNode("DataTableSchema/Columns");
            List<ExportColumn> list = new List<ExportColumn>();
            if (tableNode != null)
            {
                ExportColumn mExportColumn = null;
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    mExportColumn = new ExportColumn();
                    mExportColumn.ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value;
                    mExportColumn.Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value;
                    mExportColumn.DataType = node.Attributes["Type"] == null ? String.Empty : node.Attributes["Type"].Value;
                    mExportColumn.ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value);
                    mExportColumn.ColumnWidth = node.Attributes["ColumnWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ColumnWidth"].Value);
                    mExportColumn.Category = node.Attributes["Category"] == null ? String.Empty : node.Attributes["Category"].Value;
                    list.Add(mExportColumn);
                }
            }
            return list;
        }
        #endregion

        #region IExportHandler成员
        /// <summary>
        /// 获取导出列的列表
        /// </summary>
        /// <returns></returns>
        public List<ExportColumn> GetExportColumnList()
        {
            if (DataTableSchema == null)
            { return null; }

            XmlNode tableNode = DataTableSchema.SelectSingleNode("DataTableSchema/Columns");
            List<ExportColumn> list = new List<ExportColumn>();
            if (tableNode != null)
            {
                ExportColumn mExportColumn = null;
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    mExportColumn = new ExportColumn();
                    mExportColumn.ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value;
                    mExportColumn.Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value;
                    mExportColumn.DataType = node.Attributes["Type"] == null ? String.Empty : node.Attributes["Type"].Value;
                    mExportColumn.ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value);
                    mExportColumn.ColumnWidth = node.Attributes["ColumnWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ColumnWidth"].Value);
                    mExportColumn.Category = node.Attributes["Category"] == null ? String.Empty : node.Attributes["Category"].Value;
                    list.Add(mExportColumn);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取要导出列的列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<ExportColumn>> GetExportColumnDictionary()
        {
            if (DataTableSchema == null)
            { return null; }

            XmlNode tableNode = DataTableSchema.SelectSingleNode("DataTableSchema/Columns");
            Dictionary<string, List<ExportColumn>> dicColumns = new Dictionary<string, List<ExportColumn>>();
            if (tableNode != null)
            {
                ExportColumn mExportColumn = null;
                string categoryTemp = "";
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    categoryTemp = node.Attributes["Category"] == null ? String.Empty : node.Attributes["Category"].Value;
                    if (!dicColumns.ContainsKey(categoryTemp))
                    {
                        dicColumns.Add(categoryTemp, new List<ExportColumn>());
                    }
                    mExportColumn = new ExportColumn();
                    mExportColumn.ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value;
                    mExportColumn.Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value;
                    mExportColumn.DataType = node.Attributes["Type"] == null ? String.Empty : node.Attributes["Type"].Value;
                    mExportColumn.ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value);
                    mExportColumn.ColumnWidth = node.Attributes["ColumnWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ColumnWidth"].Value);
                    mExportColumn.Category = categoryTemp;
                    dicColumns[categoryTemp].Add(mExportColumn);
                }
            }
            return dicColumns;
        }
        /// <summary>
        /// 获取要导出列的列表
        /// </summary>
        /// <param name="category">类别</param>
        /// <returns></returns>
        public List<ExportColumn> GetExportColumnListByCategory(string category)
        {
            if (DataTableSchema == null)
            { return null; }

            XmlNode tableNode = DataTableSchema.SelectSingleNode("DataTableSchema/Columns");
            List<ExportColumn> list = new List<ExportColumn>();
            if (tableNode != null)
            {
                ExportColumn mExportColumn = null;
                string categoryTemp = "";
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    categoryTemp = node.Attributes["Category"] == null ? String.Empty : node.Attributes["Category"].Value;
                    if (categoryTemp==category)
                    {
                        mExportColumn = new ExportColumn();
                        mExportColumn.ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value;
                        mExportColumn.Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value;
                        mExportColumn.DataType = node.Attributes["Type"] == null ? String.Empty : node.Attributes["Type"].Value;
                        mExportColumn.ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value);
                        mExportColumn.ColumnWidth = node.Attributes["ColumnWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ColumnWidth"].Value);
                        mExportColumn.Category = categoryTemp;
                        list.Add(mExportColumn);
                    }//end if (categoryTemp==category)
                }
            }
            return list;
        }

        /// <summary>
        /// 将数据导出到Excel
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns></returns>
        public virtual bool ExportDataToExcel(List<ExportColumn> columns, string sheetName, string fileName, object queryCondictions)
        {
            //获取数据
            object objData = GetExportData(queryCondictions);
            if (objData==null)
            {
                return false;
            }
            DataTable dtData = objData as DataTable;
            if(dtData!=null&&dtData.Rows.Count>0)
            {
                //将DataTable的数据导出到Excel
                return ExportDataTableToExcel(columns, dtData, sheetName, fileName);
            }
            DataSet dsData = objData as DataSet;
            if (dsData != null && dsData.Tables!=null&&dsData.Tables.Count > 0)
            {
                //将DataSet的数据导出到Excel
                return ExportDataSetToExcel(columns, dsData, fileName);
            }
            List<DataTable> dataTables = objData as List<DataTable>;
            if (dataTables != null && dataTables.Count > 0)
            {
                //将ListDataTable的数据导出到Excel
                Dictionary<string, List<ExportColumn>> dicColumns = GetExportColumnDictionary();
                return ExportListDataTableToExcel(dicColumns, dataTables, sheetName, fileName);
            }
            List<ExportRow> rows = objData as List<ExportRow>;
            if (rows != null && rows.Count > 0)
            {
                //将ListRow的数据导出到Excel
                return ExportListRowToExcel(columns, rows, sheetName, fileName);
            }

            return false;
        }
        /// <summary>
        /// 根据模板将数据导出到Excel
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="templateSheetName">模板中工作簿的名称</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns></returns>
        public bool ExportDataToExcelByTemplate(string templatePath, string sheetName, string fileName, object queryCondictions)
        {
            //获取数据
            object objData = GetExportData(queryCondictions);
            if (objData == null)
            {
                return false;
            }
            Dictionary<string, DataTable> dicData = objData as Dictionary<string, DataTable>;
            if (dicData != null && dicData.Count > 0)
            {
                Dictionary<string, List<ExportColumn>> dicColumns = GetExportColumnDictionary();
                //将DataTable的数据导出到Excel
                return ExportDataToExcelByTemplate(templatePath, dicColumns, dicData, sheetName, fileName);
            }
            Dictionary<string, Dictionary<string, DataTable>> dicDicData = objData as Dictionary<string, Dictionary<string, DataTable>>;
            if (dicDicData != null && dicDicData.Count > 0)
            {
                Dictionary<string, List<ExportColumn>> dicColumns = GetExportColumnDictionary();
                //将DataTable的数据导出到Excel(多个工作簿)
                return ExportDataToExcelByTemplate(templatePath, dicColumns, dicDicData, sheetName, fileName);
            }

            return false;
        }

        /// <summary>
        /// 将DataTable的数据导出到Excel
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dtData">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportDataTableToExcel(List<ExportColumn> columns, DataTable dtData, string sheetName, string fileName)
        {
            //获取要导出的列并排序
            columns = GetExportColumns(columns);
            if (columns == null || columns.Count <= 0)
            {
                return false;
            }

            //创建生成Excel对象
            HSSFWorkbook workbook = LoadWorkbook("");
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow row = null;
            ICell cell = null;
            int index = 0;

            #region 标题行
            //标题行
            row = sheet.CreateRow(0);
            
            //表头样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Indigo.Index;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //加粗
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            index = 0;
            foreach (ExportColumn col in columns)
            {
                cell = row.CreateCell(index);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(col.Description);
                //设置列宽
                sheet.SetColumnWidth(index, col.ColumnWidth * 256);
                index++;
            }
            #endregion

            #region 数据行
            //数据行
            for (int i = 0; i < dtData.Rows.Count; i++)//所有数据行
            {
                row = sheet.CreateRow(i + 1);
                index = 0;
                foreach (ExportColumn col in columns)//所有要导出的列
                {
                    if (dtData.Columns.Contains(col.ColumnName))
                    {
                        row.CreateCell(index).SetCellValue(dtData.Rows[i][col.ColumnName] + "");
                    }
                    else
                    {
                        row.CreateCell(index).SetCellValue("");
                    }
                    index++;
                }
            }
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] bs = ms.ToArray();
                    fs.Write(bs, 0, bs.Length);
                }
            }
            return true;
        }
        /// <summary>
        /// 将ListDataTable的数据导出到Excel(导出单个个工作簿)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dataTables">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportListDataTableToExcel(Dictionary<string, List<ExportColumn>> dicColumns, List<DataTable> dataTables, string sheetName, string fileName)
        {
            //创建生成Excel对象
            HSSFWorkbook workbook = LoadWorkbook("");
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow row = null;
            ICell cell = null;
            int index = 0;

            //表头样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Indigo.Index;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //加粗
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            DataTable dtData = null;
            int k = 0;
            for (int j = 0; j < dataTables.Count;j++ )
            {
                dtData = dataTables[j];
                if (dtData == null || dtData.Rows.Count <= 0) continue;
                if(dtData.Rows.Count==1)
                {
                    #region 数据行
                    row = sheet.CreateRow(k);
                    index = 0;
                    foreach (ExportColumn col in dicColumns[dtData.TableName])//所有要导出的列
                    {
                        row.CreateCell(index).SetCellValue(col.Description);
                        index = index + 1;
                        if (dtData.Columns.Contains(col.ColumnName))
                        {
                            row.CreateCell(index).SetCellValue(dtData.Rows[0][col.ColumnName] + "");
                        }
                        else
                        {
                            row.CreateCell(index).SetCellValue("");
                        }
                        index = index + 1;
                    }
                    #endregion
                }
                else if(dtData.Rows.Count>1)
                {
                    #region 标题行
                    //标题行
                    row = sheet.CreateRow(k);
                    index = 0;
                    foreach (ExportColumn col in dicColumns[dtData.TableName])
                    {
                        cell = row.CreateCell(index);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(col.Description);
                        //设置列宽
                        sheet.SetColumnWidth(index, col.ColumnWidth * 256);
                        index++;
                    }
                    #endregion

                    #region 数据行
                    //数据行
                    for (int i = 0; i < dtData.Rows.Count; i++)//所有数据行
                    {
                        row = sheet.CreateRow(k+i + 1);
                        index = 0;
                        foreach (ExportColumn col in dicColumns[dtData.TableName])//所有要导出的列
                        {
                            if (dtData.Columns.Contains(col.ColumnName))
                            {
                                row.CreateCell(index).SetCellValue(dtData.Rows[i][col.ColumnName] + "");
                            }
                            else
                            {
                                row.CreateCell(index).SetCellValue("");
                            }
                            index++;
                        }
                    }
                    #endregion
                }
                k++;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] bs = ms.ToArray();
                    fs.Write(bs, 0, bs.Length);
                }
            }
            return true;
        }
        /// <summary>
        /// 将DataSet的数据导出到Excel(导出多个工作簿)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="dsData">要导出的数据集</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportDataSetToExcel(List<ExportColumn> columns, DataSet dsData, string fileName)
        {
            //获取要导出的列并排序
            columns = GetExportColumns(columns);
            if (columns == null || columns.Count <= 0)
            {
                return false;
            }

            //创建生成Excel对象
            HSSFWorkbook workbook = LoadWorkbook("");
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int index = 0;

            //表头样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Indigo.Index;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //加粗
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            foreach(DataTable dtData in dsData.Tables)
            {
                //工作簿
                sheet = workbook.CreateSheet(dtData.TableName);
                #region 标题行
                //标题行
                row = sheet.CreateRow(0);
                index = 0;
                foreach (ExportColumn col in columns)
                {
                    cell = row.CreateCell(index);
                    cell.CellStyle = cellStyle;
                    cell.SetCellValue(col.Description);
                    //设置列宽
                    sheet.SetColumnWidth(index, col.ColumnWidth * 256);
                    index++;
                }
                #endregion

                #region 数据行
                //数据行
                for (int i = 0; i < dtData.Rows.Count; i++)//所有数据行
                {
                    row = sheet.CreateRow(i + 1);
                    index = 0;
                    foreach (ExportColumn col in columns)//所有要导出的列
                    {
                        if (dtData.Columns.Contains(col.ColumnName))
                        {
                            row.CreateCell(index).SetCellValue(dtData.Rows[i][col.ColumnName] + "");
                        }
                        else
                        {
                            row.CreateCell(index).SetCellValue("");
                        }
                        index++;
                    }
                }
                #endregion
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] bs = ms.ToArray();
                    fs.Write(bs, 0, bs.Length);
                }
            }
            return true;
        }
        /// <summary>
        /// 将ListRow的数据导出到Excel(在客户端获取数据)
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <param name="rows">要导出的数据集</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportListRowToExcel(List<ExportColumn> columns, List<ExportRow> rows, string sheetName, string fileName)
        {
            //获取要导出的列并排序
            columns = GetExportColumns(columns);
            if(columns==null||columns.Count<=0)
            {
                return false;
            }

            //创建生成Excel对象
            HSSFWorkbook workbook = LoadWorkbook("");
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow row = null;
            ICell cell = null;
            int index = 0;

            #region 标题行
            //标题行
            row = sheet.CreateRow(0);

            //表头样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Indigo.Index;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //加粗
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            index = 0;
            foreach (ExportColumn col in columns)
            {
                cell = row.CreateCell(index);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(col.Description);
                //设置列宽
                sheet.SetColumnWidth(index, col.ColumnWidth * 256);
                index++;
            }
            #endregion

            #region 数据行
            ExportColumn dataColumn = null;
            //数据行
            for (int i = 0; i < rows.Count; i++)//所有数据行
            {
                row = sheet.CreateRow(i + 1);
                index = 0;
                dataColumn = null;
                foreach (ExportColumn col in columns)//所有要导出的列
                {
                    dataColumn = rows[i].Columns.Where(d => d.ColumnName == col.ColumnName).FirstOrDefault();
                    if (dataColumn!=null)
                    {
                        row.CreateCell(index).SetCellValue(dataColumn.DataValue + "");
                    }
                    else
                    {
                        row.CreateCell(index).SetCellValue("");
                    }
                    index++;
                }
            }
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] bs = ms.ToArray();
                    fs.Write(bs, 0, bs.Length);
                }
            }
            return true;
        }

        /// <summary>
        /// 根据模板将数据导出到Excel
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="templateSheetName">模板中工作簿的名称</param>
        /// <param name="dicColumns">要导出的列集合(如果为空默认导出所有已配置的列,字典键：类别)</param>
        /// <param name="dicData">要导出的数据集(字典键值：类别)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportDataToExcelByTemplate(string templatePath, Dictionary<string, List<ExportColumn>> dicColumns, Dictionary<string, DataTable> dicData, string sheetName, string fileName)
        {
            //创建excel模板对象
            HSSFWorkbook workbook = LoadWorkbook(templatePath);
            if (workbook == null)
            {
                return false;
            }
            //获取模板工作簿
            ISheet sheetTemplate = workbook.GetSheetAt(0);
            workbook.FirstVisibleTab = 0;

            #region 替换模板值
            string cellTemplate = "";
            object cellValue = "";
            int lineCount = 0;//明细行总数
            int lineHeaderRowIndex = 0;//明细行头行索引
            int k = 0;
            DataRow dr = null;
            //替换模板的值
            foreach (KeyValuePair<string, List<ExportColumn>> item in dicColumns)
            {
                #region 验证模板内容
                //如果数据不存在,跳过
                if (!dicData.ContainsKey(item.Key))
                {
                    continue;
                }
                #endregion

                #region 分类进行替换
                //分类进行替换
                if (item.Key.EndsWith("Line"))
                {
                    #region 替换明细值
                    //变量
                    lineHeaderRowIndex = GetTemplateRowIndexByCellCommentString(sheetTemplate, item.Key);
                    if (lineHeaderRowIndex < 0)
                    {
                        lineHeaderRowIndex = GetTemplateRowIndexByCellValue(sheetTemplate, "行号");
                    }
                    if (lineHeaderRowIndex < 0) continue;
                    lineCount = dicData[item.Key].Rows.Count;
                    //创建明细行模板
                    CreateLineTemplate(sheetTemplate, lineHeaderRowIndex + 2, lineCount);
                    //替换明细行的值
                    k = 0;
                    dr = null;
                    for (int i = lineHeaderRowIndex + 1; i < lineHeaderRowIndex + lineCount + 1; i++)
                    {
                        dr = dicData[item.Key].Rows[k];
                        //替换相应的值
                        foreach (ExportColumn col in dicColumns[item.Key])
                        {
                            cellTemplate = "{$" + col.ColumnName + "}";
                            cellValue = dr[col.ColumnName];
                            ReplaceCellValueByTemplateStr(sheetTemplate, cellTemplate, cellValue, col.DataType, i, i);
                        }
                        k++;
                    }
                    #endregion
                }
                else
                {
                    #region 替换非明细值
                    //替换非明细值
                    foreach (ExportColumn col in item.Value)
                    {
                        cellTemplate = "{$" + col.ColumnName + "}";
                        cellValue = GetTemplateStrValue(dicData[item.Key], col.ColumnName);
                        ReplaceCellValueByTemplateStr(sheetTemplate, cellTemplate, cellValue, col.DataType, sheetTemplate.FirstRowNum, sheetTemplate.LastRowNum);
                    }//end foreach (ExportColumn col in item.Value)
                    #endregion
                }
                #endregion
            }
            #endregion

            #region 生成Excel文件
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(file);
                file.Close();
            }
            #endregion

            return true;
        }
        /// <summary>
        /// 根据模板将数据导出到Excel(多个工作簿)
        /// </summary>
        /// <param name="templatePath">excel模板路径(全路径)</param>
        /// <param name="dicColumns">要导出的列集合(如果为空默认导出所有已配置的列,字典键：类别)</param>
        /// <param name="dicDicData">要导出的数据集(字典键值：类别)</param>
        /// <param name="sheetName">要导出的工作簿名称</param>
        /// <param name="fileName">导出的文件名称(包括路径)</param>
        /// <returns></returns>
        public virtual bool ExportDataToExcelByTemplate(string templatePath, Dictionary<string, List<ExportColumn>> dicColumns, Dictionary<string, Dictionary<string, DataTable>> dicDicData, string sheetName, string fileName)
        {
            //创建excel模板对象
            HSSFWorkbook workbook = LoadWorkbook(templatePath);
            if (workbook == null)
            {
                return false;
            }
            //获取模板工作簿
            HSSFSheet sheetTemplate = (HSSFSheet)workbook.GetSheetAt(0);
            foreach (KeyValuePair<string, Dictionary<string, DataTable>> itemSheet in dicDicData)
            {
                sheetTemplate.CopyTo(workbook, itemSheet.Key, true, true);//复制模板工作簿为新的工作簿
            }

            //变量
            string cellTemplate = "";
            object cellValue = "";
            int lineCount = 0;//明细行总数
            int lineHeaderRowIndex = 0;//明细行头行索引
            int k = 0;
            DataRow dr = null;

            //多个工作簿
            ISheet sheet = null;
            Dictionary<string, DataTable> dicData = null;
            foreach (KeyValuePair<string, Dictionary<string, DataTable>> itemSheet in dicDicData)
            {
                sheet = workbook.GetSheet(itemSheet.Key);//新的工作簿
                dicData = itemSheet.Value;//工作簿数据字典

                #region 替换模板值
                cellTemplate = "";
                cellValue = "";
                lineCount = 0;//明细行总数
                lineHeaderRowIndex = 0;//明细行头行索引
                k = 0;
                dr = null;
                //替换模板的值
                foreach (KeyValuePair<string, List<ExportColumn>> item in dicColumns)
                {
                    #region 验证模板内容
                    //如果数据不存在,跳过
                    if (!dicData.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    #endregion

                    #region 分类进行替换
                    //分类进行替换
                    if (item.Key.EndsWith("Line"))
                    {
                        #region 替换明细值
                        //变量
                        lineHeaderRowIndex = GetTemplateRowIndexByCellCommentString(sheet, item.Key);
                        if (lineHeaderRowIndex < 0)
                        {
                            lineHeaderRowIndex = GetTemplateRowIndexByCellValue(sheet, "行号");
                        }
                        if (lineHeaderRowIndex < 0) continue;
                        lineCount = dicData[item.Key].Rows.Count;
                        //创建明细行模板
                        CreateLineTemplate(sheet, lineHeaderRowIndex + 2, lineCount);
                        //替换明细行的值
                        k = 0;
                        dr = null;
                        for (int i = lineHeaderRowIndex + 1; i < lineHeaderRowIndex + lineCount + 1; i++)
                        {
                            dr = dicData[item.Key].Rows[k];
                            //替换相应的值
                            foreach (ExportColumn col in dicColumns[item.Key])
                            {
                                cellTemplate = "{$" + col.ColumnName + "}";
                                cellValue = dr[col.ColumnName];
                                ReplaceCellValueByTemplateStr(sheet, cellTemplate, cellValue, col.DataType, i, i);
                            }
                            k++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region 替换非明细值
                        //替换非明细值
                        foreach (ExportColumn col in item.Value)
                        {
                            cellTemplate = "{$" + col.ColumnName + "}";
                            cellValue = GetTemplateStrValue(dicData[item.Key], col.ColumnName);
                            ReplaceCellValueByTemplateStr(sheet, cellTemplate, cellValue, col.DataType, sheet.FirstRowNum, sheet.LastRowNum);
                        }//end foreach (ExportColumn col in item.Value)
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }

            workbook.RemoveSheetAt(0);//删除模板工作簿
            #region 生成Excel文件
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(file);
                file.Close();
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 获取要导出数据
        /// </summary>
        /// <param name="queryCondictions">查询条件或语句(自定义)</param>
        /// <returns></returns>
        public abstract object GetExportData(object queryCondictions);
        #endregion
        #endregion

        #region 其他方法
        /// <summary>
        /// 替换模板工作簿中的标识字符串
        /// </summary>
        /// <param name="sheetTemplate">模板工作簿</param>
        /// <param name="cellTemplate">单元格标识字符串</param>
        /// <param name="cellValue">替换的值</param>
        /// <param name="dataType">值类型</param>
        private void ReplaceCellValueByTemplateStr(ISheet sheetTemplate, string cellTemplate, object cellValue, string dataType, int rowStartIndex, int rowEndIndex)
        {
            //rowStartIndex = sheetTemplate.FirstRowNum;
            //rowEndIndex = sheetTemplate.LastRowNum;
            IRow row = null;
            ICell cell = null;
            int cellStartIndex = 0;
            int cellEndIndex = 0;
            string cellTemplateTemp = "";
            bool isReplace = false;
            //行范围控制
            for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            {
                row = sheetTemplate.GetRow(rowIndex);
                if (row == null) continue;
                cellStartIndex = row.FirstCellNum;
                cellEndIndex = row.LastCellNum;
                isReplace = false;
                //单元格范围控制
                for (int i = cellStartIndex; i < cellEndIndex; i++)
                {
                    cell = row.GetCell(i);
                    if (cell == null) continue;

                    //当前要替换的标识字符串
                    cellTemplateTemp = cell.ToString();
                    if (string.IsNullOrEmpty(cellTemplateTemp)) continue;

                    //如果找到与cellTemplate相同的字符串则替换
                    if (string.Compare(cellTemplateTemp, cellTemplate, true) == 0)
                    {
                        cell.SetCellValue(cellValue + "");
                        isReplace = true;
                        break;
                    }
                }//end for (int i = cellStartIndex; i < cellEndIndex; i++)
                if(isReplace)
                {
                    break;
                }
            }//end for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
        }
        /// <summary>
        /// 获取模板标识字符串的值
        /// </summary>
        /// <param name="dtData">数据集合</param>
        /// <param name="columnName">列</param>
        /// <returns></returns>
        private object GetTemplateStrValue(DataTable dtData,string columnName)
        {
            if (dtData == null || dtData.Rows.Count <= 0) return "";
            return dtData.Rows[0][columnName];
        }
        /// <summary>
        /// 根据单元格值返回其所在的行号
        /// </summary>
        /// <param name="sheetTemplate">模板工作簿</param>
        /// <param name="cellVale">单元格值</param>
        /// <returns>找到返回行号,找不到返回-1</returns>
        private int GetTemplateRowIndexByCellValue(ISheet sheetTemplate, string cellVale)
        {
            int rowStartIndex = sheetTemplate.FirstRowNum;
            int rowEndIndex = sheetTemplate.LastRowNum;
            IRow row = null;
            ICell cell = null;
            int cellStartIndex = 0;
            int cellEndIndex = 0;
            string cellValueTemp = "";
            bool isIndex = false;
            int index = -1;
            //行范围控制
            for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            {
                row = sheetTemplate.GetRow(rowIndex);
                if (row == null) continue;
                cellStartIndex = row.FirstCellNum;
                cellEndIndex = row.LastCellNum;
                isIndex = false;
                //单元格范围控制
                for (int i = cellStartIndex; i < cellEndIndex; i++)
                {
                    cell = row.GetCell(i);
                    if (cell == null) continue;

                    //当前单元格值
                    cellValueTemp = cell.ToString();
                    if (string.IsNullOrEmpty(cellValueTemp)) continue;

                    //找到当前值
                    if (string.Compare(cellValueTemp, cellVale, true) == 0)
                    {
                        index=rowIndex;
                        isIndex = true;
                        break;
                    }
                }//end for (int i = cellStartIndex; i < cellEndIndex; i++)
                if (isIndex)
                {
                    break;
                }
            }//end for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            return index;
        }
        /// <summary>
        /// 根据单元格值返回其所在的行号
        /// </summary>
        /// <param name="sheetTemplate">模板工作簿</param>
        /// <param name="cellCommentString">单元格批注</param>
        /// <returns>找到返回行号,找不到返回-1</returns>
        private int GetTemplateRowIndexByCellCommentString(ISheet sheetTemplate, string cellCommentString)
        {
            int rowStartIndex = sheetTemplate.FirstRowNum;
            int rowEndIndex = sheetTemplate.LastRowNum;
            IRow row = null;
            ICell cell = null;
            int cellStartIndex = 0;
            int cellEndIndex = 0;
            string cellCommentStringTemp = "";
            bool isIndex = false;
            int index = -1;
            //行范围控制
            for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            {
                row = sheetTemplate.GetRow(rowIndex);
                if (row == null) continue;
                cellStartIndex = row.FirstCellNum;
                cellEndIndex = row.LastCellNum;
                isIndex = false;
                //单元格范围控制
                for (int i = cellStartIndex; i < cellEndIndex; i++)
                {
                    cell = row.GetCell(i);
                    if (cell == null || cell.CellComment == null) continue;

                    //当前单元格批注
                    cellCommentStringTemp = cell.CellComment.String == null ? "" : cell.CellComment.String.String;
                    if (string.IsNullOrEmpty(cellCommentStringTemp)) continue;

                    //找到当前值
                    if (string.Compare(cellCommentStringTemp, cellCommentString, true) == 0)
                    {
                        index = rowIndex;
                        isIndex = true;
                        break;
                    }
                }//end for (int i = cellStartIndex; i < cellEndIndex; i++)
                if (isIndex)
                {
                    break;
                }
            }//end for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            return index;
        }
        /// <summary>
        /// 创建明细行模板
        /// </summary>
        /// <param name="sheetTemplate">模板工作簿</param>
        /// <param name="lineStartIndex">开始行号</param>
        /// <param name="lineCount">总行数</param>
        private void CreateLineTemplate(ISheet sheetTemplate, int lineStartIndex, int lineCount)
        {
            //if (lineStartIndex > sheetTemplate.LastRowNum)
            //{
            //    return;
            //}
            //复制lineCount-1行明细行
            IRow templateRow = sheetTemplate.GetRow(lineStartIndex - 1);//模板行
            IRow targetRow = null;
            for (int i = lineStartIndex; i < lineStartIndex + lineCount - 1; i++)
            {
                targetRow = templateRow.CopyRowTo(i);
                targetRow.Height = templateRow.Height;
            }
        }

        /// <summary>
        /// 获取要导出的列集合并进行排序
        /// </summary>
        /// <param name="columns">要导出的列集合(如果为空默认导出所有已配置的列)</param>
        /// <returns></returns>
        private List<ExportColumn> GetExportColumns(List<ExportColumn> columns)
        {
            if (columns == null || columns.Count <= 0)
            {
                columns = GetExportColumnList();//获取所有列
                if (columns == null || columns.Count <= 0)
                {
                    return null;
                }
            }
            //按列号进行排序
            columns = (from d in columns orderby d.ColumnNumber select d).ToList<ExportColumn>();
            return columns;
        }

        /// <summary>
        /// xml模板文档对象
        /// </summary>
        /// <param name="schemaPath">xml模板路径</param>
        /// <returns></returns>
        private XmlDocument LoadXmlDocument(string schemaPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (!string.IsNullOrEmpty(schemaPath) && File.Exists(schemaPath))
            {
                xmlDoc.Load(schemaPath);
            }
            return xmlDoc;
        }
        /// <summary>
        /// Excel模板工作簿对象
        /// </summary>
        /// <param name="templatePath">excel模板路径</param>
        /// <returns></returns>
        private HSSFWorkbook LoadWorkbook(string templatePath)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            if (!string.IsNullOrEmpty(templatePath) && File.Exists(templatePath))
            {
                using (FileStream stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
                {
                    workBook = new HSSFWorkbook(stream);
                }
            }
            return workBook;
        }
        #endregion 
    }
}
