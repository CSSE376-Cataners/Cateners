using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CatanersShared;
using System.Net;

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
            
            // TODO: 
            while (true)
            {
                TcpClient cl = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                Client client = new Client(cl);
                System.Console.WriteLine("Client Connected Start from: " + ((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
                client.queueMessagesAsync();
                System.Console.WriteLine("Client Connected Start Async from: " + ((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
            }

            //this.listener.Stop();
        }
        
    }

}
