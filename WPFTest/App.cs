
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPFTest
{
   public  class App
    {
       [STAThread]
        static void Main()
        {
            
            // 定义Application对象作为整个应用程序入口  
            Application app = new Application();
            // 通过Url的方式启动
            app.StartupUri = new Uri("/View/MainWindow.xaml", UriKind.Relative);
            app.Run();
        }

    }
}
