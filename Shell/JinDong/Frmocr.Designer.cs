namespace JinDong
{
    partial class Frmocr
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
            this.btnImage = new System.Windows.Forms.Button();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtConter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.labacc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAppName = new System.Windows.Forms.TextBox();
            this.labAddress = new System.Windows.Forms.Label();
            this.btnCreateAuthorization = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(25, 122);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(75, 23);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "OCR识别";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(142, 12);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(219, 21);
            this.txtDomain.TabIndex = 1;
            this.txtDomain.Text = "orc.oss.cn-south-1.jcloudcs.com";
            // 
            // txtConter
            // 
            this.txtConter.Location = new System.Drawing.Point(25, 163);
            this.txtConter.Multiline = true;
            this.txtConter.Name = "txtConter";
            this.txtConter.Size = new System.Drawing.Size(563, 278);
            this.txtConter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "空间外网访问域名：";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(420, 82);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 3;
            this.btnFile.Text = "文件";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // labacc
            // 
            this.labacc.AutoSize = true;
            this.labacc.Location = new System.Drawing.Point(23, 87);
            this.labacc.Name = "labacc";
            this.labacc.Size = new System.Drawing.Size(59, 12);
            this.labacc.TabIndex = 4;
            this.labacc.Text = "文件地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "AppName:";
            // 
            // txtAppName
            // 
            this.txtAppName.Location = new System.Drawing.Point(81, 45);
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.Size = new System.Drawing.Size(219, 21);
            this.txtAppName.TabIndex = 1;
            this.txtAppName.Text = "TestView";
            // 
            // labAddress
            // 
            this.labAddress.AutoSize = true;
            this.labAddress.Location = new System.Drawing.Point(88, 87);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(29, 12);
            this.labAddress.TabIndex = 4;
            this.labAddress.Text = "未知";
            // 
            // btnCreateAuthorization
            // 
            this.btnCreateAuthorization.Location = new System.Drawing.Point(420, 10);
            this.btnCreateAuthorization.Name = "btnCreateAuthorization";
            this.btnCreateAuthorization.Size = new System.Drawing.Size(75, 23);
            this.btnCreateAuthorization.TabIndex = 5;
            this.btnCreateAuthorization.Text = "生成签名";
            this.btnCreateAuthorization.UseVisualStyleBackColor = true;
            this.btnCreateAuthorization.Click += new System.EventHandler(this.btnCreateAuthorization_Click);
            // 
            // Frmocr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 476);
            this.Controls.Add(this.btnCreateAuthorization);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labacc);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConter);
            this.Controls.Add(this.txtAppName);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.btnImage);
            this.Name = "Frmocr";
            this.Text = "京东";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtConter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label labacc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAppName;
        private System.Windows.Forms.Label labAddress;
        private System.Windows.Forms.Button btnCreateAuthorization;
    }
}

