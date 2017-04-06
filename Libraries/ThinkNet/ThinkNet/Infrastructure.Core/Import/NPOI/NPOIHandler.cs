using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 继承此类只要重写一下以下方法即可：
    /// ValidData：验证数据是否合格
    /// ImportToTargetTable：导入数据到对应的表中
    /// </summary>
    public class NPOIHandler : IImportHandler
    {
        #region 属    性
        /// <summary>
        /// 内存数据
        /// </summary>
        public static IDictionary<String, List<RowInfo>> CachePool = new Dictionary<String, List<RowInfo>>();

        /// <summary>
        /// 缓存中的Key
        /// </summary>
        public String UnquieKey { get; set; }
        /// <summary>
        /// 导入模板结构Xml对象
        /// </summary>
        public XmlDocument DataTableSchema { get; set; }
        /// <summary>
        /// 导入模板结构Xml对象，根节目
        /// </summary>
        public XmlNode DataTableHeader = null;
        //private IList<RowInfo> m_DataCollection = null;
        /// <summary>
        /// 当前的临时数据
        /// </summary>
        public List<RowInfo> DataCollection
        {
            get
            {
                //if (m_DataCollection == null)
                //    m_DataCollection = CachedManager.GetCacheData<IList<RowInfo>>(UnquieKey, "ImportHelper");
                //return m_DataCollection;
                if (!CachePool.ContainsKey(UnquieKey)) return null;
                return CachePool[UnquieKey] as List<RowInfo>;
            }
        }

        /// <summary>
        /// 将修改后的数据保存到缓存中去
        /// </summary>
        public void SaveDataCollection()
        {
            //CachedManager.CacheData(UnquieKey, m_DataCollection, DateTime.Now.AddHours(1), "ImportHelper");
        }

        public bool IsAllValid
        {
            get
            {
                if (DataCollection != null)
                {
                    return !((from r in DataCollection where r.IsValid == false select r).ToList<RowInfo>().Count > 0);
                }
                return false;
            }
        }
        /// <summary>
        /// 针对快速导入
        /// </summary>
        /// <param name="columns">逗号分隔的列(可为空)</param>
        /// <returns></returns>
        public bool IsExistError(string columns)
        {
            if (DataCollection != null)
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(columns))
                {
                    list.AddRange(columns.Split(','));
                    foreach (RowInfo row in DataCollection)
                    {
                        foreach (ColumnInfo col in row.Columns)
                        {
                            if (list.Contains(col.ColumnName)&&col.VerificationType == VerificationTypes.Error)
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RowInfo row in DataCollection)
                    {
                        foreach (ColumnInfo col in row.Columns)
                        {
                            if (col.VerificationType == VerificationTypes.Error)
                            {
                                return true;
                            }
                        }
                    }
                }//end if (!string.IsNullOrEmpty(columns))
            }
            return false;
        }
        /// <summary>
        /// 针对快速导入
        /// </summary>
        /// <param name="columns">逗号分隔的列(可为空)</param>
        /// <returns></returns>
        public bool IsColumnValid(string columns)
        {
            if (DataCollection != null)
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(columns))
                {
                    list.AddRange(columns.Split(','));
                    foreach (RowInfo row in DataCollection)
                    {
                        foreach (ColumnInfo col in row.Columns)
                        {
                            if (list.Contains(col.ColumnName) && (col.VerificationType == VerificationTypes.Error 
                                || col.VerificationType == VerificationTypes.Exist
                                || col.VerificationType == VerificationTypes.MultiExist
                                || col.VerificationType == VerificationTypes.NotExist))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RowInfo row in DataCollection)
                    {
                        foreach (ColumnInfo col in row.Columns)
                        {
                            if (col.VerificationType == VerificationTypes.Error 
                                || col.VerificationType == VerificationTypes.Exist
                                || col.VerificationType == VerificationTypes.MultiExist
                                || col.VerificationType == VerificationTypes.NotExist)
                            {
                                return false;
                            }
                        }
                    }
                }//end if (!string.IsNullOrEmpty(columns))
            }
            return true;
        }

        /// <summary>
        /// 目标表名
        /// </summary>
        public String TableName { get; set; }
        /// <summary>
        /// 目标表的主健
        /// </summary>
        public String KeyColumn { get; set; }
        /// <summary>
        /// 数据源中的开始行
        /// </summary>
        public int DataStartRow { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaPath"></param>
        /// <param name="unquieKey"></param>
        public NPOIHandler(String schemaPath, string unquieKey)
        {
            UnquieKey = unquieKey;
            DataTableSchema = new XmlDocument();
            DataTableSchema.Load(schemaPath);

            DataTableHeader = DataTableSchema.SelectSingleNode("DataTableSchema/Columns");
            TableName = (DataTableHeader.Attributes["TableName"] == null) ? string.Empty : DataTableHeader.Attributes["TableName"].Value;
            KeyColumn = (DataTableHeader.Attributes["KeyColumn"] == null) ? string.Empty : DataTableHeader.Attributes["KeyColumn"].Value;
            DataStartRow = (DataTableHeader.Attributes["DataStartRow"] == null) ? 1 : Convert.ToInt32(DataTableHeader.Attributes["DataStartRow"].Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaPath"></param>
        public NPOIHandler(String schemaPath)
            : this(schemaPath, Guid.NewGuid().ToString())
        {
        }
        #endregion

        #region 获取表定义结构
        /// <summary>
        /// 获取导入表定义结构 主要用与客户端解释表结构
        /// </summary>
        /// <param name="tableSchemaPath"></param>
        /// <returns></returns>
        public static ArrayList GetTableSchema(string tableSchemaPath)
        {
            ArrayList list = new ArrayList();
            XmlDocument schemaDoc = new XmlDocument();
            schemaDoc.Load(tableSchemaPath);
            XmlNode tableNode = schemaDoc.SelectSingleNode("DataTableSchema/Columns");
            if (tableNode != null)
            {
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    var columnInfo = new
                    {
                        ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value,
                        ListWidth = node.Attributes["ListWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ListWidth"].Value),
                        ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value),
                        LockLeft = node.Attributes["LockLeft"] == null ? false : Convert.ToBoolean(node.Attributes["LockLeft"].Value),
                        Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value
                    };
                    list.Add(columnInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取导入表定义结构 主要用与客户端解释表结构
        /// </summary>
        /// <param name="tableSchemaPath"></param>
        /// <returns></returns>
        public static List<ColumnInfo> GetTableSchemaToColumnInfo(string tableSchemaPath)
        {
            XmlDocument schemaDoc = new XmlDocument();
            schemaDoc.Load(tableSchemaPath);
            XmlNode tableNode = schemaDoc.SelectSingleNode("DataTableSchema/Columns");
            List<ColumnInfo> list = new List<ColumnInfo>();
            if (tableNode != null)
            {
                ColumnInfo mColumnInfo = null;
                foreach (XmlNode node in tableNode.ChildNodes)
                {
                    mColumnInfo = new ColumnInfo();
                    mColumnInfo.ColumnName = node.Attributes["Name"] == null ? String.Empty : node.Attributes["Name"].Value;
                    mColumnInfo.ListWidth = node.Attributes["ListWidth"] == null ? 100 : Convert.ToInt32(node.Attributes["ListWidth"].Value);
                    mColumnInfo.ColumnNumber = node.Attributes["ColumnNumber"] == null ? 0 : Convert.ToInt32(node.Attributes["ColumnNumber"].Value);
                    mColumnInfo.LockLeft = node.Attributes["LockLeft"] == null ? false : Convert.ToBoolean(node.Attributes["LockLeft"].Value);
                    mColumnInfo.Description = node.Attributes["Description"] == null ? String.Empty : node.Attributes["Description"].Value;
                    list.Add(mColumnInfo);
                }
            }
            return list;
        }
        #endregion

        #region 获取列对象
        /// <summary>
        /// 通过列名获取列临时数据对象
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        protected ColumnInfo GetColumnInfo(String description)
        {
            ColumnInfo columnInfo = new ColumnInfo();
            foreach (XmlNode item in DataTableHeader.ChildNodes)
            {
                if (item.Attributes["Description"] != null && item.Attributes["Description"].Value.Trim().ToLower() == description.Trim().ToLower())
                {
                    columnInfo.DataType = ((item.Attributes["Type"] == null) ? String.Empty : item.Attributes["Type"].Value);
                    columnInfo.ColumnName = ((item.Attributes["Name"] == null) ? String.Empty : item.Attributes["Name"].Value);
                    columnInfo.Length = ((item.Attributes["Length"] == null) ? String.Empty : item.Attributes["Length"].Value);
                    columnInfo.Unique = ((item.Attributes["Unique"] == null) ? false : Convert.ToBoolean(item.Attributes["Unique"].Value));
                    columnInfo.ColumnNumber = ((item.Attributes["ColumnNumber"] == null) ? 0 : Convert.ToInt32(item.Attributes["ColumnNumber"].Value));
                    columnInfo.Description = description;
                }
            }
            return columnInfo;
        }

        /// <summary>
        /// 通过列顺序号获取列临时数据对象
        /// </summary>
        /// <param name="columnNo"></param>
        /// <returns></returns>
        protected ColumnInfo GetColumnInfo(int columnNo)
        {
            ColumnInfo columnInfo = new ColumnInfo();
            XmlNode item = DataTableSchema.SelectSingleNode(string.Format("DataTableSchema/Columns/Column[@ColumnNumber='{0}']", columnNo));
            if (item != null)
            {
                columnInfo.DataType = ((item.Attributes["Type"] == null) ? String.Empty : item.Attributes["Type"].Value);
                columnInfo.ColumnName = ((item.Attributes["Name"] == null) ? String.Empty : item.Attributes["Name"].Value);
                columnInfo.Length = ((item.Attributes["Length"] == null) ? String.Empty : item.Attributes["Length"].Value);
                columnInfo.Unique = ((item.Attributes["Unique"] == null) ? false : Convert.ToBoolean(item.Attributes["Unique"].Value));
                columnInfo.ColumnNumber = ((item.Attributes["ColumnNumber"] == null) ? 0 : Convert.ToInt32(item.Attributes["ColumnNumber"].Value));
                columnInfo.Description = ((item.Attributes["Description"] == null) ? String.Empty : item.Attributes["Description"].Value);
            }
            else
                return null;
            return columnInfo;
        }
        #endregion

        /// <summary>
        /// 根据unquieKey，获取临时表中的数据
        /// </summary>
        /// <param name="unquieKey"></param>
        /// <returns></returns>
        public static List<RowInfo> GetTmpDataByUnquieKey(string unquieKey)
        {
            if (!CachePool.ContainsKey(unquieKey)) return null;
            return CachePool[unquieKey] as List<RowInfo>;
            //var collection = CachedManager.GetCacheData<IList<RowInfo>>(unquieKey, "ImportHelper");
            //return collection;
        }

        /// <summary>
        /// 验证数据是否合格
        /// </summary>
        /// <param name="target"></param>
        public virtual Boolean ValidData(ColumnInfo target)
        {
            if (target == null) return false;
            //target.IsValid = true;//暂时屏蔽
            target.VerificationType = VerificationTypes.Correct;

            if (target.Length.StartsWith("[1"))
            {
                #region 必填校验
                if (string.IsNullOrEmpty(target.OriginalValue))
                {
                    //target.IsValid = false;//暂时屏蔽
                    target.VerificationType = VerificationTypes.Error;
                    target.ErrorMessage = "字段不能为空!";
                    return false;
                }
                #endregion
            }

            string rule = target.Length.Replace("[", "").Replace("]", "");
            var array = rule.Split('-');//长度控制
            if (target.DataType == "System.String")
            {
                #region 验证数据长度是否符合规则
                if (array.Length >= 2)
                {
                    int s = Convert.ToInt32(array[0]);
                    int e = Convert.ToInt32(array[1]);
                    int v = !string.IsNullOrEmpty(target.OriginalValue) ? target.OriginalValue.Length : 0;

                    if (v < s || v > e)
                    {
                        //target.IsValid = false;//暂时屏蔽
                        target.VerificationType = VerificationTypes.Error;
                        target.ErrorMessage = "字符长度不符合要求!";
                        return false;
                    }
                }
                #endregion
            }

            #region 验证数据是否符合类型规则
            if (!string.IsNullOrEmpty(target.OriginalValue))
            {
                switch (target.DataType)
                {
                    case "System.Boolean":
                        Int32 b = 0;
                        if (!Int32.TryParse(target.OriginalValue, out b) || b > 1 || b < 0)
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为0或1类型!";
                            return false;
                        }
                        target.DataValue = b;
                        break;
                    case "System.Int16":
                        Int16 i16 = 0;
                        if (!Int16.TryParse(target.OriginalValue, out i16))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为数字类型!";
                            return false;
                        }
                        target.DataValue = i16;
                        break;
                    case "System.Int32":
                        Int32 i32 = 0;
                        if (!Int32.TryParse(target.OriginalValue, out i32))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为数字类型!";
                            return false;
                        }
                        target.DataValue = i32;
                        break;
                    case "System.Int64":
                        Int64 i64 = 0;
                        if (!Int64.TryParse(target.OriginalValue, out i64))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为数字类型!";
                            return false;
                        }
                        target.DataValue = i64;
                        break;
                    case "System.Decimal":
                        Decimal d = 0;
                        if (!Decimal.TryParse(target.OriginalValue, out d))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为小数类型!";
                            return false;
                        }
                        //小数位数处理，不足补0
                        int precisionLength4d = Convert.ToInt32(array[1]);
                        if (array.Length >= 2 && precisionLength4d > 0)
                        {
                            target.OriginalValue = FormatDecimalPrecision(target.OriginalValue, precisionLength4d);
                            //target.OriginalValue = d.ToString("f" + array[1]);//会四舍五入
                        }
                        target.DataValue = d;
                        break;
                    case "System.Double":
                        Double dob = 0;
                        if (!Double.TryParse(target.OriginalValue, out dob))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为小数类型!";
                            return false;
                        }
                        //小数位数处理，不足补0
                        int precisionLength4dob = Convert.ToInt32(array[1]);
                        if (array.Length >= 2 && precisionLength4dob > 0)
                        {
                            target.OriginalValue = FormatDecimalPrecision(target.OriginalValue, precisionLength4dob);
                            //target.OriginalValue = dob.ToString("f" + array[1]);//会四舍五入
                        }
                        target.DataValue = dob;
                        break;
                    case "System.DateTime":
                        DateTime dateTime = DateTime.Now;
                        if (!DateTime.TryParse(target.OriginalValue, out dateTime))
                        {
                            //target.IsValid = false;//暂时屏蔽
                            target.VerificationType = VerificationTypes.Error;
                            target.ErrorMessage = "必须为时间类型!";
                            return false;
                        }
                        target.DataValue = dateTime;
                        break;
                    case "System.String":
                        target.DataValue = target.OriginalValue;
                        break;
                    default:
                        //target.IsValid = false;//暂时屏蔽
                        target.VerificationType = VerificationTypes.Error;
                        target.ErrorMessage = "不能识别的列类型!";
                        break;
                }
            }
            #endregion

            /*  暂时屏蔽
            if (target.IsValid)
                target.ErrorMessage = string.Empty;

            return target.IsValid;
             * */
            if (target.VerificationType == VerificationTypes.Correct)
            {
                target.ErrorMessage = string.Empty;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 控制小数位数,不足补0，多的截取
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="precisionLength">小数位数</param>
        /// <returns></returns>
        private string FormatDecimalPrecision(string value, int precisionLength)
        {
            string result = value;
            if (precisionLength > 0)
            {
                string[] arrayPrecision = value.Split('.');//小数长度控制
                if (arrayPrecision == null || arrayPrecision.Length <= 1)//没有小数
                {
                    value = value + ".";
                    value = value.PadRight(arrayPrecision[0].Length + 1 + precisionLength, '0');
                    result = value;
                }
                else if (arrayPrecision.Length >= 2)
                {
                    if (arrayPrecision[1].Length < precisionLength)//小数位数不足
                    {
                        value = value.PadRight(value.Length + (precisionLength - arrayPrecision[1].Length), '0');
                        result = value;
                    }
                    else if (arrayPrecision[1].Length > precisionLength)//超出了小数位数
                    {
                        result = arrayPrecision[1].Substring(0, precisionLength);
                        result = arrayPrecision[0] + "." + result;
                    }//end if (arrayPrecision[1].Length < precisionLength)
                }//end if (arrayPrecision == null || arrayPrecision.Length <= 1)
            }
            return result;
        }

        /// <summary>
        /// 读取Excel，然后写入临时表、启动数据验证、显示数据验证结果
        /// <param name="filePath">Excel文件路径</param>
        /// </summary>
        public virtual List<RowInfo> SourceToTempTable(String filePath)
        {
            #region 读取EXCEL,验证数据
            IWorkbook workbook = null;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                workbook = WorkbookFactory.Create(stream);//使用接口，自动识别excel2003/2007格式
            }
            ISheet sheet = workbook.GetSheetAt(0);//得到里面第一个sheet 

            ColumnInfo columnInfo = new ColumnInfo();//列信息
            List<RowInfo> dataTable = new List<RowInfo>();//行信息列表
            RowInfo row = null;//行信息

            IRow rowData;//Excel中当前数据行
            int iNullCount = 0;//Excel中的空列数
            int cellCount = 0;//Excel中列头的列数
            for (int i = DataStartRow; i <= sheet.LastRowNum; i++)
            {
                if (i == DataStartRow)//数据行的前一行为列头
                {
                    IRow rowHead = sheet.GetRow(i - 1);//得到列头行
                    cellCount = rowHead.LastCellNum;
                }
                row = new RowInfo();
                row.IsHandle = true;//暂时处理，设为可处理
                row.RowIndex = i;

                rowData = sheet.GetRow(i);//得到第i行 
                if (rowData == null) continue;
                iNullCount = 0;
                for (int j = 0; j < cellCount; j++)
                {
                    columnInfo = GetColumnInfo(j);
                    if (columnInfo == null)
                    {
                        continue;
                    }

                    columnInfo.OriginalValue = rowData.GetCell(j) != null ? rowData.GetCell(j).ToString().Trim() : string.Empty;
                    columnInfo.RowIndex = i;
                    ValidData(columnInfo);
                    row.Columns.Add(columnInfo);
                    if (string.IsNullOrEmpty(columnInfo.OriginalValue))
                    {
                        iNullCount++;
                    }
                }
                if (iNullCount == cellCount) continue;
                dataTable.Add(row);
            }

            #endregion

            #region 写入临时表

            ImportToTempTable(dataTable);

            #endregion

            return dataTable;
        }

        /// <summary>
        /// 导入数据到临时表
        /// </summary>
        /// <param name="target"></param>
        public virtual Boolean ImportToTempTable(List<RowInfo> target)
        {
            CachePool[UnquieKey] = target;
            return true;
            //return CachedManager.CacheData(UnquieKey, target, DateTime.Now.AddHours(1), "ImportHelper");
        }

        /// <summary>
        /// 修改要导入的数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public virtual ColumnInfo ModifyDataInTempTable(int rowIndex, String columnName, Object dataValue)
        {
            if (DataCollection == null) return null;

            var collection = DataCollection;

            var curRow = (from r in collection where r.RowIndex == rowIndex select r).ToList<RowInfo>();
            if (curRow.Count > 0)
            {
                foreach (var item in curRow[0].Columns)
                {
                    if (item.ColumnName == columnName)
                    {
                        item.OriginalValue = dataValue != null ? dataValue.ToString() : string.Empty;
                        item.DataValue = item.OriginalValue;
                        ValidData(item);
                        CachePool[UnquieKey] = collection;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        return item;
                    }
                }
            }
            return null;
        }
        public virtual ColumnInfo ModifyDataInTempTableOther(int rowIndex, String columnName, Object dataValue, String otherColumnName, Object otherDataValue)
        {
            if (DataCollection == null) return null;

            var collection = DataCollection;

            var curRow = (from r in collection where r.RowIndex == rowIndex select r).ToList<RowInfo>();
            if (curRow.Count > 0)
            {
                ColumnInfo temp = null;
                int i = 0;
                foreach (var item in curRow[0].Columns)
                {
                    if (item.ColumnName == columnName)
                    {
                        item.OriginalValue = dataValue != null ? dataValue.ToString() : string.Empty;
                        item.DataValue = item.OriginalValue;
                        ValidData(item);
                        CachePool[UnquieKey] = collection;
                        temp = item;
                        i = i + 1;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        //return item;
                    }
                    if (item.ColumnName == otherColumnName)
                    {
                        item.OriginalValue = otherDataValue != null ? otherDataValue.ToString() : string.Empty;
                        item.DataValue = item.OriginalValue;
                        ValidData(item);
                        CachePool[UnquieKey] = collection;
                        temp = item;
                        i = i + 1;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        //return item;
                    }
                    if (i == 2) return temp;
                }
            }
            return null;
        }
        public virtual Boolean ModifyDataByOriginalValue(String columnName, Object oldDataValue, Object dataValue)
        {
            if (DataCollection == null) return false;

            var collection = DataCollection;

            foreach (var row in collection)
            {
                foreach (var item in row.Columns)
                {
                    if (item.ColumnName == columnName && item.OriginalValue == oldDataValue + "")
                    {
                        item.OriginalValue = dataValue != null ? dataValue.ToString() : string.Empty;
                        item.DataValue = item.OriginalValue;
                        ValidData(item);
                        CachePool[UnquieKey] = collection;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        break;
                    }
                }
            }
            return true;
        }
        public virtual Boolean ModifyDataByOriginalValueOther(String columnName, Object oldDataValue, Object dataValue, Object oldOtherDataValue, String otherColumnName, Object otherDataValue)
        {
            if (DataCollection == null) return false;

            var collection = DataCollection;

            foreach (var row in collection)
            {
                foreach (var item in row.Columns)
                {
                    if (item.ColumnName == columnName && item.OriginalValue == oldDataValue + "")
                    {
                        item.OriginalValue = dataValue != null ? dataValue.ToString() : string.Empty;
                        item.DataValue = item.OriginalValue;
                        ValidData(item);
                        CachePool[UnquieKey] = collection;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        foreach (var item1 in row.Columns)
                        {
                            if (item1.ColumnName == otherColumnName&&item1.OriginalValue==oldOtherDataValue+"")
                            {
                                item1.OriginalValue = otherDataValue != null ? otherDataValue.ToString() : string.Empty;
                                item1.DataValue = item1.OriginalValue;
                                ValidData(item1);
                                CachePool[UnquieKey] = collection;
                                //SaveDataCollection();//将修改后的数据保存到缓存中去
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return true;
        }
        public virtual Boolean ModifyDataValueByOriginalValue(String columnName, Object originalValue, Object dataValue)
        {
            if (DataCollection == null) return false;

            var collection = DataCollection;

            foreach (var row in collection)
            {
                foreach (var item in row.Columns)
                {
                    if (item.ColumnName == columnName && item.OriginalValue == originalValue + "")
                    {
                        item.DataValue = dataValue;
                        //item.IsValid = true;//暂时屏蔽
                        item.VerificationType = VerificationTypes.Correct;
                        CachePool[UnquieKey] = collection;
                        //SaveDataCollection();//将修改后的数据保存到缓存中去
                        break;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除临时表中的行
        /// </summary>
        /// <returns></returns>
        public virtual Boolean DeleteRowInTempTable(int rowIndex)
        {
            if (DataCollection == null) return false;

            var collection = DataCollection;

            collection = (from r in collection where r.RowIndex != rowIndex select r).ToList<RowInfo>();

            CachePool[UnquieKey] = collection;
            return true;
            //return CachedManager.CacheData(UnquieKey, collection, DateTime.Now.AddHours(1), "ImportHelper");
        }

        /// <summary>
        /// 导入数据到对应的表中
        /// </summary>
        public virtual Boolean ImportToTargetTable(int operaterId, string operaterName)
        {
            return false;
        }

        /// <summary>
        /// 删除临时表中对应的数据
        /// </summary>
        public virtual string ClearHandleRows()
        {
            if (DataCollection != null)
            {
                int rows = 0;
                var collection = DataCollection;
                rows = collection.Count;
                var notHandleRow = (from r in collection where r.IsHandle == false select r).ToList<RowInfo>();
                if (notHandleRow.Count == 0)
                {
                    //CachedManager.Remove(UnquieKey, "ImportHelper");
                    CachePool.Remove(UnquieKey);
                    return "所有数据导入成功!";
                }
                else
                {
                    //CachedManager.CacheData(UnquieKey, notHandleRow, DateTime.Now.AddHours(1), "ImportHelper");
                    CachePool[UnquieKey] = notHandleRow;
                    return string.Format("总计{0}行数据，成功导入{1}行数据!", rows, rows - notHandleRow.Count);
                }

            }
            return string.Empty;
        }

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="unquieKey"></param>
        /// <returns></returns>
        public virtual bool RemoveTempTableByUnquieKey(string unquieKey)
        {
            try
            {
                if (CachePool.ContainsKey(unquieKey))
                {
                    CachePool.Remove(unquieKey);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
