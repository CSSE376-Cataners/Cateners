using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.Sockets;


namespace Cataners
{
    public class CommunicationClient
    {
        private ArrayList tempQueue;
        private int attemptCount;
        private Boolean Enabled;
        public ConcurrentQueue<String> queue;
        private static CommunicationClient instance;

        public static CommunicationClient Instance {
            get{
                return instance;  
            }
        }

        public System.Net.Sockets.TcpClient clientSocket;

        public CommunicationClient()
        {
            this.Enabled = false;
            this.clientSocket = new System.Net.Sockets.TcpClient();
            CommunicationClient.instance = this;
            queue = new ConcurrentQueue<string>();
            tempQueue = new ArrayList();
            attemptCount = 0;
            clientSocket.ReceiveTimeout = 3;
        }

        public async Task Start()
        {
            
            await clientSocket.ConnectAsync(Properties.Settings.Default.ServerAddr, Variables.serverPort);
            this.queueMessagesAsync();
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
                Start().Wait();
                instance.sendToServer(msg);
                this.attemptCount++;
            }
        }

        public async Task queueMessagesAsync()
        {
            while (Enabled && clientSocket.Connected)
            {
            Start:

                NetworkStream serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[1000];

                await serverStream.ReadAsync(inStream, 0, inStream.Length);


                foreach (byte b in inStream)
                {
                    tempQueue.Add(b);
                    int lengthOfEOM = Translation.END_OF_MESSAGE.Length;
                    if (b == Translation.END_OF_MESSAGE[Translation.END_OF_MESSAGE.Length - 1] && tempQueue.Count >= lengthOfEOM)
                    {

                        for (int i = 0; i < lengthOfEOM; i++)
                        {
                            int index = tempQueue.Count + i - lengthOfEOM;
                            byte x = (byte)(tempQueue[index]);
                            byte y = Translation.END_OF_MESSAGE[i];
                            if (x != y)
                            {
                                goto Start;
                            }
                        }
                        moveFromTempToQueue();
                    }
                }
            }
            Enabled = false;
        }

        private void moveFromTempToQueue()
        {
            byte[] temp = (byte[])tempQueue.ToArray(typeof(byte));
            string returndata = System.Text.Encoding.Unicode.GetString(temp);
            returndata = returndata.Substring(0, returndata.Length - 4);
            queue.Enqueue(returndata);
            Console.WriteLine("Message: " + returndata);
            tempQueue.Clear();
        }

        public async Task awaitingMessage(Translation.TYPE type)
        {
            //TODO: come back and finish type = null



        }
    }
}
