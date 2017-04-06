using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 下载文件对象
    /// </summary>
    public class DownloadFileInfo
    {
        #region 字    段

        #endregion

        #region 属    性
        /// <summary>
        /// 下载文件路径
        /// </summary>
        public string DownloadUrl { get; private set; }
        /// <summary>
        /// (本地)文件路径(包括名称)
        /// </summary>
        public string FileFullName { get; private set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get { return Path.GetFileName(FileFullName); } }
        /// <summary>
        /// 最新版本
        /// </summary>
        public string LastVer { get; private set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 下载文件对象
        /// </summary>
        /// <param name="downloadUrl">下载文件路径</param>
        /// <param name="fileFullName">(本地)文件路径(包括名称)</param>
        /// <param name="ver">最新版本</param>
        /// <param name="size">大小</param>
        public DownloadFileInfo(string downloadUrl, string fileFullName, string ver, int size)
        {
            this.DownloadUrl = downloadUrl;
            this.FileFullName = fileFullName;
            this.LastVer = ver;
            this.Size = size;
        }

        #endregion

        #region 基本方法
        
        #endregion

        #region 其他方法

        #endregion

    }
}
