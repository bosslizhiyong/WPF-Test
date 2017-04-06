using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class DirectoryHelper
    {
        /// <summary>
        /// 获取目录路径
        /// </summary>
        /// <param name="relativePath">相对路径(如：/App_Data/)</param>
        /// <returns></returns>
        public static string GetDirectoryPath(string relativePath)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            //WebDev.WebServer      visual studio web server
            //xxx.vhost             Winform
            //w3wp                  IIS7
            //aspnet_wp             IIS6
            string processName = p.ProcessName.ToLower();
            if (processName == "aspnet_wp" || processName == "w3wp" || processName == "webdev.webserver" || processName == "iisexpress")
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (string.IsNullOrEmpty(relativePath))
                    {
                        return System.Web.HttpContext.Current.Server.MapPath("~/");
                    }
                    else
                    {
                        return System.Web.HttpContext.Current.Server.MapPath("~/" + relativePath);
                    }
                }
                else //当控件在定时器的触发程序中使用时就为空
                {
                    return System.AppDomain.CurrentDomain.BaseDirectory;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(relativePath))
                {
                    return System.Windows.Forms.Application.StartupPath+"\\";
                }
                else
                {
                    return System.Windows.Forms.Application.StartupPath + relativePath.Replace("/","\\");
                }
            }
        }

        /// <summary>
        /// 创建目录(如果目录已经存在,不创建,否则自动创建)
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public static bool CreateDirectoryByPath(string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 是否存在目录路径
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public static bool IsExistDirectoryPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                { return false; }
                return System.IO.Directory.Exists(path);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 目录下的文件数
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public static int GetDirectoryFileCount(string path)
        {
            try
            {
                if(Directory.Exists(path))
                {
                    return Directory.GetFileSystemEntries(path).Count();
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据文件路径创建目录
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool CreateDirectoryByFilePath(string filePath)
        {
            string path = Path.GetDirectoryName(filePath);
            return CreateDirectoryByPath(path);
        }

        /// <summary>
        /// 根据文件路径获取文件名称
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameByFilePath(string filePath)
        {
            string name = Path.GetFileName(filePath);
            return name;
        }

        /// <summary>
        /// 递归获取文件夹下的所有文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static List<string> GetDirectoryFiles(string filePath)
        {
            if (!Directory.Exists(filePath))
                return null;

            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(filePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
            {
                list.Add(NextFile.FullName);
            }
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo NextFile in fileInfo)  //遍历文件
                {
                    list.Add(NextFile.FullName);
                }
            }
            return list;
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static bool CopyDirectory(string fromPath, string toPath)
        {
            if (!Directory.Exists(fromPath))
                return false;

            if (!Directory.Exists(toPath))
            {
                Directory.CreateDirectory(toPath);
            }

            string[] files = Directory.GetFiles(fromPath);
            foreach (string formFileName in files)
            {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toPath, fileName);
                File.Copy(formFileName, toFileName);
            }
            string[] fromDirs = Directory.GetDirectories(fromPath);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toPath, dirName);
                CopyDirectory(fromDirName, toDirName);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        /// <returns></returns>
        public static bool MoveDirectory(string fromPath, string toPath)
        {
            if (!Directory.Exists(fromPath))
                return false;

            bool result=CopyDirectory(fromPath, toPath);
            if (result)
            {
                Directory.Delete(fromPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="filePath">要清空的文件夹路径</param>
        public static void ClearDirectory(string filePath)
        {
            foreach (string d in Directory.GetFileSystemEntries(filePath))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        ClearDirectory(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
        } 

        #region 文件复制
        public static void CopyFolder(string strFromPath, string strToPath, bool flag)
        {

            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }

            string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") +
              1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
            if (flag)
            {
                strFolderName = "";
            }
            if (!Directory.Exists(strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(strToPath + "\\" + strFolderName);
            }

            string[] strFiles = Directory.GetFiles(strFromPath);

            for (int i = 0; i < strFiles.Length; i++)
            {

                string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);

                System.IO.File.Copy(strFiles[i], strToPath + "\\" + strFolderName + "\\" + strFileName, true);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);

            DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
            for (int j = 0; j < ZiPath.Length; j++)
            {
                string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();

                CopyFolder(strZiPath, strToPath + "\\" + strFolderName, false);
            }
        }
        #endregion
    }
}
