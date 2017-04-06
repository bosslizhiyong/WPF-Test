using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ThinkNet.Utility
{
    /// <summary>
    /// �������ڣ��̳�ISplashUI�ӿ�,
    /// </summary>
    public partial class FrmSplash : Form, ISplashUI
    {
        /// <summary>
        /// �������ڣ��̳�ISplashUI�ӿ�
        /// </summary>
        public FrmSplash()
        {
            InitializeComponent();
        }

        #region ISplashUI ��Ա

        void ISplashUI.SetInformation(string info)
        {
            lblLoadInfo.Text = info;
        }

        #endregion

        private void frmSplash_Load(object sender, EventArgs e)
        {

        }
    }
}