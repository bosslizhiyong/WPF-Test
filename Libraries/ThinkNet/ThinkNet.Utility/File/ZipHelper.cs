using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class ZipHelper
    {
        #region 压缩文件夹
        /// <summary>
        /// 压缩文件夹(含多层文件夹)
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="zipFileName">压缩后文件</param>
        public static void ZipFolder(string folderPath, string zipFileName)
        {
            ZipFolder(folderPath, zipFileName, "");
        }
        /// <summary>
        /// 压缩文件夹(含多层文件夹)
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="zipFileName">压缩后文件</param>
        /// <param name="password">加密密码</param>
        public static void ZipFolder(string folderPath, string zipFileName,string password)
        {
            if (string.IsNullOrEmpty(zipFileName))
            {
                throw new Exception("压缩后文件为空!");
            }
            using (System.IO.FileStream ZipFile = System.IO.File.Create(zipFileName))
            {
                using (ZipOutputStream stream = new ZipOutputStream(ZipFile))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        stream.Password = password;
                    }
                    stream.SetLevel(6);//压缩比 0-9
                    ZipFolder(folderPath, stream, "");
                }
            }
        }
        /// <summary>
        /// 压缩文件夹(含多层文件夹),递归遍历
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="stream"></param>
        /// <param name="parentPath"></param>
        private static void ZipFolder(string folderPath, ZipOutputStream stream, string parentPath)
        {
            if (folderPath[folderPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                folderPath += Path.DirectorySeparatorChar;
            }
            //Crc32 crc = new Crc32();

            string[] fileNames = Directory.GetFileSystemEntries(folderPath);
            foreach (string file in fileNames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\") + 1);
                    pPath += "\\";
                    ZipFolder(file, stream, pPath);
                }
                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(fileName);
                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;
                        fs.Close();

                        //crc.Reset();
                        //crc.Update(buffer);
                        //entry.Crc = crc.Value;
                        stream.PutNextEntry(entry);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        #endregion

        #region 压缩文件
        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="fileName">文件</param>
        /// <param name="zipFileName">压缩后文件</param>
        public static void ZipFile(string fileName, string zipFileName)
        {
            ZipFile(fileName, zipFileName, "");
        }
        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="fileName">文件</param>
        /// <param name="zipFileName">压缩后文件</param>
        /// <param name="password">加密密码</param>
        public static void ZipFile(string fileName, string zipFileName,string password)
        {
            ArrayList arrfile = new ArrayList();
            arrfile.Add(fileName);
            ZipFiles(arrfile, zipFileName, password);
        }
        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="fileNames">多个文件列表</param>
        /// <param name="zipFileName">压缩后文件</param>
        public static void ZipFiles(ArrayList fileNames, string zipFileName)
        {
            ZipFiles(fileNames, zipFileName,"");
        }
        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="fileNames">多个文件列表</param>
        /// <param name="zipFileName">压缩后文件</param>
        /// <param name="password">加密密码</param>
        public static void ZipFiles(ArrayList fileNames, string zipFileName,string password)
        {
            if (fileNames.Count == 0)
            {
                throw new Exception("压缩文件为空!");
            }

            foreach (object file in fileNames)
            {
                if (File.Exists(((string)file).Trim()) == false)
                {
                    throw new Exception("没找到文件(" + (string)file + ")!");
                }
            }

            if (string.IsNullOrEmpty(zipFileName))
            {
                throw new Exception("压缩后文件为空!");
            }

            using (System.IO.FileStream ZipFile = System.IO.File.Create(zipFileName))
            {
                using (ZipOutputStream stream = new ZipOutputStream(ZipFile))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        stream.Password = password;
                    }
                    stream.SetLevel(6);//压缩比 0-9
                    Crc32 crc = new Crc32();
                    //压缩文件
                    foreach (object file in fileNames)
                    {
                        ZipSingleFile((string)file, stream, crc);
                    }
                }
            }
        }

        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="fileName"></param>
        private static void ZipSingleFile(string fileName, ZipOutputStream stream,Crc32 crc)
        {
            // Open and read this file
            using(FileStream fso = File.OpenRead(fileName))
            {
                // Read this file to Buffer
                byte[] buffer = new byte[fso.Length];
                fso.Read(buffer, 0, buffer.Length);

                // Create a new ZipEntry
                ZipEntry zipEntry = new ZipEntry(fileName.Split('/')[fileName.Split('/').Length - 1]);

                zipEntry.DateTime = DateTime.Now;
                // set Size and the crc, because the information
                // about the size and crc should be stored in the header
                // if it is not set it is automatically written in the footer.
                // (in this case size == crc == -1 in the header)
                // Some ZIP programs have problems with zip files that don't store
                // the size and crc in the header.
                zipEntry.Size = fso.Length;

                fso.Close();

                // Using CRC to format the buffer
                //crc.Reset();
                //crc.Update(buffer);
                //zipEntry.Crc = crc.Value;

                // Add this ZipEntry to the ZipStream
                stream.PutNextEntry(zipEntry);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        #endregion

        #region 解压缩文件
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="zipFileName">压缩后的文件</param>
        /// <param name="unZipPath">解压后路径</param>
        public static void UnZip(string zipFileName, string unZipPath)
        {
            UnZip(zipFileName, unZipPath, "", true);
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="zipFileName">压缩后的文件</param>
        /// <param name="unZipPath">解压后路径</param>
        /// <param name="password">解压密码</param>
        public static void UnZip(string zipFileName, string unZipPath, string password)
        {
            UnZip(zipFileName, unZipPath, password, true);
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="zipFileName">压缩后的文件</param>
        /// <param name="unZipPath">解压后路径</param>
        /// <param name="IsOverWrite">是否覆盖已存在的文件</param>
        public static void UnZip(string zipFileName, string unZipPath,bool IsOverWrite)
        {
            UnZip(zipFileName, unZipPath, "", IsOverWrite);
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="zipFileName">压缩后的文件</param>
        /// <param name="unZipPath">解压后路径</param>
        /// <param name="password">解压密码</param>
        /// <param name="IsOverWrite">是否覆盖已存在的文件</param>
        public static void UnZip(string zipFileName, string unZipPath, string password, bool IsOverWrite)
        {

            if (unZipPath == "")
                unZipPath = Directory.GetCurrentDirectory();
            if (!unZipPath.EndsWith("\\"))
                unZipPath = unZipPath + "\\";

            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(zipFileName)))
            {
                if(!string.IsNullOrEmpty(password))
                {
                    stream.Password = password;
                }
                ZipEntry theEntry;
                while ((theEntry = stream.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(unZipPath + directoryName);

                    if (fileName != "")
                    {
                        if ((File.Exists(unZipPath + directoryName + fileName) && IsOverWrite) || (!File.Exists(unZipPath + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(unZipPath + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = stream.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }
                stream.Close();
            }
        }
        #endregion

        #region 解压 文件 zip 格式 rar 格式
        /// <summary>
        ///解压文件
        /// </summary>
        /// <param name="fileFromUnZip">解压前的文件路径（绝对路径）</param>
        /// <param name="fileToUnZip">解压后的文件目录（绝对路径）</param>
        public static void UnpackFile(string fileFromUnZip, string fileToUnZip)
        {
            //获取压缩类型
            string unType = fileFromUnZip.Substring(fileFromUnZip.LastIndexOf(".") + 1, 3).ToLower();
            switch (unType)
            {
                case "rar":
                    UnpackRar(fileFromUnZip, fileToUnZip);
                    break;
                case "zip":
                    UnpackZip(fileFromUnZip, fileToUnZip);
                    break;
            }
        }
        /// <summary>
        /// 解压rar格式的文件
        /// </summary>
        /// <param name="fileFromUnZip">解压前的文件路径（绝对路径）</param>
        /// <param name="fileToUnZip">解压后的文件目录（绝对路径）</param>
        private static void UnpackRar(string fileFromUnZip, string fileToUnZip)
        {
            using (Process Process1 = new Process())// 开启一个进程 执行解压工作
            {
                string ServerDir = ConfigurationManager.AppSettings["UnpackFile"].ToString();//rar工具的安装路径   必须要安装 WinRAR     //例于：C:\Program Files (x86)\WinRAR\RAR.exe
                Process1.StartInfo.UseShellExecute = false;
                Process1.StartInfo.RedirectStandardInput = true;
                Process1.StartInfo.RedirectStandardOutput = true;
                Process1.StartInfo.RedirectStandardError = true;
                Process1.StartInfo.CreateNoWindow = true;
                Process1.StartInfo.FileName = ServerDir;
                Process1.StartInfo.Arguments = " x -inul -y " + fileFromUnZip + " " + fileToUnZip;
                Process1.Start();//解压开始 
                Process1.WaitForExit();
                Process1.Close();
            }
        }
        /// <summary>
        ///  解压zip 文件
        /// </summary>
        /// <param name="fileFromUnZip">解压前的文件路径（绝对路径）</param>
        /// <param name="fileToUnZip">解压后的文件目录（绝对路径）</param>
        public static void UnpackZip(string fileFromUnZip, string fileToUnZip)
        {
            ZipInputStream inputStream = new ZipInputStream(System.IO.File.OpenRead(fileFromUnZip));
            ZipEntry theEntry;
            while ((theEntry = inputStream.GetNextEntry()) != null)
            {
                fileToUnZip += "/";
                string fileName = Path.GetFileName(theEntry.Name);
                string path = Path.GetDirectoryName(fileToUnZip) + "\\";
                path += theEntry.Name.Substring(0, theEntry.Name.LastIndexOf("/"));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                if (fileName != String.Empty)
                {
                    FileStream streamWriter = System.IO.File.Create(path + "\\" + fileName);//解压文件到指定的目录
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = inputStream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            inputStream.Close();
        }
        #endregion
    }
}
