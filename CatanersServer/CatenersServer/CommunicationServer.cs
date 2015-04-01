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


        public CommunicationServer()
        {
            this.listener = new TcpListener(System.Net.IPAddress.Any,Variables.serverPort);
        }


        
    }

}
