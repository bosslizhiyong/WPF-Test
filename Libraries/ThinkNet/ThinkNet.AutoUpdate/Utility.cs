using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ThinkNet.Infrastructure.Core;

namespace ThinkNet.AutoUpdate
{
    public class Utility
    {
        /// <summary>
        /// 下载文件的文件夹路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="systemBinUrl"></param>
        /// <param name="tempFolder"></param>
        /// <returns></returns>
        public static string GetFolderUrl(DownloadFileInfo file,string systemBinUrl,string tempFolder)
        {
            string folderPathUrl = string.Empty;
            int folderPathPoint = file.DownloadUrl.IndexOf("/", 15) + 1;
            string filepathstring = file.DownloadUrl.Substring(folderPathPoint);
            int folderPathPoint1 = filepathstring.IndexOf("/");
            string filepathstring1 = filepathstring.Substring(folderPathPoint1 + 1);
            if (filepathstring1.IndexOf("/") != -1)
            {
                string[] ExeGroup = filepathstring1.Split('/');
                for (int i = 0; i < ExeGroup.Length - 1; i++)
                {
                    folderPathUrl += "\\" + ExeGroup[i];
                }
                if (!Directory.Exists(systemBinUrl + tempFolder + folderPathUrl))
                {
                    Directory.CreateDirectory(systemBinUrl + tempFolder + folderPathUrl);
                }
            }
            return folderPathUrl;
        }
        /// <summary>
        /// 删除并移动文件
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveFolderToOld(string oldPath, string newPath)
        {
            if (File.Exists(oldPath + ".old"))
                File.Delete(oldPath + ".old");

            if (File.Exists(oldPath))
                File.Move(oldPath, oldPath + ".old");

            File.Move(newPath, oldPath);
            //File.Delete(oldPath + ".old");
        }

        /// <summary>
        /// 重启程序
        /// </summary>
        public static void RestartApplication()
        {
            try
            {
                RunningInstance.ReleaseMutex();//释放
                Process.Start(Application.ExecutablePath);
                Environment.Exit(-1);
            }
            catch(Exception)
            { }
        }
    }
}