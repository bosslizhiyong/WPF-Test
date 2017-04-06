namespace ThinkNet.Utility
{
    partial class FrmSplash
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.lblLoadInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLoadInfo
            // 
            this.lblLoadInfo.AutoSize = true;
            this.lblLoadInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadInfo.Location = new System.Drawing.Point(12, 133);
            this.lblLoadInfo.Name = "lblLoadInfo";
            this.lblLoadInfo.Size = new System.Drawing.Size(65, 12);
            this.lblLoadInfo.TabIndex = 0;
            this.lblLoadInfo.Text = "Loading...";
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(298, 167);
            this.Controls.Add(this.lblLoadInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSplash";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoadInfo;
    }
}