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

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// ���ؽ�����
    /// </summary>
    public partial class FrmHttpProgress : Form
    {
        #region ��    ��
        private bool isFinished = false;
        private List<DownloadFileInfo> downloadFileList = null;
        private List<DownloadFileInfo> allFileList = null;
        private ManualResetEvent evtDownload = null;
        private ManualResetEvent evtPerDonwload = null;
        private WebClient clientDownload = null;

        private string _SystemBinUrl = AppDomain.CurrentDomain.BaseDirectory;
        private string _TempFolder = "TempFolder";
        private long total = 0;//�ļ��ܴ�С
        private long nDownloadedTotal = 0;//�����ܴ�С
        #endregion

        #region ��    ��

        #endregion

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">��Ҫ���ص��ļ��б�</param>
        public FrmHttpProgress(List<DownloadFileInfo> downloadfileList)
            : this(downloadfileList, AppDomain.CurrentDomain.BaseDirectory, "TempFolder")
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">��Ҫ���ص��ļ��б�</param>
        /// <param name="systemBinUrl"></param>
        /// <param name="tempFolder"></param>
        /// <param name="updateConfigName"></param>
        public FrmHttpProgress(List<DownloadFileInfo> downloadfileList, string systemBinUrl, string tempFolder)
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

        #region ҳ���¼�
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isFinished && DialogResult.No == MessageBox.Show("��ǰ���ڸ��£��Ƿ�ȡ����", "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                buttonOk.Enabled = true;
                e.Cancel = true;
                return;
            }
            else
            {
                if (clientDownload != null)
                { 
                    clientDownload.CancelAsync(); 
                }

                evtDownload.Set();
                evtPerDonwload.Set();
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            evtDownload = new ManualResetEvent(true);
            evtDownload.Reset();
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcDownload));
        }

        private void OnCancel(object sender, EventArgs e)
        {
            //bCancel = true;
            //evtDownload.Set();
            //evtPerDonwload.Set();
            ShowErrorAndRestartApplication();
        }
        #endregion

        #region ��������
        private void ProcDownload(object o)
        {
            //��ʱ�ļ���
            string tempFolderPath = Path.Combine(_SystemBinUrl, _TempFolder);
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            evtPerDonwload = new ManualResetEvent(false);
            //�ļ��ܴ�С
            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                total += file.Size;
            }

            #region ����
            try
            {
                while (!evtDownload.WaitOne(0, false))
                {
                    if (this.downloadFileList.Count == 0)
                    { break; }

                    //�����ļ�
                    DownloadFileInfo file = this.downloadFileList[0];
                    //��ʾ�����ļ�����
                    this.ShowCurrentDownloadFileName(file.FileName);

                    //����
                    clientDownload = new WebClient();

                    ////Added the function to support proxy
                    //clientDownload.Proxy = System.Net.WebProxy.GetDefaultProxy();
                    //clientDownload.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    //clientDownload.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    ////End added
                    //���ؽ���
                    clientDownload.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                    {
                        try
                        {
                            this.SetProcessBar(e.ProgressPercentage, (int)((nDownloadedTotal + e.BytesReceived) * 100 / total));
                        }
                        catch
                        {
                            //log the error message,you can use the application's log code
                        }

                    };
                    //�������
                    clientDownload.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                    {
                        try
                        {
                            //DealWithDownloadErrors();
                            DownloadFileInfo dfile = e.UserState as DownloadFileInfo;
                            nDownloadedTotal += dfile.Size;
                            this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));
                            evtPerDonwload.Set();
                        }
                        catch (Exception)
                        {
                            //log the error message,you can use the application's log code
                        }

                    };

                    evtPerDonwload.Reset();

                    ////Download the folder file
                    //string tempFolderPath1 = Utility.GetFolderUrl(file, _SystemBinUrl, _TempFolder);
                    //if (!string.IsNullOrEmpty(tempFolderPath1))
                    //{
                    //    tempFolderPath = Path.Combine(_SystemBinUrl, _TempFolder);
                    //    tempFolderPath += tempFolderPath1;
                    //}
                    //else
                    //{
                    //    tempFolderPath = Path.Combine(_SystemBinUrl, _TempFolder);
                    //}

                    string filePath = Path.Combine(tempFolderPath, file.FileFullName);
                    DirectoryHelper.CreateDirectoryByFilePath(filePath);
                    clientDownload.DownloadFileAsync(new Uri(file.DownloadUrl), filePath, file);

                    //Wait for the download complete
                    evtPerDonwload.WaitOne();

                    clientDownload.Dispose();
                    clientDownload = null;

                    //Remove the downloaded files
                    this.downloadFileList.Remove(file);
                }
            }
            catch (Exception)
            {
                ShowErrorAndRestartApplication();
                //throw;
            }
            #endregion

            //When the files have not downloaded,return.
            if (downloadFileList.Count > 0)
            {
                return;
            }

            //Test network and deal with errors if there have 
            //DealWithDownloadErrors();

            #region ��������
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

            //After dealed with all files, clear the data
            this.allFileList.Clear();

            if (this.downloadFileList.Count == 0)
                Exit(true);
            else
                Exit(false);

            evtDownload.Set();
        }

        delegate void SetProcessBarCallBack(int current, int total);
        /// <summary>
        /// ���ý���
        /// </summary>
        /// <param name="current"></param>
        /// <param name="total"></param>
        private void SetProcessBar(int current, int total)
        {
            try
            {
                if (this.progressBarCurrent.InvokeRequired)
                {
                    SetProcessBarCallBack cb = new SetProcessBarCallBack(SetProcessBar);
                    this.Invoke(cb, new object[] { current, total });
                }
                else
                {
                    this.progressBarCurrent.Value = current;
                    this.progressBarTotal.Value = total;
                }
            }
            catch(Exception)
            { }
        }

        delegate void ShowCurrentDownloadFileNameCallBack(string name);
        /// <summary>
        /// ��ʾ��ǰ���ص��ļ�����
        /// </summary>
        /// <param name="name"></param>
        private void ShowCurrentDownloadFileName(string name)
        {
            if (this.labelCurrentItem.InvokeRequired)
            {
                ShowCurrentDownloadFileNameCallBack cb = new ShowCurrentDownloadFileNameCallBack(ShowCurrentDownloadFileName);
                this.Invoke(cb, new object[] { name });
            }
            else
            {
                this.labelCurrentItem.Text = name;
            }
        }

        delegate void ExitCallBack(bool success);
        /// <summary>
        /// �˳�
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

        private void DealWithDownloadErrors()
        {
            try
            {
                //Test Network is OK or not.
                UpdateConfig config = UpdateConfig.LoadUpdateConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "update.config"));
                WebClient client = new WebClient();
                client.DownloadString(config.ServerUrl);
            }
            catch (Exception)
            {
                //log the error message,you can use the application's log code
                ShowErrorAndRestartApplication();
            }
        }

        private void ShowErrorAndRestartApplication()
        {
            //MessageBox.Show("����ʧ��!", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Utility.RestartApplication();
            Exit(false);
        }
        #endregion
    }
}