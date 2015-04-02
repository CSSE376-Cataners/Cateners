using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace Cataners
{
    public class CommunicationClient
    {
        private static CommunicationClient instance;

        public static CommunicationClient Instance {
            get{
                return instance;   
            }
        }

        public System.Net.Sockets.TcpClient clientSocket;

        public CommunicationClient()
        {
            this.clientSocket = new System.Net.Sockets.TcpClient();
            CommunicationClient.instance = this;
        }

        public void Start()
        {
            clientSocket.Connect(Properties.Settings.Default.ServerAddr, Variables.serverPort);
        }

        public void sendToServer(String msg)
        {
            if (instance.clientSocket.Connected)
            {
                byte[] bytes = new byte[msg.Length * sizeof(char)];
                System.Buffer.BlockCopy(msg.ToCharArray(), 0, bytes, 0, bytes.Length);
                clientSocket.GetStream().Write(bytes, 0, bytes.Length);
            }
            else
            {
                instance.Start();
                instance.sendToServer(msg);
            }
        }
    }
}
