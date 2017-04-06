using Autofac;
using System;
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
            ////listView.DataContext = GetDataTable();
            //listView.DataContext = GetDataTable().DefaultView;
            //listView.SelectedIndex = 0;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            load();
        }
        private DataTable GetDataTable()
        {
            DataTable data = new DataTable("MyDataTable");

            DataColumn ID = new DataColumn("ID");//第一列
            ID.DataType = System.Type.GetType("System.Int32");
            //ID.AutoIncrement = true; //自动递增ID号 
            data.Columns.Add(ID);

            //设置主键
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ID;
            data.PrimaryKey = keys;

            data.Columns.Add(new DataColumn("Name", typeof(string)));//第二列
            data.Columns.Add(new DataColumn("Age", typeof(string)));//第三列

            data.Rows.Add(1, "  XiaoM", "  20");
            data.Rows.Add(2, "  XiaoF", "  122");
            data.Rows.Add(3, "  XiaoA", "  29");
            data.Rows.Add(4, "  XiaoB", "  102");
            return data;
        }

        private void load()
        {
            try
            {
                InitializeThinkCRMCo();
                ApiHost apiHost = new ApiHost();
                apiHost.StartServices();
                listView_Service.DataContext = apiHost.DtSericer;
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

        public string getNmae(string success)
        {
            return success;
        
        }



        private void ModifyUI()
        {

            // 模拟一些工作正在进行

            Thread.Sleep(TimeSpan.FromSeconds(2));

            //lblHello.Content = "欢迎你光临WPF的世界,Dispatcher";

            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {

                lblHello.Content = "欢迎你光临WPF的世界,Dispatche  同步方法 ！！";

            });

        }



        private void btnThd_Click(object sender, RoutedEventArgs e)
        {

            Thread thread = new Thread(ModifyUI);

            thread.Start();

        }
        private void btnAppBeginInvoke_Click(object sender, RoutedEventArgs e)
        {

            new Thread(() =>
            {

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,

                    new Action(() =>
                    {

                        Thread.Sleep(TimeSpan.FromSeconds(2));

                        this.lblHello.Content = "欢迎你光临WPF的世界,Dispatche 异步方法！！" + DateTime.Now.ToString();

                    }));

            }).Start();

        }

    }

}



