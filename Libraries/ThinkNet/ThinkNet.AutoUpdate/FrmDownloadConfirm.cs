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
    /// ����ȷ�ϴ���
    /// </summary>
    public partial class FrmDownloadConfirm : Form
    {
        #region ��    ��
        /// <summary>
        /// ��Ҫ���ص��ļ��б�
        /// </summary>
        List<DownloadFileInfo> _downloadFileList = null;
        #endregion

        #region ��    ��

        #endregion

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadfileList">��Ҫ���ص��ļ��б�</param>
        public FrmDownloadConfirm(List<DownloadFileInfo> downloadfileList)
        {
            InitializeComponent();

            _downloadFileList = downloadfileList;
        }
        #endregion

        #region ҳ���¼�
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

        #region ��������
        
        #endregion
    }
}