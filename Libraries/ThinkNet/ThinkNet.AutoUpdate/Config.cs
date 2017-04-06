using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ThinkNet.AotuUpdate.Core
{
    public class Config
    {
        private bool enabled = true;
        public bool Enabled { get { return enabled; } set { enabled = value; } }
        /// <summary>
        /// 公网服务器
        /// </summary>
        private string serverUrl = "";
        public string ServerUrl { get { return serverUrl; } set { serverUrl = value; } }

        /// <summary>
        /// 内网服务器
        /// </summary>
        private string intranetUrl = "";

        public string IntranetUrl {get {return intranetUrl;} set { intranetUrl = value; } }

        private UpdateFileList updateFileList = new UpdateFileList();
        public UpdateFileList UpdateFileList
        {
            get { return updateFileList; }
            set { updateFileList = value; }
        }

        public static Config LoadConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamReader sr = new StreamReader(file);
            Config config = xs.Deserialize(sr) as Config;
            sr.Close();
            return config;
        }

        public void SaveConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, this);
            sw.Close();
        }
    }

    public class UpdateFileList : List<LocalFile>
    {
    }
}
