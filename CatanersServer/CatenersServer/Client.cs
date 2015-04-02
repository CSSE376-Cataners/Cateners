﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Concurrent;
using CatanersShared;
using System.Collections;
using System.IO;

namespace CatenersServer
{
    public class Client
    {
        public TcpClient socket;

        public ConcurrentQueue<String> queue;
        public bool Enabled;


        public Client(TcpClient tcp)
        {
            this.socket = tcp;
            queue = new ConcurrentQueue<string>();
            tempQueue = new ArrayList();
            Enabled = true;
        }


        private ArrayList tempQueue;

        public async void queueMessagesAsync()
        {
            StreamReader reader = new StreamReader(socket.GetStream(), Encoding.Unicode)
            while(Enabled && socket.Connected) {

                NetworkStream serverStream = socket.GetStream();
                byte[] inStream = new byte[1000];
                string line;
                Task<String> task = reader.ReadLineAsync();
                line = await  task; 
                queue.Enqueue(line);
                sendToClient(line);
                Console.WriteLine("Message:" + line);
                 
            }

            Console.WriteLine("Clint Closed");
        }

        private void moveFromTempToQueue()
        {
            byte[] temp = (byte[]) tempQueue.ToArray(typeof(byte));
            string returndata = System.Text.Encoding.Unicode.GetString(temp);
            returndata = returndata.Substring(0, returndata.Length - 4).Trim();

            queue.Enqueue(returndata);
            Console.WriteLine("Message: " + returndata);
            tempQueue.Clear();

            // TODO Remove stuff bellow just temp;
            Console.WriteLine("Sending Client Message Back");
            sendToClient(returndata);
        }

        public void sendToClient(String msg)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(msg);
            byte[] end = new byte[bytes.Length + Translation.END_OF_MESSAGE.Length];
            Array.Copy(bytes, end, bytes.Length);
            Array.Copy(Translation.END_OF_MESSAGE, 0, end, bytes.Length, Translation.END_OF_MESSAGE.Length);
            socket.GetStream().Write(end, 0, end.Length);
        }

    }
}
