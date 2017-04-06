using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Utility;

namespace ThinkNet.AutoUpdate
{
    public delegate void ShowHandler();

    /// <summary>
    /// 客户端更新
    /// </summary>
    public class AutoUpdaterClient
    {
        #region 字    段
        /// <summary>
        /// 更新配置对象
        /// </summary>
        private UpdateConfig _mUpdateConfig = null;
        /// <summary>
        /// 是否需要重启
        /// </summary>
        private bool _isNeedRestart = false;
        /// <summary>
        /// 下载文件列表(临时缓存,回滚)
        /// </summary>
        private List<DownloadFileInfo> _downloadFileListTemp = null;
        /// <summary>
        /// 远程版本号
        /// </summary>
        private string _RemoteVersion = "";
        /// <summary>
        /// 远程描述
        /// </summary>
        private string _RemoteDescription = "";
        /// <summary>
        /// 服务代理
        /// </summary>
        private IUpdateFileService _proxy = null;

        private string _SystemBinUrl = AppDomain.CurrentDomain.BaseDirectory;
        private string _TempFolder = "TempFolder";
        private string _UpdateConfigName = "update.config";
        private string _Version = "2.0.0.0";
        private string _EnterpriseCode = "";
        private string _Address = "";//内部网WCF地址(包括端口)
        private string _UpdateServiceConfigName = "updateservice.xml";

        //FTP参数
        private Dictionary<string, string> _DicFtpParameter = null;
        /// <summary>
        /// 服务代理
        /// </summary>
        private FTPWeb _ftpWeb = null;

        public event ShowHandler OnShow;
        #endregion

        #region 属    性
        /// <summary>
        /// 是否通过外网更新
        /// </summary>
        public bool IsExtranet { get; private set; }
        /// <summary>
        /// 如果IsExtranet为true,判断是否通FTP更新
        /// </summary>
        public bool IsFtp { get; private set; }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">更新配置文件(全路径)</param>
        /// <param name="version">版本号</param>
        /// <param name="enterpriseCode">企业编号</param>
        /// <param name="address">服务器地址(IP地址+:+端口号)</param>
        public AutoUpdaterClient(string fileName, string version, string enterpriseCode, string address)
        {
            _mUpdateConfig = UpdateConfig.LoadUpdateConfig(fileName);
            _UpdateConfigName = Path.GetFileName(fileName);
            if(!string.IsNullOrEmpty(version))
            {
                _Version = version;
            }
            _EnterpriseCode = enterpriseCode;
            _Address = address;
        }

        #endregion

        #region 基本方法
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (!_mUpdateConfig.Enabled)
            { 
                return; 
            }

            #region 01.检查网络连接情况
            bool isIntranet = CheckIntranetCorrect();//内网
            bool isExtranet = CheckExtranetCorrect();//外网
            if(!isIntranet&&!isExtranet)//内外网都不通
            {
                return;
            }
            #endregion

            #region 02.获取要更新的文件列表及版本号比较
            Dictionary<string, RemoteFile> listRemotFile = null;
            XmlDocument document = new XmlDocument();
            string version="";
            string description = "";
            if(isIntranet&&!isExtranet)//内网通,外网不通
            {
                listRemotFile = ParseRemoteXmlIntranet(out version,out description);
                this.IsExtranet = false;
                _RemoteVersion = version;
                _RemoteDescription = description;
            }
            else if (!isIntranet && isExtranet)//内网不通,外网通
            {
                listRemotFile = ParseRemoteXmlExtranet(out version, out description);
                this.IsExtranet = true;
                _RemoteVersion = version;
                _RemoteDescription = description;
            }
            else if (isIntranet && isExtranet)//内网通,外网通
            {
                //内网的文件
                listRemotFile = ParseRemoteXmlIntranet(out version, out description);
                bool result = (listRemotFile == null || listRemotFile.Count <= 0) ? false : true;
                //外网的文件
                string versionExt = "";
                string descriptionExt = "";
                Dictionary<string, RemoteFile> listRemotFileExt = null;
                listRemotFileExt = ParseRemoteXmlExtranet(out versionExt, out descriptionExt);
                bool resultExt = (listRemotFileExt == null || listRemotFileExt.Count <= 0) ? false : true;
                //比较
                if (!result&&!resultExt)//内外网文件均为空
                {
                    return;
                }
                else if (result && !resultExt)//内网不为空,外网的为空
                {
                    this.IsExtranet = false;
                    _RemoteVersion = version;
                    _RemoteDescription = description;
                }
                else if (!result && resultExt)//内网为空,外网不为空
                {
                    this.IsExtranet = true;
                    listRemotFile = listRemotFileExt;
                    _RemoteVersion = versionExt;
                    _RemoteDescription = descriptionExt;
                }
                else if (result && resultExt)//内网不为空,外网不为空,比较版本
                {
                    Version vIntranet = new Version(version);
                    Version vExtranet = new Version(versionExt);
                    if (vIntranet >= vExtranet)//取内网
                    {
                        this.IsExtranet = false;
                        _RemoteVersion = version;
                        _RemoteDescription = description;
                    }
                    else//取外网
                    {
                        this.IsExtranet = true;
                        listRemotFile = listRemotFileExt;
                        _RemoteVersion = versionExt;
                        _RemoteDescription = descriptionExt;
                    }
                }
            }
            if(listRemotFile==null||listRemotFile.Count<=0)
            {
                return;
            }
            #endregion

            #region 03.版本号比较(只有服务器版本更新了,客户端才能更新)
            //远程版本与服务器版本比较(只有服务器版本更新了,客户端才能更新)
            Version vRemote = new Version(_RemoteVersion);
            Version vServer = new Version(_Version);//参数表中的版本
            Version vLocal = null;
            if (vServer>=vRemote)//服务器已更新
            {
                //服务器版本与客户端版本比较
                vLocal = new Version(_mUpdateConfig.Version);
                if(vLocal>=vServer)//客户端已是最新版
                {
                    return;
                }
            }
            else
            {
                //提示先更新服务器
                return;
            }
            #endregion

            #region 04.比较文件版本确定要下载的文件
            List<DownloadFileInfo> downloadList = new List<DownloadFileInfo>();//需要下载的文件列表
            RemoteFile mRemoteFile = null;
            //某些文件不再需要了，删除
            List<LocalFile> preDeleteFile = new List<LocalFile>();
            foreach (LocalFile file in _mUpdateConfig.LocalFileList)
            {
                if (listRemotFile.ContainsKey(file.Path))//比较本地文件与远程文件，确定要下载的文件
                {
                    mRemoteFile = listRemotFile[file.Path];
                    vRemote = new Version(mRemoteFile.LastVer);
                    vLocal = new Version(file.LastVer);
                    if (vRemote > vLocal)
                    {
                        downloadList.Add(new DownloadFileInfo(mRemoteFile.Url, file.Path, mRemoteFile.LastVer, mRemoteFile.Size));
                        file.LastVer = mRemoteFile.LastVer;
                        file.Size = mRemoteFile.Size;
                        if (mRemoteFile.NeedRestart)
                        { 
                            _isNeedRestart = true; 
                        }
                    }
                    listRemotFile.Remove(file.Path);//移除已存在的，剩下的就是新加的文件
                }
                else
                {
                    preDeleteFile.Add(file);
                }
            }
            //新增加的文件也要加入下载的列表
            foreach (RemoteFile file in listRemotFile.Values)
            {
                downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size));
                _mUpdateConfig.LocalFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size));
                if (file.NeedRestart)
                { 
                    _isNeedRestart = true; 
                }
            }
            _downloadFileListTemp = downloadList;//临时缓存，回滚

            //没有需要更新的文件
            if(downloadList.Count<=0)
            {
                return;
            }
            #endregion

            #region 05.下载并更新
            FrmDownloadConfirm frmConfirm = new FrmDownloadConfirm(downloadList);
            //frmConfirm.ShowInTaskbar = false;

            if (this.OnShow != null)
                this.OnShow();

            if (DialogResult.OK == frmConfirm.ShowDialog())
            {
                foreach (LocalFile file in preDeleteFile)
                {
                    //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
                    //if (File.Exists(filePath))
                    //    File.Delete(filePath);

                    _mUpdateConfig.LocalFileList.Remove(file);
                }
                DownloadFiles(downloadList);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollBack()
        {
            foreach (DownloadFileInfo file in _downloadFileListTemp)
            {
                //string tempUrlPath = Utility.GetFolderUrl(file,_SystemBinUrl,_TempFolder);
                string oldPath = string.Empty;
                try
                {
                    //if (!string.IsNullOrEmpty(tempUrlPath))
                    //{
                    //    oldPath = Path.Combine(_SystemBinUrl + tempUrlPath.Substring(1), file.FileName);
                    //}
                    //else
                    //{
                    //    oldPath = Path.Combine(_SystemBinUrl, file.FileName);
                    //}

                    oldPath = Path.Combine(_SystemBinUrl, file.FileFullName);

                    if (oldPath.EndsWith("_"))
                        oldPath = oldPath.Substring(0, oldPath.Length - 1);

                    Utility.MoveFolderToOld(oldPath + ".old", oldPath);

                }
                catch (Exception ex)
                {
                    //log the error message,you can use the application's log code
                }
            }
        }
        #endregion

        #region 其他方法
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="downloadList">需要下载的文件列表</param>
        private void DownloadFiles(List<DownloadFileInfo> downloadList)
        {
            DialogResult result = DialogResult.Cancel;
            if (this.IsExtranet)//从外网更新
            {
                if(IsFtp)
                {
                    FrmFtpProgress frmProgress = new FrmFtpProgress(downloadList, _SystemBinUrl, _TempFolder);
                    frmProgress.FtpWeb = _ftpWeb;
                    result = frmProgress.ShowDialog();
                }
                else
                {
                    FrmHttpProgress frmProgress = new FrmHttpProgress(downloadList, _SystemBinUrl, _TempFolder);
                    //frmProgress.ShowInTaskbar = false;
                    result = frmProgress.ShowDialog();
                }
            }
            else//从内网更新
            {
                FrmWcfProgress frmProgress = new FrmWcfProgress(downloadList, _SystemBinUrl, _TempFolder);
                //frmProgress.ShowInTaskbar = false;
                frmProgress.Proxy = _proxy;
                result = frmProgress.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                //Update successfully
                _mUpdateConfig.Version = _RemoteVersion;
                _mUpdateConfig.Description = _RemoteDescription;
                _mUpdateConfig.SaveUpdateConfig(Path.Combine(_SystemBinUrl, _UpdateConfigName));

                if (_isNeedRestart)
                {
                    //Delete the temp folder
                    Directory.Delete(Path.Combine(_SystemBinUrl, _TempFolder), true);

                    MessageBox.Show("更新完成,需要重新启动程序更新才生效,单击确定以重新启动程序!", "升级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Utility.RestartApplication();
                }
            }
        }

        /// <summary>
        /// 解释外网的远程配置文件
        /// </summary>
        /// <param name="version"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private Dictionary<string, RemoteFile> ParseRemoteXmlExtranet(out string version,out string description)
        {
            version = "";
            description = "";
            if (_mUpdateConfig == null)
            { 
                return new Dictionary<string, RemoteFile>(); 
            }

            XmlDocument document = new XmlDocument();
            if(IsFtp)
            {
                try
                {
                    _ftpWeb = new FTPWeb(_DicFtpParameter["RemoteHost"], _DicFtpParameter["RemotePort"],
                    _DicFtpParameter["RemotePath"],
                    _DicFtpParameter["UserName"], _DicFtpParameter["Password"]);
                    bool isExits = _ftpWeb.ExistFile(_UpdateServiceConfigName);//远程配置文件名称
                    if (isExits)
                    {
                        string xml = _ftpWeb.GetString(_UpdateServiceConfigName);
                        if(string.IsNullOrEmpty(xml))
                        {
                            return new Dictionary<string, RemoteFile>(); 
                        }
                        document.LoadXml(xml);
                    }
                }
                catch(Exception)
                {
                    return new Dictionary<string, RemoteFile>(); 
                }
            }
            else
            {
                document.Load(_mUpdateConfig.ServerUrl);//http
            }

            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            if (document.DocumentElement != null && document.DocumentElement.ChildNodes.Count>0)
            {
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    if (node.Name == "Version")
                    {
                        version = node.InnerText;
                    }
                    if (node.Name == "Description")
                    {
                        description = node.InnerText;
                    }
                    if (node.Name == "UpdateFileList")
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            list.Add(child.Attributes["path"].Value, new RemoteFile(child));
                        }
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 解释内网的远程配置文件
        /// </summary>
        /// <param name="version"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private Dictionary<string, RemoteFile> ParseRemoteXmlIntranet(out string version, out string description)
        {
            version = "";
            description = "";
            if (_mUpdateConfig == null)
            {
                return new Dictionary<string, RemoteFile>();
            }

            XmlDocument document = new XmlDocument();
            string xml = _proxy.GetUpdateFileXml(_UpdateServiceConfigName);//服务器的更新文件
            if (string.IsNullOrEmpty(xml))
            {
                return new Dictionary<string, RemoteFile>();
            }
            document.LoadXml(xml);

            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            if (document.DocumentElement != null && document.DocumentElement.ChildNodes.Count > 0)
            {
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    if (node.Name == "Version")
                    {
                        version = node.InnerText;
                    }
                    if (node.Name == "Description")
                    {
                        description = node.InnerText;
                    }
                    if (node.Name == "UpdateFileList")
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            list.Add(child.Attributes["path"].Value, new RemoteFile(child));
                        }
                    }
                }
            }

            return list;
        }
        
        /// <summary>
        /// 检查内部网络是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckIntranetCorrect()
        {
            if (_mUpdateConfig == null) return false;

            _proxy = CreateUpdateFileProxy();//创建内部网络服务代理
            if(_proxy==null)//内部网络不通
            {
                return false;
            }
            try
            {
                //是否能连通服务器，并判断文件是否存在
                bool result = _proxy.IsExistUpdateFileXml(_UpdateServiceConfigName);
                return result;
            }
            catch(Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 创建WCF服务代理
        /// </summary>
        /// <returns></returns>
        private IUpdateFileService CreateUpdateFileProxy()
        {
            if (_mUpdateConfig == null) return null;

            try
            {
                string address = _mUpdateConfig.IntranetUrl.ToLower().Replace("net.tcp://", "");
                string relatePath = address.Substring(address.IndexOf('/') + 1);
                address = "net.tcp://" + _Address + "/" + relatePath;

                IUpdateFileService proxy = null;
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true);
                //BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                ChannelFactory<IUpdateFileService> channel =
                    new ChannelFactory<IUpdateFileService>(
                        binding,
                        new EndpointAddress(address));
                proxy = channel.CreateChannel();

                return proxy;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 检查外部网络是否正确
        /// </summary>
        /// <returns></returns>
        public bool CheckExtranetCorrect()
        {
            try
            {
                //外部网络是否连通
                if (!string.IsNullOrEmpty(_mUpdateConfig.ServerUrl))
                {
                    //版本号
                    bool result = RegexHelper.IsMatch(_mUpdateConfig.ServerUrl, "{VersionNumber}");
                    if (result)
                    {
                        _mUpdateConfig.ServerUrl = _mUpdateConfig.ServerUrl.Replace("{VersionNumber}", _Version);
                    }
                    ////企业编号
                    //result = RegexHelper.IsMatch(_mUpdateConfig.ServerUrl, "{EnterpriseCode}");
                    //if (result)
                    //{
                    //    _mUpdateConfig.ServerUrl = _mUpdateConfig.ServerUrl.Replace("{EnterpriseCode}", _EnterpriseCode);
                    //}
                    string address = _mUpdateConfig.ServerUrl;//外部网络地址
                    //ftp或http地址
                    if (address.StartsWith("ftp"))//通过ftp更新
                    {
                        string[] arrAddress = DataTypeConvert.ToArray(address, '?');
                        if (arrAddress == null || arrAddress.Length!=2)
                        {
                            return false;
                        }
                        address = arrAddress[0];
                        address = address.Replace("ftp://", "");
                        IsFtp = true;
                        _DicFtpParameter = new Dictionary<string, string>();
                        _DicFtpParameter.Add("RemoteHost", "");
                        _DicFtpParameter.Add("RemotePort", "");
                        _DicFtpParameter.Add("UserName", "");
                        _DicFtpParameter.Add("Password", "");
                        _DicFtpParameter.Add("RemotePath", "");//配置文件所在的目录,如:VersionFiles/V2.1.0.0/0001/Update/Client
                        //获取FTP的登陆用户名和密码
                        string ftpParam = arrAddress[1];
                        string[] arrFtpParam = DataTypeConvert.ToArray(ftpParam, '&');
                        if (arrFtpParam == null || arrFtpParam.Length != 2)
                        {
                            return false;
                        }
                        _DicFtpParameter["UserName"] = arrFtpParam[0];
                        _DicFtpParameter["Password"] = CryptoFactory.Create(CrytoType.TripleDES).Decrypt(arrFtpParam[1]);
                    }
                    else if (address.StartsWith("http"))//通过http更新
                    {
                        IsFtp = false;
                        address = address.Replace("http://", "");
                    }
                    //获取主机地址
                    string hostAddress = address.Substring(0, address.IndexOf('/'));
                    if (hostAddress.Contains(":"))
                    {
                        string[] arrHost = DataTypeConvert.ToArray(hostAddress, ':');
                        if (arrHost == null || arrHost.Length != 2)
                        {
                            return false;
                        }
                        hostAddress = arrHost[0];
                        if(IsFtp)
                        {
                            _DicFtpParameter["RemoteHost"] = arrHost[0];
                            _DicFtpParameter["RemotePort"] = arrHost[1];
                            string remotePath = address.Substring(address.IndexOf('/')+1);
                            _DicFtpParameter["RemotePath"] = Path.GetDirectoryName(remotePath).Replace("\\", "/");
                        }
                    }
                    else
                    {
                        if (IsFtp)
                        {
                            _DicFtpParameter["RemoteHost"] = hostAddress;
                            _DicFtpParameter["RemotePort"] = "21";
                            string remotePath = address.Substring(address.IndexOf('/') + 1);
                            _DicFtpParameter["RemotePath"] = Path.GetDirectoryName(remotePath).Replace("\\","/");
                        }
                    }
                    return CheckNetwork("www.baidu.com");//外部网是否连通
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 检查网络是否连通
        /// </summary>
        /// <param name="address">IP地址或网址</param>
        /// <returns></returns>
        public bool CheckNetwork (string address)
        {
            try
            {
                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                string data = "Test Data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000; // Timeout 时间，单位：毫秒  
                System.Net.NetworkInformation.PingReply reply = p.Send(address, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch(Exception)
            {
                return false;
            }
        }  
        #endregion
    }
}