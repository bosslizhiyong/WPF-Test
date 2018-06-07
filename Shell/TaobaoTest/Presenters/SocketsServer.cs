namespace TaobaoTest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class SocketsServer
    {
        public string ipString = "127.0.0.1";   // 服务器端ip
        public int port = 37280;                // 服务器端口

        public Socket socket;
        public Print print;                     // 运行时的信息输出方法

        public Dictionary<string, Socket> clients = new Dictionary<string, Socket>();   // 存储连接到服务器的客户端信息
        public bool started = false;            // 标识当前是否启动了服务

        public SocketsServer(Print print = null, string ipString = null, int port = -1)
        {
            this.print = print;
            if (ipString != null) this.ipString = ipString;
            if (port >= 0) this.port = port;
        }

        //public SocketsServer(Print print = null, string ipString = null, int port = -1)
        //{
        //    this.print = print;
        //    if (ipString != null) this.ipString = ipString;
        //    if (port >= 0) this.port = port;
        //    if (frm != null) fromMain = frm;

        //}

        public SocketsServer(Print print = null, string ipString = null, int port = -1, FrmTaoBao frm = null)
        {
            this.print = print;
            if (ipString != null) this.ipString = ipString;
            if (port >= 0) this.port = port;
            //if (frm != null) fromMain = frm;
        }



        public SocketsServer(Print print = null, string ipString = null, string port = "-1")
        {
            this.print = print;
            if (ipString != null) this.ipString = ipString;

            int port_int = Int32.Parse(port);
            if (port_int >= 0) this.port = port_int;
        }

        /// <summary>
        /// Print用于输出Server的输出信息
        /// </summary>
        public delegate void Print(string info);

        /// <summary>
        /// 启动服务
        /// </summary>
        public void start()
        {
            try
            {
                IPAddress address = IPAddress.Parse(ipString);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(address, port));
                socket.Listen(10000);

                if (print != null)
                {
                    try
                    {
                        Console.WriteLine("启动服务【" + socket.LocalEndPoint.ToString() + "】成功");
                        Console.WriteLine("启动API监听服务成功");
                    }
                    catch { print = null; }
                }
                started = true;

                new Thread(listenClientConnect).Start(socket);  // 在新的线程中监听客户端连接
            }
            catch (Exception exception)
            {
                if (print != null)
                {
                    print("启动服务失败 " + exception.ToString());
                    Console.WriteLine("启动服务失败");
                }
                started = false;
            }
        }

        /// <summary>
        /// 监听客户端的连接
        /// </summary>
        private void listenClientConnect(object obj)
        {
            Socket socket = (Socket)obj;
            while (true)
            {
                Socket clientScoket = socket.Accept();
                if (print != null)
                {
                    print("客户端" + clientScoket.RemoteEndPoint.ToString() + "已连接");
                    Console.WriteLine("客户端" + clientScoket.RemoteEndPoint.ToString() + "已连接");
                }
                new Thread(receiveData).Start(clientScoket);   // 在新的线程中接收客户端信息

                Thread.Sleep(1000);                            // 延时1秒后，接收连接请求
                if (!started) return;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        public void Send(string info, string id)
        {
            if (clients.ContainsKey(id))
            {
                Socket socket = clients[id];

                try
                {
                    Send(socket, info);
                }
                catch (Exception ex)
                {
                    clients.Remove(id);
                    if (print != null) Console.WriteLine("客户端已断开，【" + id + "】");
                }
            }
        }

        /// <summary>
        /// 通过socket发送数据data
        /// </summary>
        private void Send(Socket socket, string data)
        {
            if (socket != null && data != null && !data.Equals(""))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);   // 将data转化为byte数组
                socket.Send(bytes);                            // 
            }
        }

        private string clientIp = "";
        /// <summary>
        /// 输出Server的输出信息到客户端
        /// </summary>
        public void PrintOnClient(string info)
        {
            Send(info, clientIp);
        }

        /// <summary>
        /// 接收通过socket发送过来的数据
        /// </summary>
        private void receiveData(object obj)
        {
            Socket socket = (Socket)obj;

            string clientIp = socket.RemoteEndPoint.ToString();                 // 获取客户端标识 ip和端口
            if (!clients.ContainsKey(clientIp)) clients.Add(clientIp, socket);  // 将连接的客户端socket添加到clients中保存
            else clients[clientIp] = socket;
            while (true)
            {
                try
                {
                    string str = Receive(socket);
                    if (!str.Equals(""))
                    {
                        if (str.Equals("打开"))
                        {
                            //ERPOpen();
                        }
                        else if (str.Equals("关闭"))
                        {
                            //ERPClose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (print != null) print("连接已自动断开，【" + clientIp + "】" + exception.Message);
                    Console.WriteLine("连接已自动断开，【" + clientIp + "】" + exception.Message);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                    if (clients.ContainsKey(clientIp)) clients.Remove(clientIp);
                    return;
                }

                if (!started) return;
                Thread.Sleep(200);      // 延时0.2秒后再接收客户端发送的消息
            }
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        private void runCMD(string cmd)
        {
            new Thread(runCMD_0).Start(cmd);
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        private void runCMD_0(object obj)
        {
            //string cmd = (string)obj;
            //string START = "[.RunCmd]";
            //if (cmd.StartsWith(START))
            //{
            //    cmd = cmd.Substring(START.Length);  // 获取cmd信息
            //    Cmd.Run(cmd, print);                // 执行cmd，输出执行结果到print
            //}
        }

        /// <summary>
        /// 从socket接收数据
        /// </summary>
        private string Receive(Socket socket)
        {
            string data = "";

            byte[] bytes = null;
            int len = socket.Available;
            if (len > 0)
            {
                bytes = new byte[len];
                int receiveNumber = socket.Receive(bytes);
                data = Encoding.UTF8.GetString(bytes, 0, receiveNumber);
            }

            return data;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void stop()
        {
            started = false;
        }

        //public bool ERPClose()
        //{
        //    try
        //    {
        //        if (fromMain != null) fromMain.CloseERP();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("关闭失败" + ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //public bool ERPOpen()
        //{
        //    try
        //    {
        //        if (fromMain != null) fromMain.OpenERP();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("打开失败" + ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //protected void SendCacheKey(string cacheKey)
        //{
        //    Dictionary<string, string> dic = GetCodeFunction(cacheKey);
        //    if (dic == null)
        //    {
        //        return;
        //    }
        //    foreach (ProgramInfo item in ListenServiceUtility.ProgramInfos)
        //    {
        //        if (!dic.ContainsKey(item.EnterpriseCode))
        //        {
        //            continue;
        //        }

        //        if (dic[item.EnterpriseCode].ToString() != "")
        //        {
        //            item.MListenClient.SendMessage("Cache_" + dic[item.EnterpriseCode].ToString());
        //        }
        //    }
        //}

        //protected Dictionary<string, string> GetCodeFunction(string cacheKey)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    try
        //    {
        //        string[] s = cacheKey.Split('_');
        //        if (s.Length != 0)
        //        {
        //            if (string.IsNullOrEmpty(s[0]))
        //            {
        //                return null;
        //            }
        //            if (string.IsNullOrEmpty(s[1]))
        //            {
        //                return null;
        //            }
        //            dic.Add(s[0], s[1]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return dic;
        //}
    }
}
