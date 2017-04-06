using Newtonsoft.Json;
using System.Configuration;

namespace ThinkNet.Utility
{
    /// <summary>
    /// HostService
    /// </summary>
    public class HostService
    {
        private HostService()
        {
        }

        /// <summary>
        /// API服务实例
        /// </summary>
        public static readonly HostService Service = new HostService();

        /// <summary>
        /// Api服务地址
        /// </summary>
        private static readonly string apiUrl = ConfigurationManager.AppSettings["apiUrl"];

        /// <summary>
        /// 获得公司Host信息
        /// </summary>
        /// <returns></returns>
        public Result<Host[]> GetHostInfo(string companyId)
        {
            var url = string.Format("{0}api/host/{1}", apiUrl, companyId);

            string content = HttpHelper.Get(url, null);

            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<Result<Host[]>>(content);
            }

            return new Result<Host[]>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Result<Host> GetHostInfo(string companyId, int client)
        {
            var url = string.Format("{0}api/host/{1}?client={2}", apiUrl, companyId, client);

            string content = HttpHelper.Get(url, null);

            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<Result<Host>>(content);
            }

            return new Result<Host>();
        }

        /// <summary>
        /// 分页获得Host信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Result<PageHosts> GetHostInfo(string key, int index, int size)
        {
            var url = string.Format("{0}api/host?key={1}&index={2}&size={3}", apiUrl, key, index, size);

            string content = HttpHelper.Get(url, null);

            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<Result<PageHosts>>(content);
            }

            return new Result<PageHosts>();
        }

        /// <summary>
        /// 分页获得Host信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <param name="link"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Result<PageHosts> GetHostInfo(string key, string client, string link, int index, int size)
        {
            var url = string.Format("{0}api/host?key={1}&client={4}&link={5}&index={2}&size={3}", apiUrl, key, index, size, client, link);

            string content = HttpHelper.Get(url, null);

            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<Result<PageHosts>>(content);
            }

            return new Result<PageHosts>();
        }

        /// <summary>
        /// 更新公司Host信息
        /// </summary>
        /// <returns></returns>
        public Result<Host> UpdateHostInfo(string companyId, object model)
        {
            var url = string.Format("{0}api/host/{1}", apiUrl, companyId);

            string content = HttpHelper.Put(url, model);

            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<Result<Host>>(content);

                //return result.code == 200;
            }

            return new Result<Host>();
        }

        /// <summary>
        /// 新增公司Host信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveHostInfo(object model)
        {
            var url = string.Format("{0}api/host", apiUrl);

            string content = HttpHelper.Post(url, model);

            if (!string.IsNullOrEmpty(content))
            {
                var result = JsonConvert.DeserializeObject<Result<Host>>(content);

                return result.code == 200;
            }

            return false;
        }

        /// <summary>
        /// 删除公司Host信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool DeleteHostInfo(string companyId, int client = 0)
        {
            var url = string.Format("{0}api/host/{1}?client={2}", apiUrl, companyId, client);

            string content = HttpHelper.Delete(url);

            if (!string.IsNullOrEmpty(content))
            {
                var result = JsonConvert.DeserializeObject<Result<Host>>(content);

                return result.code == 200;
            }

            return false;
        }
    }

    /// <summary>
    /// 企业主机信息
    /// </summary>
    public class Host
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int HostId { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 二级域名
        /// </summary>
        public string SubDomain { get; set; }

        /// <summary>
        /// 带宽
        /// </summary>
        public int BandWidth { get; set; }

        /// <summary>
        /// 公网IP
        /// </summary>
        public string PublicIp { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 连接模式
        /// 1：反向代理 2：直接IP地址
        /// </summary>
        public int LinkMode { get; set; }

        /// <summary>
        /// 客户端类型
        /// 1：Mobile 2：Client 4：Web
        /// </summary>
        public int ClientType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间(时间戳)
        /// </summary>
        public long Inserted { get; set; }

        /// <summary>
        /// 本地IP
        /// </summary>
        public string Localp { get; set; }
    }

    /// <summary>
    /// 返回信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string errorReason { get; set; }
    }

    /// <summary>
    /// 分页Host
    /// </summary>
    public class PageHosts
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// Host数组
        /// </summary>
        public Host[] Hosts { get; set; }
    }

    /// <summary>
    /// 连接模式
    /// </summary>
    public enum LinkModeEnum
    {
        /// <summary>
        /// 反向代理
        /// </summary>
        Ngrok = 1,

        /// <summary>
        /// 直接IP
        /// </summary>
        Normal = 2
    }

    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum ClientTypeEunm
    {
        /// <summary>
        /// 移动端
        /// </summary>
        Mobile = 1,

        /// <summary>
        /// PC客户端
        /// </summary>
        Client = 2,

        /// <summary>
        /// Web端
        /// </summary>
        Web = 4
    }
}