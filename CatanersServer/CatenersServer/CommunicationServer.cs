using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CatanersShared;

namespace CatenersServer
{
    
    public class CommunicationServer
    {
        public TcpListener listener;
        public static int maxUsers;


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
                
            }

            //this.listener.Stop();
        }
        
    }

}
