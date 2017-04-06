using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 下载确认窗口
    /// </summary>
    public partial class FrmDownloadConfirm : Form
    {
        #region 字    段
        /// <summary>
        /// 需要下载的文件列表
        /// </summary>
        List<DownloadFileInfo> _downloadFileList = null;
        #endregion

        #region 属    性

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">需要下载的文件列表</param>
        public FrmDownloadConfirm(List<DownloadFileInfo> downloadfileList)
        {
            InitializeComponent();

            _downloadFileList = downloadfileList;
        }
        #endregion

        #region 页面事件
        private void OnLoad(object sender, EventArgs e)
        {
            //panel3.Height = 66;

            ListViewItem item = null;
            foreach (DownloadFileInfo file in this._downloadFileList)
            {
                item = new ListViewItem(new string[] { file.FileName, file.LastVer, file.Size.ToString() });
                lstDownloadFile.Items.Add(item);
            }

            this.Activate();
            this.Focus();
        }
        #endregion

        #region 基本方法
        
        #endregion
    }
}