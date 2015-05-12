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
using System.Threading;

namespace CatenersServer
{
    public partial class Client
    {
        public TcpClient socket;
        public bool Enabled;

        public int userID;
        public String userName;

        public Lobby currentLobby;

        StreamWriter writer;
        StreamReader reader;

        public ServerPlayer player;
        public ServerLogic serverLogic;
        public GameLobby gameLobby;

        public Client(TcpClient tcp)
        {
            this.socket = tcp;
            writer = new StreamWriter(socket.GetStream(), Encoding.Unicode);
            reader = new StreamReader(socket.GetStream(), Encoding.Unicode);
            Enabled = true;
            userID = -1;
            userName = null;
            currentLobby = null;
            this.player = new ServerPlayer(userName, this);
        }

        public Client()
        {
            // For Testing
        }

        public async void queueMessagesAsync()
        {
            while(Enabled && socket.Connected) {
                string line;
                Task<String> task = reader.ReadLineAsync();
                try
                {
                    line = await task;
                    if (line == null)
                        continue;

                    //Console.WriteLine("Message:" + line);

                    Thread thread = new Thread(() => processesMessage(line));
                    thread.Start();

                }
                catch (System.IO.IOException)
                {
                    // Client caused exception just disconnect.
                    break;
                }
            }
            leaveLobby();
            Console.WriteLine("Client Closed: " + ((System.Net.IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString());
        }

        public virtual void sendToClient(String msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public void processesMessage(String s)
        {
            Message msg = Message.fromJson(s);
            System.Diagnostics.Debug.WriteLine(String.Format("[{0}] {1}::TYPE: {2} :: Message: {3}", DateTime.Now.ToString("T"), this.userName, msg.type, msg.message));


            // TODO Pick all messages that would be considered turn based
            if(this.gameLobby != null /* Check if its our turn */)
                if (msg.type == (Translation.TYPE.addResource | Translation.TYPE.BuySettlement | Translation.TYPE.Game | Translation.TYPE.StartTrade))
                {

                }

            switch(msg.type) {
                case Translation.TYPE.Login:
                    Login login = Login.fromJson(msg.message);
                    // TODO verification of login symbols;
                    if (login == null)
                    {
                        sendToClient(new Message("-1", Translation.TYPE.Login).toJson());
                    }
                    catanersDataSet.checkUserDataTableRow user = Database.INSTANCE.getUser(login);
                    if (user == null)
                    {
                        sendToClient(new Message("-1", Translation.TYPE.Login).toJson());
                    }
                    else
                    {
                        sendToClient(new Message(user.Username.ToString(), Translation.TYPE.Login).toJson());
                        this.userID = user.UID;
                        this.userName = user.Username;
                        this.player.Username = user.Username;
                    }
                break;

                case Translation.TYPE.Register:
                    login = Login.fromJson(msg.message);
                    // TODO verification of login symbols;
                    int id = Database.INSTANCE.registerUser(login);
                    if(id < 0 )
                        sendToClient(new Message("-1", Translation.TYPE.Register).toJson());
                    else
                        sendToClient(new Message(id.ToString(), Translation.TYPE.Register).toJson());
                break;
                
                case Translation.TYPE.RequestLobbies:
                    Message toSend = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(Data.INSTANCE.Lobbies), Translation.TYPE.RequestLobbies);
                    sendToClient(toSend.toJson());
                break;

                case Translation.TYPE.BuySettlement:
                    int parsedInt = int.Parse(msg.message);
                    bool test = serverLogic.determineSettlementAvailability(this.userName, parsedInt);
                    if (test == true)
                    {
                        Message settlementPurchased = new Message(parsedInt.ToString(), Translation.TYPE.BuySettlement);
                        String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
                        String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                        foreach (ServerPlayer p in this.currentLobby.Players)
                        {
                            p.client.sendToClient(settlementPurchased.toJson());
                            p.client.sendToClient(toReturn);
                        }
                    }
                break;
                case  Translation.TYPE.CreateLobby:
                    PM_CreateLobby(msg);
                break;
                case Translation.TYPE.ChangeReadyStatus:
                    PM_ChangeReadyStatus(msg);
                break;
                case Translation.TYPE.JoinLobby:
                    PM_JoinLobby(msg);
                break;
                case Translation.TYPE.LeaveLobby:
                    PM_LeaveLobby(msg);
                break;
                case Translation.TYPE.UpdateLobby:
                    PM_UpdateLobby(msg);
                break;
                case Translation.TYPE.RegenerateBoard:
                    PM_RegenerateBoard(msg);
                break;
                case Translation.TYPE.GetGameLobby:
                    PM_GetGameLobby(msg);
                break;
                case Translation.TYPE.StartGame:
                    PM_StartGame(msg);
                break;
                case Translation.TYPE.StartTrade:
                    PM_StartTrade(msg);
                break;
                case Translation.TYPE.Chat:
                    PM_Chat(msg);
                break;
                case Translation.TYPE.TradeResponce:
                    PM_TradeResponce(msg);
                break;
                default:
                    // We Are just going to ignore it.
                break;
            }
        }


        public bool checkReady()
        {
            int readyCount=0;
            for (int i = 0; i < currentLobby.PlayerCount; i++)
            {
                if (currentLobby.Players[i].Ready)
                {
                    readyCount++;
                }
            }

            if (readyCount == currentLobby.PlayerCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void leaveLobby()
        {
            PM_LeaveLobby(null);
        }

        public void socketClosed()
        {
            leaveLobby();
        }
    }
}
