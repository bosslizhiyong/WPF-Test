using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    /// <summary>
    /// MD5加密算法
    /// </summary>
    public class MD5:ICrypto
    {
        #region 字    段

        private System.Security.Cryptography.MD5 mCrypto = null;

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5()
        {
            mCrypto = System.Security.Cryptography.MD5.Create();
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
        /// <param name="encryptKey">加密密钥(加密密钥为null)</param>
        /// <returns></returns>
        public string Encrypt(string plainText, string encryptKey)
        {
            //将明文转为字节数组
            byte[] arrPlainText = Encoding.UTF8.GetBytes(plainText);
            //计算字节数组的哈希值
            byte[] arrHash = mCrypto.ComputeHash(arrPlainText);
            mCrypto.Clear();
            //返回加密结果
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < arrHash.Length; i++)
                result.Append(arrHash[i].ToString("x2"));
            return result.ToString();
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
        /// <param name="decryptKey">解密密钥(解密密钥为null)</param>
        /// <returns></returns>
        public string Decrypt(string cryptoText, string decryptKey)
        {
            return cryptoText;
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
