using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class FileHelper
    {
        public static bool DeleteFile(string fileName)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static bool WriteContent(string fileName, string content)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool WriteContent(string fileName, string content,Encoding encoding)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] bytes = encoding.GetBytes(content);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool WriteContentEncrypt(string fileName, string content)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
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
                throw ex;
            }
        }

        public static string ReadContent(string fileName)
        {
            string encrypt = File.ReadAllText(fileName, Encoding.Default);
            byte[] bytes = Encoding.UTF8.GetBytes(encrypt);
            string result = "";
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                result = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return result;
            }
        }
        public static string ReadContent(string fileName, Encoding encoding)
        {
            string encrypt = File.ReadAllText(fileName, encoding);
            byte[] bytes = encoding.GetBytes(encrypt);
            string result = "";
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                result = encoding.GetString(stream.ToArray());
                stream.Close();
                return result;
            }
        }
        public static string ReadContentDecrypt(string fileName)
        {
            string encrypt = File.ReadAllText(fileName, Encoding.Default);
            ICrypto crypto = CryptoFactory.Create(CrytoType.TripleDES);
            string content = crypto.Decrypt(encrypt);
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            string result = "";
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                result = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return result;
            }
        }
    }
}
