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
            return null;

            Byte[] buffer = new Byte[socket.Available];
            socket.GetStream().Read(buffer,0,socket.Available);

            Char[] chars = new Char[buffer.Length];
            
            for(int i = 0; i < buffer.Length; i++) {
                chars[i] = Convert.ToChar(buffer[i]);
            }

            return new String(chars);
        }
    }
}
