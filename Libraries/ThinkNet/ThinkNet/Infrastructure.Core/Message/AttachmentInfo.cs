using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core.Message
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public struct AttachmentInfo
    {
       
        #region 字段
        /// <summary>
        /// 附件的文件名 [如果输入路径，则自动转换为文件名]
        /// </summary>
        private string fileName;
        /// <summary>
        /// 附件的内容 [由经Base64编码的字节组成]
        /// </summary>
        private string bytes;
        #endregion

        #region 属性
        /// <summary>
        /// 附件的文件名 [如果输入路径，则自动转换为文件名]
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = Path.GetFileName(value); }
        }

        /// <summary>
        /// 附件的内容 [由经Base64编码的字节组成]
        /// </summary>
        public string Bytes
        {
            get { return bytes; }
            set { if (value != bytes) bytes = value; }
        }  
        #endregion

        #region 基本方法
        /// <summary>
        /// 从流中读取附件内容并构造
        /// </summary>
        /// <param name="ifileName">附件的文件名</param>
        /// <param name="stream">流</param>
        public AttachmentInfo(string ifileName, Stream stream)
        {
            fileName = Path.GetFileName(ifileName);
            byte[] by = new byte[stream.Length];
            stream.Read(by, 0, (int)stream.Length); // 读取文件内容
            //格式转换
            bytes = Convert.ToBase64String(by); // 转化为base64编码
        }

        /// <summary>
        /// 按照给定的字节构造附件
        /// </summary>
        /// <param name="ifileName">附件的文件名</param>
        /// <param name="ibytes">附件的内容 [字节]</param>
        public AttachmentInfo(string ifileName, byte[] ibytes)
        {
            fileName = Path.GetFileName(ifileName);
            bytes = Convert.ToBase64String(ibytes); // 转化为base64编码
        }

        /// <summary>
        /// 从文件载入并构造
        /// </summary>
        /// <param name="path"></param>
        public AttachmentInfo(string path)
        {
            fileName = Path.GetFileName(path);
            FileStream file = new FileStream(path, FileMode.Open);
            byte[] by = new byte[file.Length];
            file.Read(by, 0, (int)file.Length); // 读取文件内容
            //格式转换
            bytes = Convert.ToBase64String(by); // 转化为base64编码
            file.Close();
        } 
        #endregion
    }
}
