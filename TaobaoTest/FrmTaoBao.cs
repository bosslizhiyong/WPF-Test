using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFWeb.Co.ApiHost;

namespace TaobaoTest
{
    public partial class FrmTaoBao : FormBase
    {

        #region 属    性

        public ApiHost apiHost { get; set; }

        #endregion
        public FrmTaoBao()
        {
            InitializeComponent();
        }

        #region 窗口事件

        private void btnAppStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (apiHost == null)
                {
                    apiHost = new ApiHost();
                    apiHost.StartServices();

                    DataTable table = apiHost.DtSericer;
                    DataTable dt = table;
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        private void btnGetUserCode_Click(object sender, EventArgs e)
        {
            try
            {

                string client_id = "";

                string redirect_uri = "http://127.0.0.1:1608/AuthorityService/client/GetCallbackCode";//?code='1212341dsf'&state='1'

                string strUrl = "http://baidu.com";



                strUrl = string.Format(@"https://oauth.taobao.com/authorize?response_type=code&client_id={0}&redirect_uri={1} &view=we", client_id, redirect_uri);
          

                string strbak = "";



                //打开浏览器
                System.Diagnostics.Process.Start(strUrl);

            }
            catch (Exception)
            {
                //System.Diagnostics.Process.Start("http://blog.csdn.net/testcs_dn");
            }
        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        #endregion

        #region 其它方法



        #endregion
    }
}
