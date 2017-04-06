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
    /// 闪动窗口，继承ISplashUI接口,
    /// </summary>
    public partial class FrmSplash : Form, ISplashUI
    {
        /// <summary>
        /// 闪动窗口，继承ISplashUI接口
        /// </summary>
        public FrmSplash()
        {
            InitializeComponent();
        }

        #region ISplashUI 成员

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