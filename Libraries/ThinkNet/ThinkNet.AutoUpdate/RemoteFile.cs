using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 远程文件对象
    /// </summary>
    public class RemoteFile
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 文件路径(包括名称)
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 最新版本
        /// </summary>
        public string LastVer { get; private set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// 是否需要重启
        /// </summary>
        public bool NeedRestart { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 远程文件对象
        /// </summary>
        /// <param name="node">远程配置文件的文件节点</param>
        public RemoteFile(XmlNode node)
        {
            this.Path = node.Attributes["path"].Value;
            this.Url = node.Attributes["url"].Value;
            this.LastVer = node.Attributes["lastver"].Value;
            this.Size = Convert.ToInt32(node.Attributes["size"].Value);
            this.NeedRestart = Convert.ToBoolean(node.Attributes["needRestart"].Value);
        }

        #endregion

        #region 基本方法
        
        #endregion

        #region 其他方法

        #endregion
    }
}
