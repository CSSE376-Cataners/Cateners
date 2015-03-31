using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cataners
{
    public class CommunicationClient
    {
        public System.Net.Sockets.TcpClient clientSocket;

        public CommunicationClient()
        {
            this.clientSocket = new System.Net.Sockets.TcpClient();
        }

        public void sendToServer(String msg)
        {

        }
    }
}
