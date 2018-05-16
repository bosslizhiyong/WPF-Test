

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Top.Api;

namespace TaobaoTest
{
    public  class TaoBaoUtility
    {

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="secret"></param>
        /// <param name="signMethod"></param>
        /// <returns></returns>
        public static string SignTopRequest(IDictionary<string, string> parameters, string secret, string signMethod)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();
            if (Constants.SIGN_METHOD_MD5.Equals(signMethod))
            {
                query.Append(secret);
            }
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }

            // 第三步：使用MD5/HMAC加密
            byte[] bytes;
            if (Constants.SIGN_METHOD_HMAC.Equals(signMethod))
            {
                HMACMD5 hmac = new HMACMD5(Encoding.UTF8.GetBytes(secret));
                bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            }
            else
            {
                query.Append(secret);
                MD5 md5 = MD5.Create();
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            }

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }

        private string PostWebRequest(string url, string fileName, string filePath)
        {
            string retString = "";
            try
            {

                ////string url = @"http://*******";//这里就不暴露我们的地址啦
                //string modelId = "4f1e2e3d-6231-4b13-96a4-835e5af10394";
                //string updateTime = "2016-11-03 14:17:25";
                //string encrypt = "f933797503d6e2c36762428a280e0559";

                ////filePath = @" ";
                ////fileName = "2.html";

                //byte[] fileContentByte = new byte[1024]; // 文件内容二进制

                //#region 将文件转成二进制

                //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //fileContentByte = new byte[fs.Length]; // 二进制文件
                //fs.Read(fileContentByte, 0, Convert.ToInt32(fs.Length));
                //fs.Close();

                //#endregion


                //#region 定义请求体中的内容 并转成二进制

                //string boundary = "ceshi";
                //string Enter = "\r\n";

                //string modelIdStr = "--" + boundary + Enter
                //        + "Content-Disposition: form-data; name=\"modelId\"" + Enter + Enter
                //        + modelId + Enter;

                //string fileContentStr = "--" + boundary + Enter
                //    // + "Content-Type:application/octet-stream" + Enter
                //       + "Content-Type:application/json, text/javascript, */*; q=0.01" + Enter
                //       + "Authorization:" + authorization + Enter
                //    // + "Cookie: __jda=194339007.15263515429112072405717.1526351543.1526351543.1526351543.1; __jdv=269124599|direct|-|none|-|1526351542916; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22%24device_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E7%9B%B4%E6%8E%A5%E6%B5%81%E9%87%8F%22%2C%22%24latest_referrer%22%3A%22%22%2C%22%24latest_referrer_host%22%3A%22%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC_%E7%9B%B4%E6%8E%A5%E6%89%93%E5%BC%80%22%7D%7D; sajssdk_2015_cross_new_user=1; _ga=GA1.2.1597181775.1526351544; _gid=GA1.2.776470341.1526351544; currPage=cognitivelist; jd_503361d183eb2areaCode=bj_02; jd_503361d183eb2areaName=%E5%8D%8E%E5%8C%97; thor=63708DD2F521561F567509892777E7818884E08A30A8CED9D2ED949B06F873295A3B8B091FBD44D0041C15037E38F62B3F9F274C5604368C04826203BFC52275A697526D337C07664B01D4EEFF3E2554DC4C0AB165BF951D6325CCC8B9E2552177AC445B1EFFE1C0EE5E21E2D1A2DF18B9720FBB560FA600A368E25259E932014AC68FAC8BC50E740D2ADB6AC1A176A1F4AAFB24A8665C7E39E59919E336E988; pin=jd_503361d183eb2; unick=bossslizhiyong; __jdc=194339007; SESSION=f5e3dca2-ba83-4528-9a11-9e544e2cb14b"+ Enter



                //       + "Content-Disposition: form-data; name=\"fileContent\"; filename=\"" + fileName + "\"" + Enter + Enter;
                //string updateTimeStr = Enter + "--" + boundary + Enter
                //        + "Content-Disposition: form-data; name=\"updateTime\"" + Enter + Enter
                //        + updateTime;

                //string encryptStr = Enter + "--" + boundary + Enter
                //        + "Content-Disposition: form-data; name=\"encrypt\"" + Enter + Enter
                //        + encrypt + Enter + "--" + boundary + "--";

                //// Authorization: yGld04qyE+kr9lJppOir6Som2r58Mp26K4WF3jsdbGxqiwARKRUmBJsD1l9J2BDn2ZUzS8Wp100Q60jj62NsAQ==
                ////Cookie: __jda=194339007.15263515429112072405717.1526351543.1526351543.1526351543.1; __jdv=269124599|direct|-|none|-|1526351542916; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22%24device_id%22%3A%2216361a43b9d331-03a1d4f830a376-192a72-1296000-16361a43b9e53e%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E7%9B%B4%E6%8E%A5%E6%B5%81%E9%87%8F%22%2C%22%24latest_referrer%22%3A%22%22%2C%22%24latest_referrer_host%22%3A%22%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC_%E7%9B%B4%E6%8E%A5%E6%89%93%E5%BC%80%22%7D%7D; sajssdk_2015_cross_new_user=1; _ga=GA1.2.1597181775.1526351544; _gid=GA1.2.776470341.1526351544; currPage=cognitivelist; jd_503361d183eb2areaCode=bj_02; jd_503361d183eb2areaName=%E5%8D%8E%E5%8C%97; thor=63708DD2F521561F567509892777E7818884E08A30A8CED9D2ED949B06F873295A3B8B091FBD44D0041C15037E38F62B3F9F274C5604368C04826203BFC52275A697526D337C07664B01D4EEFF3E2554DC4C0AB165BF951D6325CCC8B9E2552177AC445B1EFFE1C0EE5E21E2D1A2DF18B9720FBB560FA600A368E25259E932014AC68FAC8BC50E740D2ADB6AC1A176A1F4AAFB24A8665C7E39E59919E336E988; pin=jd_503361d183eb2; unick=bossslizhiyong; __jdc=194339007; SESSION=f5e3dca2-ba83-4528-9a11-9e544e2cb14b

                //var modelIdStrByte = Encoding.UTF8.GetBytes(modelIdStr);//modelId所有字符串二进制

                //var fileContentStrByte = Encoding.UTF8.GetBytes(fileContentStr);//fileContent一些名称等信息的二进制（不包含文件本身）

                //var updateTimeStrByte = Encoding.UTF8.GetBytes(updateTimeStr);//updateTime所有字符串二进制

                //var encryptStrByte = Encoding.UTF8.GetBytes(encryptStr);//encrypt所有字符串二进制


                //#endregion


                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Method = "POST";
                //request.ContentType = "multipart/form-data;boundary=" + boundary;

                //Stream myRequestStream = request.GetRequestStream();//定义请求流

                //#region 将各个二进制 安顺序写入请求流 modelIdStr -> (fileContentStr + fileContent) -> uodateTimeStr -> encryptStr

                //myRequestStream.Write(modelIdStrByte, 0, modelIdStrByte.Length);

                //myRequestStream.Write(fileContentStrByte, 0, fileContentStrByte.Length);
                //myRequestStream.Write(fileContentByte, 0, fileContentByte.Length);

                //myRequestStream.Write(updateTimeStrByte, 0, updateTimeStrByte.Length);

                //myRequestStream.Write(encryptStrByte, 0, encryptStrByte.Length);

                //#endregion

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();//发送

                //Stream myResponseStream = response.GetResponseStream();//获取返回值
                //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //retString = myStreamReader.ReadToEnd();

                //myStreamReader.Close();
                //myResponseStream.Close();

            }
            catch (Exception ex)
            {


            }
            return retString;
        }
    
    }
}
