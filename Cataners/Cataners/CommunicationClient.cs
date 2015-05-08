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
    public class CommunicationClient
    {
        private StreamReader reader;
        private Boolean Enabled;
        private Boolean sceneGenerated;

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
                    if (!msg.message.Equals("-1"))
                    {
                        Data.username = msg.message;
                    }
                    queues[Translation.TYPE.Login].Add(msg.message);
                    break;
                case Translation.TYPE.Register:
                    queues[Translation.TYPE.Register].Add(msg.message);
                    break;
                case Translation.TYPE.HexMessage:
                    Console.WriteLine("Hello");
                    int[][] array = Translation.jsonToIntArrayTwo(msg.message);
                    LocalConversion.Instance.generateHexList(array);
                    LocalConversion.Instance.drawHexes();
                    break;
                case Translation.TYPE.RequestLobbies:
                    Data.Lobbies.Clear();
                    Data.Lobbies.AddRange(Lobby.jsonToLobbyList(msg.message));
                    if (JoinGameForm.INSTANCE != null)
                    {
                        JoinGameForm.INSTANCE.invokedRefresh();
                    }
                    break;
                case Translation.TYPE.UpdateLobby:
                    if (LobbyForm.INSTANCE != null)
                    {
                        Data.currentLobby = Lobby.fromJson(msg.message);
                        LobbyForm.INSTANCE.invokedRefresh();
                    }
                    break;
                case Translation.TYPE.LeaveLobby:
                    if (LobbyForm.INSTANCE != null && LobbyForm.INSTANCE.Visible)
                    {
                        bool notStart = false;
                        LobbyForm.INSTANCE.InvokedClose(notStart);
                    }
                    break;

                case Translation.TYPE.BuySettlement:
                    LocalConversion.Instance.setAsPurchasedSettle(int.Parse(msg.message));
                break;

                case Translation.TYPE.StartGame:
                    bool start = true;
                    LobbyForm.INSTANCE.startButtonClose = true;
                    LobbyForm.INSTANCE.InvokedClose(start);
                    MainGui.INSTANCE.invokedHide();
                    TradeForm trade = new TradeForm();
                    LocalConversion.Instance.generateHexList(Translation.jsonToIntArrayTwo(msg.message));
                    LocalConversion.Instance.drawHexes();
                    Program.Main();
                    break;

                case Translation.TYPE.addResource:
                    AddResource addResource = AddResource.fromJson(msg.message);
                    GameLobby currentLobby = (GameLobby)Data.currentLobby;
                    if (currentLobby == null)
                    {
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < Data.currentLobby.Players.Count; i++)
                        {
                            if (currentLobby.gamePlayers[i].Equals(addResource.player))
                            {
                                currentLobby.gamePlayers[i].resources[addResource.resourceType] += addResource.number;
                            }
                        }
                    }
                        break;
                case Translation.TYPE.GetGameLobby:
                    Data.currentLobby = GameLobby.fromJson(msg.message);
                    for (int i = 0; i < ((GameLobby)Data.currentLobby).gamePlayers.Count; i++)
                    {
                        if (((GameLobby)Data.currentLobby).gamePlayers[i].Username.Equals(Data.currentLobby.Owner.Username))
                        {
                           Data.currentGameOwner = ((GameLobby)Data.currentLobby).gamePlayers[i];
                        }

                         if (((GameLobby)Data.currentLobby).gamePlayers[i].Username.Equals(Data.username))
                         {
                             Data.currentGamePlayer = ((GameLobby)Data.currentLobby).gamePlayers[i];
                         }
                         
                    }
                    Data.currentGameLobby = (GameLobby)Data.currentLobby;
                    
                    break;
                case Translation.TYPE.OpenTradeWindow:
                    Trade tradeobj = Trade.fromJson(msg.message);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Player " + tradeobj.source.Username + " would like to trade you: ");
                    foreach (var item in tradeobj.offeredResources)
                    {
                        sb.Append(item.Value + " " + item.Key + " ");
                    }
                    sb.Append(Environment.NewLine);
                    sb.Append("in exchange for: ");
                    foreach (var item in tradeobj.wantedResources)
                    {
                        sb.Append(item.Value + " " + item.Key + " ");
                    }
                    MessageBoxResult result = System.Windows.MessageBox.Show(sb.ToString(), "Trade Request", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        sendToServer(new CatanersShared.Message(true.ToString(), Translation.TYPE.TradeResponce).toJson());
                    }
                    else
                    {
                        sendToServer(new CatanersShared.Message(false.ToString(), Translation.TYPE.TradeResponce).toJson());
                    }
                    break;
                case Translation.TYPE.Chat:
                    Chat chat = Chat.fromJson(msg.message);
                    if (ChatBox.INSTANCE != null)
                        ChatBox.INSTANCE.Invoke(new Action( () => ChatBox.INSTANCE.addChat(chat)));
                    break;
                case Translation.TYPE.PopUpMessage:
                    PopUpMessage newMsg = PopUpMessage.fromJson(msg.message);
                    switch (newMsg.type)
                    {
                        case PopUpMessage.TYPE.Notification:
                            System.Windows.MessageBox.Show(newMsg.bodyMsg, newMsg.titleMsg);
                            break;
                        case PopUpMessage.TYPE.ResponseNeeded:
                            MessageBoxResult result2 = System.Windows.MessageBox.Show(newMsg.bodyMsg, newMsg.titleMsg, MessageBoxButton.YesNo, MessageBoxImage.Question);
                            break;
                    }
                    
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
