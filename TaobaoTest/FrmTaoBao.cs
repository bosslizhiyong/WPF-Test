using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;
using Top.Tmc;
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
                strUrl = string.Format(@"https://oauth.taobao.com/authorize?response_type=code&client_id={0}&redirect_uri={1}&view=1", client_id, redirect_uri);
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
                string xmlpath = "./Configxml/ConfigTaobao.xml";
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
                int type = 1;
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
                   
                    if (!string.IsNullOrEmpty(output.ToString()))
                    {
                        JavaScriptSerializer Serializers = new JavaScriptSerializer();
                        Root objroot = Serializers.Deserialize<Root>(output.ToString());
                        txtjson.Text = objroot.refresh_token;

                    }
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

                    JavaScriptSerializer Serializers = new JavaScriptSerializer();
                    //string strTest='{"top_auth_token_create_response":{"token_result":"{\"w1_expires_in\":1800,\"refresh_token_valid_time\":1526285728986,\"taobao_user_nick\":\"b01GflzbxtAIf8KwrSO9nNQofbFFz2kii4lSP%2FHJy9oC5c%3D\",\"re_expires_in\":0,\"expire_time\":1534061728986,\"open_uid\":\"AAE3PSJFAGQsSvMnMnzd485d\",\"token_type\":\"Bearer\",\"access_token\":\"6201321761ca692cc4cdeccc132acegff62acee1e8c41771837606616\",\"taobao_open_uid\":\"AAE3PSJFAGQsSvMnMnzd485d\",\"w1_valid\":1526287528986,\"refresh_token\":\"6201121d1ef9388da8ba2d6e48e3dfh4af13ac9198572431837606616\",\"w2_expires_in\":0,\"w2_valid\":1526285728986,\"r1_expires_in\":1800,\"r2_expires_in\":0,\"r2_valid\":1526285728986,\"r1_valid\":1526287528986,\"expires_in\":7776000}","request_id":"165an4c9a92sp"}} ';

                    //json字符串转为数组对象, 反序列化
                    TaobaoRoot objs = Serializers.Deserialize<TaobaoRoot>(rsp.Body.ToString());
                    if (objs != null)
                    {
                        if (objs.top_auth_token_create_response != null)
                        {
                            string strroot = objs.top_auth_token_create_response.token_result;
                                Root objroot = Serializers.Deserialize<Root>(strroot);
                        }
                    }
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

        private void btnMoney_Click(object sender, EventArgs es)
        {
            TmcClient client = new TmcClient(appkey, secret, "default");
            client.OnMessage += (s, e) =>
            {
                try
                {
                    Console.WriteLine(e.Message.Content);
                    Console.WriteLine(e.Message.Topic);
                    // 默认不抛出异常则认为消息处理成功  
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.StackTrace);
                    e.Fail(); // 消息处理失败回滚，服务端需要重发  
                }
            };
            client.Connect("ws://mc.api.taobao.com/");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}
