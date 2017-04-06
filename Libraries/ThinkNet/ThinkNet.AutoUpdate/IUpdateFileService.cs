using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ThinkNet.AutoUpdate
{
    /// <summary>
    /// Wcf下载更新文件契约
    /// </summary>
    [ServiceContract]
    public interface IUpdateFileService
    {
        /// <summary>
        /// 是否存在需要更新的文件列表配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistUpdateFileXml(string fileName);
        /// <summary>
        /// 获取需要更新的文件列表(xml文件内容)
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        [OperationContract]
        string GetUpdateFileXml(string fileName);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="mFileInfo">下载文件信息</param>
        /// <returns></returns>
        [OperationContract]
        UpdateFileInfo DownLoadFile(UpdateFileInfo mFileInfo);
    }

    /// <summary>
    /// 下载更新文件的信息
    /// </summary>
    [Serializable]
    public class UpdateFileInfo
    {
        /// <summary>
        /// 文件原始名称
        /// </summary>
        public string FileName
        { get; set; }

        /// <summary>
        /// 文件保存名称
        /// </summary>
        public string FileSaveName
        { get; set; }

        /// <summary>
        /// 传递的字节
        /// </summary>
        public List<byte> FileData
        { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Length
        { get; set; }

        /// <summary>
        /// 文件的偏移量
        /// </summary>
        public long Offset
        { get; set; }

        /// <summary>
        /// 文件分类，如：Supplier
        /// </summary>
        public string FileCategory
        { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string ErrorMessage
        { get; set; }
    }
}
