using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// 更新配置对象
    /// </summary>
    public class UpdateConfig
    {
        #region 字    段

        #endregion

        #region 属    性
        /// <summary>
        /// 是否启用更新
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 外网地址
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 内网地址
        /// </summary>
        public string IntranetUrl { get; set; }
        /// <summary>
        /// 本地文件列表
        /// </summary>
        public LocalFileList LocalFileList { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UpdateConfig()
        {

        }

        #endregion

        #region 基本方法

        /// <summary>
        /// 加载配置文件到对象
        /// </summary>
        /// <param name="fileName">配置文件路径</param>
        /// <returns></returns>
        public static UpdateConfig LoadUpdateConfig(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpdateConfig));
            StreamReader sr = new StreamReader(fileName);
            UpdateConfig config = xs.Deserialize(sr) as UpdateConfig;
            sr.Close();

            return config;
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="fileName">配置文件路径</param>
        public void SaveUpdateConfig(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpdateConfig));
            StreamWriter sw = new StreamWriter(fileName);
            xs.Serialize(sw, this);
            sw.Close();
        }

        #endregion

        #region 其他方法

        #endregion
    }

    /// <summary>
    /// 本地文件列表
    /// </summary>
    public class LocalFileList : List<LocalFile>
    {

    }
}