using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// HttpHelper
    /// </summary>
    public class HttpHelper
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Put(string url, object data, int timeout = 20 * 1000)
        {
            string content = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json;charset=utf8";
            request.Timeout = timeout;

            try
            {
                string strJson = JsonConvert.SerializeObject(data);
                byte[] btyData = Encoding.UTF8.GetBytes(strJson);
                request.ContentLength = btyData.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(btyData, 0, btyData.Length);

                    WebResponse response = request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        content = reader.ReadToEnd();
                    }
                    response.Close();
                }
            }
            catch
            {
                content = string.Empty;
            }
            return content;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userAgent"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Get(string url, string userAgent, int timeout = 20 * 1000)
        {
            string content = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            request.Timeout = timeout;

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }

            try
            {
                WebResponse response = request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    content = reader.ReadToEnd();
                }
                response.Close();
            }
            catch
            {
                content = string.Empty;
            }

            return content;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Post(string url, object data, int timeout = 20 * 1000)
        {
            string content = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf8";
            request.Timeout = timeout;

            try
            {
                string strJson = JsonConvert.SerializeObject(data);
                byte[] btyData = Encoding.UTF8.GetBytes(strJson);
                request.ContentLength = btyData.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(btyData, 0, btyData.Length);

                    WebResponse response = request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        content = reader.ReadToEnd();
                    }
                    response.Close();
                }
            }
            catch
            {
                content = string.Empty;
            }
            return content;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Delete(string url, int timeout = 20 * 1000)
        {
            string content = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.Timeout = timeout;

            try
            {
                WebResponse response = request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    content = reader.ReadToEnd();
                }
                response.Close();
            }
            catch
            {
                content = string.Empty;
            }

            return content;
        }
    }
}