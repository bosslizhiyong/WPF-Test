using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JinDong
{
    public partial class Frmocr : Form
    {

        string rand = "";

        string timestamp = "";

        string authorization = "";

        public Frmocr()
        {
            InitializeComponent();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            string domain = txtDomain.Text;
            string strAppName = txtAppName.Text;
            string filPath = labAddress.Text;
            string fileName = "";
            if (btnFile.Tag != null)
            {
                fileName = btnFile.Tag.ToString();
            }
            if (timestamp == "")
            {
                timestamp = GetTimeStamp(DateTime.Now);
                rand = getRandNum();
            }


            //识别文件

            //appKey: d85c5234e5244f798d070eca12c844bd
            //appName: TestView
            string appKey = "d85c5234e5244f798d070eca12c844bd";
            string secretKey = "3a07NbW4kmkej8i4IMG3ma==";

            string strUrl = string.Format(@"http://cognitive-console.jdcloud.com/service-ai/ocr/detect-text?appKey={0}&appName={1}&timestamp={2}&rand={3}&bucketDomain={4}"
                , appKey, strAppName, timestamp, rand, domain);

            // http://cognitive-console.jdcloud.com/service-ai/ocr/detect-text?appKey=d85c5234e5244f798d070eca12c844bd&appName=TestView&timestamp=1526360275690&rand=8623
            // SendFilePhp(strUrl, filpath);

            string strresurt = PostWebRequest(strUrl, fileName, filPath);
            txtConter.Text = strresurt;

            //HttpPost(strUrl,)
        }

        private void btnFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            labAddress.Text = ofd.FileName;
            btnFile.Tag = ofd.SafeFileName;
        }

        /// <summary>  
        /// POST请求与获取结果  
        /// </summary>  
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }


        private string PostWebRequest(string url, string fileName, string filePath)
        {
            string retString = "";
            try
            {

                //string url = @"http://*******";//这里就不暴露我们的地址啦
                string modelId = "4f1e2e3d-6231-4b13-96a4-835e5af10394";
                string updateTime = "2016-11-03 14:17:25";
                string encrypt = "f933797503d6e2c36762428a280e0559";

                //filePath = @" ";
                //fileName = "2.html";

                byte[] fileContentByte = new byte[1024]; // 文件内容二进制

                #region 将文件转成二进制

                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                fileContentByte = new byte[fs.Length]; // 二进制文件
                fs.Read(fileContentByte, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                #endregion


                #region 定义请求体中的内容 并转成二进制

                string boundary = "ceshi";
                string Enter = "\r\n";

                string modelIdStr = "--" + boundary + Enter
                        + "Content-Disposition: form-data; name=\"modelId\"" + Enter + Enter
                        + modelId + Enter;

                string fileContentStr = "--" + boundary + Enter
                    // + "Content-Type:application/octet-stream" + Enter
                       + "Content-Type:application/json, text/javascript, */*; q=0.01" + Enter
                       + "Authorization:" + authorization + Enter
                    // + "Cookie: __jda=194339007.15263515429112072405717.1526351543.1526351543.1526351543.1; __jdv=269124599|direct|-|none|-|1526351542916; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22%24device_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E7%9B%B4%E6%8E%A5%E6%B5%81%E9%87%8F%22%2C%22%24latest_referrer%22%3A%22%22%2C%22%24latest_referrer_host%22%3A%22%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC_%E7%9B%B4%E6%8E%A5%E6%89%93%E5%BC%80%22%7D%7D; sajssdk_2015_cross_new_user=1; _ga=GA1.2.1597181775.1526351544; _gid=GA1.2.776470341.1526351544; currPage=cognitivelist; jd_503361d183eb2areaCode=bj_02; jd_503361d183eb2areaName=%E5%8D%8E%E5%8C%97; thor=63708DD2F521561F567509892777E7818884E08A30A8CED9D2ED949B06F873295A3B8B091FBD44D0041C15037E38F62B3F9F274C5604368C04826203BFC52275A697526D337C07664B01D4EEFF3E2554DC4C0AB165BF951D6325CCC8B9E2552177AC445B1EFFE1C0EE5E21E2D1A2DF18B9720FBB560FA600A368E25259E932014AC68FAC8BC50E740D2ADB6AC1A176A1F4AAFB24A8665C7E39E59919E336E988; pin=jd_503361d183eb2; unick=bossslizhiyong; __jdc=194339007; SESSION=f5e3dca2-ba83-4528-9a11-9e544e2cb14b"+ Enter



                       + "Content-Disposition: form-data; name=\"fileContent\"; filename=\"" + fileName + "\"" + Enter + Enter;
                string updateTimeStr = Enter + "--" + boundary + Enter
                        + "Content-Disposition: form-data; name=\"updateTime\"" + Enter + Enter
                        + updateTime;

                string encryptStr = Enter + "--" + boundary + Enter
                        + "Content-Disposition: form-data; name=\"encrypt\"" + Enter + Enter
                        + encrypt + Enter + "--" + boundary + "--";

                // Authorization: yGld04qyE+kr9lJppOir6Som2r58Mp26K4WF3jsdbGxqiwARKRUmBJsD1l9J2BDn2ZUzS8Wp100Q60jj62NsAQ==
                //Cookie: __jda=194339007.15263515429112072405717.1526351543.1526351543.1526351543.1; __jdv=269124599|direct|-|none|-|1526351542916; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22%24device_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E7%9B%B4%E6%8E%A5%E6%B5%81%E9%87%8F%22%2C%22%24latest_referrer%22%3A%22%22%2C%22%24latest_referrer_host%22%3A%22%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC_%E7%9B%B4%E6%8E%A5%E6%89%93%E5%BC%80%22%7D%7D; sajssdk_2015_cross_new_user=1; _ga=GA1.2.1597181775.1526351544; _gid=GA1.2.776470341.1526351544; currPage=cognitivelist; jd_503361d183eb2areaCode=bj_02; jd_503361d183eb2areaName=%E5%8D%8E%E5%8C%97; thor=63708DD2F521561F567509892777E7818884E08A30A8CED9D2ED949B06F873295A3B8B091FBD44D0041C15037E38F62B3F9F274C5604368C04826203BFC52275A697526D337C07664B01D4EEFF3E2554DC4C0AB165BF951D6325CCC8B9E2552177AC445B1EFFE1C0EE5E21E2D1A2DF18B9720FBB560FA600A368E25259E932014AC68FAC8BC50E740D2ADB6AC1A176A1F4AAFB24A8665C7E39E59919E336E988; pin=jd_503361d183eb2; unick=bossslizhiyong; __jdc=194339007; SESSION=f5e3dca2-ba83-4528-9a11-9e544e2cb14b

                var modelIdStrByte = Encoding.UTF8.GetBytes(modelIdStr);//modelId所有字符串二进制

                var fileContentStrByte = Encoding.UTF8.GetBytes(fileContentStr);//fileContent一些名称等信息的二进制（不包含文件本身）

                var updateTimeStrByte = Encoding.UTF8.GetBytes(updateTimeStr);//updateTime所有字符串二进制

                var encryptStrByte = Encoding.UTF8.GetBytes(encryptStr);//encrypt所有字符串二进制


                #endregion


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "multipart/form-data;boundary=" + boundary;

                Stream myRequestStream = request.GetRequestStream();//定义请求流

                #region 将各个二进制 安顺序写入请求流 modelIdStr -> (fileContentStr + fileContent) -> uodateTimeStr -> encryptStr

                myRequestStream.Write(modelIdStrByte, 0, modelIdStrByte.Length);

                myRequestStream.Write(fileContentStrByte, 0, fileContentStrByte.Length);
                myRequestStream.Write(fileContentByte, 0, fileContentByte.Length);

                myRequestStream.Write(updateTimeStrByte, 0, updateTimeStrByte.Length);

                myRequestStream.Write(encryptStrByte, 0, encryptStrByte.Length);

                #endregion

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//发送

                Stream myResponseStream = response.GetResponseStream();//获取返回值
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

            }
            catch (Exception ex)
            {


            }
            return retString;
        }


        public string GetTimeStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t.ToString();
        }



        private string HttpPostData(string url, int timeOut, string fileKeyName,
                                    string filePath, NameValueCollection stringDict)
        {

            string strImage = "";
            //初始化类  
            Bitmap bmp = new Bitmap(Image.FromFile(filePath));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Flush();
            //将二进制数据存到byte数字中  
            byte[] bmpBytes = ms.ToArray();
            // richTextBox1.Text = Convert.ToBase64String(bmpBytes);  
            foreach (var item in bmpBytes)
            {
                strImage += item;
            }
            txtConter.Text = strImage;

            string responseContent;
            var memStream = new MemoryStream();
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // 边界符  
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符  
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            // 最后的结束符  
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            // 设置属性  
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            // 写入文件  
            const string filePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                 "Content-Type: application/octet-stream\r\n\r\n"
                 + "Authorization : yGld04qyE+kr9lJppOir6Som2r58Mp26K4WF3jsdbGxqiwARKRUmBJsD1l9J2BDn2ZUzS8Wp100Q60jj62NsAQ==\r\n\r\n";
            var header = string.Format(filePartHeader, fileKeyName, filePath);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // =0  

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }

            // 写入字符串的Key  
            var stringKeyHeader = "\r\n--" + boundary +
                                   "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                   "\r\n\r\n{1}\r\n";

            foreach (byte[] formitembytes in from string key in stringDict.Keys
                                             select string.Format(stringKeyHeader, key, stringDict[key])
                                                 into formitem
                                                 select Encoding.UTF8.GetBytes(formitem))
            {
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }

            // 写入最后的结束边界符  
            memStream.Write(endBoundary, 0, endBoundary.Length);

            webRequest.ContentLength = memStream.Length;

            var requestStream = webRequest.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                            Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }

            fileStream.Close();
            httpWebResponse.Close();
            webRequest.Abort();

            return responseContent;
        }

        public bool SendFilePhp(string strUrl, string filepath)
        {
            bool isok = false;
            try
            {
                NameValueCollection dicr = new NameValueCollection();
                string flieName = "文字图片.png";
                string sttuas = HttpPostData(strUrl, 100000, flieName, filepath, dicr);
                if (sttuas.IndexOf("10000") >= 0)
                {
                    txtConter.Text = sttuas;
                    isok = true;
                }
            }
            catch (Exception ex)
            {
                isok = false;
            }
            return isok;
        }

        private void btnCreateAuthorization_Click(object sender, EventArgs e)
        {

            string appKey = "d85c5234e5244f798d070eca12c844bd";
            string secretKey = "3a07NbW4kmkej8i4IMG3ma==";
            string appName = "TestView";
            timestamp = GetTimeStamp(DateTime.Now);
            rand = getRandNum();
            string str = "appKey=" + appKey + "&appName=" + appName + "&timestamp=" + timestamp + "&rand=" + rand;

            // string appSecret = "no7ucMMx/m58F/jAmuYbw7==";//appSecret,用户可在控制台点击‘查看密匙’获取
            //byte[] strBytes = str.getBytes("UTF-8");//字符串转为byte[]
            //byte[] appSecretBytes =appSecret.getBytes("UTF-8");//secret转为byte[]

            // byte[] signBytes = encodeHmacSHA512(strBytes, appSecretBytes);//生成签名byte[]

            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            byte[] appSecretBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] signBytes = encodeHmacSHA512(strBytes, appSecretBytes);
             string sign1 = BitConverter.ToString(signBytes).Replace("-", "");
            //authorization = sign;

             string sign2 = HmacSha512(str, secretKey);
           
          
            // signBytes

             authorization = EncodeBase64(Encoding.UTF8, sign1);
        }

        public static string EncodeBase64(Encoding encode, string source)
        {
            string enString = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                enString = Convert.ToBase64String(bytes);
            }
            catch
            {
                enString = source;
            }
            return enString;
        }

        private static byte[] encodeHmacSHA512(byte[] data, byte[] key)
        {
            HMACSHA512 hmacsh = new HMACSHA512(key);
            return hmacsh.ComputeHash(data);
        }



        private string getRandNum()
        {
            Random random = new Random();
            return random.Next(10000).ToString();
        }


        /// <summary>
        /// HmacSha512 加密
        /// </summary>
        /// <param name="clearMessage"></param>
        /// <param name="secretKeyString"></param>
        /// <returns></returns>
        protected string HmacSha512(string clearMessage, string secretKeyString)
        {
            Encoding encoder = Encoding.UTF8;

            //Transform the clear query string to a byte array
            byte[] messageBytes = encoder.GetBytes(clearMessage);

            //Transform the secret key string to a byte array
            var key = Convert.ToBase64String(encoder.GetBytes(secretKeyString));

            byte[] secretKeyBytes = encoder.GetBytes(key);

            //Init the Hmac SHA512 generator with the key
            HMACSHA512 hmacsha512 = new HMACSHA512(secretKeyBytes);

            //Hash the message
            byte[] hashValue = hmacsha512.ComputeHash(messageBytes);

            //Transform the hash bytes array to a string string
            string hmac = BitConverter.ToString(hashValue).Replace("-", "");

            //Force the case of the HMAC key to Uppercase
            return hmac;
        }

    }
}
