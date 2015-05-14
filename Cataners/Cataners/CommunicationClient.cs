using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics.CodeAnalysis;



namespace Cataners
{
    public partial class CommunicationClient
    {
        private StreamReader reader;
        private Boolean Enabled;
        private Boolean sceneGenerated;
        private Dictionary<int, string> colorDict = new Dictionary<int, string>();

        public Dictionary<Translation.TYPE, BlockingCollection<Object>> queues;


        private static CommunicationClient instance;

        public static CommunicationClient Instance
        {
            get
            {
                return instance;
            }
        }

        public System.Net.Sockets.TcpClient clientSocket;

        public CommunicationClient()
        {
            this.colorDict.Add(0, "Blue");
            this.colorDict.Add(1, "Red");
            this.colorDict.Add(2, "Green");
            this.colorDict.Add(3, "Purple");
            new LocalConversion();
            this.Enabled = false;
            this.clientSocket = new System.Net.Sockets.TcpClient();
            CommunicationClient.instance = this;
            clientSocket.ReceiveTimeout = 3;
            this.sceneGenerated = false;
            this.hexesReady = false;
            queues = new Dictionary<Translation.TYPE, BlockingCollection<object>>();

            foreach (Translation.TYPE t in Enum.GetValues(typeof(Translation.TYPE)))
            {
                queues.Add(t, new BlockingCollection<object>());
            }

        }

        [ExcludeFromCodeCoverage]
        public async void Start()
        {
            try
            {
                await clientSocket.ConnectAsync(Properties.Settings.Default.ServerAddr, Variables.serverPort);
                this.Enabled = true;

                Thread clientThread = new Thread(queueMessagesAsync);
                clientThread.Start();

                writer = new StreamWriter(clientSocket.GetStream(), Encoding.Unicode);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Our servers seem to be having some trouble - we apologize for the inconvenience.", "Error - Server Not Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }
        }

        StreamWriter writer;
        private bool hexesReady;
        public virtual void sendToServer(String msg)
        {
            if (instance.clientSocket.Connected)
            {
                writer.WriteLine(msg);
                writer.Flush();
                Console.WriteLine("Sending To server: " + msg);
            }
        }

        public async void queueMessagesAsync()
        {
            Console.WriteLine("Started Listening");
            while (Enabled && clientSocket.Connected)
            {
                this.reader = new StreamReader(clientSocket.GetStream(), Encoding.Unicode);
                string line;
                Task<String> task = reader.ReadLineAsync();
                try
                {
                    line = await task;
                    Console.WriteLine("Message: " + line);
                    Thread thread = new Thread(() => processesMessage(line));
                    thread.Start();
                }
                catch (IOException)
                {
                    System.Windows.Forms.MessageBox.Show("You've been disconnected from the server", "Error - I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }
            }
            Enabled = false;
        }

        public void processesMessage(String s)
        {
            CatanersShared.Message msg;
            try
            {
                msg = CatanersShared.Message.fromJson(s);
            }
            catch (Exception)
            {
                Console.WriteLine("Recived Invalid Message: " + s);
                return;
            }


            switch (msg.type)
            {
                case Translation.TYPE.Login:
                    PM_Login(msg);
                    break;
                case Translation.TYPE.Register:
                    PM_Register(msg);
                    break;
                case Translation.TYPE.HexMessage:
                    PM_HexMessage(msg);
                    break;
                case Translation.TYPE.RequestLobbies:
                    PM_RequestLobbies(msg);
                    break;
                case Translation.TYPE.UpdateLobby:
                    PM_UpdateLobby(msg);
                    break;
                case Translation.TYPE.LeaveLobby:
                    PM_LeaveLobby(msg);
                    break;
                case Translation.TYPE.BuySettlement:
                    PM_BuySettlement(msg);
                break;
                case Translation.TYPE.StartGame:
                    PM_StartGame(msg);
                    break;
                case Translation.TYPE.addResource:
                    PM_addResource(msg);
                    break;
                case Translation.TYPE.GetGameLobby:
                    PM_GetGameLobby(msg);
                    break;
                case Translation.TYPE.StartTrade:
                    PM_StartTrade(msg);
                    break;
                case Translation.TYPE.Chat:
                    PM_Chat(msg);
                    break;
                case Translation.TYPE.PopUpMessage:
                    PM_PopUpMessage(msg);                    
                    break;
                case Translation.TYPE.UpdateResources:
                    PM_UpdateResources(msg);
                    break;
                case Translation.TYPE.EndTurn:
                    PM_EndTurn(msg);
                 break;
                case Translation.TYPE.DiceRoll:
                    PM_DiceRoll(msg);
                 break;
            }
        }
        /*
        public async Task<string> awaitingMessage(Translation.TYPE type)
        {
            //TODO: come back and finish type = null

            return "";
        }
         */

        internal void setGenerated()
        {
            this.sceneGenerated = !this.sceneGenerated;
        }

        internal void setHexesReady()
        {
            this.hexesReady = !this.hexesReady;
        }
    }
}
