using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    public static class GridviewControl
    {
        /// <summary>
        /// 主从表
        /// </summary>
        /// <param name="master">主表</param>
        /// <param name="ID">主键ID</param>
        /// <param name="slave">从表</param>
        /// <param name="slaveID">从表ID</param>
        /// <param name="slaveTitleName">从表标题名称</param>
        /// <param name="Display">是否显示从表的列</param>
        /// <returns></returns>
        public static DataTable MasterSlave(DataTable master, string masterID, DataTable slave, string slaveID, string slaveTitleName, List<string> display = null, bool IsCreateConstraints=true)
        {
            DataSet myDs = new DataSet();
            //主表
            master.TableName = "Role";
            myDs.Tables.Add(master);
            //从表明细
            slave.TableName = "detailed";
            myDs.Tables.Add(slave);
            //建立关系
            myDs.Relations.Add(slaveTitleName, myDs.Tables["Role"].Columns[masterID], myDs.Tables["detailed"].Columns[slaveID],IsCreateConstraints);
             //隐藏从表列字段
            if (display != null && display.Count > 0)
            {
                DataTable detailed = myDs.Tables["detailed"];
                display.ForEach(new Action<string>((colName) =>{
                    if (detailed.Columns.Contains(colName))
                        myDs.Tables["detailed"].Columns[colName].ColumnMapping = MappingType.Hidden;
                }));
            }
            return myDs.Tables["Role"];
        }

    }
}
