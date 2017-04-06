using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace ThinkNet.Utility
{
    public static class LicenseHelper
    {
        /// <summary>
        /// 获取CUP序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCPU()
        {
            string cpu = "";
            ManagementClass mc = new ManagementClass("win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpu = mo["ProcessorId"].ToString();
            }
            return cpu;
        }
        /// <summary>
        /// 获取硬盘的卷标号 
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            string disk = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject mo = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            mo.Get();
            disk = mo.GetPropertyValue("VolumeSerialNumber").ToString();
            return disk;
        }
        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMAC()
        {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"])
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }

        /// <summary>
        /// 生成随机机器码
        /// </summary>
        /// <returns></returns>
        public static string GetRandomMachineNumberByMac8Disk()
        {
            string result = "";
            string temp = GetMAC() + GetDiskVolumeSerialNumber();
            string[] arrNumber = new string[24];
            Random r = new Random();
            for (int i = 0; i < 24; i++)
            {
                arrNumber[i] = temp.Substring(r.Next(0, temp.Length), 1);
                result += arrNumber[i];
            }
            return result;
        }
        /// <summary>
        /// 生成随机机器码
        /// </summary>
        /// <returns></returns>
        public static string GetRandomMachineNumberByCpu8Disk()
        {
            string result = "";
            string temp = GetCPU() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号 
            string[] arrNumber = new string[24];
            Random r = new Random();
            for (int i = 0; i < 24; i++)//把字符赋给数组 
            {
                arrNumber[i] = temp.Substring(r.Next(0, temp.Length), 1);//从数组随机抽取24个字符组成新的字符生成机器三 
                result += arrNumber[i];
            }
            return result;
        }
        /// <summary>
        /// 生成固定机器码
        /// </summary>
        /// <returns></returns>
        public static string GetFixedMachineNumberByMac8Disk()
        {
            string result = "";
            string temp = GetMAC() + GetDiskVolumeSerialNumber();
            result = temp.Substring(0, 24);    //截取前24位作为机器码
            return result;
        }
        /// <summary>
        /// 生成固定机器码
        /// </summary>
        /// <returns></returns>
        public static string GetFixedMachineNumberByCpu8Disk()
        {
            string result = "";
            string temp = GetCPU() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号 
            result = temp.Substring(0, 24);    //截取前24位作为机器码
            return result;
        }

        public static int[] arrCode = new int[126];
        public static int[] arrNumber = new int[24];
        public static char[] arrChar = new char[24];
        public static void SetIntCode() 
        {
            for (int i = 0; i < arrCode.Length; i++)
            {
                arrCode[i] = i % 9;
            }

            //Random r = new Random();
            //for (int i = 0; i < arrCode.Length; i++)
            //{
            //    arrCode[i] = r.Next(0, 9);
            //}
        }

        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <param name="machineNumber">机器码</param>
        /// <returns></returns>
        public static string GetRegisterNumber(string machineNumber)
        {
            string registerNumber = "";

            SetIntCode();

            for (int i = 0; i < 24; i++)
            {
                arrChar[i] = Convert.ToChar(machineNumber.Substring(i, 1));
            }
            for (int i = 0; i < 24; i++)
            {
                arrNumber[i] = arrCode[Convert.ToInt32(arrChar[i])] + Convert.ToInt32(arrChar[i]);
            }
            for (int i = 0; i < arrNumber.Length; i++)
            {
                if (arrNumber[i] >= 48 && arrNumber[i] <= 57)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else if (arrNumber[i] >= 65 && arrNumber[i] <= 90)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else if (arrNumber[i] >= 97 && arrNumber[i] <= 122)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else
                {
                    if (arrNumber[i] > 122)
                    {
                        registerNumber += (Char)(arrNumber[i] - 10);
                    }
                    else
                    {
                        registerNumber += (Char)(arrNumber[i] - 9);
                    }
                }
            }
            return registerNumber;
        }

        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <param name="machineNumber">机器码</param>
        /// <returns></returns>
        public static string GetRegisterNumber(string machineNumber,string splitChar)
        {
            string registerNumber = "";

            SetIntCode();

            for (int i = 0; i < 24; i++)
            {
                arrChar[i] = Convert.ToChar(machineNumber.Substring(i, 1));
            }
            for (int i = 0; i < 24; i++)
            {
                arrNumber[i] = arrCode[Convert.ToInt32(arrChar[i])] + Convert.ToInt32(arrChar[i]);
            }
            for (int i = 0; i < arrNumber.Length; i++)
            {
                if (arrNumber[i] >= 48 && arrNumber[i] <= 57)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else if (arrNumber[i] >= 65 && arrNumber[i] <= 90)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else if (arrNumber[i] >= 97 && arrNumber[i] <= 122)
                {
                    registerNumber += (Char)arrNumber[i];
                }
                else
                {
                    if (arrNumber[i] > 122)
                    {
                        registerNumber += (Char)(arrNumber[i] - 10);
                    }
                    else
                    {
                        registerNumber += (Char)(arrNumber[i] - 9);
                    }
                }
            }
            if (string.IsNullOrEmpty(splitChar))
            {
                return registerNumber;
            }
            else
            {
                return SplitByLen(registerNumber, 6, splitChar);
            }
        }

        private static string SplitByLen(string tempstr, int length, string splitChar)
        {
            int len = 0;
            if (tempstr.Length % length == 0)
            {
                len = tempstr.Length / length;
            }
            else
            {
                len = tempstr.Length / length + 1;
            }
            string result = "";
            int i = 0;
            for (; i < len - 1; i++)
            {
                if (result != "") result += splitChar;
                result += tempstr.Substring(length * i, length);
            }
            if (result != "") result += splitChar;
            result += tempstr.Substring(length * i);
            return result;
        }
    }
}
