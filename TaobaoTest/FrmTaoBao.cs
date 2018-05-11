using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;
using WCFWeb.Co.ApiHost;

namespace TaobaoTest
{
    public partial class FrmTaoBao : FormBase
    {
        #region 字    段
        string url = "";
        string appkey = "";
        string secret = "";
        string callbackUrl = "";
        string code = "";
        #endregion

        #region 属    性

        public ApiHost apiHost { get; set; }

        #endregion

        #region 构造函数
        public FrmTaoBao()
        {
            InitializeComponent();
        }


        #endregion

        #region 窗体事件


        private void FrmTaoBao_Load(object sender, EventArgs e)
        {
            InitData();
            LoadData();

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, appkey, secret, "json");
            TimeGetRequest req = new TimeGetRequest();
            TimeGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            this.labTest.Text = rsp.Body;
        }


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
                    if (dt != null)
                    {
                        labTest.Text = "WCF启动成功";
                    }
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
                string strUrl = "";
                string client_id = appkey;          //appkey
                string redirect_uri = callbackUrl; //"http://127.0.0.1:1608/AuthorityService/client/GetCallbackCode";//?code='1212341dsf'&state='1'
                //权限授权
                strUrl = string.Format(@"https://oauth.taobao.com/authorize?response_type=code&client_id={0}&redirect_uri={1} &view=we", client_id, redirect_uri);
                //打开浏览器
                System.Diagnostics.Process.Start(strUrl);
                labTest.Text = "正在授权";
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlpath = "/Configxml/ConfigTaobao.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlpath);
                XmlNode xn = doc.SelectSingleNode("//code");
                string transDate = xn.InnerText;
                this.labTest.Text = transDate;
                code = transDate;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {
            try
            {
                int type = 2;
                if (type == 1)
                {
                    #region 方式1
                    WebUtils webUtils = new WebUtils();
                    IDictionary<string, string> pout = new Dictionary<string, string>();
                    pout.Add("grant_type", "authorization_code");
                    pout.Add("client_id", appkey);
                    pout.Add("client_secret", secret);
                    pout.Add("code", code);
                    pout.Add("redirect_uri", callbackUrl);
                    string output = webUtils.DoPost("https://oauth.taobao.com/token", pout);
                    Console.Write(output);
                    txtjson.Text = output;
                    #endregion
                }
                else if (type == 2)
                {
                    #region 方式2
                    ITopClient client = new DefaultTopClient(url, appkey, secret, "json");
                    TopAuthTokenCreateRequest req = new TopAuthTokenCreateRequest();
                    req.Code = code;
                    req.Uuid = ""; //可选
                    TopAuthTokenCreateResponse rsp = client.Execute(req);
                    Console.WriteLine(rsp.Body);
                    // txtBoy.Text = rsp.Body;
                    txtjson.Text = rsp.Body;
                    #endregion
                }

            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        #endregion

        #region 基本方法


        protected override void InitData()
        {
            this.url = System.Configuration.ConfigurationManager.AppSettings["appUrl"];
            this.appkey = System.Configuration.ConfigurationManager.AppSettings["appKey"];
            this.secret = System.Configuration.ConfigurationManager.AppSettings["appSecret"];
            this.callbackUrl = System.Configuration.ConfigurationManager.AppSettings["callbackUrl"];
        }

        protected override void LoadData()
        {
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //UserBuyerGetRequest req = new UserBuyerGetRequest();
            //req.Fields = "nick,sex";
            //UserBuyerGetResponse rsp = client.Execute(req, sessionKey);
            //Console.WriteLine(rsp.Body);
        }


        #endregion


    }
}
