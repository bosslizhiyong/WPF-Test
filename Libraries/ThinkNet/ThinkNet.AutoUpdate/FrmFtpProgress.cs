using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml;
using ThinkNet.Utility;
using ThinkNet.Infrastructure.Core;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 下载进度条
    /// </summary>
    public partial class FrmFtpProgress : Form
    {
        #region 字    段
        private bool isFinished = false;
        private List<DownloadFileInfo> downloadFileList = null;
        private List<DownloadFileInfo> allFileList = null;
        private Thread downLoadThread = null;

        private string _SystemBinUrl = AppDomain.CurrentDomain.BaseDirectory;
        private string _TempFolder = "TempFolder";
        #endregion

        #region 属    性
        /// <summary>
        /// 
        /// </summary>
        public FTPWeb FtpWeb
        { private get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">需要下载的文件列表</param>
        public FrmFtpProgress(List<DownloadFileInfo> downloadfileList)
            : this(downloadfileList, AppDomain.CurrentDomain.BaseDirectory, "TempFolder")
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">需要下载的文件列表</param>
        /// <param name="systemBinUrl"></param>
        /// <param name="tempFolder"></param>
        /// <param name="updateConfigName"></param>
        public FrmFtpProgress(List<DownloadFileInfo> downloadfileList, string systemBinUrl, string tempFolder)
        {
            InitializeComponent();

            this.downloadFileList = downloadfileList;
            _SystemBinUrl = systemBinUrl;
            _TempFolder = tempFolder;
            allFileList = new List<DownloadFileInfo>();
            foreach (DownloadFileInfo file in downloadfileList)
            {
                allFileList.Add(file);
            }
        }
        #endregion

        #region 页面事件
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isFinished && DialogResult.No == MessageBox.Show("正在更新，是否取消？", "升级提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                buttonOk.Enabled = true;
                e.Cancel = true;
                return;
            }
            else
            {
                if(downLoadThread!=null)
                {
                    downLoadThread.Abort();
                }
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            downLoadThread = new Thread(new ThreadStart(DownLoadFile));
            downLoadThread.IsBackground = true;
            downLoadThread.SetApartmentState(ApartmentState.STA);
            downLoadThread.Name = "downLoadThreade";
            downLoadThread.Start();  
        }

        private void OnCancel(object sender, EventArgs e)
        {
            //bCancel = true;
            //evtDownload.Set();
            //evtPerDonwload.Set();
            ShowErrorAndRestartApplication();
        }
        #endregion

        #region 基本方法
        private void DownLoadFile()
        {
            //临时文件夹
            string tempFolderPath = Path.Combine(_SystemBinUrl, _TempFolder);
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            //总进度条
            if (progressBarTotal.InvokeRequired)
            {
                progressBarTotal.Invoke((ThreadStart)delegate
                {
                    progressBarTotal.Maximum = downloadFileList.Count;
                });
            }
            if (progressBarCurrent.InvokeRequired)
            {
                progressBarCurrent.Invoke((ThreadStart)delegate
                {
                    progressBarCurrent.Value = downloadFileList.Count;
                    //System.Threading.Thread.Sleep(300);
                });
            }

            #region 下载文件
            string url = "";
            string dirName = "";
            string fileName = "";
            int i = 1;
            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                //正在下载的文件名称
                ShowCurrentlabelText(file.FileName);

                url = file.DownloadUrl.Replace("ftp://" + FtpWeb.RemoteHost + ":" + FtpWeb.RemotePort + "/", "");
                dirName = Path.GetDirectoryName(url).Replace("\\", "/");
                fileName = Path.GetFileName(url);
                try
                {
                    FtpWeb.ChangeDir(dirName);
                    bool isExits = FtpWeb.ExistFile(fileName);
                    if (isExits)
                    {
                        DirectoryHelper.CreateDirectoryByFilePath(Path.Combine(tempFolderPath, file.FileFullName));
                        //下载文件
                        FtpWeb.Get(fileName, tempFolderPath, file.FileFullName);
                    }
                }
                catch(Exception ex)
                {
                    ShowCurrentlabelText(ex.Message);
                    break;
                }

                //进度条
                if (progressBarCurrent.InvokeRequired)
                {
                    progressBarCurrent.Invoke((ThreadStart)delegate
                    {
                        progressBarCurrent.Value = i;
                        //System.Threading.Thread.Sleep(300);
                    });
                }
                
                //总进度
                if (progressBarTotal.InvokeRequired)
                {
                    progressBarTotal.Invoke((ThreadStart)delegate
                    {
                        progressBarTotal.Value = i;
                        System.Threading.Thread.Sleep(300);
                    });
                }
                i++;
            }
            if (this.downloadFileList.Count==(i-1))
            {
                this.downloadFileList.Clear();
            }
            #endregion

            #region 处理下载
            this.Invoke(new MethodInvoker(delegate
            {
                buttonOk.Enabled = false;
            }));
            //Debug.WriteLine("All Downloaded");
            foreach (DownloadFileInfo file in this.allFileList)
            {
                //string tempUrlPath = Utility.GetFolderUrl(file,_SystemBinUrl,_TempFolder);
                string oldPath = string.Empty;
                string newPath = string.Empty;
                try
                {
                    oldPath = Path.Combine(_SystemBinUrl, file.FileFullName);
                    newPath = Path.Combine(_SystemBinUrl + _TempFolder, file.FileFullName);

                    //if (!string.IsNullOrEmpty(tempUrlPath))
                    //{
                    //    oldPath = Path.Combine(_SystemBinUrl + tempUrlPath.Substring(1), file.FileName);
                    //    newPath = Path.Combine(_SystemBinUrl + _TempFolder + tempUrlPath, file.FileName);
                    //}
                    //else
                    //{
                    //    oldPath = Path.Combine(_SystemBinUrl, file.FileName);
                    //    newPath = Path.Combine(_SystemBinUrl + _TempFolder, file.FileName);
                    //}

                    ////just deal with the problem which the files EndsWith xml can not download
                    //System.IO.FileInfo f = new FileInfo(newPath);
                    //if (!file.Size.ToString().Equals(f.Length.ToString()) && !file.FileName.ToString().EndsWith(".xml"))
                    //{
                    //    ShowErrorAndRestartApplication();
                    //}


                    //Added for dealing with the config file download errors
                    string newfilepath = string.Empty;
                    if (newPath.Substring(newPath.LastIndexOf(".") + 1).Equals("config_"))
                    {
                        if (System.IO.File.Exists(newPath))
                        {
                            if (newPath.EndsWith("_"))
                            {
                                newfilepath = newPath;
                                newPath = newPath.Substring(0, newPath.Length - 1);
                                oldPath = oldPath.Substring(0, oldPath.Length - 1);
                            }
                            File.Move(newfilepath, newPath);
                        }
                    }
                    //End added

                    Utility.MoveFolderToOld(oldPath, newPath);

                    //if (File.Exists(oldPath))
                    //{
                    //    Utility.MoveFolderToOld(oldPath, newPath);
                    //}
                    //else
                    //{
                    //    //Edit for config_ file
                    //    if (!string.IsNullOrEmpty(tempUrlPath))
                    //    {
                    //        if (!Directory.Exists(_SystemBinUrl + tempUrlPath.Substring(1)))
                    //        {
                    //            Directory.CreateDirectory(_SystemBinUrl + tempUrlPath.Substring(1));


                    //            Utility.MoveFolderToOld(oldPath, newPath);
                    //        }
                    //        else
                    //        {
                    //            Utility.MoveFolderToOld(oldPath, newPath);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Utility.MoveFolderToOld(oldPath, newPath);
                    //    }

                    //}
                }
                catch (Exception)
                {
                    //log the error message,you can use the application's log code
                }

            }
            #endregion

            this.allFileList.Clear();

            //下载完成
            if (this.downloadFileList.Count == 0)
            { 
                Exit(true); 
            }
            else//下载出问题
            { 
                Exit(false); 
            }
        }

        /// <summary>
        /// 显示正在下载的信息
        /// </summary>
        /// <param name="text"></param>
        private void ShowCurrentlabelText(string text)
        {
            if (labelCurrentItem.InvokeRequired)
            {
                labelCurrentItem.Invoke((ThreadStart)delegate
                {
                    labelCurrentItem.Text = text;
                });
            }
        }

        delegate void ExitCallBack(bool success);
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="success"></param>
        private void Exit(bool success)
        {
            if (this.InvokeRequired)
            {
                ExitCallBack cb = new ExitCallBack(Exit);
                this.Invoke(cb, new object[] { success });
            }
            else
            {
                this.isFinished = success;
                this.DialogResult = success ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }

        private void ShowErrorAndRestartApplication()
        {
            //MessageBox.Show("更新失败!", "升级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Utility.RestartApplication();
            Exit(false);
        }
        #endregion
    }
}