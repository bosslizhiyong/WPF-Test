using System.Collections.Generic;
using System.ServiceModel.Activation;
using WCFWeb.Co.Contract;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Utility;
using System.Data;
using System.Data.OleDb;
using ThinkCRM.Infrastructure.DataEntity.Co;

namespace WCFWeb.Co
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : ServiceBase, IUserService
    {
        public User GetUserInfo(User user)
        {
            //JSonHelper.Serialize(）
            return user;
        }

        public User GetUser(int id)
        {
            User user = new User();
            user.ID = 1;
            user.Name = "张三";
            return user;
        }

        public List<User> GetUserList(string where)
        {
            List<User> list = new List<User>();
            User user = new User();
            user.ID = 1;
            user.Name = "rwrewtrewt";
            list.Add(user);
            user = new User();
            user.ID = 2;
            user.Name = "汪洪斌";
            list.Add(user);
            return list;
        }

        public ApiResult<List<User>> GetUserJson(string where)
        {
            if (string.IsNullOrEmpty(where))
            {
                return null;
            }
            List<User> list = new List<User>();
            User user = new User();
            user.ID = 1;
            user.Name = "rwrewtrewt";
            list.Add(user);
            user = new User();
            user.ID = 2;
            user.Name = "汪洪斌";
            list.Add(user);

            ApiResult<List<User>> t = new ApiResult<List<User>>(200, list);
            return t;
        }

        public string GetTest()
        {
            string strSql = " SELECT * FROM CO_ProductCategory WHERE [Status] = 0 ";
            DataTable table = QueryService.ExecuteDataTable(strSql);

            //
            //var c = QueryService.GetTable<CM_Module>(_mQueryParamater);


            return null;
        }

        static string exePath = System.Environment.CurrentDirectory;//本程序所在路径

        public string GetAccess()
        {
            //创建连接对象
            OleDbConnection conn = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + exePath + @"\ConnCRMCo.mdb");
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