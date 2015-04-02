using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Concurrent;

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
            Enabled = true;
        }

        public async Task queueMessagesAsync()
        {
            while(Enabled && socket.Connected) {
                NetworkStream serverStream = socket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = socket.ReceiveBufferSize;
                await serverStream.ReadAsync(inStream, 0, buffSize);
                // TODO 
                string returndata = System.Text.Encoding.Unicode.GetString(inStream);
                String readData = "" + returndata;
                System.Console.WriteLine(readData);
                queue.Enqueue(readData);
            }
        }

        public String getNextMessage() {
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
