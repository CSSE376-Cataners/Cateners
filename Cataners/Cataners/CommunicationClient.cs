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
        private int attemptCount;
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
            attemptCount = 0;
            clientSocket.ReceiveTimeout = 3;
        }

        public void Start()
        {
            clientSocket.Connect(Properties.Settings.Default.ServerAddr, Variables.serverPort);
        }

        public void sendToServer(String msg)
        {
            if (instance.clientSocket.Connected)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(msg);
                byte[] end = new byte[bytes.Length + Translation.END_OF_MESSAGE.Length];
                Array.Copy(bytes, end, bytes.Length);
                Array.Copy(Translation.END_OF_MESSAGE, 0, end, bytes.Length, Translation.END_OF_MESSAGE.Length);
                clientSocket.GetStream().Write(end, 0, end.Length);
                this.attemptCount = 0;
            }
            else if (this.attemptCount < 3)
            {
                clientSocket.ConnectAsync(Properties.Settings.Default.ServerAddr, Variables.serverPort);
                instance.sendToServer(msg);
                this.attemptCount++;
            }
        }

        public async Task awaitingMessage(Translation.TYPE type)
        {
            //TODO: come back and finish type = null



        }
    }
}
