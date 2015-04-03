using System;
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

        public CCQueue queue;
        public bool Enabled;


        public Client(TcpClient tcp)
        {
            this.socket = tcp;
            queue = new CCQueue();
            tempQueue = new ArrayList();
            writer = new StreamWriter(socket.GetStream(), Encoding.Unicode);
            Enabled = true;
        }


        private ArrayList tempQueue;

        public async void queueMessagesAsync()
        {
            StreamReader reader = new StreamReader(socket.GetStream(), Encoding.Unicode);
            while(Enabled && socket.Connected) {
                string line;
                Task<String> task = reader.ReadLineAsync();
                line = await  task; 
                queue.push(line);

                Console.WriteLine("Message:" + line);

                // TODO Use process Handler;
                processesMessage(line);
            }

            Console.WriteLine("Clint Closed");
        }

        StreamWriter writer;

        public void sendToClient(String msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public void processesMessage(String s)
        {
            Message msg = Message.fromJson(s);

            switch(msg.type) {
                case Translation.TYPE.Login:
                    Login login = Login.fromJson(msg.message);
                    // TODO verification of login symbols;
                    int id = Database.INSTANCE.getUserID(login);
                    sendToClient(id.ToString());
                break;
                case Translation.TYPE.Register:

                break;
            }
        }

    }
}
