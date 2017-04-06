using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ThinkNet.Utility;

namespace ThinkNet.Infrastructure.Core
{
    public class ExcelExport
    {
        /// <summary>
        /// 生成excel
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="saveFolder">保存文件夹路径</param>
        /// <param name="sheetName">工作薄名</param>
        /// <param name="ds">数据源</param>
        public static string ExecuteExcelExport(string templatePath, string saveFolder, string sheetName, DataSet ds)
        {
            try
            {
                HSSFWorkbook hssfworkbook = InitializeWorkbook(templatePath);

                if (hssfworkbook == null)
                {
                    return null;
                }

                ISheet sheet1 = hssfworkbook.GetSheet(sheetName);

                //删除其他工作薄
                int sheetIndex = hssfworkbook.GetSheetIndex(sheet1);
                if (hssfworkbook.NumberOfSheets > 1)
                {
                    for (int i = hssfworkbook.NumberOfSheets - 1; i >= 0; i--)
                    {
                        if (i != sheetIndex)
                        {
                            hssfworkbook.RemoveSheetAt(i);
                        }
                    }
                }
                hssfworkbook.FirstVisibleTab = 0;

                //相关参数
                int columnHeaderIndex = GetGridRowIndex(sheet1, "行号");
                if (columnHeaderIndex <= 0)
                {
                    columnHeaderIndex = GetGridRowIndex(sheet1, "序号");
                }
                int gridRowCount = ds.Tables["Detail"].Rows.Count;
                DataTable dtEnterprise = ds.Tables["Enterprise"];
                DataTable dtMain = ds.Tables["Main"];
                DataTable dtDetail = ds.Tables["Detail"];

                //重新构造工作薄
                CreateSheet(sheet1, columnHeaderIndex + 2, gridRowCount);

                decimal totalMoney = 0;//总金额
                decimal totalVolume = 0;//总体积
                int totalBoxAmount = 0;//总箱数
                int totalAmount = 0;//总数量
                //企业信息
                SetCellValueByTemplateStr(sheet1, "{$EnterpriseName}", dtEnterprise.Rows[0]["EnterpriseName"]);
                SetCellValueByTemplateStr(sheet1, "{$EnterprisePhone}", dtEnterprise.Rows[0]["Telephone"]);
                SetCellValueByTemplateStr(sheet1, "{$EnterpriseFax}", dtEnterprise.Rows[0]["Fax"]);
                SetCellValueByTemplateStr(sheet1, "{$EnterpriseMobile}", dtEnterprise.Rows[0]["Mobile"]);
                //数据
                for (int i = 0; i < dtMain.Columns.Count; i++)
                {
                    SetCellValueByTemplateStr(sheet1, "{$" + dtMain.Columns[i].ColumnName + "}", dtMain.Rows[0][i]);
                }
                //表明细
                for (int i = 0; i < dtDetail.Rows.Count; i++)
                {
                    for (int j = 0; j < dtDetail.Columns.Count; j++)
                    {
                        SetCellValueByTemplateStr(sheet1, "{$" + dtDetail.Columns[j].ColumnName + "}", dtDetail.Rows[i][j], columnHeaderIndex + 1, gridRowCount, true);
                    }
                    totalMoney+=DataTypeConvert.ToDecimal(dtDetail.Rows[i]["Money"]);
                    totalBoxAmount+=DataTypeConvert.ToInt32(dtDetail.Rows[i]["BoxAmount"]);
                    totalAmount += DataTypeConvert.ToInt32(dtDetail.Rows[i]["Amount"]);
                    totalVolume += DataTypeConvert.ToDecimal(dtDetail.Rows[i]["OutVolumeDisplay"]);
                }
                //合计
                SetCellValueByTemplateStr(sheet1, "{$TotalMoneyCH}",ConvertChIAmountHelper.ConvertSum(totalMoney + ""));//大写
                SetCellValueByTemplateStr(sheet1, "{$TotalBoxAmount}", "共:" + totalBoxAmount + "箱");//总箱数
                SetCellValueByTemplateStr(sheet1, "{$TotalAmount}", totalAmount);//总数量
                SetCellValueByTemplateStr(sheet1, "{$TotalMoney}", totalMoney);//总金额
                SetCellValueByTemplateStr(sheet1, "{$TotalVolume}", totalVolume.ToString("f2"));//总体积

                //生成文件
                if (!System.IO.Directory.Exists(saveFolder))
                {
                    System.IO.Directory.CreateDirectory(saveFolder);
                }

                string savePath = saveFolder + dtMain.Rows[0]["OrderCode"] + ".xls";
                using (FileStream file = new FileStream(savePath, FileMode.Create))
                {
                    hssfworkbook.Write(file);
                    file.Close();
                }
                return dtMain.Rows[0]["OrderCode"] + ".xls";
            }
            catch (Exception e)
            {
            }
            return "";
        }

        #region 模板导出基本方法
        /// <summary>
        /// 初始化Excel模板
        /// </summary>
        /// <param name="path">模板路径</param>
        /// <returns></returns>
        private static HSSFWorkbook InitializeWorkbook(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            file.Close();
            return hssfworkbook;
        }
        /// <summary>
        /// 根据Excel模板单元格内容，找出单元格，并设置单元格的值
        /// </summary>
        /// <param name="sheet">ExcelSheet</param>
        /// <param name="cellTemplateValue">模板内容</param>
        /// <param name="cellFillValue">单元格值</param>
        ///  <param name="rowFristIndex">表行索引，配合conNextRow=true使用</param>
        ///  <param name="rowCount">表总行数，配合conNextRow=true使用</param>
        ///  <param name="conNextRow">是否承接下一行，即：填充下一行单元格模板内容,填写表明细数据时才用到</param>

        private static void SetCellValueByTemplateStr(ISheet sheet, string cellTemplateValue, object cellFillValue, int rowFristIndex = 0, int rowCount = 0, bool conNextRow = false)
        {
            int rowStartIndex = sheet.FirstRowNum;
            int rowEndIndex = sheet.LastRowNum;
            for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            {
                int cellStartIndex = sheet.GetRow(rowIndex).FirstCellNum;
                int cellEndIndex = sheet.GetRow(rowIndex).LastCellNum;
                for (int i = cellStartIndex; i < cellEndIndex; i++)
                {
                    ICell cell = sheet.GetRow(rowIndex).GetCell(i);
                    if (cell != null)
                    {
                        if (cell.CellType == CellType.String)
                        {
                            string strCellValue = sheet.GetRow(rowIndex).GetCell(i).StringCellValue;
                            if (string.Compare(strCellValue, cellTemplateValue, true) == 0)
                            {
                                Type type = cellFillValue.GetType();
                                switch (type.ToString())
                                {
                                    case "System.String":
                                        string strValue = cellFillValue.ToString();
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(strValue);
                                        break;
                                    case "System.Int32":
                                        int intValue32 = Convert.ToInt32(cellFillValue.ToString());
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(intValue32);
                                        break;
                                    case "System.Int64":
                                        long intValue64 = Convert.ToInt64(cellFillValue.ToString());
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(intValue64);
                                        break;
                                    case "System.Decimal":
                                        double decimalValue = Convert.ToDouble(cellFillValue.ToString());
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(decimalValue);
                                        break;
                                    case "System.DateTime":
                                        DateTime dateTimeValue = Convert.ToDateTime(cellFillValue.ToString());
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(dateTimeValue);
                                        break;
                                    case "System.DBNull":
                                        string nullValue=cellFillValue.ToString();
                                        sheet.GetRow(rowIndex).GetCell(i).SetCellValue(nullValue);
                                        break;
                                }
                                if (conNextRow && rowIndex < rowFristIndex + rowCount - 1)//如果承接下一行，则设置下一行单元格里的模板内容
                                {
                                    sheet.GetRow(rowIndex + 1).GetCell(i).SetCellValue(cellTemplateValue);
                                    sheet.GetRow(rowIndex + 1).GetCell(i).SetCellType(CellType.String);
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 根据表明细行数生成虚拟工作薄
        /// </summary>
        /// <param name="sheet">ExcelSheet</param>
        /// <param name="footIndex">表尾索引</param>
        /// <param name="gridRowCount">表行数</param>
        /// <param name="gridColumnCount">表列数</param>
        private static void CreateSheet(ISheet sheet, int footStartIndex, int gridRowCount)
        {
            if (footStartIndex > sheet.LastRowNum)
            {
                return;
            }
            int footEndIndex = sheet.LastRowNum;
            //批量移动行
            sheet.ShiftRows(footStartIndex, footEndIndex, gridRowCount - 1);
            //插入行
            IRow sourceRow = sheet.GetRow(footStartIndex - 1);
            int num = 0;
            for (int i = footStartIndex; i < footStartIndex + gridRowCount - 1; i++)
            {
                num++;
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;

                targetRow = sheet.CreateRow(i);

                for (int j = sourceRow.FirstCellNum; j < sourceRow.LastCellNum; j++)
                {
                    sourceCell = sourceRow.GetCell(j);
                    if (sourceCell == null)
                        continue;
                    targetCell = targetRow.CreateCell(j);

                    //targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }
        }
        /// <summary>
        /// 获取表头索引号
        /// </summary>
        /// <param name="sheet">ExcelSheet</param>
        /// <param name="cellTemplateValue">模板内容</param>
        /// <returns></returns>
        private static int GetGridRowIndex(ISheet sheet, string cellTemplateValue)
        {
            int rowStartIndex = sheet.FirstRowNum;
            int rowEndIndex = sheet.LastRowNum;
            for (int rowIndex = rowStartIndex; rowIndex < rowEndIndex + 1; rowIndex++)
            {
                int cellStartIndex = sheet.GetRow(rowIndex).FirstCellNum;
                int cellEndIndex = sheet.GetRow(rowIndex).LastCellNum;
                for (int i = cellStartIndex; i < cellEndIndex; i++)
                {
                    ICell cell = sheet.GetRow(rowIndex).GetCell(i);
                    if (cell != null)
                    {
                        if (cell.CellType == CellType.String)
                        {
                            string strCellValue = sheet.GetRow(rowIndex).GetCell(i).StringCellValue;
                            if (string.Compare(strCellValue, cellTemplateValue, true) == 0)
                            {
                                return rowIndex;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        #endregion
    }
}
