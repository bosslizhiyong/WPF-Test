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
    public partial class FrmWcfProgress : Form
    {
        #region ��    ��
        private bool isFinished = false;
        private List<DownloadFileInfo> downloadFileList = null;
        private List<DownloadFileInfo> allFileList = null;
        private Thread downLoadThread = null;

        private string _SystemBinUrl = AppDomain.CurrentDomain.BaseDirectory;
        private string _TempFolder = "TempFolder";
        #endregion

        #region ��    ��
        /// <summary>
        /// 
        /// </summary>
        public IUpdateFileService Proxy
        { private get; set; }
        #endregion

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">��Ҫ���ص��ļ��б�</param>
        public FrmWcfProgress(List<DownloadFileInfo> downloadfileList)
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
        public FrmWcfProgress(List<DownloadFileInfo> downloadfileList, string systemBinUrl, string tempFolder)
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
            if (!isFinished && DialogResult.No == MessageBox.Show("���ڸ��£��Ƿ�ȡ����", "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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

        #region ��������
        private void DownLoadFile()
        {
            //��ʱ�ļ���
            string tempFolderPath = Path.Combine(_SystemBinUrl, _TempFolder);
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            //�ܽ�����
            if (progressBarTotal.InvokeRequired)
            {
                progressBarTotal.Invoke((ThreadStart)delegate
                {
                    progressBarTotal.Maximum = downloadFileList.Count;
                });
            }

            #region �����ļ�
            UpdateFileInfo mFileInfo = null;
            string fileSavePath = "";
            int i = 1;
            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                //�������ص��ļ�����
                ShowCurrentlabelText(file.FileName);

                //�ļ���Ϣ
                string filePath = Path.Combine(tempFolderPath, file.FileFullName);
                DirectoryHelper.CreateDirectoryByFilePath(filePath);
                mFileInfo = new UpdateFileInfo();
                mFileInfo.FileName = filePath;//�����ļ���������·��
                mFileInfo.FileSaveName = file.FileFullName;//Ҫ���ص��ļ�����
                mFileInfo.FileCategory = "";

                //��ʼ�����ļ�
                mFileInfo = Proxy.DownLoadFile(mFileInfo);
                if (!string.IsNullOrEmpty(mFileInfo.ErrorMessage))
                {
                    ShowCurrentlabelText(mFileInfo.ErrorMessage);
                    break;
                }
                if (mFileInfo != null && mFileInfo.FileData != null && mFileInfo.FileData.Count > 0)
                {
                    fileSavePath = mFileInfo.FileName;
                    while (mFileInfo.Length != mFileInfo.Offset)  //ѭ���Ķ�ȡ�ļ�,�ϴ���ֱ���ļ��ĳ��ȵ����ļ���ƫ����
                    {
                        if (mFileInfo.Offset != 0)
                        {
                            mFileInfo = Proxy.DownLoadFile(mFileInfo);
                        }
                        if (mFileInfo.FileData == null || mFileInfo.FileData.Count < 1)
                        {
                            break;
                        }
                        using (System.IO.FileStream fs = new System.IO.FileStream(fileSavePath, System.IO.FileMode.OpenOrCreate))
                        {
                            long offset = mFileInfo.Offset;  //file.Offset �ļ�ƫ��λ��,��ʾ�����λ�ÿ�ʼ���к�����������
                            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))//��ʼ���ļ�д����
                            {
                                writer.Seek((int)offset, System.IO.SeekOrigin.Begin);//�����ļ���д��λ��
                                writer.Write(mFileInfo.FileData.ToArray());//д������

                                mFileInfo.Offset = fs.Length;//����׷�����ݺ���ļ�λ��
                                mFileInfo.FileData = null;

                                if (writer != null)
                                    writer.Close();
                            }
                            if (fs != null)
                                fs.Close();
                        }//end using (System.IO.FileStream fs = new System.IO.FileStream(fileSavePath, System.IO.FileMode.OpenOrCreate))
                        //������
                        int percentValue = (int)(((decimal)mFileInfo.Offset / (decimal)mFileInfo.Length) * (decimal)100);
                        if (progressBarCurrent.InvokeRequired)
                        {
                            progressBarCurrent.Invoke((ThreadStart)delegate
                            {
                                progressBarCurrent.Value = percentValue;
                                //System.Threading.Thread.Sleep(300);
                            });
                        }
                    }//end while (mFileInfo.Length != mFileInfo.Offset)
                }//end if (mFileInfo != null && mFileInfo.FileData != null && mFileInfo.FileData.Count > 0)
                //�ܽ���
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

            this.allFileList.Clear();

            //�������
            if (this.downloadFileList.Count == 0)
            { 
                Exit(true); 
            }
            else//���س�����
            { 
                Exit(false); 
            }
        }

        /// <summary>
        /// ��ʾ�������ص���Ϣ
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

        private void ShowErrorAndRestartApplication()
        {
            //MessageBox.Show("����ʧ��!", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Utility.RestartApplication();
            Exit(false);
        }
        #endregion
    }
}