using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 本地文件对象
    /// </summary>
    public class LocalFile
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 文件路径(包括名称)
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }
        /// <summary>
        /// 最新版本
        /// </summary>
        [XmlAttribute("lastver")]
        public string LastVer { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        [XmlAttribute("size")]
        public int Size { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LocalFile()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ver"></param>
        /// <param name="size"></param>
        public LocalFile(string path, string ver, int size)
        {
            this.Path = path;
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
