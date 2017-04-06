using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Socket调用的结果
    /// </summary>
    [Serializable]
    public class CallResult
    {
        private int _resultCode;
        private string _resultCodeEx;
        private string _resultMessage;
        private object _resultData;

        /// <summary>
        /// 
        /// </summary>
        public int ResultCode
        {
            get{ return _resultCode; }
            set {_resultCode  = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        public string ResultCodeEx
        {
            get { return _resultCodeEx; }
            set { _resultCodeEx = value; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string ResultMessage
        {
            get { return _resultMessage; }
            set { _resultMessage = value; }
        }

        /// <summary>
        /// 比如输出参数 多个可以采用数组或字典形式
        /// </summary>
        public object ResultData
        {
            get { return _resultData; }
            set { _resultData = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CallResult()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeEx"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public CallResult(int code, string codeEx, string message, object data)
        {
            this._resultCode = code;
            this._resultCodeEx = codeEx;
            this._resultMessage = message;
            this._resultData = data;
        }

        /// <summary>
        /// 设置信息提示
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeEx"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void SetCallResult(int code, string codeEx, string message,object data)
        {
            this._resultCode = code;
            this._resultCodeEx = codeEx;
            this._resultMessage = message;
            this._resultData = data;
        }
    }
}
