using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ThinkNet.Utility;

namespace WCFWeb.Infrastructure.Co
{
    public class ConfigSettings
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public static string Company_Name { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string System_Name { get; set; }
        /// <summary>
        /// 企业编号(针对不同企业共用同一个服务器的，需要在配置文件上手动配置)
        /// </summary>
        public static string Enterprise_Code { get; set; }

        /// <summary>
        /// 是否有退出按钮
        /// </summary>
        public static bool IsExit { get; set; }

        /// <summary>
        /// 初始化数据是否加密
        /// </summary>
        public static bool IsEncryptInitializeData { get; set; }
        /// <summary>
        /// 压缩/解压缩密码
        /// </summary>
        public static string ZipPassword { get; set; }
        /// <summary>
        /// 当前路径
        /// </summary>
        public static string CurrentPath { get; set; }
        /// <summary>
        /// App_Data文件夹完整路径
        /// </summary>
        public static string AppDataFullPath { get; set; }
        /// <summary>
        /// 下载文件的总目录
        /// </summary>
        public static string DownloadFilePath { get; set; }
        /// <summary>
        /// 下载文件的完整目录
        /// </summary>
        public static string DownloadFileFullPath { get; set; }

        /// <summary>
        /// 是否开启权限控制
        /// </summary>
        public static bool IsAuthority { get; set; }
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword { get; set; }
        /// <summary>
        /// Log文件夹目录
        /// </summary>
        public static string LogPath { get; set; }
        /// <summary>
        /// Log文件夹完整路径
        /// </summary>
        public static string LogFullPath { get; set; }
        /// <summary>
        /// 上传文件的总目录
        /// </summary>
        public static string UploadFilePath { get; set; }
        /// <summary>
        /// 上传文件的完整目录
        /// </summary>
        public static string UploadFileFullPath { get; set; }
        /// <summary>
        /// 备份文件的总目录
        /// </summary>
        public static string BackupFilePath { get; set; }
        /// <summary>
        /// 备份文件的完整目录
        /// </summary>
        public static string BackupFileFullPath { get; set; }
        /// <summary>
        /// 备份文件保留天数
        /// </summary>
        public static int BackupKeepDays { get; set; }
        /// <summary>
        /// 更新文件的总目录
        /// </summary>
        public static string UpdateFilePath { get; set; }
        /// <summary>
        /// 更新文件的完整目录
        /// </summary>
        public static string UpdateFileFullPath { get; set; }

        /// <summary>
        /// API程序名称
        /// </summary>
        public static string ApiExeName { get; set; }

        public static void Initialize()
        {
            string settingValue = "";
            //公司名称
            settingValue = ConfigurationManager.AppSettings["Company_Name"];
            Company_Name = !string.IsNullOrEmpty(settingValue) ? settingValue : "";
            //系统名称
            settingValue = ConfigurationManager.AppSettings["System_Name"];
            System_Name = !string.IsNullOrEmpty(settingValue) ? settingValue : "客户关系管理系统";
            //企业编号
            settingValue = ConfigurationManager.AppSettings["EnterpriseCode"];
            Enterprise_Code = !string.IsNullOrEmpty(settingValue) ? settingValue : "";

            //是否有退退按钮
            settingValue = ConfigurationManager.AppSettings["IsExit"];
            IsExit = !string.IsNullOrEmpty(settingValue) ? DataTypeConvert.ToBoolean(settingValue, true) : true;

            //初始化数据是否加密
            settingValue = ConfigurationManager.AppSettings["IsEncryptInitializeData"];
            IsEncryptInitializeData = !string.IsNullOrEmpty(settingValue) ? DataTypeConvert.ToBoolean(settingValue, false) : false;
            //压缩/解压缩密码
            settingValue = ConfigurationManager.AppSettings["ZipPassword"];
            ZipPassword = !string.IsNullOrEmpty(settingValue) ? settingValue : "abc123";
            //当前路径
            CurrentPath = DirectoryHelper.GetDirectoryPath("");
            //App_Data文件夹完整路径
            AppDataFullPath = DirectoryHelper.GetDirectoryPath("/App_Data/"); //HttpContext.Current.Server.MapPath(@"~/App_Data/");
            //下载文件的总目录
            settingValue = ConfigurationManager.AppSettings["DownloadFilePath"];
            DownloadFilePath = !string.IsNullOrEmpty(settingValue) ? settingValue : "/DownloadFiles/";
            DownloadFileFullPath = DirectoryHelper.GetDirectoryPath(DownloadFilePath); //HttpContext.Current.Server.MapPath(@"~" + DownloadFilePath);

            //是否开启权限控制
            settingValue = ConfigurationManager.AppSettings["IsAuthority"];
            IsAuthority = !string.IsNullOrEmpty(settingValue) ? DataTypeConvert.ToBoolean(settingValue, true) : true;
            //默认密码
            settingValue = ConfigurationManager.AppSettings["DefaultPassword"];
            DefaultPassword = !string.IsNullOrEmpty(settingValue) ? settingValue : "abc123";
            //Log文件夹目录
            settingValue = ConfigurationManager.AppSettings["LogPath"];
            LogPath = !string.IsNullOrEmpty(settingValue) ? settingValue : "/Log/";
            LogFullPath = DirectoryHelper.GetDirectoryPath(LogPath);// HttpContext.Current.Server.MapPath(@"~/Log/");
            //上传文件的总目录
            settingValue = ConfigurationManager.AppSettings["UploadFilePath"];
            UploadFilePath = !string.IsNullOrEmpty(settingValue) ? settingValue : "/UploadFiles/";
            UploadFileFullPath = DirectoryHelper.GetDirectoryPath(UploadFilePath);// HttpContext.Current.Server.MapPath(@"~" + UploadFilePath);
            //备份文件的总目录
            settingValue = ConfigurationManager.AppSettings["BackupFilePath"];
            BackupFilePath = !string.IsNullOrEmpty(settingValue) ? settingValue : "/Backup/";
            BackupFileFullPath = DirectoryHelper.GetDirectoryPath(BackupFilePath);// HttpContext.Current.Server.MapPath(@"~" + BackupFilePath);
            BackupKeepDays = 14;
            //更新文件的总目录
            settingValue = ConfigurationManager.AppSettings["UpdateFilePath"];
            UpdateFilePath = !string.IsNullOrEmpty(settingValue) ? settingValue : "/UpdateFiles/";
            UpdateFileFullPath = DirectoryHelper.GetDirectoryPath(UpdateFilePath);// HttpContext.Current.Server.MapPath(@"~" + UpdateFilePath);

            //API程序名称
            settingValue = ConfigurationManager.AppSettings["apiExeName"];
            ApiExeName = !string.IsNullOrEmpty(settingValue) ? settingValue : "ThinkCRM.Win.Co.ApiHost";
        }

    }
}
