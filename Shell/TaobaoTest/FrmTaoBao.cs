using APITest;
using LitJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using Taobao.Common;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Utility;
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
        string sessionKey = "";


        public delegate void ChagelabState();


        #endregion

        #region 属    性

        public ApiHost apiHost { get; set; }

        /// <summary>
        /// Socket
        /// </summary>
        public SocketsServer server;
        //  public string SessionKey { get { }; set; } 

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
            int type = 1;
            if (type == 1)
            {
                //method	String	是	API接口名称。
                //app_key	String	是	TOP分配给应用的AppKey。
                //target_app_key	String	否	被调用的目标AppKey，仅当被调用的API为第三方ISV提供时有效。
                //sign_method	String	是	签名的摘要算法，可选值为：hmac，md5。
                //sign	String	是	API输入参数签名结果，签名算法介绍请点击这里。
                //session	String	否	用户登录授权成功后，TOP颁发给应用的授权信息，详细介绍请点击这里。当此API的标签上注明：“需要授权”，则此参数必传；“不需要授权”，则此参数不需要传；“可选授权”，则此参数为可选。
                //timestamp	String	是	时间戳，格式为yyyy-MM-dd HH:mm:ss，时区为GMT+8，例如：2015-01-01 12:00:00。淘宝API服务端允许客户端请求最大时间误差为10分钟。
                //format	String	否	响应格式。默认为xml格式，可选值：xml，json。
                //v	String	是	API协议版本，可选值：2.0。
                //partner_id	String	否	合作伙伴身份标识。
                //simplify	Boolean	否	是否采用精简JSON返回格式，仅当format=json时有效，默认值为：false。
                // string strUrl = "http://gw.api.taobao.com/router/rest";

                // string method = "taobao.time.get";
                // string sign_method = Constants.SIGN_METHOD_MD5;//hmac
                // string appKey = appkey;
                // string format = "json";
                // DateTime timestamp = DateTime.Now; ///GetTimeStamp(DateTime.Now):


                // string xmlpath = "./Configxml/ConfigTaobao.xml";
                // XmlDocument doc = new XmlDocument();
                // doc.Load(xmlpath);
                // XmlNode xn = doc.SelectSingleNode("//session");
                // string session = xn.InnerText;



                // TopDictionary txtParams = new TopDictionary();
                // txtParams.Add(Constants.METHOD, method);
                // txtParams.Add(Constants.SIGN_METHOD, sign_method);
                // txtParams.Add(Constants.APP_KEY, appKey);
                // txtParams.Add(Constants.FORMAT, format);
                // txtParams.Add(Constants.VERSION, "2.0");
                // txtParams.Add(Constants.TIMESTAMP, timestamp);
                // txtParams.Add(Constants.TARGET_APP_KEY, "");
                // txtParams.Add(Constants.SESSION, session);

                // txtParams.Add(Constants.QM_CUSTOMER_ID, "1");
                // //customerId

                // string sign = TaoBaoUtility.SignTopRequest(txtParams, secret, sign_method);
                //txtParams.Add(Constants.SIGN, sign);

                // string resurt = HttpResponseTool.CreatePostHttpResponse(strUrl, txtParams, null);



                string strUrl = "http://127.0.0.1:1608/AuthorityService/client/GetPostTest";
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                pParams.Add("code", "s112134");
                string resurt = HttpResponseTool.CreatePostHttpResponse(strUrl, pParams, null);
                this.labTest.Text = resurt;
            }
            else
            {
                ITopClient client = new DefaultTopClient(url, appkey, secret, "json");
                TimeGetRequest req = new TimeGetRequest();
                TimeGetResponse rsp = client.Execute(req);
                Console.WriteLine(rsp.Body);
                this.labTest.Text = rsp.Body;
            }

        }

        private void btnAppStart_Click(object sender, EventArgs e)
        {
            try
            {
                //监听API
                StartAPISocket();
                if (apiHost == null)
                {
                    Task t = new Task(new Action(() =>
                    {
                        apiHost = new ApiHost();
                        apiHost.StartServices();
                        //  apiHost.ConnectService();
                        DataTable table = apiHost.DtSericer;
                        DataTable dt = table;
                        if (dt != null)
                        {
                            //   labTest.Text = "WCF启动成功";
                        }
                    }));
                    t.Start();
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
                string xmlpath = "./Configxml/configtaobao.xml";
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
                        txtJson.Text = objroot.refresh_token;

                        string xmlPath = "./Configxml/configtaobao.xml";
                        XmlDocument doc = new XmlDocument();
                        doc.Load(xmlPath);
                        XmlNode xn = doc.SelectSingleNode("//session");
                        xn.InnerText = objroot.refresh_token;
                        sessionKey = objroot.refresh_token;
                        doc.Save(xmlPath);
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
                    txtJson.Text = rsp.Body;

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

        private void btnMoney_Click(object sender, EventArgs es)
        {
            try
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
            catch (Exception ex)
            {

            }

        }

        //权限测试 --自定义
        private void btnAuthorizationTest_Click(object sender, EventArgs e)
        {
            string method = "qimen.taobao.lijing.test.read";
            string sign_method = Constants.SIGN_METHOD_MD5;//hmac
            string appKey = appkey;
            string format = "json";
            DateTime timestamp = DateTime.Now; ///GetTimeStamp(DateTime.Now):


            string xmlpath = "./Configxml/configtaobao.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlpath);
            XmlNode xn = doc.SelectSingleNode("//session");
            string session = xn.InnerText;
            //--qimen.taobao.lijing.test.read (黎婧测试)

            //method	String	是	API接口名称。
            //app_key	String	是	TOP分配给应用的AppKey。
            //session	String	否	用户登录授权成功后，TOP颁发给应用的授权信息，详细介绍请点击这里。当此API的标签上注明：“需要授权”，则此参数必传；“不需要授权”，则此参数不需要传；“可选授权”，则此参数为可选。
            //timestamp	String	是	时间戳，格式为yyyy-MM-dd HH:mm:ss，时区为GMT+8，例如：2015-01-01 12:00:00。淘宝API服务端允许客户端请求最大时间误差为10分钟。
            //format	String	否	响应格式。默认为xml格式，可选值：xml，json。
            //v	String	是	API协议版本，可选值：2.0。
            //customerId	String	是	WMS颁发给用户的ID
            //sign_method	String	是	签名的摘要算法，md5。
            //sign	String	是	API输入参数签名结果，签名算法介绍请点击这里。



            TopDictionary txtParams = new TopDictionary();
            txtParams.Add(Constants.METHOD, method);
            txtParams.Add(Constants.SIGN_METHOD, sign_method);
            txtParams.Add(Constants.APP_KEY, appKey);
            txtParams.Add(Constants.FORMAT, format);
            txtParams.Add(Constants.VERSION, "2.0");
            txtParams.Add(Constants.TIMESTAMP, timestamp);
            //txtParams.Add(Constants.TARGET_APP_KEY, request.GetTargetAppKey());
            txtParams.Add(Constants.SESSION, session);
            txtParams.Add(Constants.QM_CUSTOMER_ID, "1");
            //customerId
            //  txtParams.AddAll(this.systemParameters);


            #region
            #endregion

            string sign = TaoBaoUtility.SignTopRequest(txtParams, secret, sign_method);
            txtParams.Add(Constants.SIGN, sign);


            string lijingurl = "http://qimen.api.taobao.com/router/qm";
            string resurt = HttpResponseTool.CreatePostHttpResponse(lijingurl, txtParams, null);
        }

        /// <summary>
        /// 查询卖家用户信息（只能查询有店铺的用户） 只能卖家类应用调用。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSellInfo_Click(object sender, EventArgs e)
        {
            //令牌
            if (sessionKey == "")
            {
                Console.WriteLine("sessionKey 为空");
                return;
            }

            // ITopClient client = new DefaultTopClient(url, appkey, secret);
            //UserSellerGetRequest req = new UserSellerGetRequest();
            // req.Fields = "nick,sex";
            //UserSellerGetResponse rsp = client.Execute(req, sessionKey);
            // Console.WriteLine(rsp.Body);


            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //OpensecurityUidGetRequest req = new OpensecurityUidGetRequest();
            //req.TbUserId = 123456L;
            //OpensecurityUidGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
        }

        private void btnOAuth_Click(object sender, EventArgs e)
        {
            try
            {
                oauth_config config = oauth_helper.get_config("taobao");

                string strUrl = "";
                string client_id = config.oauth_app_id; //appkey;          //appkey
                string redirect_uri = config.return_uri;  //callbackUrl; //"http://127.0.0.1:1608/AuthorityService/client/GetCallbackCode";//?code='1212341dsf'&state='1'
                //权限授权
                strUrl = string.Format(@"https://oauth.taobao.com/authorize?response_type=code&client_id={0}&redirect_uri={1}&view=1", client_id, redirect_uri);
                //打开浏览器
                System.Diagnostics.Process.Start(strUrl);
                labTest.Text = "正在授权";

                //回掉事件
                btnOAuth.Text = "确认已平台验证";
                config = oauth_helper.get_config("taobao");
                if (config.code != "0" && config.code != "")
                {
                    Dictionary<string, object> obj = taobao_helper.get_access_token(config.code);

                    #region 方式1
                    //WebUtils webUtils = new WebUtils();
                    //IDictionary<string, string> pout = new Dictionary<string, string>();
                    //pout.Add("grant_type", "authorization_code");
                    //pout.Add("client_id", appkey);
                    //pout.Add("client_secret", secret);
                    //pout.Add("code", code);
                    //pout.Add("redirect_uri", callbackUrl);
                    //string output = webUtils.DoPost("https://oauth.taobao.com/token", pout);
                    //Console.Write(output);

                    //if (!string.IsNullOrEmpty(output.ToString()))
                    //{
                    //    JavaScriptSerializer Serializers = new JavaScriptSerializer();
                    //    Root objroot = Serializers.Deserialize<Root>(output.ToString());
                    //    txtjson.Text = objroot.refresh_token;

                    //    string xmlPath = "./Configxml/configtaobao.xml";
                    //    XmlDocument doc = new XmlDocument();
                    //    doc.Load(xmlPath);
                    //    XmlNode xn = doc.SelectSingleNode("//session");
                    //    xn.InnerText = objroot.refresh_token;
                    //    sessionKey = objroot.refresh_token;
                    //    doc.Save(xmlPath);
                    //}
                    #endregion
                }
                else
                {
                    //   MessageBox.Show("请确认在平台验证");
                }

            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        private void butTest1_Click(object sender, EventArgs e)
        {
            oauth_config config = oauth_helper.get_config("taobao");
            string appkey = config.oauth_app_id;// "test";

            string secret = config.oauth_app_key;// "test";

            string session = config.session;// "test";

            string appurl = config.oauth_app_url;

            IDictionary<string, string> param = new Dictionary<string, string>();

            param.Add("fields", "user_id,nick,type");

            // param.Add("nick", "sandbox_c_1");

            //  appurl = "http://gw.api.tbsandbox.com/router/rest";

            //Util.Post 集成了 系统参数 和 计算 sign的方法

            string str = string.Format("返回结果：" + Util.Post(appurl, appkey, secret, "taobao.user.seller.get", session, param, "json"));

            txtJson.Text = str;
        }


        private void btnlijing_Click(object sender, EventArgs e)
        {
            oauth_config config = oauth_helper.get_config("taobao");
            string appkey = config.oauth_app_id;// "test";

            string secret = config.oauth_app_key;// "test";

            string session = config.session;// "test";

            string appurl = config.oauth_app_url;

            IDictionary<string, string> param = new Dictionary<string, string>();

            //param.Add("fields", "user_id,nick,type");

            // param.Add("nick", "sandbox_c_1");

            //  appurl = "http://gw.api.tbsandbox.com/router/rest";

            //Util.Post 集成了 系统参数 和 计算 sign的方法

            string str = string.Format("返回结果：" + Util.Post(appurl, appkey, secret, "qimen.taobao.lijing.test.read", session, param, "json"));

            txtJson.Text = str;
        }

        private void btnkeyword_Click(object sender, EventArgs e)
        {


            oauth_config config = oauth_helper.get_config("taobao");
            string appkey = config.oauth_app_id;// "test";

            string secret = config.oauth_app_key;// "test";

            string session = config.session;// "test";

            string appurl = config.oauth_app_url;

            IDictionary<string, string> param = new Dictionary<string, string>();

            param.Add("content", "文本信息");

            // param.Add("nick", "sandbox_c_1");

            //  appurl = "http://gw.api.tbsandbox.com/router/rest";

            //Util.Post 集成了 系统参数 和 计算 sign的方法

            string str = string.Format("返回结果：" + Util.Post(appurl, appkey, secret, "taobao.kfc.keyword.search", session, param, "json"));

            txtJson.Text = str;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            oauth_config config = oauth_helper.get_config("taobao");
            string appkey = config.oauth_app_id;// "test";

            string secret = config.oauth_app_key;// "test";

            string session = config.session;// "test";

            string appurl = config.oauth_app_url;

            IDictionary<string, string> param = new Dictionary<string, string>();

            // param.Add("content", "文本信息");

            // param.Add("nick", "sandbox_c_1");

            //  appurl = "http://gw.api.tbsandbox.com/router/rest";

            //Util.Post 集成了 系统参数 和 计算 sign的方法
            string resurtJson = Util.Post(appurl, appkey, secret, "taobao.top.ipout.get", session, param, "json");
            JsonData jd = JsonMapper.ToObject(resurtJson);

            string str = string.Format("返回结果：" + jd["top_ipout_get_response"]["ip_list"].ToString());

            txtJson.Text = str;

        }
        private void btnBuy_Click(object sender, EventArgs e)
        {

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

        public string GetTimeStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t.ToString();
        }

        //打开监听
        public void StartAPISocket()
        {
            try
            {

                if (server == null)
                {
                    string ipString = System.Configuration.ConfigurationManager.AppSettings["ServerIP"];
                    string portString = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
                    server = new SocketsServer(SeverPrint, ipString, DataTypeConvert.ToInt32(portString, 3780), this);
                    Console.WriteLine("socket启动成功");
                }
                if (!server.started) server.start();
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        // 服务器端输出信息
        private void SeverPrint(string info)
        {
            Console.WriteLine(info);

            if (labTest.IsDisposed) server.print = null;
            else
            {
                if (labTest.InvokeRequired)
                {
                    SocketsServer.Print F = new SocketsServer.Print(SeverPrint);
                    this.Invoke(F, new object[] { info });
                }
                else
                {
                    if (info != "")
                    {
                        labTest.Text = info;
                        btnOAuth.Text = "授权成功";
                        txtJson.Text = "";
                        oauth_config config = oauth_helper.get_config("taobao");

                        if (config == null) return;
                        foreach (System.Reflection.PropertyInfo p in config.GetType().GetProperties())
                        {
                            txtJson.Text += string.Format("Name:{0} Value:{1}", p.Name, p.GetValue(config, null)) + "\r\n";
                        }
                        //labTest.SelectionColor = Color.Green;
                        //labTest.AppendText(info);
                        //labTest.AppendText(Environment.NewLine);
                        //labTest.ScrollToCaret();
                    }
                }
            }
        }
        #endregion




    }
}
