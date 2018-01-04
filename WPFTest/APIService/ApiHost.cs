using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using WCFWeb.Co;
using ThinkNet.Component;
using ThinkNet.Utility;
using WCFWeb.Co.ApiHost;

namespace WCFWeb.Co.ApiHost
{
    public class ApiHost : ApiBase
    {
        #region 字    段

        /// <summary>
        /// 程序集路径
        /// </summary>
        private string _path = "";

        /// <summary>
        /// WCF服务宿主
        /// </summary>
        private List<ServiceHost> _hosts = null;

        /// <summary>
        /// WCF服务集合
        /// </summary>
        private DataTable _dtService = null;


        public DataTable DtSericer { get { return _dtService; } }

        //sockets
        //public SocketsClient client;    // 客户端实例

        #endregion 字    段

        public ApiHost()
        {
            try
            {
                //程序集路径
                _path = Assembly.GetEntryAssembly().Location;
                //ConnectService();
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }

        #region 启动和关闭WCF服务

        /// <summary>
        /// 启动全部服务
        /// </summary>
        public bool StartServices()
        {
            try
            {
                _hosts = new List<ServiceHost>();
                _dtService = new DataTable();
                _dtService.Columns.Add("ServiceName");
                _dtService.Columns.Add("ServiceStatus");
                _dtService.Columns.Add("ServiceMemo");
                _dtService.Columns.Add("ServiceContent");
                string serviceDesc = "";
                int numID = 0;
                //配置文件
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(_path);
                //配置的所有服务
                ServiceModelSectionGroup serviceGroup = (ServiceModelSectionGroup)config.GetSectionGroup("system.serviceModel");
                foreach (ServiceElement el in serviceGroup.Services.Services)
                {
                    try
                    {
                        Type serviceType = Assembly.Load("WCFWeb.Co").GetType(el.Name);
                        if (serviceType == null)
                        {
                            throw new Exception("Invalid Service Type " + el.Name + " in configuration file.");
                        }
                        StartService(serviceType, numID++);//启动服务
                        serviceDesc = GetServiceDescription(serviceType.GetCustomAttributes(typeof(DescriptionAttribute), false), serviceType.Name);
                        AddServiceToDataTable(serviceDesc, "运行", el.Name,"");
                    }
                    catch (Exception ex)
                    {
                        AddServiceToDataTable(el.Name, "失败", el.Name,ex.Message);
                      
                    }
             
                }
                Console.WriteLine("ApiHost服务启动成功！");
                Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ApiHost服务启动失败！");
                Console.WriteLine(ex.Message);
                WriteExceptionLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="t">服务的类型</param>
        private void StartService(Type t, int numID)
        {
            ServiceHost host = null;
            host = new ServiceHost(t);
            if (host != null)
            {
                host.Open();
                Console.WriteLine(numID + "-" + t.ToString() + "成功");
                _hosts.Add(host);
            }
        }

        /// <summary>
        /// 获取服务描述
        /// </summary>
        /// <param name="attributes">服务特性数组</param>
        /// <param name="defaultDesc">默认描述</param>
        /// <returns></returns>
        private string GetServiceDescription(object[] attributes, string defaultDesc)
        {
            if (attributes != null && attributes.Length > 0)
            {
                foreach (DescriptionAttribute attr in attributes)
                {
                    return attr.Description;
                }
            }
            return defaultDesc;
        }

        /// <summary>
        /// 添加服务到数据表
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceStatus">服务状态</param>
        /// <param name="serviceMemo">服务描述</param>
        private void AddServiceToDataTable(string serviceName, string serviceStatus, string serviceMemo,string serviceCountent)
        {
            DataRow row = _dtService.NewRow();
            row["ServiceName"] = serviceName;
            row["ServiceStatus"] = serviceStatus;
            row["ServiceMemo"] = serviceMemo;
            row["ServiceContent"] = serviceCountent;
            _dtService.Rows.Add(row);
        }

        /// <summary>
        /// 关闭全部服务
        /// </summary>
        private void CloseServices()
        {
            Console.WriteLine("ApiHost服务关闭！");
            if (_hosts != null)
            {
                foreach (ServiceHost host in _hosts)
                {
                    if (host.Description.ConfigurationName == "ThinkCRM.Api.Co.SettingService") continue;
                    host.Close();
                    _hosts.Remove(host);
                }
            }
        }

        /// <summary>
        /// 程序外部调用
        /// </summary>
        public void CloseAPI()
        {
            //if (Program.ApiHostObj != null)
            //{
            //    Program.ApiHostObj.CloseServices();
            //    return;
            //}
        }

        #endregion 启动和关闭WCF服务


        public Socket socket;
       // public ThinkCRM.Api.Co.SocketsClient.Print print;// 运行时的信息输出方法
        public bool connected = false;          // 标识当前是否连接到服务器
        public string localIpPort = "";         // 记录本地ip端口信息

        ////sockets
        //private void ConnectService()
        //{
        //    try
        //    {
        //        if (client == null)
        //        {
        //            string ipString = System.Configuration.ConfigurationManager.AppSettings["ServerIP"];
        //            string portString = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
        //            client = new SocketsClient(print, ipString, DataTypeConvert.ToInt32(portString, 3780));
        //        }
        //        if (!client.connected) client.start();
        //        if (client != null) Console.WriteLine("客户端" + "" + client.localIpPort);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("客户端" + "" + ex.Message);
        //    }
        //}
        ////关闭
        //public void SeedcColeseSertvice()
        //{
        //    try
        //    {
        //        client.Send("关闭");
        //        Console.WriteLine("关闭服务端");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (print != null) print("服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】");
        //    }

        //}
        ////打开
        //public void SeedOpenSertvice()
        //{
        //    try
        //    {
        //        client.Send("打开");
        //        Console.WriteLine("打开服务端");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (print != null) print("服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】");
        //    }
        //}
    }
}