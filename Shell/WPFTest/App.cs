using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WCFWeb.Infrastructure.Co;

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
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            app.Exit += app_Exit;
            app.Run();
          
        }

       static void app_Exit(object sender, ExitEventArgs e)
       {
           MessageBox.Show(""+e.ApplicationExitCode);
       }
    }
}
