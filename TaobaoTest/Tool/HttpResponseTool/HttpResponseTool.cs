#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          HttpResponseTool.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-05-16 10:50:45
 *
 * 公  司（Copyright）：          www.Leapline.com.cn
 * ----------------------------------------------------------------
 * 描  述（Description）：
 * 
 * ----------------------------------------------------------------
 * 修改记录（Revision History）
 *      R1 -
 *         修改人（Editor）：
 *         修改日期（Date）：
 *         修改原因（Reason）：
 *----------------------------------------------------------------*/
#endregion Copyright

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Utility;


namespace TaobaoTest
{
    public class HttpResponseTool
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }

        public static string CreatePostHttpResponse(string url, IDictionary<string, string> parameters, IDictionary<string, string> headersParameters)
        {
            string paraUrlCoded = JSonHelper.Serialize(parameters);
            //string str = HttpPost(url, paraUrlCoded);


            Task t = new Task(new Action(() =>
            {
                //推送产品
                string str = PostUrl(url, paraUrlCoded); 


            }));
            t.Start();
            // string str =PostUrl(url, paraUrlCoded); 
            return "";
            //获取权限
            HttpWebResponse request = CreatePostHttpResponse(url, parameters, headersParameters, Encoding.UTF8);
            //实例化一个StreamReader对象来获取Response的GetResponseStream返回的响应的体  
            StreamReader Info_Reader = new StreamReader(request.GetResponseStream(), Encoding.UTF8);
            return Info_Reader.ReadToEnd() + "";

        }


        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, IDictionary<string, string> headersParameters, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 20000;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            if (!(headersParameters == null || headersParameters.Count == 0))
            {
                foreach (string key in headersParameters.Keys)
                {
                    request.Headers.Add(key, headersParameters[key]);
                }
            }
            //   request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json; charset=utf-8";
            // string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
            request.UserAgent = DefaultUserAgent;
            //如果需要POST数据   
            if (!(parameters == null || parameters.Count == 0))
            {
                //StringBuilder buffer = new StringBuilder();
                //int i = 0;
                //foreach (string key in parameters.Keys)
                //{
                //    if (i > 0)
                //    {
                //        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                //    }
                //    else
                //    {
                //        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                //    }
                //    i++;
                //}
                //byte[] data = charset.GetBytes(buffer.ToString());

                //using (Stream stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
                //parameters
                //Newtonsoft.Json

                string paraUrlCoded = JSonHelper.Serialize(parameters);
                request.ContentLength = Encoding.UTF8.GetByteCount(paraUrlCoded);

                byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                request.ContentLength = payload.Length;
                Stream writer = request.GetRequestStream();
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(payload, 0, payload.Length);
                }



            }
            WebResponse response = request.GetResponse();



            Stream stream1 = response.GetResponseStream();
            var memoryStream = new MemoryStream();
            //将基础流写入内存流
            const int bufferLength = 1024;
            byte[] buffer1 = new byte[bufferLength];
            int actual = stream1.Read(buffer1, 0, bufferLength);
            if (actual > 0)
            {
                memoryStream.Write(buffer1, 0, actual);
            }
            memoryStream.Position = 0;
            string str = "";

            str = Convert.ToBase64String(memoryStream.ToArray());
            return response as HttpWebResponse;
        }

        private static string HttpPost(string Url, string postDataStr)
        {

            postDataStr = "     " + postDataStr;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Accept = " */*";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.Timeout = 2000;


            //request.ServicePoint.Expect100Continue = false;

            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);


            //发送请求，获得请求流 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流



            String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            string postContent = "";
            if (response != null)
            {
                Stream s = response.GetResponseStream();
                Stream postData = response.GetResponseStream();
                StreamReader sRead = new StreamReader(s);
                postContent = sRead.ReadToEnd();
                sRead.Close();

            }

            return postContent;//返回Json数据



            ////myStreamWriter.Close();
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream myResponseStream = response.GetResponseStream();
            //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //string retString = myStreamReader.ReadToEnd();
            //myStreamReader.Close();
            //myResponseStream.Close();
            //return retString;





        }

        public static string PostUrl(string url, string postData)
        {
            //


            string result = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Host = "127.0.0.1:1608";
            req.UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0 ";
            req.Accept = "*/*";
            req.Method = "POST";

            req.Timeout = 2000;//设置请求超时时间，单位为毫秒

            req.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(postData);

            req.ContentLength = data.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);

                reqStream.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            Stream stream = resp.GetResponseStream();

            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
