#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          Form2.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2017-02-15 17:35:50
 *
 * 公  司（Copyright）：          www.Leapline.com.cn
 * ----------------------------------------------------------------
 * 描  述（Description）：
 * 
 * ----------------------------------------------------------------
 * 修改记录（Revision History）
 *      R1 -
 *         修改人（Editor）：
 *         修改日期（Date）：
 *         修改原因（Reason）：
 *----------------------------------------------------------------*/
#endregion Copyright

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebKit;

namespace TestImg
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Bitmap m_Bitmap;
        private string m_FileName = "11";
        private void Form2_Load(object sender, EventArgs e)
        {
            //WebKit.WebKitBrowser browser = new WebKitBrowser();
            //browser.Dock = DockStyle.Fill;
            //this.Controls.Add(browser);
            //browser.DocumentCompleted += WebBrowser_DocumentCompleted;
            //browser.Navigate("http://www.baidu.com");
            
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
          //  // Capture 
          //  var browser = (WebKitBrowser)sender;

          // var s =  browser.Document;
          // browser.ClientSize = new Size( browser.PageSettings.PaperSize.Width, browser.PageSettings.PaperSize.Height)  ;
          ////  PageSettings = {[PageSettings: Color=True, Landscape=False, Margins=[Margins Left=100 Right=100 Top=100 Bottom=100], PaperSize=[PaperSize A4 Kind=A4 Height=1169 Width=827], PaperSource=[PaperSource unknown Kind=FormSource], PrinterResolution=[PrinterResolution X=600 Y=600]]}
          //  //   browser.ScrollBarsEnabled = false;
          //  string strText = browser.DocumentText;
          //  m_Bitmap = new Bitmap(browser.PageSettings.PaperSize.Width, browser.PageSettings.PaperSize.Width);
          //  browser.BringToFront();
          //  browser.DrawToBitmap(m_Bitmap, browser.Bounds);
          //  // Save as file? 
          //  if (m_FileName.Length > 0)
          //  {
          //      // Save 
          //      m_Bitmap.SaveJPG100("C://aaa.jpg");
          //  }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }
    }
}
