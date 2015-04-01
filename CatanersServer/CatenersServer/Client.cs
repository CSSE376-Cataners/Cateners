using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CatenersServer
{
    public class Client
    {
        public TcpClient socket;


        public Client(TcpClient tcp)
        {
            this.socket = tcp;
        }

        public String getNextMessage() {
            return false;
            Byte[] buffer = new Byte[socket.Available];
            socket.GetStream().Read(buffer,0,socket.Available);
            socket.Available
        }
    }
}
