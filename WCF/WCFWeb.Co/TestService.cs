using System.Collections.Generic;
using System.ServiceModel.Activation;
using WCFWeb.Co.Contract;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Utility;
using System.Data;
using System.Data.OleDb;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkCRM.Commands.Co;
using ThinkNet.Query.Core;
using System.Collections;
using System;

namespace WCFWeb.Co
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestService : ServiceBase, ITestService
    {
      
        public string GetTest()
        {
            try
            {
                int i = 0;
                int s = 60 / i;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex.Message);
            }
            string strSql = " SELECT * FROM CO_ProductCategory WHERE [Status] = 0 ";
            DataTable table = QueryService.ExecuteDataTable(strSql);

            //
            var c = QueryService.GetTable<CM_Module>(_mQueryParamater);
            return null;
        }

        public string GetAccess()
        {
            //创建连接对象
            OleDbConnection conn = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExePath + @"\ConnCRMCo.mdb");
            conn.Open();
            //获取数据表
            //string sql = "select * from 表名 order by 字段1";
            //查询
            string sql = "select * from Student ";

            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn); //创建适配对象
            DataTable dt = new DataTable(); //新建表对象
            da.Fill(dt); //用适配对象填充表对象
            conn.Close();
            return "";
        }

        public string GetAccessTable()
        {
            string strConn = string.Format("provider=Microsoft.Jet.OLEDB.4.0;Data Source= {0}" + @"\ThinkCRMCo4Developer.mdb");
            OleDBHelper olr = new OleDBHelper("");


            return "";
            //  olr.ExecuteTable()
        }



    }
}