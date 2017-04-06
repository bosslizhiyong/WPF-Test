using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace ThinkNet.Utility
{
    /// <summary>
    /// Splasher类，可以显示、关闭闪动屏幕及设置动态信息
    /// </summary>
    public class Splasher
    {
        private static Form _frmSplash = null;
        private static ISplashUI _splashInterface = null;
        private static Thread _splashThread = null;
        private static string _tempInformation = string.Empty;


        /// <summary>
        /// 显示闪动窗口画面,该窗口要继承ISplashUI接口
        /// </summary>
        /// <param name="splashFormType">闪动窗口画面(继承ISplashUI接口)的类型</param>
        public static void Show(Type splashFormType)
        {
            if (_splashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("必须设置启动窗体"));
            }

            _splashThread = new Thread(
                new ThreadStart(delegate() 
                { 
                    CreateInstance(splashFormType);
                    Application.Run(_frmSplash);
                }
            ));
            _splashThread.IsBackground = true;
            _splashThread.SetApartmentState(ApartmentState.STA);
            _splashThread.Start();
        }

        

        /// <summary>
        /// 状态属性，设置动态信息
        /// </summary>
        public static string Information
        {
            set
            {
                if (_splashInterface == null || _frmSplash == null)
                {
                    _tempInformation = value;
                    return;
                }
                _frmSplash.Invoke(
                        new SplashInformationChangedHandle(delegate(string str) { _splashInterface.SetInformation(str); }),
                        new object[] { value }
                    );
            }

        }

        /// <summary>
        /// 关闭闪动窗口画面
        /// </summary>
        public static void Close()
        {
            if (_splashThread == null || _frmSplash == null) return;

            try
            {
                _frmSplash.Invoke(new MethodInvoker(_frmSplash.Close));
            }
            catch (Exception)
            {
            }
            _splashThread = null;
            _frmSplash = null;
        }
       
        /// <summary>
        /// 创建闪动窗口实例
        /// </summary>
        /// <param name="formType"></param>
        private static void CreateInstance(Type formType)
        {

            object obj = formType.InvokeMember(null,
                                BindingFlags.DeclaredOnly |
                                BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            _frmSplash = obj as Form;
            _splashInterface = obj as ISplashUI;
            if (_frmSplash == null)
            {
                throw (new Exception("启动窗体类型必须是System.Windows.Forms.Form的子类"));
            }
            if (_splashInterface == null)
            {
                throw (new Exception("启动窗体必须实现ISplashUI接口"));
            }

            if (!string.IsNullOrEmpty(_tempInformation))
                _splashInterface.SetInformation(_tempInformation);
        }


        private delegate void SplashInformationChangedHandle(string info);

    }
}
