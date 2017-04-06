using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // webBrowser1.Navigate(textBox1.Text);
           // c:\\aaaaaa.jpg
            WebsiteToImage websiteToImage = new WebsiteToImage("http://www.jb51.net", @"c:\\aaaaaa.jpg");
            websiteToImage.Generate();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Size mySize = new Size(500, 500);// webBrowser1.Document.Window.Size;
            Bitmap myPic = new Bitmap(mySize.Width, mySize.Height);
            Rectangle myRec = new Rectangle(0, 0, mySize.Width, mySize.Height);
            webBrowser1.Size = mySize;
            webBrowser1.DrawToBitmap(myPic, myRec);
            myPic.Save("c:\\aaaaaa.jpg");
            MessageBox.Show("Ok");
        }
    }
}
