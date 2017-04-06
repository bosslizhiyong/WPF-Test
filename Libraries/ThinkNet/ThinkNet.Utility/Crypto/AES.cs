using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 高级加密标准,又称Rijndael加密法
    /// </summary>
    public class AES:ICrypto
    {
        #region 字    段

        private Rijndael mCrypto = null;
        private string key;
        private string iv;

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AES()
            : this("{A090CB24-AF38-4544-92F8-A5B9F1A36ABD}")
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        public AES(string key)
            : this(key, "#$^%&&*Yisifhsfjsljfslhgosdshf26382837sdfjskhf97(*&(*")
        { 
        
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">矢量</param>
        public AES(string key, string iv)
        {
            this.key = key;
            this.iv = iv;

            mCrypto = Rijndael.Create(); 
            mCrypto.Key = GetLegalKey();
            mCrypto.IV = GetLegalIV();
            mCrypto.Mode = CipherMode.CBC;//CBC模式加密
            mCrypto.Padding = PaddingMode.PKCS7;
        }

        #endregion

        #region 基本方法

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            return Encrypt(plainText, null);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns></returns>
        public string Encrypt(string plainText, string encryptKey)
        {
            if (!string.IsNullOrEmpty(encryptKey))
            {
                this.key = encryptKey;
                mCrypto.Key = GetLegalKey();
            }

            byte[] arrPlainText = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform encryptor = mCrypto.CreateEncryptor();
                using (CryptoStream cStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    cStream.Write(arrPlainText, 0, arrPlainText.Length);
                    cStream.FlushFinalBlock();
                }
                byte[] outputBytes = stream.ToArray();
                return Convert.ToBase64String(outputBytes);
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cryptoText">密文</param>
        /// <returns></returns>
        public string Decrypt(string cryptoText)
        {
            return Decrypt(cryptoText, null);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cryptoText">密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public string Decrypt(string cryptoText, string decryptKey)
        {
            if (!string.IsNullOrEmpty(decryptKey))
            {
                this.key = decryptKey;
                mCrypto.Key = GetLegalKey();
            }

            byte[] arrCryptoText = Convert.FromBase64String(cryptoText);
            using (MemoryStream stream = new MemoryStream(arrCryptoText, 0, arrCryptoText.Length))
            {
                ICryptoTransform decryptor = mCrypto.CreateDecryptor();
                using (CryptoStream cStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                {
                    StreamReader reader = new StreamReader(cStream);
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion

        #region 其他方法

        /// <summary>
        /// 获得密钥数组
        /// </summary>
        /// <returns>密钥数组</returns>
        private byte[] GetLegalKey()
        {
            string result = key;
            mCrypto.GenerateKey();
            byte[] keyBytes = mCrypto.Key;
            int keyLength = keyBytes.Length;
            if (result.Length > keyLength)
                result = result.Substring(0, keyLength);
            else if (result.Length < keyLength)
                result = result.PadRight(keyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(result);
        }

        /// <summary>
        /// 获得初始向量IV数组
        /// </summary>
        /// <returns>初试向量IV数组</returns>
        private byte[] GetLegalIV()
        {
            string result = iv;
            mCrypto.GenerateIV();
            byte[] ivBytes = mCrypto.IV;
            int ivLength = ivBytes.Length;
            if (result.Length > ivLength)
                result = result.Substring(0, ivLength);
            else if (result.Length < ivLength)
                result = result.PadRight(ivLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(result);
        }

        #endregion
    }
}
