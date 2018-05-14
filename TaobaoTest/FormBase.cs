#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          FormBase.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2018-05-10 17:43:25
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkNet.Component;
using ThinkNet.Utility;
using ThinkNet.Autofac;
using WCFWeb.Infrastructure.Co;

namespace TaobaoTest
{
    public partial class FormBase : Form
    {

        #region 属    性
        /// <summary>
        /// 日志管理
        /// </summary>
        protected ILogger LogManager
        {
            get
            {
                return ObjectContainer.Resolve<ILoggerFactory>().Create("LoggerError");
            }
        }
        #endregion

        public FormBase()
        {
            InitializeComponent();

            ConfigSettings.Initialize();

            //系统组件(Autofac,log4net,Newtonsoft.Json)
            Configuration
                .Create()
                .UseAutofac()
                //   .RegisterCommonComponents()
                .UseLog4Net();
               // .UseJsonNet();

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


        /// <summary>
        /// 初始化数据
        /// </summary>
        protected virtual void InitData()
        {
           
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        protected virtual void LoadData()
        {

        }


        #region 错误日志
        protected void WriteExceptionLog(string errInfo)
        {
            System.Diagnostics.StackTrace mStackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame mStackFrame = mStackTrace.GetFrame(1);// 0为本身的方法；1为调用方法
            string className = mStackFrame.GetMethod().ReflectedType.Name; // 类名
            string methodName = mStackFrame.GetMethod().Name; // 方法名
            LogManager.Error(className + "-->" + methodName + "：" + errInfo);
            //LogManager.Error(this.Text + "：" + errInfo);
            // CloseWaitDialog();
        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        protected void WriteExceptionLog(Exception ex)
        {

            System.Diagnostics.StackTrace mStackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame mStackFrame = mStackTrace.GetFrame(1);// 0为本身的方法；1为调用方法
            string className = mStackFrame.GetMethod().ReflectedType.Name; // 类名
            string methodName = mStackFrame.GetMethod().Name; // 方法名
            LogManager.Error(className + "-->" + methodName + "：" + ex.Message);
            //LogManager.Error(this.Text + "：" + ex.Message);
            // CloseWaitDialog();

            //解决无法通信问题
           // if (ex.Message.Contains("System.ServiceModel.Channels.ServiceChannel") && ex.Message.Contains("无法用于通信") && ex.Message.Contains("出错"))
           // {
                //System.Threading.Tasks.Task tProxy = new System.Threading.Tasks.Task(new Action(() =>
                //{
                //    try
                //    {
                //        System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly;
                //        System.Reflection.FieldInfo[] fields = null;
                //        System.Reflection.MethodInfo methodInfo = null;

                //        Form form = this;
                //        fields = form.GetType().GetFields(flag);
                //        if (fields != null && fields.Length > 0)
                //        {
                //            foreach (System.Reflection.FieldInfo f in fields)
                //            {
                //                //不是服务的，跳过
                //                if (!f.FieldType.ToString().EndsWith("Service"))
                //                { continue; }
                //                //设为空值
                //                f.SetValue(form, null);
                //                //清空缓存代理
                //                //?  Proxy.Instance().ClearProxyByKey(f.FieldType.ToString());
                //            }//end foreach (System.Reflection.FieldInfo f in fields)
                //        }
                //        methodInfo = form.GetType().GetMethod("CreateProxy");
                //        if (methodInfo != null)
                //        {
                //            methodInfo.Invoke(form, null);
                //        }
                //    }
                //    catch (Exception)
                //    { }
                //}));
                //tProxy.Start();
            //}
        }

        #endregion 
    
    }
}
