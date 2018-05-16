using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkNet.Component;
using WCFWeb.Infrastructure.Co;
using ThinkNet.Component;
using ThinkNet.Utility;
using ThinkNet.Autofac;

namespace TaobaoTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmTaoBao());
            InitializeThinkCRMCo();
        }

        public static void InitializeThinkCRMCo()
        {

            ConfigSettings.Initialize();

            //系统组件(Autofac,log4net,Newtonsoft.Json)
            Configuration
                .Create()
                .UseAutofac()
                //   .RegisterCommonComponents()
                .UseLog4Net();
            //.UseJsonNet();

            //IOC依赖注入
            //var assemblies = new[]
            //{
            //   // Assembly.Load("ThinkCRM.Win.Co.Server"),
            //    //Assembly.Load("ThinkNet.Component"),
            //    //Assembly.Load("ThinkNet.Autofac"),
            //    //Assembly.Load("ThinkNet"),
            //    //Assembly.Load("ThinkCRM.Commands.Co"),
            //    //Assembly.Load("ThinkCRM.CommandExecutors.Co"),
            //    //Assembly.Load("ThinkCRM.Query.Co"),
            //    //Assembly.Load("ThinkCRM.Infrastructure.Persistence.Co"),
            //    //Assembly.Load("ThinkCRM.Domain.Co"),
            //    //Assembly.Load("ThinkCRM.Infrastructure.Repository.Co")
            //};
            //var container = (ObjectContainer.Current as AutofacObjectContainer).Container;

        }
    }
}
