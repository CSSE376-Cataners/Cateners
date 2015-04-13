﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;


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
            this.attemptCount = 0;
            clientSocket.ReceiveTimeout = 3;
            
        }

        public async void Start()
        {
            try
            {
                await clientSocket.ConnectAsync(Properties.Settings.Default.ServerAddr, Variables.serverPort);
            }
            catch
            {
                Console.WriteLine("Ended");
            }
            this.Enabled = true;
            this.queueMessagesAsync();
            writer = new StreamWriter(clientSocket.GetStream(), Encoding.Unicode);
        }

        StreamWriter writer;
        public void sendToServer(String msg)
        {
            if (instance.clientSocket.Connected && this.attemptCount < 3)
            {
                writer.WriteLine(msg);
                writer.Flush();
                this.attemptCount = 0;
            }
            else if (this.attemptCount < 3)
            {
                Console.WriteLine("Here");
                Start();
                instance.sendToServer(msg);
                this.attemptCount++;
            }
        }

        public async void queueMessagesAsync()
        {
            StreamReader reader = new StreamReader(clientSocket.GetStream(), Encoding.Unicode);
            Console.WriteLine("Started Listening");
            while (Enabled && clientSocket.Connected)
            {
                string line;
                Task<String> task = reader.ReadLineAsync();
                try
                {
                    line = await task;
                    queue.Enqueue(line);
                    Console.WriteLine("Message:" + line);
                }
                catch(IOException)
                {
                    MessageBox.Show("You've been disconnected from the server", "Error - I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Console.WriteLine("bitches be crazy");
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

        /*
        public async Task<string> awaitingMessage(Translation.TYPE type)
        {
            //TODO: come back and finish type = null

            return "";
        }
         */
    }
}
