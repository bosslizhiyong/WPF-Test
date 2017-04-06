using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.IO;
using System.Text;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Infrastructure.Core.Message;

namespace SendMail
{
    public class MailSender
    {
        #region �ֶ�
        //SMTP����������
        private string server = "";

        //SMTP�������˿� [Ĭ��Ϊ25]
        private int port = 25;

        //�û��� [�����Ҫ�����֤�Ļ�]
        private string userName = "";

        //���� [�����Ҫ�����֤�Ļ�]
        private string password = "";

        //�����˵�ַ
        private string from = "";

        //�ռ��˵�ַ
        private string to = "";

        //����������
        private string fromName = "";

        //�ռ�������
        private string toName = "";

        //�ʼ�������
        private string subject = "";

        //�ʼ�����
        private string body = "";

        //���ı���ʽ���ʼ�����
        private string htmlBody = "";

        // �Ƿ���html��ʽ���ʼ�
        private bool isHtml = false;

        //���Ա��� [Ĭ��ΪGB2312]
        private string languageEncoding = "GB2312";

        //�ʼ����� [Ĭ��Ϊ8bit]
        private string encoding = "8bit";

        //�ʼ����ȼ� [Ĭ��Ϊ3]
        private int priority = 3;

        //���� [AttachmentInfo]
        private ArrayList attachments = new ArrayList();
	    #endregion

        #region ����
        
        /// <summary>
        /// SMTP����������
        /// </summary>
        public string Server
        {
            get { return server; }
            set { if (value != server) server = value; }
        } 
      

        /// <summary>
        /// SMTP�������˿� [Ĭ��Ϊ25]
        /// </summary>
        public int Port
        {
            get { return port; }
            set { if (value != port) port = value; }
        }
 

        /// <summary>
        /// �û��� [�����Ҫ�����֤�Ļ�]
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { if (value != userName) userName = value; }
        } 

        /// <summary>
        /// ���� [�����Ҫ�����֤�Ļ�]
        /// </summary>
        public string Password
        {
            get { return password; }
            set { if (value != password) password = value; }
        } 

        /// <summary>
        /// �����˵�ַ
        /// </summary>
        public string From
        {
            get { return from; }
            set { if (value != from) from = value; }
        } 

        /// <summary>
        /// �ռ��˵�ַ
        /// </summary>
        public string To
        {
            get { return to; }
            set { if (value != to) to = value; }
        } 

        /// <summary>
        /// ����������
        /// </summary>
        public string FromName
        {
            get { return fromName; }
            set { if (value != fromName) fromName = value; }
        } 

        /// <summary>
        /// �ռ�������
        /// </summary>
        public string ToName
        {
            get { return toName; }
            set { if (value != toName) toName = value; }
        } 
       

        /// <summary>
        /// �ʼ�������
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set { if (value != subject) subject = value; }
        } 
        

        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Body
        {
            get { return body; }
            set { if (value != body) body = value; }
        } 
      

        /// <summary>
        /// ���ı���ʽ���ʼ�����
        /// </summary>
        public string HtmlBody
        {
            get { return htmlBody; }
            set { if (value != htmlBody) htmlBody = value; }
        } 
     

        /// <summary>
        /// �Ƿ���html��ʽ���ʼ�
        /// </summary>
        public bool IsHtml
        {
            get { return isHtml; }
            set { if (value != isHtml) isHtml = value; }
        } 
        

        /// <summary>
        /// ���Ա��� [Ĭ��ΪGB2312]
        /// </summary>
        public string LanguageEncoding
        {
            get { return languageEncoding; }
            set { if (value != languageEncoding) languageEncoding = value; }
        } 
     

        /// <summary>
        /// �ʼ����� [Ĭ��Ϊ8bit]
        /// </summary>
        public string MailEncoding
        {
            get { return encoding; }
            set { if (value != encoding) encoding = value; }
        } 
     

        /// <summary>
        /// �ʼ����ȼ� [Ĭ��Ϊ3]
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set { if (value != priority) priority = value; }
        } 
     

        /// <summary>
        /// ���� [AttachmentInfo]
        /// </summary>
        public IList Attachments
        {
            get { return attachments; }
        }

        #endregion

        #region ��������
        /// <summary>
        /// �����ʼ�
        /// </summary>
        public void Send()
        {
            try
            {
                // ����TcpClient���� ����������
                TcpClient tcp = null;
                try
                {
                    tcp = new TcpClient(server, port);

                }
                catch (Exception)
                {
                    throw new Exception("�޷����ӷ�����");
                }

                ReadString(tcp.GetStream());//��ȡ������Ϣ

                // ��ʼ���з�������֤
                // ���״̬����250���ʾ�����ɹ�
                if (!Command(tcp.GetStream(), "EHLO Localhost", "250"))
                {
                    throw new Exception("��½�׶�ʧ��");
                }

                /*������Ŀ������Ҫ���������֤*/
                if (userName != "")
                {
                    // ��Ҫ�����֤                    
                    if (!Command(tcp.GetStream(), "AUTH LOGIN", "334"))
                    {
                        throw new Exception("�����֤�׶�ʧ��");
                    }
                    // �˴���usernameת��ΪBase64��
                    string nameB64 = ToBase64(userName);
                    if (!Command(tcp.GetStream(), nameB64, "334"))
                    {
                        throw new Exception("�����֤�׶�ʧ��");
                    }
                    // �˴���passwordת��ΪBase64��
                    string passB64 = ToBase64(password);
                    if (!Command(tcp.GetStream(), passB64, "235"))
                    {
                        throw new Exception("�����֤�׶�ʧ��");
                    }
                }


                // ׼������
                WriteString(tcp.GetStream(), "mail From: " + from);
                WriteString(tcp.GetStream(), "rcpt to: " + to);
                WriteString(tcp.GetStream(), "data");

                // �����ʼ�ͷ
                System.Threading.Thread.Sleep(2000);
                WriteString(tcp.GetStream(), "Date: " + DateTime.Now.ToLocalTime().ToString("r")); // ʱ��
                WriteString(tcp.GetStream(), "From: " + fromName + "<" + from + ">"); // ������
                WriteString(tcp.GetStream(), "Subject: " + subject); // ����
                WriteString(tcp.GetStream(), "To:" + toName + "<" + to + ">"); // �ռ���

                //�ʼ���ʽ 
                WriteString(tcp.GetStream(), "Content-Type: multipart/mixed; boundary=\"unique-boundary-1\"");
                WriteString(tcp.GetStream(), "Reply-To:" + from); // �ظ���ַ
                WriteString(tcp.GetStream(), "X-Priority:" + priority); // ���ȼ�
                WriteString(tcp.GetStream(), "MIME-Version:1.0"); // MIME�汾

                // ����ID,����
                WriteString(tcp.GetStream(), "Content-Transfer-Encoding:" + encoding); // ���ݱ���
                WriteString(tcp.GetStream(), "X-Mailer:JcPersonal.Utility.MailSender"); // �ʼ�������
                WriteString(tcp.GetStream(), "");

                WriteString(tcp.GetStream(), ToBase64("This is a multi-part message in MIME format."));
                WriteString(tcp.GetStream(), "");

                // �Ӵ˴���ʼ���зָ�����
                WriteString(tcp.GetStream(), "--unique-boundary-1");

                // �ڴ˴�����ڶ����ָ���
                WriteString(tcp.GetStream(), "Content-Type: multipart/alternative;Boundary=\"unique-boundary-2\"");
                WriteString(tcp.GetStream(), "");

                if (!isHtml)
                {
                    // �ı���Ϣ
                    WriteString(tcp.GetStream(), "--unique-boundary-2");
                    WriteString(tcp.GetStream(), "Content-Type: text/plain;charset=" + languageEncoding);
                    WriteString(tcp.GetStream(), "Content-Transfer-Encoding:" + encoding);
                    WriteString(tcp.GetStream(), "");
                    WriteString(tcp.GetStream(), body);
                    WriteString(tcp.GetStream(), "");//һ������д��֮���д�����Ϣ���ֶ�
                    WriteString(tcp.GetStream(), "--unique-boundary-2--");//�ָ����Ľ������ţ�β�ͺ������--
                    WriteString(tcp.GetStream(), "");
                }
                else
                {
                    //html��Ϣ
                    WriteString(tcp.GetStream(), "--unique-boundary-2");
                    WriteString(tcp.GetStream(), "Content-Type: text/html;charset=" + languageEncoding);
                    WriteString(tcp.GetStream(), "Content-Transfer-Encoding:" + encoding);
                    WriteString(tcp.GetStream(), "");
                    WriteString(tcp.GetStream(), htmlBody);
                    WriteString(tcp.GetStream(), "");
                    WriteString(tcp.GetStream(), "--unique-boundary-2--");//�ָ����Ľ������ţ�β�ͺ������-- 
                    WriteString(tcp.GetStream(), "");
                }

                // ���͸���
                // ���ļ��б���ѭ��
                for (int i = 0; i < attachments.Count; i++)
                {
                    WriteString(tcp.GetStream(), "--unique-boundary-1"); // �ʼ����ݷָ���
                    WriteString(tcp.GetStream(), "Content-Type: application/octet-stream;name=\"" + ((AttachmentInfo)attachments[i]).FileName + "\""); // �ļ���ʽ
                    WriteString(tcp.GetStream(), "Content-Transfer-Encoding: base64"); // ���ݵı���
                    WriteString(tcp.GetStream(), "Content-Disposition:attachment;filename=\"" + ((AttachmentInfo)attachments[i]).FileName + "\""); // �ļ���
                    WriteString(tcp.GetStream(), "");
                    WriteString(tcp.GetStream(), ((AttachmentInfo)attachments[i]).Bytes); // д���ļ�������
                    WriteString(tcp.GetStream(), "");
                }

                Command(tcp.GetStream(), ".", "250"); // ���д���ˣ�����"."

                // �ر�����
                tcp.Close();            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
        #endregion

        #region ��������
        /// <summary>
        /// ������д���ַ�
        /// </summary>
        /// <param name="netStream">����TcpClient����</param>
        /// <param name="str">д����ַ�</param>
        protected void WriteString(NetworkStream netStream, string str)
        {
            str = str + "\r\n"; // ���뻻�з�

            // ��������ת��Ϊbyte[]
            byte[] bWrite = Encoding.GetEncoding(languageEncoding).GetBytes(str.ToCharArray());

            // ����ÿ��д������ݴ�С�������Ƶģ���ô���ǽ�ÿ��д������ݳ��ȶ��ڣ������ֽڣ�һ������ȳ����ˣ������ͷֲ�д�롣
            int start = 0;
            int length = bWrite.Length;
            int page = 0;
            int size = 75;
            int count = size;
            try
            {
                if (length > 75)
                {
                    // ���ݷ�ҳ
                    if ((length / size) * size < length)
                        page = length / size + 1;
                    else
                        page = length / size;
                    for (int i = 0; i < page; i++)
                    {
                        start = i * size;
                        if (i == page - 1)
                            count = length - (i * size);
                        netStream.Write(bWrite, start, count);// ������д�뵽��������
                    }
                }
                else
                    netStream.Write(bWrite, 0, bWrite.Length);
            }
            catch (Exception)
            {
                // ���Դ���
            }
        }

        /// <summary>
        /// �����ж�ȡ�ַ�
        /// </summary>
        /// <param name="netStream">����TcpClient����</param>
        /// <returns>��ȡ���ַ�</returns>
        protected string ReadString(NetworkStream netStream)
        {
            string sp = null;
            byte[] by = new byte[1024];
            int size = netStream.Read(by, 0, by.Length);// ��ȡ������
            if (size > 0)
            {
                sp = Encoding.Default.GetString(by);// ת��ΪString
            }
            return sp;
        }

        /// <summary>
        /// ��������жϷ�����Ϣ�Ƿ���ȷ
        /// </summary>
        /// <param name="netStream">����TcpClient����</param>
        /// <param name="command">����</param>
        /// <param name="state">��ȷ��״̬��</param>
        /// <returns>�Ƿ���ȷ</returns>
        protected bool Command(NetworkStream netStream, string command, string state)
        {
            string sp = null;
            bool success = false;
            try
            {
                WriteString(netStream, command);// д������
                sp = ReadString(netStream);// ���ܷ�����Ϣ
                if (sp.IndexOf(state) != -1)// �ж�״̬���Ƿ���ȷ
                    success = true;
            }
            catch (Exception)
            {
                // ���Դ���
            }
            return success;
        }

        /// <summary>
        /// �ַ�������ΪBase64
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>Base64������ַ���</returns>
        protected string ToBase64(string str)
        {
            try
            {
                byte[] by = Encoding.Default.GetBytes(str.ToCharArray());
                str = Convert.ToBase64String(by);
            }
            catch (Exception)
            {
                // ���Դ���
            }
            return str;
        } 
        #endregion
    }
}
