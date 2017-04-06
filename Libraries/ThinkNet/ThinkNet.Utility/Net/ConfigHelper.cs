using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ThinkNet.Utility
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 修改system.serviceModel下所有服务终结点的IP地址
        /// </summary>
        /// <param name="configPath">Assembly.GetEntryAssembly().Location</param>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        public static void UpdateServiceModelConfig(string configPath, string serverIP, string serverPort)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            ConfigurationSectionGroup sec = config.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sec as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                string address = "";
                //端口号为空
                if (string.IsNullOrEmpty(serverPort))
                {
                    address = item.Address.Scheme + "://" + serverIP + item.Address.AbsolutePath;
                }
                else
                {
                    address = item.Address.Scheme + "://" + serverIP + ":" + serverPort + item.Address.AbsolutePath;
                }
                item.Address = new Uri(address);
                //string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d+\b";
                //string address = item.Address.ToString();
                //string replacement = string.Format("{0}:{1}", serverIP, serverPort);
                //address = Regex.Replace(address, pattern, replacement);
                //item.Address = new Uri(address);
            }
            foreach (ServiceElement el in serviceModelSectionGroup.Services.Services)
            {
                string address = el.Host.BaseAddresses[0].BaseAddress;
                int idx = address.IndexOf("/");
                string scheme = address.Substring(0, idx+2);
                address = address.Replace("net.tcp://", "")
                    .Replace("net.pipe://", "")
                    .Replace("net.msmq://", "")
                    .Replace("http://", "")
                    .Replace("https://", "")
                    .Replace("net.p2p://", "");
                idx = address.IndexOf("/");
                string absolutePath = address.Substring(idx);
                //端口号为空
                if (string.IsNullOrEmpty(serverPort))
                {
                    address = scheme + serverIP + absolutePath;
                }
                else
                {
                    address = scheme + serverIP + ":" + serverPort + absolutePath;
                }
                el.Host.BaseAddresses[0].BaseAddress = address;
                //string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d+\b";
                //string baseAddress = el.Host.BaseAddresses[0].BaseAddress;
                //string replacement = string.Format("{0}:{1}", serverIP, serverPort);
                //string address = Regex.Replace(baseAddress, pattern, replacement);
                //el.Host.BaseAddresses[0].BaseAddress = address;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.serviceModel");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public static bool IsSameServiceIP(string configPath, string serverIP)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            ConfigurationSectionGroup sec = config.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sec as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;
            string[] arrIP8Port = GetServiceModelIP8Port(configPath);
            string currIp = "";
            if (arrIP8Port != null && arrIP8Port.Length >= 1)
            {
                currIp = arrIP8Port[0];
            }
            if (currIp != serverIP)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取WCF的IP地址和端口号
        /// </summary>
        /// <param name="configPath">Assembly.GetEntryAssembly().Location</param>
        public static string [] GetServiceModelIP8Port(string configPath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            ConfigurationSectionGroup sec = config.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sec as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                string temp = item.Address.Authority;
                if (!string.IsNullOrEmpty(temp))
                {
                    return temp.Split(':');
                }
                //string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d*\b";
                //string address = item.Address.ToString();
                //Match m = Regex.Match(address, pattern);
                //address = m.Groups[0].Value;
                //if (!string.IsNullOrEmpty(address))
                //{
                //    return address.Split(':');
                //}
            }
            foreach (ServiceElement el in serviceModelSectionGroup.Services.Services)
            {
                string address = el.Host.BaseAddresses[0].BaseAddress;
                address = address.Replace("net.tcp://", "")
                    .Replace("net.pipe://", "")
                    .Replace("net.msmq://", "")
                    .Replace("http://", "")
                    .Replace("https://", "")
                    .Replace("net.p2p://", "");
                int idx = address.IndexOf("/");
                address = address.Substring(0, idx);
                return address.Split(':');
                //string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d*\b";
                //string address = el.Host.BaseAddresses[0].BaseAddress;
                //Match m = Regex.Match(address, pattern);
                //address = m.Groups[0].Value;
                //if (!string.IsNullOrEmpty(address))
                //{
                //    return address.Split(':');
                //}
            }
            return null;
        }

        ///<summary> 
        ///更新连接字符串  
        ///</summary> 
        ///<param name="configPath">Assembly.GetEntryAssembly().Location</param>
        ///<param name="connectionStringName">连接字符串名称</param> 
        ///<param name="connectionString">连接字符串内容</param> 
        ///<param name="providerName">数据提供程序名称</param> 
        public static void UpdateConnectionStringsConfig(string configPath, string connectionStringName, string connectionString, string providerName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);

            //如果要更改的连接串已经存在  
            if (config.ConnectionStrings.ConnectionStrings[connectionStringName] != null)
            {
                config.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
                if (!string.IsNullOrEmpty(providerName))
                {
                    config.ConnectionStrings.ConnectionStrings[connectionStringName].ProviderName = providerName;
                }
            }
            else
            {
                if(string.IsNullOrEmpty(providerName))
                {
                    providerName = "System.Data.SqlClient";
                }
                //新建一个连接字符串实例  
                ConnectionStringSettings mySettings =
                    new ConnectionStringSettings(connectionStringName, connectionString, providerName);
                // 将新的连接串添加到配置文件中.  
                config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            }
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }
        
        /// <summary>
        /// 删除连接字符串
        /// </summary>
        /// <param name="configPath">Assembly.GetEntryAssembly().Location</param>
        public static void DeleteConnectionStringsConfig(string configPath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            int count = config.ConnectionStrings.ConnectionStrings.Count;
            string name = "";
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                name = config.ConnectionStrings.ConnectionStrings[i].Name;
                if (name == "ConnCRMCo" || name == "ConnQueryCRMCo") continue;
                list.Add(name);
            }
            foreach(string tmp in list)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(tmp);
            }
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }
        /// <summary>
        /// 删除连接字符串
        /// </summary>
        /// <param name="configPath">Assembly.GetEntryAssembly().Location</param>
        public static void DeleteConnectionStringsConfig(string configPath, string connectionStringName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            int count = config.ConnectionStrings.ConnectionStrings.Count;
            string name = "";
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                name = config.ConnectionStrings.ConnectionStrings[i].Name;
                if (name == "ConnCRMCo" || name == "ConnQueryCRMCo") continue;
                if (name == connectionStringName)
                {
                    list.Add(name);
                    break;
                }
            }
            if (list != null && list.Count>0)
            {
                foreach (string tmp in list)
                {
                    config.ConnectionStrings.ConnectionStrings.Remove(tmp);
                }
                // 保存对配置文件所作的更改  
                config.Save(ConfigurationSaveMode.Modified);
                // 强制重新载入配置文件的ConnectionStrings配置节  
                ConfigurationManager.RefreshSection("ConnectionStrings");
            }
        }

        /// <summary>
        /// 更新appSettings配置的键值对
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateAppSettings(string configPath, string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// 返回appSettings配置节的value值
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingsConfig(string configPath, string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            foreach (string k in config.AppSettings.Settings.AllKeys)
            {
                if (k == key)
                {
                    return config.AppSettings.Settings[key].Value.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// 依据连接串名字connectionName返回数据连接字符串
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>

        public static string GetConnectionStringsConfig(string configPath, string connectionName)
        {
            //指定config文件读取
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            string connectionString =config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }
        /// <summary>
        /// 是否存在连接串名字为connectionName的连接字符串
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>

        public static bool ExistConnectionStringsConfig(string configPath, string connectionName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            int count = config.ConnectionStrings.ConnectionStrings.Count;
            string name = "";
            for (int i = 0; i < count; i++)
            {
                name = config.ConnectionStrings.ConnectionStrings[i].Name;
                if (name == connectionName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 测试与服务器数据库是否成功连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static bool TestConntion(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return false;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        







        // 修改applicationSettings中App.Properties.Settings中服务的IP地址
        public static void UpdateConfig(string configPath, string serverIP)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
            ConfigurationSectionGroup sec = config.SectionGroups["applicationSettings"];
            ConfigurationSection configSection = sec.Sections["DataService.Properties.Settings"];
            ClientSettingsSection clientSettingsSection = configSection as ClientSettingsSection;
            if (clientSettingsSection != null)
            {
                SettingElement element1 = clientSettingsSection.Settings.Get("DataService_SystemManagerWS_SystemManagerWS");
                if (element1 != null)
                {
                    clientSettingsSection.Settings.Remove(element1);
                    string oldValue = element1.Value.ValueXml.InnerXml;
                    element1.Value.ValueXml.InnerXml = GetNewIP(oldValue, serverIP);
                    clientSettingsSection.Settings.Add(element1);
                }

                SettingElement element2 = clientSettingsSection.Settings.Get("DataService_EquipManagerWS_EquipManagerWS");
                if (element2 != null)
                {
                    clientSettingsSection.Settings.Remove(element2);
                    string oldValue = element2.Value.ValueXml.InnerXml;
                    element2.Value.ValueXml.InnerXml = GetNewIP(oldValue, serverIP);
                    clientSettingsSection.Settings.Add(element2);
                }
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("applicationSettings");
        }

        private static string GetNewIP(string oldValue, string serverIP)
        {
            string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
            string replacement = string.Format("{0}", serverIP);
            string newvalue = Regex.Replace(oldValue, pattern, replacement);
            return newvalue;
        }
    }
}