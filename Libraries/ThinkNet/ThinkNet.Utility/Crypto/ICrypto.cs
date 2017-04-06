using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    /*  
     *  一、散列:散列是一种单向算法，一旦数据被转换，将无法再获得其原始值。SHA1 和 MD5.
     *  MD5 使用的加密密钥比 SHA1 使用的密钥大，因此 MD5 散列较难破解。
     *  SHA1 从实践或理论上来讲没有发生冲突的可能性。MD5 从理论上讲有发生冲突的可能性。
     *  
     *  二、对称算法（或密钥算法）--DES、RC2、AES（Rijndael）、TripleDES 
     * 
     *  三、不对称算法（或公钥算法）
     *  没有对称算法快，但其代码较难破密。这些算法取决于两个密钥，一个是私钥，另一个是公钥。公钥用来加密消息，
     *  私钥是可以解密该消息的唯一密钥。公钥和私钥通过数学方法链接在一起，因此要成功进行加密交换，必须获得这两个密钥。
     *  
     * 
     */

    /// <summary>
    /// 加密类型
    /// </summary>
    [Flags]
    public enum CrytoType
    {
        /// <summary>
        /// MD5
        /// </summary>
        [Description("MD5")]
        MD5 = 1,
        /// <summary>
        /// TripleDES
        /// </summary>
        [Description("TripleDES")]
        TripleDES = 2,
        /// <summary>
        /// SHA1
        /// </summary>
        [Description("SHA1")]
        SHA1 = 3,
        /// <summary>
        /// SHA256
        /// </summary>
        [Description("SHA256")]
        SHA256 = 4,
        /// <summary>
        /// AES（Rijndael）
        /// </summary>
        [Description("AES（Rijndael）")]
        AES = 5,
        /// <summary>
        /// DES
        /// </summary>
        [Description("DES")]
        DES = 6,
        /// <summary>
        /// RC2
        /// </summary>
        [Description("RC2")]
        RC2 = 7
    }

    /// <summary>
    /// 加密接口
    /// </summary>
    public interface ICrypto
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns></returns>
        string Encrypt(string plainText);
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns></returns>
        string Encrypt(string plainText, string encryptKey);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cryptoText">密文</param>
        /// <returns></returns>
        string Decrypt(string cryptoText);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cryptoText">密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        string Decrypt(string cryptoText,string decryptKey);
    }
}
