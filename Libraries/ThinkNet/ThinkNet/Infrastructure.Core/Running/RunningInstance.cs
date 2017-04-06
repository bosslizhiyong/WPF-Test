using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 运行控制
    /// </summary>
    public class RunningInstance
    {

        #region API实现
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 获取应用程序进程实例,如果没有匹配进程，返回Null
        /// </summary>
        /// <returns>返回当前Process实例</returns>
        public static Process GetRunningInstance()
        {
            //获取当前进程
            Process current = Process.GetCurrentProcess();
            //获取进程名为ProcessName的Process数组。
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //获取当前运行程序完全限定名
            string currentFileName = current.MainModule.FileName;
            
            //遍历有相同进程名称正在运行的进程
            foreach (Process process in processes)
            {
                if (process.MainModule.FileName == currentFileName)
                {
                    if (process.Id != current.Id)//根据进程ID排除当前进程
                    {
                        return process;//返回已运行的进程实例
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 是否运行实例
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningInstance()
        {
            Process instance = GetRunningInstance();
            if (instance != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 显示已运行的例程
        /// </summary>
        public static bool ShowRunningInstance()
        {
            Process instance = GetRunningInstance();

            //确保窗口没有被最小化或最大化 
            ShowWindowAsync(instance.MainWindowHandle, 1);
            //设置真实例程为foreground window
            return SetForegroundWindow(instance.MainWindowHandle);
        }
        #endregion

        #region Mutex实现
        //声明同步基元
        private static Mutex mutex = null; 

        /// <summary>
        /// 创建应用程序进程Mutex
        /// </summary>
        /// <returns>返回创建结果，true表示创建成功，false创建失败。</returns>
        public static bool CreateMutex()
        {
            return CreateMutex(System.Reflection.Assembly.GetEntryAssembly().FullName);
        }

        /// <summary>
        /// 创建应用程序进程Mutex
        /// </summary>
        /// <param name="name">Mutex名称</param>
        /// <returns>返回创建结果，true表示创建成功，false创建失败。</returns>
        public static bool CreateMutex(string name)
        {
            bool result = false;
            mutex = new Mutex(true, name, out result);
            return result;
        }

        /// <summary>
        /// 释放Mutex
        /// </summary>
        public static void ReleaseMutex()
        {
            if (mutex != null)
            {
                mutex.Close();
            }
        }
        #endregion 

        #region 设置标志实现
        //标志文件名称
        private static string runFlagFullname = null;

        /// <summary>
        /// 初始化程序运行标志，如果设置成功，返回true，已经设置返回false，设置失败将抛出异常
        /// </summary>
        /// <returns>返回设置结果</returns>
        public static bool InitRunFlag()
        {
            if (File.Exists(RunFlag))
            {
                return false;
            }
            using (FileStream fs = new FileStream(RunFlag, FileMode.Create))
            {
            }
            return true;
        }

        /// <summary>
        /// 释放初始化程序运行标志，如果释放失败将抛出异常
        /// </summary>
        public static void DisposeRunFlag()
        {
            if (File.Exists(RunFlag))
            {
                File.Delete(RunFlag);
            }
        }

        /// <summary>
        /// 获取或设置程序运行标志，必须符合Windows文件命名规范
        /// 这里实现生成临时文件为依据，如果修改成设置注册表，那就不需要符合文件命名规范。
        /// </summary>
        public static string RunFlag
        {

            get
            {
                if (runFlagFullname == null)
                {
                    string assemblyFullName = System.Reflection.Assembly.GetEntryAssembly().FullName;
                    //CommonApplicationData：//"C:\\Documents and Settings\\All Users\\Application Data"
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                    //"C:\\Program Files\\Common Files"
                    //string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                    runFlagFullname = Path.Combine(path, assemblyFullName);
                }
                return runFlagFullname;
            }
            set
            {
                runFlagFullname = value;
            }
        }
        #endregion
    }
}
