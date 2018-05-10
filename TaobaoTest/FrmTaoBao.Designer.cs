namespace TaobaoTest
{
    partial class FrmTaoBao
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
            this.btnAppStart = new System.Windows.Forms.Button();
            this.btnGetUserCode = new System.Windows.Forms.Button();
            this.btnGetToken = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAppStart
            // 
            this.btnAppStart.Location = new System.Drawing.Point(25, 21);
            this.btnAppStart.Name = "btnAppStart";
            this.btnAppStart.Size = new System.Drawing.Size(117, 23);
            this.btnAppStart.TabIndex = 0;
            this.btnAppStart.Text = "启动AppWcf";
            this.btnAppStart.UseVisualStyleBackColor = true;
            this.btnAppStart.Click += new System.EventHandler(this.btnAppStart_Click);
            // 
            // btnGetUserCode
            // 
            this.btnGetUserCode.Location = new System.Drawing.Point(172, 21);
            this.btnGetUserCode.Name = "btnGetUserCode";
            this.btnGetUserCode.Size = new System.Drawing.Size(117, 23);
            this.btnGetUserCode.TabIndex = 0;
            this.btnGetUserCode.Text = "获取用户Code";
            this.btnGetUserCode.UseVisualStyleBackColor = true;
            this.btnGetUserCode.Click += new System.EventHandler(this.btnGetUserCode_Click);
            // 
            // btnGetToken
            // 
            this.btnGetToken.Location = new System.Drawing.Point(318, 21);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(117, 23);
            this.btnGetToken.TabIndex = 0;
            this.btnGetToken.Text = "获取用户令牌";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.btnGetToken_Click);
            // 
            // FrmTaoBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 373);
            this.Controls.Add(this.btnGetUserCode);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.btnAppStart);
            this.Name = "FrmTaoBao";
            this.Text = "淘宝测试窗口";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAppStart;
        private System.Windows.Forms.Button btnGetUserCode;
        private System.Windows.Forms.Button btnGetToken;
    }
}

