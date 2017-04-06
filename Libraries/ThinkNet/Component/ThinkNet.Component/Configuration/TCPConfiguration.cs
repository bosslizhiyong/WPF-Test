using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public class TCPConfiguration
    {
        public int MaxSendPacketSize;
        public int SocketCloseTimeoutMs;
        public int AcceptBacklogCount;
        public int ConcurrentAccepts;
        public int AcceptPoolSize;
        public int ConnectPoolSize;
        public int SendReceivePoolSize;
        public int BufferChunksCount;
        public int SocketBufferSize;

        public TCPConfiguration()
        {
            MaxSendPacketSize = 64 * 1024;
            SocketCloseTimeoutMs = 500;
            AcceptBacklogCount = 1000;
            ConcurrentAccepts = 1;
            AcceptPoolSize = ConcurrentAccepts * 2;
            ConnectPoolSize = 32;
            SendReceivePoolSize = 512;
            BufferChunksCount = 512;
            SocketBufferSize = 8 * 1024;
        }
    }
}
