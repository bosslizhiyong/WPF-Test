﻿namespace TaobaoTest
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
            this.btnGetUrlCode = new System.Windows.Forms.Button();
            this.btnGetToken = new System.Windows.Forms.Button();
            this.labTest = new System.Windows.Forms.Label();
            this.btnGetCode = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtjson = new System.Windows.Forms.TextBox();
            this.btnMoney = new System.Windows.Forms.Button();
            this.btnAuthorizationTest = new System.Windows.Forms.Button();
            this.btnSellInfo = new System.Windows.Forms.Button();
            this.btnOAuth = new System.Windows.Forms.Button();
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
            // btnGetUrlCode
            // 
            this.btnGetUrlCode.Location = new System.Drawing.Point(172, 21);
            this.btnGetUrlCode.Name = "btnGetUrlCode";
            this.btnGetUrlCode.Size = new System.Drawing.Size(117, 23);
            this.btnGetUrlCode.TabIndex = 0;
            this.btnGetUrlCode.Text = "引导页面授权";
            this.btnGetUrlCode.UseVisualStyleBackColor = true;
            this.btnGetUrlCode.Click += new System.EventHandler(this.btnGetUserCode_Click);
            // 
            // btnGetToken
            // 
            this.btnGetToken.Location = new System.Drawing.Point(476, 21);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(117, 23);
            this.btnGetToken.TabIndex = 0;
            this.btnGetToken.Text = "获取用户令牌";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.btnGetToken_Click);
            // 
            // labTest
            // 
            this.labTest.AutoSize = true;
            this.labTest.Location = new System.Drawing.Point(170, 81);
            this.labTest.Name = "labTest";
            this.labTest.Size = new System.Drawing.Size(53, 12);
            this.labTest.TabIndex = 1;
            this.labTest.Text = "测试数据";
            // 
            // btnGetCode
            // 
            this.btnGetCode.Location = new System.Drawing.Point(330, 21);
            this.btnGetCode.Name = "btnGetCode";
            this.btnGetCode.Size = new System.Drawing.Size(117, 23);
            this.btnGetCode.TabIndex = 0;
            this.btnGetCode.Text = "获取用户Code";
            this.btnGetCode.UseVisualStyleBackColor = true;
            this.btnGetCode.Click += new System.EventHandler(this.btnGetCode_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(25, 76);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(91, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试淘宝连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtjson
            // 
            this.txtjson.Location = new System.Drawing.Point(3, 293);
            this.txtjson.Multiline = true;
            this.txtjson.Name = "txtjson";
            this.txtjson.Size = new System.Drawing.Size(868, 199);
            this.txtjson.TabIndex = 3;
            // 
            // btnMoney
            // 
            this.btnMoney.Location = new System.Drawing.Point(654, 21);
            this.btnMoney.Name = "btnMoney";
            this.btnMoney.Size = new System.Drawing.Size(75, 23);
            this.btnMoney.TabIndex = 4;
            this.btnMoney.Text = "检查商家CRM预存余额是否足够进行活动";
            this.btnMoney.UseVisualStyleBackColor = true;
            this.btnMoney.Click += new System.EventHandler(this.btnMoney_Click);
            // 
            // btnAuthorizationTest
            // 
            this.btnAuthorizationTest.Location = new System.Drawing.Point(770, 21);
            this.btnAuthorizationTest.Name = "btnAuthorizationTest";
            this.btnAuthorizationTest.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorizationTest.TabIndex = 5;
            this.btnAuthorizationTest.Text = "授权测试";
            this.btnAuthorizationTest.UseVisualStyleBackColor = true;
            this.btnAuthorizationTest.Click += new System.EventHandler(this.btnAuthorizationTest_Click);
            // 
            // btnSellInfo
            // 
            this.btnSellInfo.Location = new System.Drawing.Point(699, 76);
            this.btnSellInfo.Name = "btnSellInfo";
            this.btnSellInfo.Size = new System.Drawing.Size(146, 23);
            this.btnSellInfo.TabIndex = 5;
            this.btnSellInfo.Text = "查询卖家用户信息";
            this.btnSellInfo.UseVisualStyleBackColor = true;
            this.btnSellInfo.Click += new System.EventHandler(this.btnSellInfo_Click);
            // 
            // btnOAuth
            // 
            this.btnOAuth.Location = new System.Drawing.Point(3, 262);
            this.btnOAuth.Name = "btnOAuth";
            this.btnOAuth.Size = new System.Drawing.Size(75, 23);
            this.btnOAuth.TabIndex = 6;
            this.btnOAuth.Text = "授权";
            this.btnOAuth.UseVisualStyleBackColor = true;
            this.btnOAuth.Click += new System.EventHandler(this.btnOAuth_Click);
            // 
            // FrmTaoBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 504);
            this.Controls.Add(this.btnOAuth);
            this.Controls.Add(this.btnSellInfo);
            this.Controls.Add(this.btnAuthorizationTest);
            this.Controls.Add(this.btnMoney);
            this.Controls.Add(this.txtjson);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.labTest);
            this.Controls.Add(this.btnGetUrlCode);
            this.Controls.Add(this.btnGetCode);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.btnAppStart);
            this.Name = "FrmTaoBao";
            this.Text = "淘宝测试窗口";
            this.Load += new System.EventHandler(this.FrmTaoBao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAppStart;
        private System.Windows.Forms.Button btnGetUrlCode;
        private System.Windows.Forms.Button btnGetToken;
        private System.Windows.Forms.Label labTest;
        private System.Windows.Forms.Button btnGetCode;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtjson;
        private System.Windows.Forms.Button btnMoney;
        private System.Windows.Forms.Button btnAuthorizationTest;
        private System.Windows.Forms.Button btnSellInfo;
        private System.Windows.Forms.Button btnOAuth;
    }
}
