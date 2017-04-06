using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace ThinkNet.Utility
{
    /// <summary>
    /// Splasher�࣬������ʾ���ر�������Ļ�����ö�̬��Ϣ
    /// </summary>
    public class Splasher
    {
        private static Form _frmSplash = null;
        private static ISplashUI _splashInterface = null;
        private static Thread _splashThread = null;
        private static string _tempInformation = string.Empty;


        /// <summary>
        /// ��ʾ�������ڻ���,�ô���Ҫ�̳�ISplashUI�ӿ�
        /// </summary>
        /// <param name="splashFormType">�������ڻ���(�̳�ISplashUI�ӿ�)������</param>
        public static void Show(Type splashFormType)
        {
            if (_splashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("����������������"));
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
        /// ״̬���ԣ����ö�̬��Ϣ
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
        /// �ر��������ڻ���
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
        /// ������������ʵ��
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
                throw (new Exception("�����������ͱ�����System.Windows.Forms.Form������"));
            }
            if (_splashInterface == null)
            {
                throw (new Exception("�����������ʵ��ISplashUI�ӿ�"));
            }

            if (!string.IsNullOrEmpty(_tempInformation))
                _splashInterface.SetInformation(_tempInformation);
        }


        private delegate void SplashInformationChangedHandle(string info);

    }
}
