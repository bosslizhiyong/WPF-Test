using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ThinkNet.Autofac;
using ThinkNet.Component;
using WCFWeb.Co.ApiHost;
using WCFWeb.Infrastructure.Co;

namespace WPFTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //listView.DataContext = GetDataTable();
           // listView.DataContext = GetDataTable().DefaultView;
            //listView.SelectedIndex = 0;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            load();
        }
     
        private void load()
        {
            try
            {
                InitializeThinkCRMCo();
                ApiHost apiHost = new ApiHost();
                apiHost.StartServices();

                DataTable table = apiHost.DtSericer;

                DataTable dt = table;
                dt.Columns.Add("Operation", typeof(string));
                foreach (DataRow dr in dt.Rows)
                    dr["Operation"] = dr["ServiceStatus"].ToString()=="运行"? "停止":"启动";

                listView_Service.DataContext = apiHost.DtSericer.DefaultView;

            }
            catch (Exception ex)
            {

            }
        }

        private void InitializeThinkCRMCo()
        {
            //初始系统变量
            ConfigSettings.Initialize();
            //系统组件(Autofac,log4net,Newtonsoft.Json)
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet();

            //IOC依赖注入
            var assemblies = new[]
            {
               // Assembly.Load("WCFWeb.Co.ApiHost"),
                Assembly.Load("ThinkNet.Component"),
                Assembly.Load("ThinkNet.Autofac"),
                Assembly.Load("ThinkNet"),
                //Assembly.Load("ThinkCRM.Commands.Co"),
                //Assembly.Load("ThinkCRM.CommandExecutors.Co"),
                Assembly.Load("WCFWeb.Query.Co"),
              //  Assembly.Load("WCFWeb.Infrastructure.Persistence.Co"),
                //Assembly.Load("ThinkCRM.Domain.Co"),
                //Assembly.Load("ThinkCRM.Infrastructure.Repository.Co")
            };

            var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
            var builder = new ContainerBuilder();
            //命令注入
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("CommandBus"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Command"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Executor"))
                .AsImplementedInterfaces();
            //查询注入
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("QueryService"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Query"))
                .AsImplementedInterfaces();
            //DAO注入
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("DAOCenter"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("DAO"))
                .AsImplementedInterfaces();
            //仓储注入
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            //仓储上下文注入
            builder.RegisterType<ThinkNet.Domain.Core.SQLRepositoryContext>().As<ThinkNet.Domain.Core.IRepositoryContext>().SingleInstance();
            //服务注入
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("DomainService"))
                .AsImplementedInterfaces();
            //服务上下文注入
            builder.RegisterType<ThinkNet.Domain.Core.SQLDomainServiceContext>().As<ThinkNet.Domain.Core.IDomainServiceContext>().SingleInstance();

            builder.Update(container);//更新依赖注入

            //_queryService = ObjectContainer.Resolve<IDynamicQuery>();//多数据库下,查询的是默认数据库
            ////设置系统变量
            //ConfigurationSettings.SetThinkCRMCo(null, _queryService);

            ////系统组件(log4net,Newtonsoft.Json)
            //Configuration.Instance
            //    .UseLog4Net()
            //    .UseJsonNet();
        }

        //点击事件
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }



    }

}



