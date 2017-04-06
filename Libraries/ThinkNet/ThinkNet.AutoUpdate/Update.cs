using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace ThinkNet.AotuUpdate.Core
{
    public class Update
    {
        string FILENAME = "update.config";
        private Config config = null;
        private bool bNeedRestart = false;
        public Update(string FILENAME)
        {
            this.FILENAME = FILENAME;
            config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));
        }
        public bool CheckTask()
        {
            if (!config.Enabled)
                return false;
            WebClient client = new WebClient();
            string strXml = "";
            bool adopt = false;

            #region 验证外网服务器是否畅通，如果能访问外网就走外网,否则就走内网
            if (!string.IsNullOrEmpty(config.ServerUrl))
            {
                //外网
                adopt = Ping(config.ServerUrl.ToString().Substring(config.ServerUrl.IndexOf("http://") + 7, config.ServerUrl.LastIndexOf(":") - 7));
                if (adopt)
                {
                    strXml = client.DownloadString(config.ServerUrl);
                }
                else
                {
                    //内网
                    adopt = Ping(config.IntranetUrl.ToString().Substring(config.IntranetUrl.IndexOf("http://") + 7, config.IntranetUrl.LastIndexOf(":") - 7));
                    if (adopt)
                    {
                        if (!string.IsNullOrEmpty(config.IntranetUrl))
                        {
                            strXml = client.DownloadString(config.IntranetUrl);
                            DownloadFileInfo.isExtranet = false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
            if (string.IsNullOrEmpty(strXml))
            {
                return false;
            }
            Dictionary<string, RemoteFile> listRemotFile = ParseRemoteXml(strXml);

            List<DownloadFileInfo> downloadList = new List<DownloadFileInfo>();

            //某些文件不再需要了，删除
            List<LocalFile> preDeleteFile = new List<LocalFile>();
            foreach (LocalFile file in config.UpdateFileList)
            {
                if (listRemotFile.ContainsKey(file.Path))
                {
                    RemoteFile rf = listRemotFile[file.Path];
                    if (rf.LastVer != file.LastVer)
                    {
                        downloadList.Add(new DownloadFileInfo(rf.Url, file.Path, rf.LastVer, rf.Size));
                        file.LastVer = rf.LastVer;
                        file.Size = rf.Size;
                        if (rf.NeedRestart)
                            bNeedRestart = true;
                    }
                    listRemotFile.Remove(file.Path);
                }
                else
                {
                    preDeleteFile.Add(file);
                }
            }
            foreach (RemoteFile file in listRemotFile.Values)
            {
                downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size));
                config.UpdateFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size));
                if (file.NeedRestart)
                    bNeedRestart = true;
            }
            if (downloadList.Count > 0)
            {
                return true;
            }
            return false;
        }
        private Dictionary<string, RemoteFile> ParseRemoteXml(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                list.Add(node.Attributes["path"].Value, new RemoteFile(node));
            }
            return list;
        }
        public bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒  
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }  
    }
}
