using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class IPHelper
    {
        /// <summary>
        /// 获取本地计算机名称
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            string hostName = Dns.GetHostName();//得到主机名
            return hostName;
        }
        /// <summary>
        /// 获取本地IPv4的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPv4()
        {
            string hostName = Dns.GetHostName();//得到主机名
            IPHostEntry mIpEntry = Dns.GetHostEntry(hostName);
            for (int i = 0; i < mIpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (mIpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return mIpEntry.AddressList[i].ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 获取本地IPv6的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPv6()
        {
            string hostName = Dns.GetHostName();//得到主机名
            IPHostEntry mIpEntry = Dns.GetHostEntry(hostName);
            for (int i = 0; i < mIpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (mIpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetworkV6)
                {
                    return mIpEntry.AddressList[i].ToString();
                }
            }
            return "";
        }

        //获取外网IP
        public static string GetExternalIP()
        {
            string direction = "";
            WebRequest request = WebRequest.Create("http://www.ip138.com/ip2city.asp");
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.Default))
                {
                    direction = stream.ReadToEnd();
                }
            }

            int i = direction.IndexOf("[") + 1;
            string tempip = direction.Substring(i, 15);
            string ip = tempip.Replace("]", "").Replace(" ", "");
            return ip;
        }

        /// <summary>
        /// 在WCF服务端获取客户端的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetWCFClientIP()
        {
            //提供方法执行的上下文环境
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ipAddress = endpoint.Address;
            return ipAddress;
        }


        /// <summary>
        /// 是否是IP地址
        /// </summary>
        /// <param name="ip">当前IP地址</param>
        /// <returns></returns>
        public static bool IsIp(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            //匹配IP的正则表达式
            string regex = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            bool isIp = RegexHelper.IsMatch(ip, regex);
            return isIp;
        }
    }
}
