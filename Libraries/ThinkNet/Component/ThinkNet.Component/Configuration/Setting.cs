using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public class Setting
    {
        public int RetryActionDefaultPeriod { get; set; }
        public TCPConfiguration TcpConfiguration { get; set; }

        public Setting()
        {
            RetryActionDefaultPeriod = 1000;
            TcpConfiguration = new TCPConfiguration();
        }
    }
}
