using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// FTP文件传输
    /// </summary>
    public class FTPWeb
    {
        #region 字    段
        
        #endregion

        #region 属    性
        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        public string RemoteHost { get; set; }
        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public string RemotePort { get; set; }

        /// <summary>
        /// 当前服务器目录
        /// </summary>
        public string RemotePath { get; set; }

        /// <summary>
        /// 登录用户账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// FTP请求路径
        /// </summary>
        public string FtpURI { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="remotePort"></param>
        /// <param name="remotePath"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public FTPWeb(string remoteHost, string remotePort, string remotePath, string userName, string password)
        {
            this.RemoteHost = remoteHost;
            this.RemotePort = remotePort;
            this.RemotePath = remotePath;
            this.UserName = userName;
            this.Password = password;
            if (!string.IsNullOrEmpty(remotePath))
            {
                if (!string.IsNullOrEmpty(remotePort))
                {
                    this.FtpURI = "ftp://" + remoteHost + ":" + remotePort + "/" + remotePath + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + remoteHost + "/" + remotePath + "/";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(remotePort))
                {
                    this.FtpURI = "ftp://" + remoteHost + ":" + remotePort + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + remoteHost + "/";
                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ftpConfig"></param>
        /// <param name="remotePath"></param>
        public FTPWeb(FTPConfig ftpConfig, string remotePath)
        {
            this.RemoteHost = ftpConfig.RemoteHost;
            this.RemotePort = ftpConfig.RemotePort;
            this.RemotePath = remotePath;
            this.UserName = ftpConfig.UserName;
            this.Password = ftpConfig.Password;
            if (!string.IsNullOrEmpty(remotePath))
            {
                if (!string.IsNullOrEmpty(this.RemotePort))
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + ":" + this.RemotePort + "/" + remotePath + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + "/" + remotePath + "/";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.RemotePort))
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + ":" + this.RemotePort + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + "/";
                }
            }
        }
        #endregion

        #region 基本方法
        #region 文件操作
        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="strDirName"></param>
        /// <returns></returns>
        public bool ExistDirectory(string strDirName)
        {
            //获取当前目录下所有的文件夹列表
            string[] dirList = DirDirectorys();
            if (dirList != null && dirList.Length>0)
            {
                //循环文件夹，如果存在该目录名称，则返回true，否则返回false。
                foreach (string str in dirList)
                {
                    if (str.Trim() == strDirName.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] DirDirectorys()
        {
            //获取当前目录下的文件夹和文件
            string[] drectory = DirDirectoryFiles();
            string m = string.Empty;
            //循环目录，取出目录下的文件，然后添加到返回结果中
            int idx = 0;
            if (drectory != null && drectory.Length>0)
            {
                foreach (string str in drectory)
                {
                    //if (str.Trim().Substring(0, 1).ToUpper() == "D")
                    idx = str.IndexOf("<DIR>");
                    if (idx != -1)
                    {
                        m += str.Substring(idx + 5).Trim() + "\n";
                    }
                }
            }
            char[] n = new char[] { '\n' };
            if(!string.IsNullOrEmpty(m))
            {
                return m.Split(n);
            }
            return null;
        }
        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] DirDirectoryFiles()
        {
            string[] directoryFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI));
                //ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                //定义执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取WebResponse对象
                WebResponse response = reqFTP.GetResponse();
                //定义ftp流
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)//如果还没读完文件
                {
                    result.Append(line);
                    result.Append("\n");

                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();

                directoryFiles = null;
                if (result.Length>0)
                {
                    result.Remove(result.ToString().LastIndexOf("\n"), 1);
                    directoryFiles = result.ToString().Split('\n');
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return directoryFiles;
        }
        
        /// <summary>
        /// 获得文件列表(仅文件)
        /// </summary>
        /// <param name="strMask">文件名的匹配字符串</param>
        public string[] Dir(string strMask)
        {
            string[] files;
            FtpWebRequest reqFTP;
            try
            {
                StringBuilder result = new StringBuilder();
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI));
                //指定数据传输类型
                reqFTP.UseBinary = true;
                //ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                //ftp操作什么命令
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取WebResponse相应对象
                WebResponse response = reqFTP.GetResponse();
                //获取ftp流
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                string ext = "";
                while (line != null) //如果没有读完
                {
                    ext = Path.GetExtension(line);
                    if(!string.IsNullOrEmpty(ext))
                    {
                        //如果匹配类型不是匹配所有文件名
                        if (strMask.Trim() != string.Empty && strMask.Trim() != "*.*")
                        {
                            //根据匹配符匹配文件名，并加到结果列表中
                            string mask_ = strMask.Substring(0, strMask.IndexOf("*"));
                            if (line.Substring(0, mask_.Length) == mask_)
                            {
                                result.Append(line);
                                result.Append("\n");
                            }
                        }
                        else
                        {
                            //将所有的文件名加到结果列表中
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    line = reader.ReadLine();  
                }
                reader.Close();
                response.Close();

                files = null;
                if (result.Length > 0)
                {
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    files = result.ToString().Split('\n');
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return files;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名</param>
        public void Delete(string strFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strFileName));
                //ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //定义执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                //删除文件
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="strOldFileName">旧文件名</param>
        /// <param name="strNewFileName">新文件名</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strOldFileName));
                //定义要执行的命令方式
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                //修改的名字
                reqFTP.RenameTo = strNewFileName;
                //数据传输方式定义
                reqFTP.UseBinary = true;
                //ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取ftp相应对象
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流
                Stream ftpStream = response.GetResponseStream();

                //关闭流
                ftpStream.Close();
                //关闭相应对象
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string strFileName)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strFileName));
                //定义要执行的命令方式
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                //指定传输数据类型
                reqFTP.UseBinary = true;
                //给ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取请求对象
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流
                Stream ftpStream = response.GetResponseStream();
                //获取要操作的文件长度
                fileSize = response.ContentLength;

                //关闭流
                ftpStream.Close();
                //关闭相应对象
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //返回文件大小信息
            return fileSize;
        }

        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="strRemoteFileName"></param>
        /// <returns></returns>
        public bool ExistFile(string strRemoteFileName)
        {
            //获取当前目录下的文件列表
            string[] fileList = Dir("*.*");

            //如果对象不为空
            if (fileList != null)
            {
                //循环结果集，如果有存在要查询的文件名，则返回true，否则返回false
                foreach (string str in fileList)
                {
                    if (str.Trim() == strRemoteFileName.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 上传和下载
        /// <summary>
        /// 下载一批文件
        /// </summary>
        /// <param name="strFileNameMask">文件名的匹配字符串</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            string[] strFiles = Dir(strFileNameMask);
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//一般来说strFiles的最后一个元素可能是空字符串
                {
                    Get(strFile, strFolder, strFile);
                }
            }
        }
        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //创建本地文件流
                FileStream outputStream = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strRemoteFileName));
                //指定操作命令
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                //指定传输数据的类型
                reqFTP.UseBinary = true;
                //ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取ftp请求对象
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流
                Stream ftpStream = response.GetResponseStream();
                //获取要下载的文件的长度
                long cl = response.ContentLength;

                //定义缓存为2kb
                int bufferSize = 2048;
                //定义每次读取的文件长度
                int readCount;
                byte[] buffer = new byte[bufferSize];
                //读文件并返回标志位
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                //如果标志位大于0
                while (readCount > 0)
                {
                    //将缓存中的内容写入文件中
                    outputStream.Write(buffer, 0, readCount);
                    //读文件并返回标志位
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                //关闭ftp流
                ftpStream.Close();
                //关闭文件流
                outputStream.Close();
                //关闭请求对象
                response.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 下载一个文件的内容(字符串)
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <returns>返回文件内容</returns>
        public string GetString(string strRemoteFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strRemoteFileName));
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;
                //关闭代理
                reqFTP.Proxy = null;
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取ftp请求对象
                FtpWebResponse resoponse = (FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流对象
                Stream ftpStream = resoponse.GetResponseStream();
                //读取流内容
                StreamReader readStream = new StreamReader(ftpStream, Encoding.Default);
                //转化成字符串
                string receivestream = readStream.ReadToEnd();

                //关闭流
                ftpStream.Close();
                //关闭ftp请求
                resoponse.Close();

                //返回文件内容
                return receivestream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 下载一个文件的内容(字节)
        /// </summary>
        /// <param name="strRemoteFileName"></param>
        /// <returns></returns>
        public Byte[] GetByte(string strRemoteFileName)
        {
            byte[] attByte = null;
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(this.FtpURI + strRemoteFileName));
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;
                //关闭代理
                reqFTP.Proxy = null;
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取ftp请求对象
                FtpWebResponse resoponse = (System.Net.FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流对象
                Stream ftpStream = resoponse.GetResponseStream();
                //读取流内容
                StreamReader readStream = new StreamReader(ftpStream, Encoding.Default);
                //转化成字符串
                attByte = Encoding.Default.GetBytes(readStream.ReadToEnd());

                //关闭流
                ftpStream.Close();
                //关闭ftp请求
                resoponse.Close();

                //返回文件内容
                return attByte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 上传一批文件
        /// </summary>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strFileNameMask">文件名匹配字符(可以包含*和?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {
            string[] strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            foreach (string strFile in strFiles)
            {
                Put(strFile);
            }
        }
        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名(包含路径)</param>
        public void Put(string strFileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                string fileName = "";
                if (Path.GetExtension(strFileName) == "")
                {
                    fileName = Path.GetFileNameWithoutExtension(strFileName);
                }
                else
                {
                    fileName = Path.GetFileName(strFileName);
                }
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(this.FtpURI + fileName));
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;

                //定义缓存2kb
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                //记录读取的长度
                int contentLen;

                FileStream fileStream = new FileStream(strFileName, FileMode.Open);
                //获取ftp流
                Stream strm = reqFTP.GetRequestStream();
                //读出文件流中的2kb
                contentLen = fileStream.Read(buff, 0, buffLength);
                //如果记录读取的长度不为0，循环读取
                while (contentLen != 0)
                {
                    //写入ftp流
                    strm.Write(buff, 0, contentLen);
                    //从文件流中读取文件内容
                    contentLen = fileStream.Read(buff, 0, buffLength);
                }
                //关闭ftp流
                strm.Close();
                //关闭文件流
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 上传一个文件(文件流)
        /// </summary>
        /// <param name="strFileName">本地文件名(不包含路径)</param>
        /// <param name="fileStream">文件流</param>
        public void PutStream(string strFileName, Stream fileStream)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(this.FtpURI + strFileName));
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;
                //定义缓存2kb
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                //记录读取的长度
                int contentLen;
                //获取ftp流
                Stream strm = reqFTP.GetRequestStream();
                //读出文件流中的2kb
                contentLen = fileStream.Read(buff, 0, buffLength);
                //如果记录读取的长度不为0，循环读取
                while (contentLen != 0)
                {
                    //写入ftp流
                    strm.Write(buff, 0, contentLen);
                    //从文件流中读取文件内容
                    contentLen = fileStream.Read(buff, 0, buffLength);
                }
                //关闭ftp流
                strm.Close();
                //关闭文件流
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 上传一个文件(字节)
        /// </summary>
        /// <param name="strFileName">本地文件名(不包含路径)</param>
        /// <param name="bytes">文件字节</param>
        public void PutByte(string strFileName, byte[] bytes)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(this.FtpURI + strFileName));
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;
                // 把上传的文件写入流 
                System.IO.Stream strm = reqFTP.GetRequestStream();

                // 把内容从file stream 写入 upload stream 
                strm.Write(bytes, 0, bytes.Length);

                // 关闭流 
                strm.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 上传一个文件(字符串)
        /// </summary>
        /// <param name="strFileName">本地文件名(不包含路径)</param>
        /// <param name="content">字符串内容</param>
        public void PutString(string strFileName, string content)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(this.FtpURI + strFileName));
                // ftp用户名和密码 
                reqFTP.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                // 指定执行什么命令 
                reqFTP.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                // 指定数据传输类型 
                reqFTP.UseBinary = true;

                byte[] contents = System.Text.Encoding.Default.GetBytes(content);
                // 把上传的文件写入流 
                System.IO.Stream strm = reqFTP.GetRequestStream();

                // 把内容从file stream 写入 upload stream 
                strm.Write(contents, 0, contents.Length);

                // 关闭流 
                strm.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 目录操作
        /// <summary>
        /// 改变目录
        /// </summary>
        /// <param name="strDirName">新的工作目录名</param>
        public void ChangeDir(string strDirName)
        {
            //修改当前路径
            this.RemotePath = strDirName;

            if (!string.IsNullOrEmpty(this.RemotePath))
            {
                if (!string.IsNullOrEmpty(this.RemotePort))
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + ":" + this.RemotePort + "/" + this.RemotePath + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + "/" + this.RemotePath + "/";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.RemotePort))
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + ":" + this.RemotePort + "/";
                }
                else
                {
                    this.FtpURI = "ftp://" + this.RemoteHost + "/";
                }
            }
            //if (!string.IsNullOrEmpty(this.RemotePort))
            //{
            //    this.FtpURI = "ftp://" + this.RemoteHost + ":" + this.RemotePort + "/" + this.RemotePath + "/";
            //}
            //else
            //{
            //    this.FtpURI = "ftp://" + this.RemoteHost + "/" + this.RemotePath + "/";
            //}
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void CreateDir(string strDirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //已存在不创建
                if (ExistDirectory(strDirName))
                {
                    return;
                }
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strDirName));
                //定义要执行的命令方式
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                //指定传输数据类型
                reqFTP.UseBinary = true;
                //给ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                reqFTP.KeepAlive = false;
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  
                //获取请求对象
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //获取ftp流
                Stream ftpStream = response.GetResponseStream();

                //关闭ftp流
                ftpStream.Close();
                //关闭请求对象
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建目录(多级)
        /// </summary>
        /// <param name="strDirNameMore">要创建的目录(多级)</param>
        /// <returns></returns>
        public void CreateDirMore(string strDirNameMore)
        {
            try
            {
                if (string.IsNullOrEmpty(strDirNameMore))
                {
                    return;
                }
                strDirNameMore = strDirNameMore.Replace("\\", "/");
                string[] directors = strDirNameMore.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if(directors!=null&&directors.Length>0)
                {
                    string parentDirector = "";
                    foreach (string director in directors)
                    {
                        CreateDir(director);
                        parentDirector = parentDirector+(string.IsNullOrEmpty(parentDirector) ? "" : "/") + director;
                        ChangeDir(parentDirector);
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void RemoveDir(string strDirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpURI + strDirName));
                //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
                reqFTP.KeepAlive = false;
                //给ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(this.UserName, this.Password);
                //选择主动还是被动模式 ,  这句要加上的。
                //reqFTP.UsePassive = false;  

                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        #region 其他方法
        
        #endregion 
    }
}
