using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JinDong
{
    public partial class JinDong : Form
    {
        public JinDong()
        {
            InitializeComponent();



        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            Frmocr frm = new Frmocr();
            frm.Show();


           // string appKey = "d85c5234e5244f798d070eca12c844bd";
          //  string secretKey = "3a07NbW4kmkej8i4IMG3ma==";

            //string strUrl = string.Format(@"http://cognitive-console.jdcloud.com/service-ai/ocr/detect-text?appKey={0}&appName=[]&timestamp=[]&rand=[]&bucketDomain=[]", appKey, secretKey);
        }
    }
}
