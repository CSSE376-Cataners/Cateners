using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CatanersShared;
using System.Net;
using System.Threading;

namespace CatenersServer
{
    
    public class CommunicationServer
    {
        public TcpListener listener;
        public static int maxUsers = 100;


        public CommunicationServer()
        {
            this.listener = new TcpListener(System.Net.IPAddress.Any,Variables.serverPort);
        }

        public async Task Start()
        {
            this.listener.Start(maxUsers);
            Console.WriteLine("Server Started.");
            Client client = null;
            try
            {
                while (true)
                {
                    TcpClient cl = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    client = new Client(cl);
                    System.Console.WriteLine("Client Connected Start from: " + ((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
                    Thread clientThread = new Thread(client.queueMessagesAsync);
                    clientThread.Start();
                    System.Console.WriteLine("Client Connected Start Async from: " + ((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Write("Server Ended With Exception: " + e.Message);
            }
            this.listener.Stop();

            if (client != null)
            {
                client.socketClosed();
            }
            
        }
        
    }

}
