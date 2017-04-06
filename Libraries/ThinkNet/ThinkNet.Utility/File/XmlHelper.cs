using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ThinkNet.Utility
{
    public class XmlHelper
    {
        #region XML文档创建并保存
        /// <summary>
        /// 创建XML文档并保存为文件
        /// </summary>
        /// <param name="xmlFileName">文件名称(包含路径)</param>
        /// <param name="rootName">根结点</param>
        /// <returns></returns>
        public static XmlDocument Create(string xmlFileName, string rootName)
        {
            return Create(xmlFileName, rootName, "UTF-8");
        }
        /// <summary>
        /// 创建XML文档并保存为文件
        /// </summary>
        /// <param name="xmlFileName">文件名称(包含路径)</param>
        /// <param name="rootName">根结点</param>
        /// <param name="encoding">GB2312或UTF-8</param>
        /// <returns></returns>
        public static XmlDocument Create(string xmlFileName, string rootName, string encoding)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", encoding, null);
            doc.AppendChild(dec);
            //根结点
            XmlElement root = doc.CreateElement(rootName);
            doc.AppendChild(root);
            doc.Save(xmlFileName);
            return doc;
        }

        /// <summary>
        /// 加载指定路径的XML文件(如果文件不存则自动创建空的文件)
        /// </summary>
        /// <param name="xmlFileName">xml文件路径（如：CurrentPath + "\\Xml4UserSetting.xml"）</param>
        /// <param name="rootName">根结点</param>
        /// <returns></returns>
        public static XmlDocument LoadXmlDocument(string xmlFileName, string rootName)
        {
            XmlDocument document = new XmlDocument();
            if (!File.Exists(xmlFileName))
            {
                XmlHelper.Create(xmlFileName, rootName);
            }
            document.Load(xmlFileName);
            return document;
        }
        #endregion

        #region XML文档创建
        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <returns></returns>
        public static XmlDocument Create()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            return doc;
        }
        /// <summary>
        /// 创建XML文档(输出根节点)
        /// </summary>
        /// <param name="rootName">根节点名称</param>
        /// <param name="rootElement">根节点</param>
        /// <returns></returns>
        public static XmlDocument Create(string rootName, out XmlElement rootElement)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            rootElement = AddRootChild(doc, rootName);
            return doc;
        }
        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <param name="encoding">GB2312或UTF-8</param>
        /// <returns></returns>
        public static XmlDocument Create(string encoding)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", encoding, null);
            doc.AppendChild(dec);
            return doc;
        }
        /// <summary>
        /// 创建XML文档(输出根节点)
        /// </summary>
        /// <param name="encoding">GB2312或UTF-8</param>
        /// <param name="rootName">根节点名称</param>
        /// <param name="rootElement">根节点</param>
        /// <returns></returns>
        public static XmlDocument Create(string encoding, string rootName, out XmlElement rootElement)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", encoding, null);
            doc.AppendChild(dec);
            rootElement = AddRootChild(doc, rootName);
            return doc;
        }

        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <param name="xmlString">xml节点字符串(如：<book genre='novel' ISBN='1-861001-57-5'><title>Pride And Prejudice</title></book>)</param>
        /// <returns></returns>
        public static XmlDocument CreateFromString(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(dec, root);
            return doc;
        }
        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <param name="xmlString">xml节点字符串(如：<book genre='novel' ISBN='1-861001-57-5'><title>Pride And Prejudice</title></book>)</param>
        /// <param name="encoding">GB2312或UTF-8</param>
        /// <returns></returns>
        public static XmlDocument CreateFromString(string xmlString, string encoding)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", encoding, null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(dec, root);
            return doc;
        }

        /// <summary>
        /// 向XML文档添加根节点
        /// </summary>
        /// <param name="doc">XML文档</param>
        /// <param name="rootName">根节点名称</param>
        public static XmlElement AddRootChild(XmlDocument doc, string rootName)
        {
            if (string.IsNullOrEmpty(rootName))
            {
                return null;
            }
            XmlElement el = doc.CreateElement(rootName);
            doc.AppendChild(el);
            return el;
        }

        /// <summary>
        /// 向XML文档添加子节点
        /// </summary>
        /// <param name="doc">XML文档</param>
        /// <param name="parentXmlElement">父级节点</param>
        /// <param name="childName">子节点名称</param>
        public static XmlElement AddChild(XmlDocument doc, XmlElement parentXmlElement, string childName)
        {
            XmlElement el = doc.CreateElement(childName);
            parentXmlElement.AppendChild(el);
            return el;
        }
        /// <summary>
        /// 向XML文档添加子节点
        /// </summary>
        /// <param name="doc">XML文档</param>
        /// <param name="parentXmlElement">父级节点</param>
        /// <param name="childName">子节点名称</param>
        /// <param name="childValue">子节点值</param>
        public static XmlElement AddChild(XmlDocument doc, XmlElement parentXmlElement, string childName, string childValue)
        {
            XmlElement el = doc.CreateElement(childName);
            parentXmlElement.AppendChild(el);
            XmlText val = doc.CreateTextNode(childValue);
            el.AppendChild(val);
            return el;
        }
        #endregion

        #region XML文档转为字符串
        /// <summary>
        /// 将XML文档转为字符串形式
        /// </summary>
        /// <param name="doc">XML文档</param>
        /// <returns></returns>
        public static string ConertToString(XmlDocument doc)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(stream, null);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                stream.Position = 0;
                string xmlString = sr.ReadToEnd();
                sr.Close();
                return xmlString;
            }
        }
        /// <summary>
        /// 将XML文档转为字符串形式
        /// </summary>
        /// <param name="xmlString">xml节点字符串(如：<book genre='novel' ISBN='1-861001-57-5'><title>Pride And Prejudice</title></book>)</param>
        /// <returns></returns>
        public static string ConertToString(string xmlString)
        {
            XmlDocument doc = CreateFromString(xmlString);
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(stream, null);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                stream.Position = 0;
                string xmlStr = sr.ReadToEnd();
                sr.Close();
                return xmlStr;
            }
        }
        /// <summary>
        /// 获取xml文件中的所有内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static String GetXmlContent(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc == null ? String.Empty : doc.OuterXml;
        }
        #endregion

        #region XML文档操作
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }
        #endregion

        #region XML文档序列化与反序列化
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Serialize(obj, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Serialize(object obj, System.Text.Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                SerializeInternal(stream, obj, encoding);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        private static void SerializeInternal(Stream stream, object obj, System.Text.Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="xmlString">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T Deserialize<T>(string xmlString, System.Text.Encoding encoding)
        {
            if (string.IsNullOrEmpty(xmlString))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString)
        {
            return Deserialize<T>(xmlString, System.Text.Encoding.UTF8);
        }
        #endregion

        #region XML文档序列化并加密与解密并反序列化
        /// <summary>
        /// XML序列化并加密
        /// </summary>
        /// <param name="xmlFileName">文件路径</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool SerializeEncrypt(string xmlFileName, object obj)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            try
            {
                if (!File.Exists(xmlFileName))
                {
                    FileInfo fi = new FileInfo(xmlFileName);
                    if (!fi.Directory.Exists)
                    {
                        Directory.CreateDirectory(fi.Directory.FullName);
                    }
                }

                using (Stream stream = new FileStream(xmlFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    string content = "";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer format = new XmlSerializer(obj.GetType());
                        format.Serialize(ms, obj, ns);
                        ms.Seek(0, 0);
                        content = Encoding.UTF8.GetString(ms.ToArray());
                    }

                    ICrypto crypto = CryptoFactory.Create(CrytoType.TripleDES);
                    string content1 = crypto.Encrypt(content);
                    byte[] bytes = Encoding.UTF8.GetBytes(content1);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 解密并进行XML反序列化
        /// </summary>
        /// <param name="xmlFileName">文件路径</param>
        /// <returns></returns>
        public static T DeserializeDecrypt<T>(string xmlFileName)
        {
            string encrypt = File.ReadAllText(xmlFileName, Encoding.UTF8);
            ICrypto crypto = CryptoFactory.Create(CrytoType.TripleDES);
            string content = crypto.Decrypt(encrypt);
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));

                stream.Seek(0, SeekOrigin.Begin);
                Object obj = formatter.Deserialize(stream);
                stream.Close();
                return (T)obj;
            }
        }

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="xmlFileName">文件路径</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool SerializeFile(string xmlFileName, object obj)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            try
            {
                if (!File.Exists(xmlFileName))
                {
                    FileInfo fi = new FileInfo(xmlFileName);
                    if (!fi.Directory.Exists)
                    {
                        Directory.CreateDirectory(fi.Directory.FullName);
                    }
                }

                using (Stream stream = new FileStream(xmlFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    string content = "";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer format = new XmlSerializer(obj.GetType());
                        format.Serialize(ms, obj, ns);
                        ms.Seek(0, 0);
                        content = Encoding.UTF8.GetString(ms.ToArray());
                    }

                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 解密并进行XML反序列化
        /// </summary>
        /// <param name="xmlFileName">文件路径</param>
        /// <returns></returns>
        public static T DeserializeFile<T>(string xmlFileName)
        {
            string content = File.ReadAllText(xmlFileName, Encoding.UTF8);
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));

                stream.Seek(0, SeekOrigin.Begin);
                Object obj = formatter.Deserialize(stream);
                stream.Close();
                return (T)obj;
            }
        }
        #endregion
    }
}
