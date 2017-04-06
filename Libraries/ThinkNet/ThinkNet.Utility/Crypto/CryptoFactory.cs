using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 加密工厂类
    /// </summary>
    public class CryptoFactory
    {
        public static ICrypto Create(CrytoType mCrytoType)
        {
            ICrypto obj = null;
            switch (mCrytoType)
            {
                case CrytoType.MD5:
                    obj = new MD5();
                    break;
                case CrytoType.TripleDES:
                    obj = new TripleDES();
                    break;
                case CrytoType.SHA1:
                    obj = new SHA1();
                    break;
                case CrytoType.SHA256:
                    obj = new SHA256();
                    break;
                case CrytoType.AES:
                    obj = new AES();
                    break;
                case CrytoType.DES:
                    obj = new DES();
                    break;
                case CrytoType.RC2:
                    obj = new RC2();
                    break;
            }
            return obj;
        }
    }
}
